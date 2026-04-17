using System;
using System.IO;
using System.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;
using ApiWeb.Entities.Corpo;
using ApiWeb.Entities.Periculosidade;
namespace ApiWeb.Services.PericulosidadeServices
{
    public class PericulosidadeServices
    {
        internal void GerarDocumentoPericulosidade(Periculosidade periculo, string tipo)
        {       
        var caminhoModelo = Path.Combine(AppContext.BaseDirectory, "Modelos", "ESQUELETO.docx");
        
        var caminhoSaida = Path.Combine(AppContext.BaseDirectory, "Modelos", $"Laudo_{periculo.NumeroPericia}.docx");

        var nomesReclamantes = periculo.Reclamante
            .Select((r, i) => $"{r}")
            .Where(r => !string.IsNullOrEmpty(r));

            string resultadoReclamantes  = string.Join("\n          ", nomesReclamantes);

        var nomes = periculo.Reclamada
            .Select((r, i) => $"{r}")
            .Where(r => !string.IsNullOrEmpty(r));
            string resultadoReclamadas = string.Join("\n          ", nomes);

        
        var lista = periculo.Reclamada ?? new List<string>();
        var nomeReclamada = lista
            .Where(r => !string.IsNullOrWhiteSpace(r))
            .ToList();
            string resultado;

                if (nomeReclamada.Count == 0)
                {
                    resultado = "";
                }
                else if (nomeReclamada.Count == 1)
                {
                    resultado = nomeReclamada[0];
                }
                else if (nomeReclamada.Count == 2)
                {
                    resultado = $"{nomeReclamada[0]} e {nomeReclamada[1]}";
                }
                else
                {
                    resultado = string.Join(", ", nomeReclamada.Take(nomeReclamada.Count - 1))
                            + " e "
                            + nomeReclamada.Last();
                }

        using (var doc = DocX.Load(caminhoModelo))
        {
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{NumeroPericia}}",
                NewValue = periculo.NumeroPericia
               
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{NumeroVara}}",
                NewValue = periculo.NumeroVara?.ToString() ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{DataPericia}}",
                NewValue = periculo.DataPericia ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{DATALAUDO}}",
                NewValue = periculo.DataLaudo ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{CIDADE}}",
                NewValue = periculo.Endereco?.Cidade ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{BAIRRO}}",
                NewValue = periculo.Endereco?.Bairro ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{RUA}}",
                NewValue = periculo.Endereco?.Rua ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{NUMERO}}",
                NewValue = periculo.Endereco?.Numero ?? ""
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{Reclamantes}} ",
                NewValue = resultadoReclamantes
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "{{Reclamada}}",
                NewValue = resultadoReclamadas
            });
            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#Reclamadas",
                NewValue = resultado
            });
            
            doc.SaveAs(caminhoSaida);
        }
        }
    }
}