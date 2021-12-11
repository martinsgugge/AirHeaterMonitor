create or replace function get_tags()
RETURNS TABLE(o_tag_id integer,
			  o_tag text,
			  o_physical_unit text,
			  o_description text) AS $$

begin
	return query

	select * from tag
	ORDER BY tag_id asc;
end;
$$
language plpgsql

select * from get_tags()
