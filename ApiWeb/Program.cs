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
        Console.WriteLine($"- {nome}");
    }

    return Results.Ok();
});

app.Run();

public record LaudoRequest(string Tipo);