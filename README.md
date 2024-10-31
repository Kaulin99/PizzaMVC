# PizzaMVC

Projeto ASP.NET MVC para Gerenciamento de Pizzas e Ingredientes.

## Descrição

PizzaMVC é uma aplicação web desenvolvida em ASP.NET MVC para gerenciar um cardápio de pizzas, permitindo a criação, edição, listagem e exclusão de pizzas e seus ingredientes. Este projeto serve como uma ferramenta de aprendizado para desenvolvimento de aplicativos web com ASP.NET, focando no uso de operações CRUD, views, controllers, DAO, procedures armazenadas e personalização de interface.

## Funcionalidades

- **Gerenciamento de Pizzas**: CRUD completo para pizzas com associação de ingredientes.
- **Cadastro de Ingredientes**: CRUD de ingredientes, associados a pizzas específicas.
- **Validação de Dados**: Validações básicas para assegurar que dados essenciais estão presentes antes de salvar ou atualizar registros.
- **Interface Responsiva**: Uso de CSS para uma apresentação visual simplificada e responsiva das páginas.
- **Redirecionamento e Preservação de Parâmetros**: Preserva os IDs de pizzas e ingredientes ao navegar entre as telas, garantindo que os dados corretos sejam exibidos.

## Estrutura do Projeto

- **Models**: Classes que representam as entidades `tbPizzaViewModel` e `tbIngredientesViewModel`.
- **Controllers**: Controladores `tbPizzaController` e `tbIngredientesController` para manipulação de dados.
- **DAO (Data Access Object)**: Camadas de acesso a dados `tbPizzaDAO` e `tbIngredientesDAO`, usando procedures armazenadas para operações de banco de dados.
- **Views**: Interfaces para criação, edição e listagem de pizzas e ingredientes, organizadas em HTML/CSS com Razor syntax.

## Principais Arquivos

- ## Controllers/tbPizzaController.cs: Lógica de controle para as ações relacionadas a pizzas.
- ## Controllers/tbIngredientesController.cs: Lógica para ações relacionadas aos ingredientes.
- ## DAO/tbPizzaDAO.cs: Métodos de acesso a dados para a entidade Pizza.
- ## DAO/tbIngredientesDAO.cs: Métodos de acesso a dados para ingredientes.
- ## Views/tbPizza/Index.cshtml: Página inicial de listagem de pizzas.
- ## Views/tbIngredientes/Index.cshtml: Página de listagem de ingredientes para uma pizza específica.
