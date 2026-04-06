using Microsoft.AspNetCore.Mvc;
using ApiWeb.Entities.Corpo;
using System.Text.Json;

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



app.MapPost("/api/laudo-completo", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    // Opcional: parsear só pra organizar melhor
    var json = JsonDocument.Parse(body);
    var root = json.RootElement;

    Console.WriteLine("=== DADOS SEPARADOS ===");

    Console.WriteLine($"Tipo: {root.GetProperty("tipo")}");
    
    Console.WriteLine($"Número: {root.GetProperty("numeroPericia")}");

    Console.WriteLine("Reclamantes:");
    foreach (var item in root.GetProperty("reclamantes").EnumerateArray())
        Console.WriteLine($"- {item}");

    Console.WriteLine("Reclamadas:");
    foreach (var item in root.GetProperty("reclamadas").EnumerateArray())
        Console.WriteLine($"- {item}");

    Console.WriteLine("Anexos:");
    foreach (var item in root.GetProperty("anexos").EnumerateArray())
        Console.WriteLine($"- {item}");

    Console.WriteLine("Documentos:");
    foreach (var item in root.GetProperty("documentos").EnumerateArray())
        Console.WriteLine($"- {item}");

    Console.WriteLine("Local de Trabalho:");
    var local = root.GetProperty("localTrabalho");
    Console.WriteLine(local.GetProperty("local"));
    Console.WriteLine(local.GetProperty("paredes"));
    Console.WriteLine(local.GetProperty("pisos"));
    Console.WriteLine(local.GetProperty("iluminacao"));
    Console.WriteLine(local.GetProperty("ventilacao"));

    Console.WriteLine("Endereço:");
    var endereco = root.GetProperty("endereco");
    Console.WriteLine(endereco.GetProperty("rua"));
    Console.WriteLine(endereco.GetProperty("numero"));
    Console.WriteLine(endereco.GetProperty("bairro"));
    Console.WriteLine(endereco.GetProperty("cidade"));

    Console.WriteLine($"Data Laudo: {root.GetProperty("dataLaudo")}");
    Console.WriteLine($"Data Perícia: {root.GetProperty("dataPericia")}");

    return Results.Ok();
});
app.Run();

public record LaudoRequest(string Tipo);