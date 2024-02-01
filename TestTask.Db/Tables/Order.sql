create table if not exists "order"."order"
(
	id bigserial primary key not null,
	"cost" real not null,
	order_time timestamptz not null,
	status_id int not null,
	client_id bigint not null,
	foreign key (status_id) references "order".order_status(id),
	foreign key (client_id) references "order".client(id)
	
);