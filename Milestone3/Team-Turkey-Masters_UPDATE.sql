uu# Update Statements for DataBase Project MileStone 2
# Nathan VelaBorja & Jacob Krahling
# March 22, 2016
# CptS 451 Spring 2016

UPDATE Businesses
		SET num_revs = COUNT (SELECT rid FROM Reviews WHERE Reviews.bid = bid);
		
UPDATE Businesses
		SET avg_rev = 
			SUM(SELECT stars FROM Reviews WHERE Reviews.bid = bid) /
			COUNT (SELECT * FROM Reviews WHERE Reviews.bid = bid);
SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='NC' AND (B.bid IN (SELECT bid FROM Categories WHERE name = 'American (Traditional)') AND (SELECT bid FROM Categories WHERE name = 'Bars') ORDER BY C.bid);

SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='NC' AND B.bid IN (SELECT DISTINCT bid FROM Categories WHERE name = 'American (Traditional)' ORDER BY C.bid) AND (SELECT DISTINCT bid FROM Categories WHERE name = 'Breakfast & Brunch' ORDER BY C.bid);
SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.bid = C.bid AND B.state_code='NC' AND B.city ='CHARLOTTE' AND B.zipcode='28202' AND B.bid IN (SELECT bid FROM Categories WHERE name = 'American (Traditional)') ORDER BY C.bid;

select b.name from businesses b JOIN categories c ON b.bid=c.bid and b.state_code='NC'
where B.bid IN (SELECT bid FROM Categories WHERE name = 'American (Traditional)') AND (SELECT bid FROM Categories WHERE name = 'Bars';

select 




select distinct temp1.name from (select distinct b.name, b.bid as b1 from businesses b, categories c where b.bid = c.bid and b.state_code = 'NC' and c.name='American (Traditional)') as temp1

inner join 

(select distinct b.name, b.bid as b1 from businesses b, categories c where b.bid = c.bid and b.state_code = 'NC' and c.name='Bars') as temp2

on temp1.name=temp2.name;




select distinct temp1.name from (select distinct b.name, b.bid as b1 from businesses b, categories c where b.bid = c.bid and b.state_code = 'NC' and c.name='American (Traditional)') as temp1

inner join 

(select distinct b.name, b.bid as b1 from businesses b, categories c where b.bid = c.bid and b.state_code = 'NC' and c.name='Bars') as temp2

inner join 

(select distinct b.name, b.bid as b1 from businesses b, categories c where b.bid = c.bid and b.state_code = 'NC' and c.name='Sports Bars') as temp3

on temp1.name=temp2.name AND temp2.name=temp3.name;