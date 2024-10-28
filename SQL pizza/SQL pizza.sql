create table tbPizza
(id int not null primary key, descricao varchar(100) not null )

create table tbIngredientesPizza 
(id int not null primary key, pizzaId int not null, descricao varchar(100) not null )