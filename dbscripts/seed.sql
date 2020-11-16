\connect freightdb

CREATE TABLE customer
(
    id serial PRIMARY KEY,
    customername  VARCHAR (50)  NOT NULL,
    carriername  VARCHAR (50)  NOT NULL,
    loadweight  int,
    fromstate VARCHAR(2) NOT NULL,
    tostate VARCHAR(2) NOT NULL,
    chargeamount decimal(5,2) NOT NULL
);

CREATE TABLE tax
(
    id serial PRIMARY KEY,
    statename VARCHAR(2) NOT NULL,
    taxpercent decimal(5,2) NOT NULL
);

ALTER TABLE "customer" OWNER TO freightuser;
ALTER TABLE "tax" OWNER TO freightuser;

INSERT INTO customer(customername, carriername, loadweight, fromstate, tostate, chargeamount) VALUES
    ( 'James','FedEx', 1000, 'AL', 'IL', 20.50),
    ( 'Marshall','UPS', 2000, 'AL', 'NY', 50.0),
    ( 'Willie','FedEx', 3000, 'TX', 'GA', 100.0),
    ( 'Wallace','FedEx', 1000, 'TX', 'NY', 15.0);

INSERT INTO tax(statename, taxpercent) VALUES
    ('AL', 10.0),
    ('IL', 12.0),
    ('NY', 15.0),
    ('GA', 11.0);
