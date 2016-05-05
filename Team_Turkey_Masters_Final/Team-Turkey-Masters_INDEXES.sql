# Indexes for Business Analyst 
# Nathan VelaBorja & Jacob Krahling
# April 26, 2016
# CptS 451 Spring 2016

create index reviewBidInd using hash on reviews(bid);

create index businessCityIndex using hash on businesses(city);

create index businessBidIndex using hash on businesses(bid);