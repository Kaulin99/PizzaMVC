# PizzaMVC
Crie um CRUD para cadastrar pizzas. 

Na listagem de pizzas exiba um novo ícone que irá permitir cadastrar os ingredientes da pizza na forma de registro em uma tabela. Sendo assim quando se clicar neste link para os ingredientes deve-se exibir uma listagem de todos os ingredientes já cadastrados para a pizza selecionada. Exiba "Ingredientes da pizza XXXXX" onde XXXX é o nome da pizza. Deve ter um botão para cadastrar um novo ingrediente e para cada ingrediente listado deve haver uma opção para editar e excluí-lo. Também deve haver nesta tela um botão para retornar à listagem de pizzas.

Tabelas:

create table tbPizza(id int not null primary key, descricao varchar(100) not null )

create table tbIngredientesPizza (id int not null primary key,  pizzaId  int not null, descricao varchar(100) not null )

Validação:  Todos os campos são obrigatórios.

Utilize Stored Procedures.
