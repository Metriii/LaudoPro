using System.Diagnostics;
using DocViewer.Models.Requests;
using DocViewer.Entities.Periculosidade;
using DocViewer.Entities.Corpo;
using DocViewer.Entities.Insalubridade;
using DocViewer.Entities.Ambos;
using DocViewer.Services.PericulosidadeServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<PericulosidadeServices>();
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

app.MapPost("/api/laudo-pericu", (PericulosidadeRequest request, PericulosidadeServices service) =>
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
app.MapPost("/api/laudo-insalub", (InsalubridadeRequest request) =>
{
    if (string.IsNullOrEmpty(request.NumeroPericia))
        return Results.BadRequest("NumeroPericia é obrigatório.");

    if (request.LocalTrabalho == null || request.Endereco == null)
        return Results.BadRequest("Dados incompletos.");

    var insalub = new Insalubridade(
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
        request.PresentesFuncao // 👈 você adicionou agora
    );

    Console.WriteLine(insalub);
    return Results.Ok("Laudo de insalubridade gerado!");
});
app.MapPost("/api/laudo-ambos", (AmbosRequest request) =>
{
    if (string.IsNullOrEmpty(request.NumeroPericia))
        return Results.BadRequest("NumeroPericia é obrigatório.");

    var ambos = new Ambos(
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
        request.AnexosPericulosidade,
        request.AnexosInsalubridade
    );

    Console.WriteLine(ambos);

    return Results.Ok("Laudo (Ambos) gerado com sucesso!");
});

app.Run();