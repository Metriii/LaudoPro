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
        public List<string> AnexosPericu { get; set; } = new List<string>();

        public Periculosidade(){}
        public Periculosidade(string numeroPericia, string dataPericia, string dataLaudo, LocalDeTrabalho atividades, Endereco endereco) : base(numeroPericia, dataPericia, dataLaudo, atividades, endereco)
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
        public Periculosidade(string numeroPericia)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
        }

        // ADD RECLAMANTES/RECLAMADAS
        public Periculosidade(List<string> reclamantes, List<string> reclamadas){
            foreach(var reclamante in reclamantes) { 
                Reclamante.Add(reclamante);
            }
            foreach(var reclamada in reclamadas) {
                Reclamada.Add(reclamada);
            }
        }

        // ENDEREÇO

        public Periculosidade(Endereco endereco)
        {
            Endereco = new Endereco(endereco.Rua, endereco.Numero, endereco.Cidade, endereco.Bairro);
        }
        // DATAS

        public Periculosidade(string dataPericia, string dataLaudo)
        {
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
        }
        // ATIVIDADES
        public Periculosidade(LocalDeTrabalho atividades)
        {
            Atividades = new LocalDeTrabalho(atividades.Local, atividades.Paredes, atividades.Pisos, atividades.Iluminacao, atividades.Ventilacao);
        }

        // DOCUMENTOS
        public Periculosidade(List<string> documentos){
            foreach(var documento in documentos) { 
                Documentos.Add(documento);
            }
         }
        public void AddAnexos(List<string> anexos)
        {
            foreach(var anexo in anexos) { 
                
                AnexosPericu.Add("anexo_" + anexo);
            }
        }
 
    }
}