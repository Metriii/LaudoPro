using System.Diagnostics;
using DocViewer.Models.Requests;
using DocViewer.Entities.Periculosidade;
using DocViewer.Entities.Corpo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();


// Gera PDF ao iniciar
string docx = "wwwroot/docs/ESQUELETO.docx";

if (File.Exists(docx))
{
    Process.Start(
        "libreoffice",
        $"--headless --convert-to pdf {docx} --outdir wwwroot/docs"
    );
}


// Abre no editor padrão do sistema
app.MapGet("/editar", () =>
{
    string arquivo = Path.GetFullPath(
        "wwwroot/docs/ESQUELETO.docx"
    );

    Process.Start(new ProcessStartInfo
    {
        FileName = arquivo,
        UseShellExecute = true
    });

    return Results.Redirect("/Viewer");
});


// Regenera PDF após edição
app.MapGet("/atualizar-pdf", () =>
{
    Process.Start(
        "libreoffice",
        "--headless --convert-to pdf " +
        "wwwroot/docs/ESQUELETO.docx " +
        "--outdir wwwroot/docs"
    );

    return Results.Redirect("/Viewer");
});

app.MapPost("/api/laudo-pericu", (PericulosidadeRequest request) =>
{
    if (string.IsNullOrEmpty(request.NumeroPericia))
        return Results.BadRequest("NumeroPericia é obrigatório.");

    if (request.LocalTrabalho == null || request.Endereco == null)
        return Results.BadRequest("Dados incompletos.");

    var periculo = new Periculosidade(
        request.NumeroPericia,
        request.DataPericia,
        request.DataLaudo,
        new LocalDeTrabalho(
            request.LocalTrabalho.Local,
            request.LocalTrabalho.Paredes,
            request.LocalTrabalho.Pisos,
            request.LocalTrabalho.Iluminacao,
            request.LocalTrabalho.Ventilacao
        ),
        new Endereco(
            request.Endereco.Rua,
            request.Endereco.Numero,
            request.Endereco.Cidade,
            request.Endereco.Bairro
        ),
        request.Reclamantes,
        request.Reclamadas,
        request.Documentos,
        request.Anexos,
        request.PresentesFuncao
    );

    Console.WriteLine(periculo);

    return Results.Ok("Laudo gerado com sucesso!");
});

app.Run();