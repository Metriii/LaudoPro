using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Xceed.Document.NET;
using Xceed.Words.NET;
using DocViewer.Entities.Corpo;
using DocViewer.Entities.Periculosidade;
namespace DocViewer.Services.PericulosidadeServices
{
    public class PericulosidadeServices()
    {

        internal void GerarDocumentoPericulosidade(Periculosidade periculo)
        
        {
        var caminhoModelo = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "docs",
            "ESQUELETO.docx"
        );

        var caminhoSaida = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "docs",
            $"Laudo_{periculo.NumeroPericia}.docx"
        );

        var nomesReclamantes = periculo.Reclamante
            .Select(r => $"{r}")
            .Where(r => !string.IsNullOrEmpty(r));

        string resultadoReclamantes = string.Join("\n          ", nomesReclamantes);

        var nomes = periculo.Reclamada
            .Select(r => $"{r}")
            .Where(r => !string.IsNullOrEmpty(r));

        string resultadoReclamadas = string.Join("\n          ", nomes);

        var lista = periculo.Reclamada ?? new List<string>();
        var nomeReclamada = lista
            .Where(r => !string.IsNullOrWhiteSpace(r))
            .ToList();

        string resultado;

        if (nomeReclamada.Count == 0)
            resultado = "";
        else if (nomeReclamada.Count == 1)
            resultado = nomeReclamada[0];
        else if (nomeReclamada.Count == 2)
            resultado = $"{nomeReclamada[0]} e {nomeReclamada[1]}";
        else
            resultado = string.Join(", ", nomeReclamada.Take(nomeReclamada.Count - 1))
                        + " e "
                        + nomeReclamada.Last();

    string resultadoPresentes = string.Join(Environment.NewLine,
        periculo.PresentesFuncao
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => "• " + p.Trim())
    );



        using (var doc = DocX.Load(caminhoModelo))
        {
            var substituicoes = new Dictionary<string, string>
            {
                { "{{NumeroPericia}}", periculo.NumeroPericia ?? "" },
                { "{{NumeroVara}}", periculo.NumeroVara?.ToString() ?? "" },
                { "{{DataPericia}}", periculo.DataPericia ?? "" },
                { "{{DATALAUDO}}", periculo.DataLaudo ?? "" },
                { "{{CIDADE}}", periculo.Endereco?.Cidade ?? "" },
                { "{{BAIRRO}}", periculo.Endereco?.Bairro ?? "" },
                { "{{RUA}}", periculo.Endereco?.Rua ?? "" },
                { "{{NUMERO}}", periculo.Endereco?.Numero ?? "" },
                { "{{Reclamantes}} ", resultadoReclamantes },
                { "{{Reclamada}}", resultadoReclamadas },
                { "#Reclamadas", resultado },
                { "{{TipoDoLaudo}}", "Periculosidade" ?? "" },
                { "{{Anexos}}", LerAnexos(periculo.AnexosPericu) ?? "" },
                { "{{TipoNorma}}", "NR-16" },
                { "{{LocalDeTrabalho}}", periculo.Atividades?.Local ?? "" },
                { "{{Paredes}}", periculo.Atividades?.Paredes ?? "" },
                { "{{Pisos}}", periculo.Atividades?.Pisos ?? "" },
                { "{{Iluminacao}}", periculo.Atividades?.Iluminacao ?? "" },
                { "{{Ventilacao}}", periculo.Atividades?.Ventilacao ?? "" },
                {"{{Objetivo}}", LerTextoDocx(Path.Combine(AppContext.BaseDirectory, "ANEXOS - PERICULOSIDADE", "Objetivo.docx")) ?? "" },
            };
            doc.ReplaceText(new StringReplaceTextOptions()
                {
                    SearchValue = "{{presentes}}",
                    NewValue = resultadoPresentes
                });

            foreach (var item in substituicoes)
            {
                doc.ReplaceText(new StringReplaceTextOptions
                {
                    SearchValue = item.Key,
                    NewValue = item.Value ?? ""
                });
            }
            doc.SaveAs(caminhoSaida);
        }
}
public string LerAnexos(List<string> anexos)
{
    string pasta = Path.Combine(AppContext.BaseDirectory, "ANEXOS - PERICULOSIDADE");
    string resultado = "";

    var acoes = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
    {
        ["anexos_1_pericu"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_1_pericu.docx")) + Environment.NewLine,
        ["anexos_1_pericu.docx"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_1_pericu.docx")) + Environment.NewLine,

        ["anexos_2_pericu"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_2_pericu.docx")) + Environment.NewLine,
        ["anexos_2_pericu.docx"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_2_pericu.docx")) + Environment.NewLine,

        ["anexos_3_pericu"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_3_pericu.docx")) + Environment.NewLine,
        ["anexos_3_pericu.docx"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_3_pericu.docx")) + Environment.NewLine,

        ["anexos_4_pericu"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_4_pericu.docx")) + Environment.NewLine,
        ["anexos_4_pericu.docx"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_4_pericu.docx")) + Environment.NewLine,

        ["anexos_5_pericu"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_5_pericu.docx")) + Environment.NewLine,
        ["anexos_5_pericu.docx"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_5_pericu.docx")) + Environment.NewLine,

        // 🔥 suporte ao X (resultado do *)
        ["anexos_X_pericu"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_X_pericu.docx")) + Environment.NewLine,
        ["anexos_X_pericu.docx"] = () => resultado += LerTextoDocx(Path.Combine(pasta, "anexos_X_pericu.docx")) + Environment.NewLine,
    };

    foreach (var nome in anexos)
    {
        if (string.IsNullOrWhiteSpace(nome))
            continue;

        // 🔥 AQUI é o que você queria
        var nomeNormalizado = nome.Trim().Replace("*", "X");

        // garante extensão
        if (!nomeNormalizado.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            nomeNormalizado += ".docx";

        if (acoes.TryGetValue(nomeNormalizado, out var acao))
        {
            acao.Invoke();
        }
        else
        {
            Console.WriteLine($"Anexo desconhecido: '{nomeNormalizado}'");
        }
    }

    return resultado;
}
public string LerTextoDocx(string caminho)
{
    using (var doc = Xceed.Words.NET.DocX.Load(caminho))
    {
        return doc.Text;
    }
}


    }
}