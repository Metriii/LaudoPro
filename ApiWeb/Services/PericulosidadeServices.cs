using System;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ApiWeb.Entities.Periculosidade;

namespace ApiWeb.Services.PericulosidadeServices
{
    internal class PericulosidadeServices
    {
        public PericulosidadeServices()
        {
        }

        public string GerarDocumentoPericulosidade(Periculosidade pericia)
        {
            if (pericia == null)
                throw new ArgumentNullException(nameof(pericia));

            if (string.IsNullOrWhiteSpace(pericia.NumeroPericia))
                throw new ArgumentException("NumeroPericia não pode ser nulo ou vazio.", nameof(pericia.NumeroPericia));

            string numeroPericiaSeguro = string.Concat(
                pericia.NumeroPericia.Where(c => !Path.GetInvalidFileNameChars().Contains(c))
            );

            if (string.IsNullOrWhiteSpace(numeroPericiaSeguro))
                throw new ArgumentException("NumeroPericia inválido para nome de arquivo.", nameof(pericia.NumeroPericia));

            string caminhoModelo = Path.Combine(AppContext.BaseDirectory, "Modelos", "ESQUELETO.docx");

            if (!File.Exists(caminhoModelo))
                throw new FileNotFoundException("O arquivo modelo não foi encontrado.", caminhoModelo);

            string pastaGerados = Path.Combine(AppContext.BaseDirectory, "Gerados");
            Directory.CreateDirectory(pastaGerados);

            string outputPath = Path.Combine(pastaGerados, $"Laudo_{numeroPericiaSeguro}.docx");

            File.Copy(caminhoModelo, outputPath, true);

            using (WordprocessingDocument doc = WordprocessingDocument.Open(outputPath, true))
            {
                var mainPart = doc.MainDocumentPart
                    ?? throw new InvalidOperationException("O documento não possui MainDocumentPart.");

                var document = mainPart.Document
                    ?? throw new InvalidOperationException("O documento principal não foi carregado.");

                var body = document.Body
                    ?? throw new InvalidOperationException("O documento não possui Body.");

                foreach (var text in body.Descendants<Text>())
                {
                    if (!string.IsNullOrEmpty(text.Text) && text.Text.Contains("{{NumeroPericia}}"))
                    {
                        text.Text = text.Text.Replace("{{NumeroPericia}}", pericia.NumeroPericia);
                    }
                }

                document.Save();
            }

            return outputPath;
        }
    }
}