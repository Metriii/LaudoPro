using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Corpo
{
    internal class CorpoLaudo
    {
        protected string NumeroPericia {  get; set; }
        public char NumeroVara { get; set; }
        protected List<string> Reclamante { get; set; } = new List<string>();
        protected List<string> Reclamada { get; set; } = new List<string>();
        protected Endereco Endereco { get; set; }
        protected DateTime DataPericia { get; set; }
        protected DateTime DataLaudo { get; set; }
        protected LocalDeTrabalho Atividades { get; set; }
        protected List<DocumentosAnalisados> Documentos {get; set; } = new List<DocumentosAnalisados>();

        public CorpoLaudo(string numeroPericia, DateTime dataPericia, DateTime dataLaudo, LocalDeTrabalho atividades, Endereco endereco)
        {
            NumeroPericia = numeroPericia;
            NumeroVara = NumeroPericia[NumeroPericia.Length - 1];
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
            Atividades = atividades;
            Endereco = endereco;

        }
    }
}