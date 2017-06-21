create database Library;

use Library;

create table Users (
	ID int not null primary key,
	Name nvarchar(30),
	Age int not null
	);

create table Books(
	ID int not null primary key,
	Name nvarchar(30) not null,
	Author nvarchar(30) not null,
	Publisher nvarchar(30) not null,
	Year int not null,
	UserId int,
	foreign key (UserId) references Users(ID)
	);

insert Users (ID, Name, Age) values
	(1, 'Vasya', 42),
	(2, 'Olya', 13),
	(3, 'Yulya', 32),
	(4, 'Dima', 17),
	(5, 'Oleg', 28)

insert books (ID, Name, Author, Publisher, Year, UserId) values
	(1, 'Book1', 'Author1', 'Publisher1', 1986, 2),
	(2, 'Book2', 'Author2', 'Publisher2', 2017, 1),
	(3, 'Book3', 'Author3', 'Publisher1', 1973, 2),
	(4, 'Book4', 'Author4', 'Publisher2', 2001, 4),
	(5, 'Book5', 'Author3', 'Publisher3', 1986, null)