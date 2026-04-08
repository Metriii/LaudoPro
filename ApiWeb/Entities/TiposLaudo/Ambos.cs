using ApiWeb.Entities.Corpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Ambos
{
    internal class Ambos : CorpoLaudo
    {
        public List<string> AnexosInsa { get; set; } = new List<string>();
        public List<string> AnexosPericu { get; set; } = new List<string>();

        public Ambos(){}
        public Ambos(string numeroPericia, string dataPericia, string dataLaudo, LocalDeTrabalho atividades, Endereco endereco) : base(numeroPericia, dataPericia, dataLaudo, atividades, endereco)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
            Atividades = atividades;
            Endereco = endereco;
        }
        // N° PROCESSO
        public Ambos(string numeroPericia)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
        }

        // ADD RECLAMANTES/RECLAMADAS
        public Ambos(List<string> reclamantes, List<string> reclamadas){
            foreach(var reclamante in reclamantes) { 
                Reclamante.Add(reclamante);
            }
            foreach(var reclamada in reclamadas) {
                Reclamada.Add(reclamada);
            }
        }

        // ENDEREÇO

        public Ambos(Endereco endereco)
        {
            Endereco = new Endereco(endereco.Rua, endereco.Numero, endereco.Cidade, endereco.Bairro);
        }
        // DATAS

        public Ambos(string dataPericia, string dataLaudo)
        {
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
        }
        // ATIVIDADES
        public Ambos(LocalDeTrabalho atividades)
        {
            Atividades = new LocalDeTrabalho(atividades.Local, atividades.Paredes, atividades.Pisos, atividades.Iluminacao, atividades.Ventilacao);
        }

        // DOCUMENTOS
        public Ambos(List<string> documentos){
            foreach(var documento in documentos) { 
                Documentos.Add(documento);
            }
         }
        public void AddAnexosInsa(List<string> anexos)
        {
            foreach(var anexo in anexos) { 
                
                AnexosInsa.Add("anexo_" + anexo);
            }
        }
        public void AddAnexosPericu(List<string> anexos)
        {
            foreach(var anexo in anexos) { 
                
                AnexosPericu.Add("anexo_" + anexo);
            }
         }
    }
}