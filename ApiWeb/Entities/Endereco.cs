using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWeb.Entities.Corpo
{
    internal class Endereco
    {
        private string _Cidade {  get; set; }
        private string _Estado { get; set; }
        private string _Numero {  get; set; }
        private string _Rua {  get; set; }
        private string _Bairro { get; set; }

        public Endereco(string cidade, string estado, string numero, string rua, string bairro)
        {
            _Cidade = cidade;
            _Estado = estado;
            _Numero = numero;
            _Rua = rua;
            _Bairro = bairro;
        }
    }
}