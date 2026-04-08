namespace ApiWeb.Entities.Corpo
{
    public class Endereco
    {
        public string Cidade { get; set; }= "";
        public string Numero { get; set; }= "";
        public string Rua { get; set; }= "";
        public string Bairro { get; set; }= "";


        public Endereco(string rua, string numero, string cidade, string bairro)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Bairro = bairro;
        }
    }


}