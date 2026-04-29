using System.Diagnostics;

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

app.Run();