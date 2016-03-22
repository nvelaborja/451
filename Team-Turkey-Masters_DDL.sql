# Create Table Statements for DataBase Project MileStone 2
# Nathan VelaBorja & Jacob Krahling
# March 20, 2016
# CptS 451 Spring 2016


CREATE TABLE Demographics (
	zipcode		varchar(10)	PRIMARY KEY,
	state		varchar(15),
	state_code	char(2),
	city		varchar(30),
	population	integer,
	under18		real		CHECK (under18 <= 100.0),
	18to24		real		CHECK (18to24 <= 100.0),
	25to44		real		CHECK (25to44 <= 100.0),
	45to64		real		CHECK (45to64 <= 100.0),
	65andover	real		CHECK (65andover <= 100.0),
	med_age		tinyint,
	perc_fe		real		CHECK (perc_fee <= 100.0),
	num_emp		integer,
	annual_payroll	integer,
	avg_income	integer
);

CREATE TABLE Categories (
	name		varchar(50)	PRIMARY KEY
);

CREATE TABLE Businesses (
	bid		smallint	PRIMARY KEY,
	name		varchar(50),
	avg_rev		real		CHECK (avg_rev <= 5.0),
	num_revs	integer,
	city		varchar(30)	REFERENCES Demographics(city),
	state		varchar(15)	REFERENCES Demographics(state),
	zipcode		varchar(10)	REFERENCES Demographics(zipcode),
	category	varchar(50)	REFERENCES Categories(name)
);

CREATE TABLE Users (
	uid		smallint	PRIMARY KEY,
	name		varchar(50)
);

CREATE TABLE Reviews (
	rid		smallint	PRIMARY KEY,
	bid		smallint	REFERENCES Businesses(bid),
	date		date,
	rating		real		CHECK (rating <= 5.0),
	votes		integer,		
	text		text
);

CREATE TABLE Votes (
	uid		smallint	REFERENCES Users(vid),
	rid		smallint	REFERENCES Reviews,
	PRIMARY KEY(vid,rid)
);


# ******* NEED ********
# Trigger to update number of review and average reviews for Business
# Figure out how date will look in Reviews and stuff
# Trigger to update votes counter in Reviews
# Order of Insertion:
#	Demographics / Categories
#	Businesses
#	Users
#	Reviews
#	Votes
# Probably need some UPDATE and DELETE handling