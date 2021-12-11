create or replace function get_tag_measurements(i_tag_id integer, 
												i_start timestamp without time zone, 
												i_stop timestamp without time zone)
RETURNS TABLE(o_meas_time timestamp,
					o_meas_value real) AS $$
begin
	return query

	select meas_time, meas_value 
	from measurement 
	where tag_id = i_tag_id and meas_time between i_start and i_stop
	ORDER BY meas_time asc;

end;
$$
language plpgsql

select * from get_tag_measurements(1, '2021-11-16 20:00', '2021-11-23 23:00')
select * from get_tag_measurements(@i_tag_id, @i_start, @i_stop);
