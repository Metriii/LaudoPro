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
        internal void GerarDocumentoPericulosidade(Periculosidade periculo)
        {       
        var caminhoModelo = Path.Combine(AppContext.BaseDirectory, "Modelos", "ESQUELETO.docx");
        
        var caminhoSaida = Path.Combine(AppContext.BaseDirectory, "Modelos", $"Laudo_{periculo.NumeroPericia}.docx");

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
            doc.SaveAs(caminhoSaida);
        }
        }
    }
}