namespace PizzaMVC.Models
{
    public class tbIngredientesViewModel 
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int pizzaId { get; set; }

        public string NomePizza {get; set; }
    }
}
