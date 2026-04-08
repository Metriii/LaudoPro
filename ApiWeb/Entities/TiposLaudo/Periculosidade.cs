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
        protected List<string> AnexosPericu { get; set; } = new List<string>();

        public Periculosidade(){}
        public Periculosidade(string numeroPericia, string dataPericia, string dataLaudo, LocalDeTrabalho atividades, Endereco endereco, List<string> reclamantes, List<string> reclamadas, List<string> documentos, List<string> anexosPericu) : base(numeroPericia, dataPericia, dataLaudo, atividades, endereco)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
            foreach(var reclamante in reclamantes) { 
                Reclamante.Add(reclamante);
            }
            foreach(var reclamada in reclamadas) {
                Reclamada.Add(reclamada);
            }

            Endereco = endereco;
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
            Atividades = atividades;

            foreach(var documento in documentos) { 
                Documentos.Add(documento);
            }
            foreach(var anexo in anexosPericu) { 
                AnexosPericu.Add("anexos_" + anexo + "_pericu");
            }
            
        }
        public override string ToString()
        {
            return $"Número Perícia: {NumeroPericia}\nData Perícia: {DataPericia}\nData Laudo: {DataLaudo}\nAtividades: {Atividades}\nEndereço: {Endereco}\nReclamantes: {string.Join(", ", Reclamante)}\nReclamadas: {string.Join(", ", Reclamada)}\nDocumentos: {string.Join(", ", Documentos)}\nAnexos Periculosidade: {string.Join(", ", AnexosPericu)}";
        }
    }
}