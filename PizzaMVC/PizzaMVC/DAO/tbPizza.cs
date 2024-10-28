using JogoMVC.DAO;
using PizzaMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace PizzaMVC.DAO
{
    public class tbPizza 
    {
        private SqlParameter[] EnviaParametros(tbPizzaViewModel tbpizza)
        {
            SqlParameter[] envia = new SqlParameter[]
            {
                new SqlParameter("id",tbpizza.id),
                new SqlParameter("descricao",tbpizza.descricao)
            };
            return envia;
        }

        private tbPizzaViewModel RecebeParametros(DataRow recebe)
        {
            tbPizzaViewModel p = new tbPizzaViewModel();
            p.id = Convert.ToInt32(recebe["id"]);
            p.descricao = Convert.ToString(recebe["desc"]);

            return p;        
        }

        /*----------------------------------------*/

        public List<tbPizzaViewModel> CriaLista()
        {
            var lista = new List<tbPizzaViewModel>();

            SqlParameter[] parametro = new SqlParameter[]
            {
                new SqlParameter("tabela","tbPizza")
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagem", parametro);

            foreach (DataRow row in tabela.Rows)
            {
                lista.Add(RecebeParametros(row));
            }
            
            return lista;
        }

        public tbPizzaViewModel Consulta(int id)
        {
            SqlParameter[] parametro = new SqlParameter[]
                {
                    new SqlParameter("id",id),
                    new SqlParameter("tabela","tbPizza")
                };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsulta", parametro);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return RecebeParametros(tabela.Rows[0]);
        }

        public int IdAutomatico()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("tabela","tbPizza")
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spIdAutomatico", parametros);
            return Convert.ToInt16(tabela.Rows[0]["MAIOR"]);
        }

        /*----------------------------------------*/

        /*public void Inserir(tbPizzaViewModel tbpizza)
        {
            HelperDAO.ExecutaProc("spInserir", tbpizza);
        }*/
    }
}
