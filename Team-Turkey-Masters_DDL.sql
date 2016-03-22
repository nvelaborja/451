# Create Table Statements for DataBase Project MileStone 2
# Nathan VelaBorja & Jacob Krahling
# March 20, 2016
# CptS 451 Spring 2016


CREATE TABLE Demographics (
	zipcode 		varchar(10) PRIMARY KEY,
	state 			varchar(15),
	state_code 		char(2),
	city 			varchar(30),
	population 		integer,
	under18 		real 		CHECK (under18 <= 100.0),
	18to24 			real 		CHECK (18to24 <= 100.0),
	25to44 			real 		CHECK (25to44 <= 100.0),
	45to64 			real 		CHECK (45to64 <= 100.0),
	65andover 		real 		CHECK (65andover <= 100.0),
	med_age 		tinyint,
	perc_fe 		real 		CHECK (perc_fee <= 100.0),
	num_emp 		integer,
	annual_payroll 	integer,
	avg_income 		integer
);

CREATE TABLE Businesses (
	bid 		varchar(30) PRIMARY KEY,
	name 		varchar(50),
	avg_rev 	real 		CHECK (avg_rev <= 5.0),
	num_revs 	integer,
	city 		varchar(30) REFERENCES Demographics(city),
	state_code 	char(2) 	REFERENCES Demographics(state_code),
	zipcode 	varchar(10) REFERENCES Demographics(zipcode)
);

CREATE TABLE Categories (
	name 		varchar(50),
	bid 		varchar(30),
	PRIMARY KEY(name, bid)
);

CREATE TABLE Users (
	uid 		varchar(30) PRIMARY KEY,
	name 		varchar(50)
);

CREATE TABLE Reviews (
	rid 	varchar(30) PRIMARY KEY,
	uid 	varchar(30) REFERENCES Users(vid),
	bid 	varchar(30) REFERENCES Businesses(bid),
	stars 	tinyint 	CHECK(stars <= 5),
	funny 	smallint,
	useful 	smallint,
	cool 	smallint,
	date 	char(10),
	text 	text
);


# ******* NEED ********
# Trigger to update number of review and average reviews for Business
# Trigger to update votes counter in Reviews
# Order of Insertion:
#	Demographics
#	Businesses
#	Categories
#	Users
#	Reviews
# Probably need some UPDATE and DELETE handling