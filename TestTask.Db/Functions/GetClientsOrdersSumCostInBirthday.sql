create or replace function "order".get_clients_orders_sum_cost_in_birthday()
returns table
(
	id bigint,
	first_name text,
	last_name text,
	summary_cost real
)
as $$
	begin
	return query
		with summary_cost_in_client_birthday as (
			select 
				o.client_id as client_id
				, sum(cost)::real as summary_cost 
			from 
				"order"."order" o 
			where 
				(date_part('day', o.order_time), date_part('month', o.order_time)) 
				= (select 
					 date_part('day', birth_date)
					, date_part('month', birth_date)
				from 
					"order".client c
				where o.client_id = c.id 
				limit 1)
			and status_id = 3
		group by client_id
		)
		select 
			c.id, c.first_name, c.last_name, scicb.summary_cost 
		from
			"order".client c 
		inner join 
			summary_cost_in_client_birthday scicb on scicb.client_id = c.id; 
	end;
$$
language plpgsql
;