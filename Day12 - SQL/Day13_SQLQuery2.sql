-- Day 13 --


use Wiprodatabase_1
select name from sys.tables
select * from customer

------------/////////////----------

--creating table valued functions

create function dbo.empcheck()
returns table
as
return
select * from customer where id = 102

==========
create function dbo.empcheck()
returns table
as 
return
select * from customer where id = 103



--Scalar valued function :-

create function dbo.CalculateTotal
(
	@price decimal(10, 2)
	@quantity int
)
returns decimal (10, 2)
as 
begin
	ruturn @price * @quantity;
end


select dbo.Calculate(100.50, 3) as totalamount;


1.--- create a scalar valued function to claculate and display area of circle


for ex:-

create function dbo.getDiscountPrice
(
	@Price decimal (10, 2),
	@Discount decimal (5, 2)
)
return decimal (10, 2)
as
begin
	return @Price - (@Price * @Discountrate / 100);
end


create function dbo.GetAreaOfCircle
(
	@Radious decimal(5, 2)
)
return decimal(5, 2)
as 
begin 
	return (PI() * @Radius * @Radious)
end



================================================

--JOIN-----

1. equi join
2. non equi join
3. outer join

4. self join 
5. cross join
6. inner join

select * from employee

use Wiprodatabase

-- Primary Table
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY,
    Name VARCHAR(100),
    Department VARCHAR(100)
);

INSERT INTO Employee (EmployeeID, Name, Department) VALUES
(1, 'Kapil', 'IT'),
(2, 'Pooja', 'HR'),
(3, 'Ravi', 'Finance');



-- Foreign Table

CREATE TABLE Company (
    CompanyID INT PRIMARY KEY,
    CompanyName VARCHAR(100),
    EmployeeID INT, -- foreign key to Employee table
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

INSERT INTO Company (CompanyID, CompanyName, EmployeeID) VALUES
(106, 'IBM', 1),
(107, 'Infosys', 2),
(108, 'Wipro', 3);


------------
--Innerjoin--

select * from employee e
join company c
on e.employeeid = c.employeeid

--Outer Join-- 
select * from employee e
left join company c
on e.employeeid = c.employeeid
--right join --
select * from employee e
right join company c
on e.employeeid = c.employeeid

--full join--
full join company c
on e.employeeid = c.employeeid

--cross join--
select * from employee 
cross join company

select * FROM employee
where Salary IS NOT NULL;

---self join--


select * from employee

select * from employee e1, employee e2
where e1.employeeid > e2.employeeid

==================

--UNION---

select * from employee
union
select * from company

--intersect--
select companyid from company where CompanyName='Wipro'
intersect
select companyid from company where Companyname='Wipro'


--except--
select employeeid from employee where name='kapil'
except
select employeeid from employee where name='kapil'

select * from employee

select * from company


--cartesian product--





=================================


--Stored procedure--

create proc getallemployeedetails
as
begin
select * from employee;
end;

exec getallemployeedetails

--ex.1
create proc getdetailsofNULlemail
as
begin
select * from employee where email is null
end
exec getdetailsofNULlemail


--ex.2 
create proc greterthan
as
begin
select * from employee where employeeid > 1
end;

exec greterthan

--ex.2--
create proc lessThan
as
begin
select * from employee where employeeid < 5
end;

exec lessThan

--types od stored proceeder--
types:
1. with parameters:-
--delete--
create proc deleteComId
(@eid int)
as
begin
delete from company where companyid=@eid
end

select * from employee
select * from company

exec deleteComID 104

--update--
create proc insertdata
as 
begin
update employee 
set name = 'PulanDevi'
where employeeid = 4
end

exec insertdata


-- insert--

create

---------------
Input/Output Parameters

create procedure simpleInOutPro
@input int,
@InOutParam int output
as
begin
	set @inoutparam = @inOutparam + @input
end

declare @val int = 5;
Exec simpleInOutpro @input = 3, @inoutparam = @val output
print @val -- output wil 8

--

create proc SimpleIOProc
	@input int output,
	@inout int 
as begin
	set @inout= @inout + @input 
End

declare @val int =5
exec SimpleIOProc @input=@val output, @inout=3 
print @val;

-----------
Q.1==> create a stored procedure to display empid in employee table--in ascending order

Q.2==> create a stored procedure to display empid in employee table in descending order

1)
create proc assendingOrder
as
begin
	select * from employee order by employeeid asc
end
exec assendingOrder

2)
create proc dssendingOrder
as
begin
	select * from employee order by employeeid desc
end
exec dssendingOrder

=========================================
--if else -- 
if, if...else if

declare @salesAmount int = 600;

if @salesAmount > 500
begin
	print 'sales target completed'
end

else

begin
	print 'sales target not yet achived'
end;


===========================================

Exception handling in sql server

Declare @val4 int;
Declare @val5 int;

BEGIN TRY
   Set @val4=8;

BEGIN TRY
    Set @val4=8;
    Set @val5=@val4/0; /* Error Occur Here */
END TRY

BEGIN CATCH
    Print 'Error Occur that is:'
    Print Error_Message()
END CATCH

Set @val5=@val4/0; /* Error Occur Here */
END TRY

BEGIN CATCH
    Print 'Error Occur that is:'
    Print Error_Message()
END CATCH

--------------

User defined exception---


Declare @val1 int;
Declare @val2 int;

BEGIN TRY
Set @val1=8;
Set @val2=@val1%2;

if @val1=1
    Print ' Error Not Occur'
else
Begin
    Print 'Error Occur';
    Throw 60000,'Number Is Even',5
End

END TRY
BEGIN CATCH
Print 'Error Occur that is:'
Print Error_Message()
END CATCH