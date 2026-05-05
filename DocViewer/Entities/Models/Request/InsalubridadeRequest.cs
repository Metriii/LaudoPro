using DocViewer.Models.Request;

namespace DocViewer.Models.Requests
{
    public class InsalubridadeRequest
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

}