using Microsoft.AspNetCore.Mvc;
using PizzaMVC.Models;
using PizzaMVC.DAO;

namespace PizzaMVC.Controllers
{
    public class tbIngredientesController : Controller
    {
        public IActionResult Index(int pizzaId)
        {
            try
            {
                tbIngredientesDAO dao = new tbIngredientesDAO();
                var lista = dao.CriaLista(pizzaId);

                // Recupera a descrição da pizza e define o ID e nome na ViewBag
                tbPizzaDAO daopizza = new tbPizzaDAO();
                tbPizzaViewModel pizza = daopizza.Consulta(pizzaId);

                if (pizza != null)
                {
                    ViewBag.pizzaId = pizzaId;
                    ViewBag.pizza = pizza.descricao;  // Define a descrição na ViewBag
                }

                return View("Index", lista);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }


        public IActionResult Criar(int pizzaId)
        {
            try
            {
                ViewBag.Operacao = "C";

                tbIngredientesViewModel i = new tbIngredientesViewModel();
                tbIngredientesDAO dao = new tbIngredientesDAO();
                i.id = dao.IdAutomatico();

                tbPizzaDAO daopizza = new tbPizzaDAO();
                tbPizzaViewModel pizza = daopizza.Consulta(pizzaId);
                i.pizzaId = Convert.ToInt32(pizza.id);
                i.NomePizza = Convert.ToString(pizza.descricao);
                i.pizzaId = pizza.id;
                ViewBag.pizza = pizza.descricao;  // Define a descrição na ViewBag

                i.NomePizza = pizza?.descricao;

                return View("Form", i);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Editar(int id, int pizzaId)
        {
            try
            {
                ViewBag.Operacao = "E";

                tbIngredientesDAO dao = new tbIngredientesDAO();
                tbIngredientesViewModel i = dao.Consulta(id);
                i.pizzaId = pizzaId;
                ViewBag.pizza = i.NomePizza;

                if (i == null)
                    return RedirectToAction("Index");
                else
                    return View("Form", i);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Excluir(int id,int pizzaId)
        {
            try
            {
                tbIngredientesDAO dao = new tbIngredientesDAO();
                dao.Excluir(id);
                return RedirectToAction("Index", new { pizzaId = pizzaId });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(tbIngredientesViewModel i, string Operacao, string pizza)
        {
            try
            {
                ValidaDados(i, Operacao);

                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", i);
                }
                else
                {
                    tbIngredientesDAO dao = new tbIngredientesDAO();

                    if (Operacao == "C")
                        dao.Inserir(i);
                    else
                        dao.Editar(i);

                    // Passa o pizzaId ao redirecionar para Index
                    return ListaIngredientes(i.pizzaId, pizza);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }


        private void ValidaDados(tbIngredientesViewModel i, string Operacao)
        {
            ModelState.Clear();
            tbIngredientesDAO dao = new tbIngredientesDAO();

            if (string.IsNullOrEmpty(i.descricao))
                ModelState.AddModelError("descricao", "Preencha o nome do ingrediente");
        }

        public IActionResult ListaIngredientes(int pizzaId,string pizza)
        {
            try
            {
                ViewBag.pizzaId = pizzaId; // necessário para que na inclusão seja o ingrediente seja associado à pizza 
                ViewBag.pizza = pizza;
                tbIngredientesDAO dao =new tbIngredientesDAO();
                var lista = dao.CriaLista(pizzaId); // cast foi necessário para acessar método específico dessa classe, que não tem no padrao
                return View("Index", lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult RedirecionaParaIndex(tbIngredientesViewModel model)
        {
            return RedirectToAction("ListaIngredientes",
                routeValues: new { pizzaId = model.pizzaId });
        }
    }
}
