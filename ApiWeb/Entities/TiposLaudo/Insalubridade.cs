using ApiWeb.Entities.Corpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Insalubridade
{
    internal class Insalubridade : CorpoLaudo
    {
        public List<string> AnexosInsa { get; set; } = new List<string>();

        public Insalubridade(){}
        public Insalubridade(string numeroPericia, string dataPericia, string dataLaudo, LocalDeTrabalho atividades, Endereco endereco) : base(numeroPericia, dataPericia, dataLaudo, atividades, endereco)
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
        public Insalubridade(string numeroPericia)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
        }

        // ADD RECLAMANTES/RECLAMADAS
        public Insalubridade(List<string> reclamantes, List<string> reclamadas){
            foreach(var reclamante in reclamantes) { 
                Reclamante.Add(reclamante);
            }
            foreach(var reclamada in reclamadas) {
                Reclamada.Add(reclamada);
            }
        }

        // ENDEREÇO

        public Insalubridade(Endereco endereco)
        {
            Endereco = new Endereco(endereco.Rua, endereco.Numero, endereco.Cidade, endereco.Bairro);
        }
        // DATAS

        public Insalubridade(string dataPericia, string dataLaudo)
        {
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
        }
        // ATIVIDADES
        public Insalubridade(LocalDeTrabalho atividades)
        {
            Atividades = new LocalDeTrabalho(atividades.Local, atividades.Paredes, atividades.Pisos, atividades.Iluminacao, atividades.Ventilacao);
        }

        // DOCUMENTOS
        public Insalubridade(List<string> documentos){
            foreach(var documento in documentos) { 
                Documentos.Add(documento);
            }
         }
        public void AddAnexos(List<string> anexos)
        {
            foreach(var anexo in anexos) { 
                
                AnexosInsa.Add("anexo_" + anexo);
            }
        }
    }
}