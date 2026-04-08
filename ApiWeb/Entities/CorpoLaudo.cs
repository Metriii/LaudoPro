using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Corpo
{
    internal class CorpoLaudo
    {
        protected string? NumeroPericia {  get; set; } = "";
        protected char? NumeroVara { get; set; }
        protected List<string> Reclamante { get; set; } = new List<string>();
        protected List<string> Reclamada { get; set; } = new List<string>();
        protected Endereco? Endereco { get; set; }
        protected string? DataPericia { get; set; }
        protected string? DataLaudo { get; set; }
        protected LocalDeTrabalho? Atividades { get; set; }
        protected List<string> Documentos {get; set; } = new List<string>();

        public CorpoLaudo() { }

        public CorpoLaudo(string numeroPericia, string dataPericia, string dataLaudo, LocalDeTrabalho atividades, Endereco endereco)
        {
            NumeroPericia = numeroPericia;
            NumeroVara = NumeroPericia[NumeroPericia.Length - 1];
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
            Atividades = atividades;
            Endereco = endereco;

        }
        //N° PROCESSO
        public CorpoLaudo(string numeroPericia)
        {
            NumeroPericia = numeroPericia;
            char numeroVara = NumeroPericia[NumeroPericia.Length - 1];
            NumeroVara = numeroVara;
        }

        // ADD RECLAMANTES/RECLAMADAS
        public CorpoLaudo(List<string> reclamantes, List<string> reclamadas){
            foreach(var reclamante in reclamantes) { 
                Reclamante.Add(reclamante);
            }
            foreach(var reclamada in reclamadas) {
                Reclamada.Add(reclamada);
            }
        }

        // ENDEREÇO

        public CorpoLaudo(Endereco endereco)
        {
            Endereco = new Endereco(endereco.Rua, endereco.Numero, endereco.Cidade, endereco.Bairro);
        }
        // DATAS

        public CorpoLaudo(string dataPericia, string dataLaudo)
        {
            DataPericia = dataPericia;
            DataLaudo = dataLaudo;
        }
        // ATIVIDADES
        public CorpoLaudo(LocalDeTrabalho atividades)
        {
            Atividades = new LocalDeTrabalho(atividades.Local, atividades.Paredes, atividades.Pisos, atividades.Iluminacao, atividades.Ventilacao);
        }

        // DOCUMENTOS
        public CorpoLaudo(List<string> documentos){
            foreach(var documento in documentos) { 
                Documentos.Add(documento);
            }
         }

    }
}