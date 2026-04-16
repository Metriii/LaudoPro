using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Corpo
{
    internal class CorpoLaudo
    {
        public string NumeroPericia { get; protected set; } = "";
        public char? NumeroVara { get; protected set; }

        public List<string> Reclamante { get; protected set; } = new List<string>();
        public List<string> Reclamada { get; protected set; } = new List<string>();

        public Endereco? Endereco { get; protected set; }

        public string? DataPericia { get; protected set; }
        public string? DataLaudo { get; protected set; }

        public LocalDeTrabalho? Atividades { get; protected set; }

        public List<string> Documentos { get; protected set; } = new List<string>();

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