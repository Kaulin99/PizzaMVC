use Pizzaria
GO

create table tbPizza
(id int not null primary key identity, descricao varchar(100) not null )

create table tbIngredientesPizza 
(id int not null primary key, pizzaId int not null, descricao varchar(100) not null )

/*----------------------------------------------------------------------------------*/
                              --Procedures genéricas--
/*----------------------------------------------------------------------------------*/

create or alter procedure spDeletar(
	@id int,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'delete from ' + @tabela + ' where id = ' +  cast( @id as varchar(max))
	exec(@sql)
end
go

create or alter procedure spConsulta(
	@id int,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from ' + @tabela + ' where id = ' + cast( @id as varchar(max))
	exec(@sql)
end
go

create or alter procedure spListagem(
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from ' + @tabela
	exec(@sql)
end
go

create or alter procedure spIdAutomatico(
	@tabela varchar(max)
)
as
begin
	exec('select isnull(max(id)+1,1) as MAIOR from ' + @tabela) 
end
go


/*----------------------------------------------------------------------------------*/
					 --Procedures específicas da tabela pizza--
/*----------------------------------------------------------------------------------*/

create or alter procedure spInserirPizza(
	@descricao varchar(max))
as
begin
	insert into tbPizza(descricao)
	values (@descricao)
end

go


create or alter procedure spEditarPizza(
	@Id int,
	@descricao varchar(50)
)
as
begin
	update tbPizza set descricao = @descricao
	where id = @Id
end
go

/*----------------------------------------------------------------------------------*/
					--Procedures específicas da tabela ingredientes--
/*----------------------------------------------------------------------------------*/

create or alter procedure spInserirIngredientes(
	@descricao varchar(max))
as
begin
	insert into tbIngredientesPizza(descricao)
	values (@descricao)
end

go


create or alter procedure spEditarIngredientes(
	@Id int,
	@descricao varchar(50),
	@pizzaID int
)
as
begin
	UPDATE tbIngredientesPizza 
	SET descricao = @descricao, pizzaId = @pizzaID
	WHERE id = @Id;
end
go