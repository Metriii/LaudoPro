using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Corpo
{
    internal class LocalDeTrabalho
    {
        protected string LocalAtividades {  get; set; }
        protected string Paredes { get; set; }
        protected string Pisos { get; set; }
        protected string Iluminacao { get; set; }
        protected string Ventilacao { get; set; }

        
        public LocalDeTrabalho(string local, string paredes, string piso, string iluminacao, string ventilacao)
        {
            LocalAtividades = local;
            Paredes = paredes;
            Pisos = piso;
            Iluminacao = iluminacao;
            Ventilacao = ventilacao;
        }


    }
}