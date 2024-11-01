use Pizzaria
GO

create table tbPizza
(id int not null primary key identity, descricao varchar(100) not null )

create table tbIngredientesPizza 
(id int not null primary key identity, pizzaId int not null, descricao varchar(100) not null )

/*----------------------------------------------------------------------------------*/
                              --Procedures genéricas--
/*----------------------------------------------------------------------------------*/

create or alter procedure spExcluir(
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
	@descricao varchar(max),
	@pizzaId int)
as
begin
	insert into tbIngredientesPizza(descricao,pizzaId)
	values (@descricao,@pizzaId)
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

CREATE OR ALTER PROCEDURE spListagemIngredientes(
    @tabela1 VARCHAR(MAX),
    @tabela2 VARCHAR(MAX),
    @id INT
)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    SET @sql = 'SELECT t2.descricao,t2.id,t2.pizzaId,t1.descricao FROM ' + @tabela1 + ' t1 ' +
               'LEFT JOIN ' + @tabela2 + ' t2 ON t1.id = t2.pizzaId ' +
               'WHERE t1.id = @idParam';

    EXEC sp_executesql @sql, N'@idParam INT', @idParam = @id;
END
GO
