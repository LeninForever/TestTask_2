create table if not exists "order".client
(
	id bigserial primary key not null,
	first_name text not null,
	last_name text not null,
	birth_date date not null
);