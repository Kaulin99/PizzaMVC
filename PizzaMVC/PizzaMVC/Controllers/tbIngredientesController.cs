using Microsoft.AspNetCore.Mvc;
using PizzaMVC.Models;
using PizzaMVC.DAO;

namespace PizzaMVC.Controllers
{
    public class tbIngredientesController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                tbIngredientesDAO dao = new tbIngredientesDAO();
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

                tbIngredientesViewModel i = new tbIngredientesViewModel();
                tbIngredientesDAO dao = new tbIngredientesDAO();
                i.id = dao.IdAutomatico();

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

        public IActionResult Excluir(int id)
        {
            try
            {
                tbIngredientesDAO dao = new tbIngredientesDAO();
                dao.Excluir(id);
                return View("Index");
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

                    if (Operacao == "C")
                        dao.Inserir(i);
                    else
                        dao.Editar(i);

                    return RedirectToAction("Index");
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
    }
}
