using ApiWeb.Entities.Corpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Periculosidade
{
    internal class Periculosidade : CorpoLaudo
    {
        public List<PericuAnexos> AnexosPericu { get; set; } = new List<PericuAnexos>();

        public Periculosidade(string numeroPericia, DateTime dataPericia, DateTime dataLaudo, LocalDeTrabalho atividades, Endereco endereco) : base(numeroPericia, dataPericia, dataLaudo, atividades, endereco)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = (char) NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
            Atividades = atividades;
            Endereco = endereco;
        }

        

        public void AddReclamantes(List<string> reclamantes)
        {
            foreach (var reclamante in reclamantes)
            {
                Reclamante.Add(reclamante);
            }
        }
        public void AddReclamada(List<string> reclamadas)
        {
            foreach (var reclamada in reclamadas)
            {
                Reclamada.Add(reclamada);
            }
        }

        public void AddAnexos(List<PericuAnexos> anexos)
        {
            foreach(var anexo in anexos) { 
                
                AnexosPericu.Add(anexo);
            }
        }
    }
}