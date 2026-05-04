namespace DocViewer.Models.Requests
{
    public class PericulosidadeRequest
    {
        public string NumeroPericia { get; set; } = "";
        public string Tipo { get; set; } = "";

        public string DataPericia { get; set; } = "";
        public string DataLaudo { get; set; } = "";

        public List<string> Reclamantes { get; set; } = new();
        public List<string> Reclamadas { get; set; } = new();
        public List<string> Documentos { get; set; } = new();
        public List<string> Anexos { get; set; } = new();
        public List<string> PresentesFuncao { get; set; } = new();

        public LocalRequest LocalTrabalho { get; set; } = new();
        public EnderecoRequest Endereco { get; set; } = new ();
    }

    public class LocalRequest
    {
        public string Local { get; set; } = "";
        public string Paredes { get; set; } = "";
        public string Pisos { get; set; } = "";
        public string Iluminacao { get; set; }= "";
        public string Ventilacao { get; set; } = "";
    }

    public class EnderecoRequest
    {
        public string Rua { get; set; } = "";
        public string Numero { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Cidade { get; set; } = "";
    }
}