create user charter with password 'chart123!';
GRANT CONNECT ON DATABASE air_heater TO charter;
GRANT select ON ALL TABLES IN SCHEMA PUBLIC TO charter;
GRANT INSERT ON tag TO charter;

SELECT usename, valuntil FROM pg_user;

ALTER USER charter VALID UNTIL 'infinity';
