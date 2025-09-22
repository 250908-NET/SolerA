-- SETUP:
    -- Create a database server (docker)
        -- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Revature2024" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    -- Connect to the server (Azure Data Studio / Database extension)
    -- Test your connection with a simple query (like a select)
    -- Execute the Chinook database (to create Chinook resources in your db)

    

-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
SELECT FirstName, LastName, CustomerID, Country FROM Customer WHERE Country != 'USA';
-- List all customers from Brazil
SELECT * FROM Customer WHERE Country = 'Brazil';    
-- List all sales agents
SELECT * FROM Employee WHERE Title = 'Sales Support Agent'; 
-- Retrieve a list of all countries in billing addresses on invoices
SELECT BillingCountry FROM Invoice;
-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?
SELECT Count(InvoiceDate) FROM Invoice WHERE YEAR(InvoiceDate) = 2009;
-- (challenge: find the invoice count sales total for every year using one query)
SELECT Year(InvoiceDate) as Year, Count(InvoiceDate) as '# of Invoices', SUM(Total) as 'Total Sales' FROM Invoice GROUP BY Year(InvoiceDate);
-- how many line items were there for invoice #37
SELECT Count(Quantity) FROM InvoiceLine WHERE InvoiceId = 37;
-- how many invoices per country? BillingCountry  # of invoices -
Select BillingCountry, Count(*) as '# of Invoices' FROM Invoice GROUP BY BillingCountry;
-- Retrieve the total sales per country, ordered by the highest total sales first.
Select BillingCountry, SUM(Total) as 'Total Sales' FROM Invoice GROUP BY BillingCountry ORDER BY 'Total Sales' DESC;

-- JOINS CHALLENGES
-- Every Album by Artist
SELECT Album.*, Artist.Name FROM Album JOIN Artist ON Album.ArtistId = Artist.ArtistID;
-- All songs of the rock genre
SELECT Track.*, Genre.Name as 'Genre' From Track JOIN Genre ON Track.GenreId = Genre.GenreId WHERE Genre.Name = 'Rock';
-- Show all invoices of customers from brazil (mailing address not billing)
SELECT Invoice.* FROM Invoice JOIN Customer ON Invoice.CustomerID = Customer.CustomerID WHERE Country = 'Brazil';

-- Show all invoices together with the name of the sales agent for each one
SELECT Invoice.*, Employee.LastName, Employee.FirstName
FROM Invoice 
JOIN Customer ON Invoice.CustomerId = Customer.CustomerId
JOIN Employee ON Customer.SupportRepId = Employee.EmployeeId;

-- Which sales agent made the most sales in 2009?
SELECT Employee.EmployeeId, Employee.LastName, Employee.FirstName, Sum(Invoice.Total) 
FROM Employee
JOIN Customer ON Employee.EmployeeID = Customer.SupportRepId
JOIN Invoice ON Customer.CustomerId = Invoice.CustomerId
WHERE Year(Invoice.InvoiceDate) = 2009
GROUP BY Employee.EmployeeId, Employee.LastName, Employee.FirstName;

-- How many customers are assigned to each sales agent?
SELECT Employee.EmployeeId, Employee.LastName, Employee.FirstName, COUNT(Customer.CustomerId) as 'Assigned Customers'
FROM Employee
JOIN Customer ON Employee.EmployeeId = Customer.SupportRepID
WHERE Employee.Title = 'Sales Support Agent'
GROUP BY Employee.EmployeeId, Employee.LastName, Employee.FirstName;

-- Which track was purchased the most in 2010?
SELECT Track.Name, SUM(InvoiceLine.Quantity) as 'Albums Sold'
FROM InvoiceLine
JOIN Invoice ON Invoice.InvoiceId = Invoice.InvoiceId
JOIN Track ON InvoiceLine.TrackId = Track.TrackId
WHERE Year(Invoice.InvoiceDate) = 2010
GROUP BY Track.Name
ORDER BY 'Albums Sold' DESC;

-- Show the top three best selling artists.
SELECT TOP 3 Artist.Name, SUM(InvoiceLine.Quantity) as 'Albums Sold'
FROM InvoiceLine
JOIN Track ON InvoiceLine.TrackId = Track.TrackId
JOIN Album ON Track.AlbumId = Album.AlbumId
JOIN Artist ON Album.ArtistId = Artist.ArtistId
GROUP BY Artist.Name
ORDER BY 'Albums Sold' DESC;

-- Which customers have the same initials as at least one other customer?


-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.


-- DML exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.
