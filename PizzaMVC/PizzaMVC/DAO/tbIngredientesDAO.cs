using PizzaMVC.Models;
using PizzaMVC.DAO;
using System.Data.SqlClient;
using System.Data;

namespace PizzaMVC.DAO
{
    public class tbIngredientesDAO 
    {
        private SqlParameter[] EnviaParametros(tbIngredientesViewModel ingredientes)
        {
            SqlParameter[] envia = new SqlParameter[]
            {
                new SqlParameter("id",ingredientes.id),
                new SqlParameter("descricao",ingredientes.descricao),
                new SqlParameter("pizzaId",ingredientes.pizzaId)
            };
            return envia;
        }

        private tbIngredientesViewModel RecebeParametros(DataRow recebe)
        {
            tbIngredientesViewModel i = new tbIngredientesViewModel();
            i.id = Convert.ToInt32(recebe["id"]);
            i.descricao = Convert.ToString(recebe["descricao"]);

            tbPizzaDAO dao = new tbPizzaDAO();
            tbPizzaViewModel pizzaID = dao.Consulta(i.id);
            i.pizzaId = Convert.ToInt32(pizzaID.id);

            return i;
        }

        /*----------------------------------------*/

        public List<tbIngredientesViewModel> CriaLista()
        {
            var lista = new List<tbIngredientesViewModel>();

            SqlParameter[] parametro = new SqlParameter[]
            {
                new SqlParameter("tabela","tbIngredientesPizza")
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagem", parametro);

            foreach (DataRow row in tabela.Rows)
            {
                lista.Add(RecebeParametros(row));
            }

            return lista;
        }

        public tbIngredientesViewModel Consulta(int id)
        {
            SqlParameter[] parametro = new SqlParameter[]
                {
                    new SqlParameter("id",id),
                    new SqlParameter("tabela","tbIngredientesPizza")
                };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsulta", parametro);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return RecebeParametros(tabela.Rows[0]);
        }

        /*----------------------------------------*/

        public int IdAutomatico()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("tabela","tbIngredientesPizza")
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spIdAutomatico", parametros);
            return Convert.ToInt16(tabela.Rows[0]["MAIOR"]);
        }

        /*----------------------------------------*/

        public void Inserir(tbIngredientesViewModel i)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("descricao",i.descricao)
            };

            HelperDAO.ExecutaProc("spInserirIngredientes", parametros);
        }

        public void Editar(tbIngredientesViewModel i)
        {
            HelperDAO.ExecutaProc("spEditarIngrediente", EnviaParametros(i));
        }

        public void Excluir(int id)
        {
            var parametros = new SqlParameter[]
           {
                new SqlParameter ("id",id),
                new SqlParameter("tabela","tbIngredientesPizza")
           };

            HelperDAO.ExecutaProc("spExcluir", parametros);
        }

    }
}
