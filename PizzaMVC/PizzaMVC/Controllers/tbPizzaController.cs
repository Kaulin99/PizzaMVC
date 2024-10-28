using Microsoft.AspNetCore.Mvc;
using PizzaMVC.Models;
using PizzaMVC.DAO;

namespace PizzaMVC.Controllers
{
    public class tbPizzaController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                tbPizzaDAO dao = new tbPizzaDAO();
                var lista = dao.CriaLista();
                return View("Index", lista);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Criar()
        {
            try
            {
                ViewBag.Operacao = "C";

                tbPizzaViewModel p = new tbPizzaViewModel();
                tbPizzaDAO dao = new tbPizzaDAO();
                p.id = dao.IdAutomatico();

                return View("Form", p);
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

                tbPizzaDAO dao = new tbPizzaDAO();
                tbPizzaViewModel p = dao.Consulta(id);

                if (p == null)
                    return RedirectToAction("Index");
                else
                    return View("Form", p);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                tbPizzaDAO dao = new tbPizzaDAO();
                dao.Excluir(id);
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Salvar(tbPizzaViewModel p, string Operacao)
        {
            try
            {
                ValidaDados(p, Operacao);

                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", p);
                }
                else
                {
                    tbPizzaDAO dao = new tbPizzaDAO();

                    if (Operacao == "C")
                        dao.Inserir(p);
                    else 
                        dao.Editar(p);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(tbPizzaViewModel p, string Operacao)
        {
            ModelState.Clear();
            tbPizzaDAO dao = new tbPizzaDAO();

            if (string.IsNullOrEmpty(p.descricao))
                ModelState.AddModelError("descricao", "Preencha o nome da ferramenta");
        }
    }
}
