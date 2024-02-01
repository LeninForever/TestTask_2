create or replace function "order".get_avg_cost_by_hours()
returns table
(
	"hour" int,
	avg_cost real
)
as $$
	begin
		return query
			with recursive hour_sequence as (
    			select '00:00'::time as order_time
    			union all
    			select (order_time + interval '1 hour')::time
    			from hour_sequence
    			where order_time < '23:00'
    		),
    		completed_orders as (
    			select 
    				extract (hour from o.order_time)::int as "hour"
    				,o.cost as "cost"
    			from
    				"order"."order" o
    			where 
    				o.status_id = 3
    		)
    		select 
	  			extract (hour from hs.order_time)::int as "hour"
				,coalesce(avg(co."cost")::real, 0) 
			from 
				hour_sequence hs
			LEFT JOIN 
    		completed_orders co ON co."hour" = extract (hour from hs.order_time)::int
    		
		group by 
			hs.order_time 
		order by 
			hs.order_time desc;
	end;
$$
language plpgsql
;
