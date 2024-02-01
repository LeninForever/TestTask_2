create schema "order";

create table if not exists "order".order_status
(
	id int primary key not null,
	title text not null
);

create table if not exists "order".client
(
	id bigserial primary key not null,
	first_name text not null,
	last_name text not null,
	birth_date date not null
);

create table if not exists "order"."order"
(
	id bigserial primary key not null,
	"cost" real not null,
	order_time timestamp not null,
	status_id int not null,
	client_id bigint not null,
	foreign key (status_id) references "order".order_status(id),
	foreign key (client_id) references "order".client(id)
	
);


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


--Fill test data

INSERT INTO "order".order_status (id,title) VALUES (1,'Not processed'),(2,'Cancelled'), (3,'Completed');


	 INSERT INTO "order".client (first_name,last_name,birth_date) VALUES
	 ('Steven','Becken','2002-03-02'),
	 ('Idaline','McSkeagan','1997-12-16'),
	 ('Rodge','Skeermor','2000-02-20'),
	 ('Ginnie','Froude','1984-04-13'),
	 ('Dorian','Pomphrett','1971-12-12'),
	 ('Anselm','Bigley','1994-11-21'),
	 ('Linnell','Sailer','1982-06-08'),
	 ('Barbabra','Kerbey','2002-01-06'),
	 ('Benjie','Nanson','1995-04-22'),
	 ('Leland','Sinton','1971-06-22'),
	 ('Cassey','Blonfield','1978-09-12'),
	 ('Virginia','Beddow','1977-05-30'),
	 ('Alon','Matthias','1987-07-08'),
	 ('Marc','Haquard','2002-01-31'),
	 ('Mela','Pandie','1990-08-09'),
	 ('Cori','Challace','1985-03-24'),
	 ('Test','Tester','2024-01-31'),
	 ('Test','Tester','2024-01-31');

INSERT INTO "order"."order" ("cost",order_time,status_id,client_id) VALUES
	 (109.02,'2022-02-20 02:52:58+03',3,3),
	 (128.82,'2022-02-20 12:36:12+03',2,3),
	 (21.88,'2022-07-10 04:38:29+03',3,3),
	 (120.17,'2022-02-20 22:36:29+03',3,3),
	 (86.32,'2022-05-09 06:26:37+03',1,7),
	 (65.81,'2022-05-04 03:51:51+03',1,1),
	 (197.67,'2022-07-11 20:45:36+03',1,7),
	 (5.74,'2022-04-21 12:56:50+03',3,3),
	 (116.7,'2022-10-23 19:12:34+03',2,4),
	 (82.93,'2022-06-08 22:22:34+03',3,7);
INSERT INTO "order"."order" ("cost",order_time,status_id,client_id) VALUES
	 (71.21,'2023-01-22 10:12:13+03',1,13),
	 (191.67,'2022-03-16 10:33:25+03',1,9),
	 (26.53,'2022-02-18 07:21:53+03',1,9),
	 (187.26,'2022-02-19 12:16:27+03',1,3),
	 (17.28,'2022-10-07 22:09:41+03',1,14),
	 (85.85,'2022-07-06 11:52:41+03',2,3),
	 (146.95,'2022-09-26 21:09:05+03',1,10),
	 (41.52,'2022-05-19 22:12:07+03',1,14),
	 (28.57,'2022-11-21 03:53:35+03',1,6),
	 (148.97,'2022-08-19 06:36:17+03',2,4);
