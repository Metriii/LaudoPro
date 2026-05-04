using DocViewer.Entities.Corpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocViewer.Entities.Ambos
{
    internal class Ambos : CorpoLaudo
    {
        public List<string> AnexosInsa { get; set; } = new List<string>();
        public List<string> AnexosPericu { get; set; } = new List<string>();

        public Ambos(){}
        public Ambos(string numeroPericia, string dataPericia, string dataLaudo, LocalDeTrabalho atividades, Endereco endereco, List<string> reclamantes, List<string> reclamadas, List<string> documentos, List<string> anexosPericu, List<string> anexosInsalub) : base(numeroPericia, dataPericia, dataLaudo, atividades, endereco)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
            Atividades = atividades;
            Endereco = endereco;

            foreach(var reclamante in reclamantes) { 
                Reclamante.Add(reclamante);
            }
            foreach(var reclamada in reclamadas) {
                Reclamada.Add(reclamada);
            }

            foreach(var documento in documentos) { 
                Documentos.Add(documento);
            }
            foreach(var anexo in anexosInsalub) { 
                AnexosInsa.Add("anexos_" + anexo + "_insalub");
            }
             foreach(var anexo in anexosPericu) { 
                AnexosPericu.Add("anexos_" + anexo + "_pericu");
             }
        }

        public override string ToString()
        {
            return $"Número Perícia: {NumeroPericia}\nData Perícia: {DataPericia}\nData Laudo: {DataLaudo}\nAtividades: {Atividades}\nEndereço: {Endereco}\nReclamantes: {string.Join(", ", Reclamante)}\nReclamadas: {string.Join(", ", Reclamada)}\nDocumentos: {string.Join(", ", Documentos)}\nAnexos Insalubridade: {string.Join(", ", AnexosInsa)}\nAnexos Periculosidade: {string.Join(", ", AnexosPericu)}";
        }
    }
}