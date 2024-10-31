using PizzaMVC.Models;
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
            if (recebe["id"] != DBNull.Value)
                i.id = Convert.ToInt32(recebe["id"]);
            i.descricao = Convert.ToString(recebe["descricao"]);

            tbPizzaDAO dao = new tbPizzaDAO();
            tbPizzaViewModel pizzaID = new tbPizzaViewModel();

            if (recebe["pizzaId"]!=DBNull.Value) 
                pizzaID = dao.Consulta(Convert.ToInt32(recebe["pizzaId"]));
            i.NomePizza = Convert.ToString(pizzaID.descricao);

            if (recebe["id"] == DBNull.Value)
                return null;
            else
                return i;
        }

        /*----------------------------------------*/

        public List<tbIngredientesViewModel> CriaLista(int id)
        {
            var lista = new List<tbIngredientesViewModel>();

            SqlParameter[] parametro = new SqlParameter[]
            {
                new SqlParameter("tabela1","tbPizza"),
                new SqlParameter("tabela2","tbIngredientesPizza"),
                new SqlParameter("id",id)
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemIngredientes", parametro);

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
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("descricao",i.descricao),
                new SqlParameter("pizzaId",i.pizzaId)
            };

            HelperDAO.ExecutaProc("spInserirIngredientes", parametros);
        }

        public void Editar(tbIngredientesViewModel i)
        {
            HelperDAO.ExecutaProc("spEditarIngredientes", EnviaParametros(i));
        }

        public void Excluir(int pizzaId)
        {
            var parametros = new SqlParameter[]
           {
                new SqlParameter ("id",pizzaId),
                new SqlParameter("tabela","tbIngredientesPizza")
           };

            HelperDAO.ExecutaProc("spExcluir", parametros);
        }

    }
}
