create database Library;

use Library;

create table Users (
	ID int identity (1,1) not null primary key,
	Name nvarchar(30) not null,
	Age int not null
	);

create table Books(
	ID int identity (1,1) not null primary key,
	Name nvarchar(30) not null,
	Author nvarchar(30) not null,
	Publisher nvarchar(30) not null,
	Year int not null,
	UserId int,
	foreign key (UserId) references Users(ID)
	);

insert Users (Name, Age) values
	('Vasya', 42),
	('Olya', 13),
	('Yulya', 32),
	('Dima', 17),
	('Oleg', 28)

insert books (Name, Author, Publisher, Year, UserId) values
	('Book1', 'Author1', 'Publisher1', 1986, 2),
	('Book2', 'Author2', 'Publisher2', 2017, 1),
	('Book3', 'Author3', 'Publisher1', 1973, 2),
	('Book4', 'Author4', 'Publisher2', 2001, 4),
	('Book5', 'Author3', 'Publisher3', 1986, null)