namespace ClienteCRUD.WebAPI.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; } 
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; } 
    }
}