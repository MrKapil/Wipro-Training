use Wiprodatabase_1


-- Day 12 ---

-- 1. Primary Key --

create table person
(
	id int primary key,
	FName varchar(100) not null,
	LName varchar(100),
	Age int
)
-- primary key == unique Key+ not null
insert into person (id , FName, LNAme,Age)values(101, 'Kapil', 'rathod',25)
select * from person


-- 2. Check Constraint -- 


create table person1
(
id int primary key,
FName varchar not null,
LName Varchar,
Age int check (Age >= 18)
)

insert into person (id , FName, LNAme,Age)values(102, 'Kapil', 'rathod',12)
select * from person

sp_databases
EXEC sp_tables

-- 3. Default Contraint--

create table person2
(
id int not null,
FName varchar(100) not null,
LName Varchar(100),
City varchar(100) default 'Surat'
)
insert into person2(id , FName, LNAme)values(102, 'Kapil', 'rathod')
select * from person2


-- 4. Not null Contraint--

create table person3
(
id int not null,
FName varchar(100) not null,
LName Varchar(100),
age int
)
insert into person3(id , FName, LNAme, age)values(102, 'Kapil', 'rathod',35)
select * from person3



-- Unique Contraint --

create table person4
(
id int not null,
FName varchar(100) not null,
LName Varchar(100),
City varchar(100) default 'Surat'
)
insert into person2(id , FName, LNAme)values(102, 'Kapil', 'rathod')
select * from person2


========================================

--Built in functions--

Date and time function
Mathmatical function 
string funcrion 
aggregate function



3. string function

select ascii('a')  --  97 ->'a' And 65 ->A
select replace('india is big counry', 'india', 'US')
select replicate('India', 10)
select ltrim('             india Country')
select rtrim('India country       ')
select trim('       India     country   ')

select ltrim(rtrim(   'india..country   '))
select lower('INDIA'), upper('india')
select len('India is a country', 0, 6)
select substring('India')
select reverse('india')
select left('india is a county', 5)
select right('india is a country', 7)



4. Agregate function

selct salary(sum) from employee
selct salary(avg) from employee
selct salary(min) from employee
selct salary(count) from employee
selct salary(max) from employee


1. Date And Time Function

select getdate()
select dateadd(day, 2,getdate())
select dateadd(month, 2,getdate())
select dateadd(year, 2,getdate())

select datepart(day, getdate())
select datepart(month,getdate())
select datepart(year,getdate())

select datepart(day, '2025-08-1')
select datepart(month, '2025-08-1')
select datepart(year, '2025-08-1')
select datepart(dayofweek, '2025-08-01')

--display name of day
select datename(dw, '1/8/2025')


-- diplay name of month -- 

select datename(month, 2025/8/9)as MonthName
select datediff(day, '2025/8/1', )

select * from Customer

grant select, update on Customer to WiproUser1

revoke select, 

begin
	delete from employee where id

