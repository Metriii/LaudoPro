namespace ApiWeb.Entities.Corpo
{
    public class LocalDeTrabalho
    {
        public string Local { get; set; } = "";
        public string Paredes { get; set; } = "";
        public string Pisos { get; set; }= "";
        public string Iluminacao { get; set; } = "";
        public string Ventilacao { get; set; } = "";

        public LocalDeTrabalho(string local, string paredes, string pisos, string iluminacao, string ventilacao)
        {
            Local = local;
            Paredes = paredes;
            Pisos = pisos;
            Iluminacao = iluminacao;
            Ventilacao = ventilacao;
        }

        public override string ToString()
        {
            return $"Local: {Local}, Paredes: {Paredes}, Pisos: {Pisos}, Iluminação: {Iluminacao}, Ventilação: {Ventilacao}";
        }
    }
}