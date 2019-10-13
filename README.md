# Pools


Pools reservation WEB API written for NET Core 2.1

The service should have the following endpoints:
GET / v1 / pools / - This service should return available pools. Each pool has its name, size (Olympic, kids or small) and depth.

GET / v1 / pools / {id} sessions - This service should return the available dates for one pool. Each term has a term time (from to), a term name (morning, afternoon, evening).

POST / v1 / pools / sessions / {id} / reserve - This service should enable appointment booking. To book an appointment, you need a mobile phone, name and email address.
