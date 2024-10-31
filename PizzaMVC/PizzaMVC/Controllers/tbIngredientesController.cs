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
                tbPizzaDAO daopizza = new tbPizzaDAO();

                ViewBag.pizzaId = pizzaId; // Passa o ID da pizza para a view
                ViewBag.pizza = daopizza.ConsultaPizzaDescricao(pizzaId); // Passa o nome da pizza, assumindo que você tenha um método para buscar a descrição pelo ID

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

                if (pizza != null)
                {
                    i.pizzaId = pizza.id;
                    ViewBag.pizza = pizza.descricao;  // Define a descrição na ViewBag
                }

                i.pizzaId = pizzaId;
                i.NomePizza = pizza?.descricao;

                return View("Form", i);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Editar(int id)
        {
            try
            {
                ViewBag.Operacao = "E";

                tbIngredientesDAO dao = new tbIngredientesDAO();
                tbIngredientesViewModel i = dao.Consulta(id);

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

        public IActionResult Excluir(int pizzaId)
        {
            try
            {
                tbIngredientesDAO dao = new tbIngredientesDAO();
                dao.Excluir(pizzaId);
                return RedirectToAction("Index",pizzaId);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(tbIngredientesViewModel i, string Operacao)
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
                    tbPizzaViewModel pizza = new tbPizzaViewModel();

                    if (Operacao == "C")
                        dao.Inserir(i);
                    else
                        dao.Editar(i);

                    return RedirecionaParaIndex(i);
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
