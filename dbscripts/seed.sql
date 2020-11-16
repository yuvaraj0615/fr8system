\connect freightdb

CREATE TABLE customer
(
    id serial PRIMARY KEY,
    customername  VARCHAR (50)  NOT NULL,
    carriername  VARCHAR (50)  NOT NULL,
    loadweight  int,
    fromstate VARCHAR(2) NOT NULL,
    tostate VARCHAR(2) NOT NULL,
    chargeamount int NOT NULL
);

CREATE TABLE tax
(
    id serial PRIMARY KEY,
    statename VARCHAR(2) NOT NULL,
    taxpercent int NOT NULL
);

ALTER TABLE "customer" OWNER TO freightuser;
ALTER TABLE "tax" OWNER TO freightuser;

INSERT INTO customer(customername, carriername, loadweight, fromstate, tostate, chargeamount) VALUES
    ( 'James','FedEx', 1000, 'AL', 'IL', 2000),
    ( 'Marshall','UPS', 2000, 'AL', 'NY', 5000),
    ( 'Willie','FedEx', 3000, 'TX', 'GA', 10000),
    ( 'Wallace','FedEx', 1000, 'TX', 'NY', 20000);

INSERT INTO tax(statename, taxpercent) VALUES
    ('AL', 10),
    ('IL', 10),
    ('NY', 15),
    ('GA', 11);
