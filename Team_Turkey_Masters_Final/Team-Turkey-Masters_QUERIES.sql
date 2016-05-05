# Sample Queries for Business Searches in Business Analyst
# Nathan VelaBorja & Jacob Krahling
# March 22, 2016
# CptS 451 Spring 2016

# Page One Example Query #

SELECT DISTINCT temp1.name 
FROM 
(SELECT DISTINCT B.name, B.bid AS B1 
	FROM Businesses B, Categories C 
	WHERE B.bid = C.bid 
		AND B.state_code='NC' 
		AND B.city ='CHARLOTTE' 
		AND B.zipcode = '28202' 
		AND C.name = 'American (Traditional)') 
	AS temp1 
INNER JOIN 
(SELECT DISTINCT B.name, B.bid AS B1 
	FROM Businesses B, Categories C 
	WHERE B.bid = C.bid 
		AND B.state_code ='NC' 
		AND B.city ='CHARLOTTE' 
		AND B.zipcode = '28202' 
		AND C.name = 'Bars') 
	AS temp2 
ON temp1.name=temp2.name ;

# Page Two Example Query #

SELECT name 
FROM Businesses B 
WHERE B.state_code='NC' 
	AND B.city='CHARLOTTE' 
	AND B.zipcode='28202' 
	AND B.avg_rev >= 0 
	AND B.avg_rev <= 5 
	AND B.num_revs >= 0 
	AND B.num_revs <= 1000000 
	AND B.bid IN 
	(SELECT bid 
		FROM Categories C 
		WHERE C.name = 'American (Traditional)' GROUP BY C.bid)
	AND B.bid IN 
	(SELECT bid 
		FROM Attributes A 
		WHERE A.name = 'Alcohol' 
			AND A._value = 'full_bar' GROUP BY A.bid);

# Page Two Example Query For Reviews #
SELECT text 
FROM Reviews 
WHERE Reviews.bid IN 
	( SELECT bid FROM Businesses 
	WHERE name = 'Harvest Moon Grille' 
		AND city = 'Charlotte');