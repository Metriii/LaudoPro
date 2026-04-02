using Microsoft.AspNetCore.Mvc;
using ApiWeb.Entities.Corpo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapPost("/api/laudos", (LaudoRequest request) =>
{


    switch (request.Tipo)
    {
        case "periculosidade":
            System.Console.WriteLine("Oi periculosidade");
             
            break;

        case "insalubridade":
            System.Console.WriteLine("Oi insalubridade");
            break;

        case "periculosidade-insalubridade":
            System.Console.WriteLine("Oi periculosidade/insalubridade");
            break;

        default:
            return Results.BadRequest("Tipo inválido");
    }

    return Results.Ok();
});

app.MapPost("/api/reclamantes", (List<string> lista) =>
{
    Console.WriteLine("=== RECLAMANTES ===");

    foreach (var nome in lista)
    {
        Console.WriteLine($"- {nome} reclamantes");
    }

});
app.MapPost("/api/reclamadas", (List<string> lista) =>
{
    Console.WriteLine("=== RECLAMADAS ===");

    foreach (var nome in lista)
    {
        Console.WriteLine($"- {nome} reclamada");
    }

});
app.MapPost("/api/anexos", (List<string> lista) =>
{
    Console.WriteLine("=== ANEXOS ===");

    foreach (var nome in lista)
    {
        Console.WriteLine($"- {nome} anexos");
    }

});

app.MapPost("/api/documentos", (List<string> lista) =>
{
    Console.WriteLine("=== DOCUMENTOS ===");

    foreach (var nome in lista)
    {
        Console.WriteLine($"- {nome} documentos");
    }

   
});

app.MapPost("/api/processo", ([FromBody]string numero) =>
{
        Console.WriteLine($"- {numero} N° processo");
    

    
});

app.MapPost("/api/local-trabalho", (LocalDeTrabalho local) =>
{
    Console.WriteLine("=== LOCAL DE TRABALHO ===");

    Console.WriteLine(local.Local);
    Console.WriteLine(local.Paredes);
    Console.WriteLine(local.Pisos);
    Console.WriteLine(local.Iluminacao);
    Console.WriteLine(local.Ventilacao);

    
});
app.MapPost("/api/endereco", (Endereco endereco) =>
{
    Console.WriteLine("CHEGOU!");

    Console.WriteLine(endereco.Rua);
    Console.WriteLine(endereco.Numero);
    Console.WriteLine(endereco.Bairro);
    Console.WriteLine(endereco.Cidade);

    
});
app.Run();

public record LaudoRequest(string Tipo);