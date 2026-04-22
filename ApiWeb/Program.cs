using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiWeb.Entities.Insalubridade;
using ApiWeb.Entities.Periculosidade;
using ApiWeb.Entities.Corpo;
using ApiWeb.Entities.Ambos;
using ApiWeb.Services.PericulosidadeServices;

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



app.MapPost("/api/laudo-pericu", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var json = JsonDocument.Parse(body);
    var root = json.RootElement;

    Console.WriteLine("=== PERICULOSIDADE ===");

string tipo = root.TryGetProperty("tipo", out var t)? t.GetString() ?? "": "";
Console.WriteLine($"Tipo: {tipo}");

string numeroPericia = root.TryGetProperty("numeroPericia", out var n)? n.GetString() ?? "": "";
    List<string> reclamantes = new List<string>();
    
    foreach (var item in root.GetProperty("reclamantes").EnumerateArray())
    {
        var valor = item.GetString();
        if (valor != null)
            reclamantes.Add(valor);
    }
    
    
    
    List<string> reclamadas = new List<string>();
    foreach (var item in root.GetProperty("reclamadas").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            reclamadas.Add(valor);
    }

    List<string> anexosPericu = new List<string>();
    foreach (var item in root.GetProperty("anexos").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            anexosPericu.Add(valor);
    }

    List<string> documentos = new List<string>();
    foreach (var item in root.GetProperty("documentos").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            documentos.Add(valor);
    }
    List<string> presentesFuncao = new List<string>();
    foreach (var item in root.GetProperty("presentesFuncao").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
        {
            presentesFuncao.Add(valor);
        }
    }   

    var local = root.GetProperty("localTrabalho");
    string localAtividade = local.GetProperty("local").GetString() ?? "";
    string paredes = local.GetProperty("paredes").GetString() ?? "";
    string pisos = local.GetProperty("pisos").GetString() ?? "";
    string iluminacao = local.GetProperty("iluminacao").GetString() ?? "";
    string ventilacao = local.GetProperty("ventilacao").GetString() ?? "";


    var endereco = root.GetProperty("endereco");
    string rua    = endereco.GetProperty("rua").GetString() ?? "";
    string numero = endereco.GetProperty("numero").GetString() ?? "";
    string bairro = endereco.GetProperty("bairro").GetString() ?? "";
    string cidade = endereco.GetProperty("cidade").GetString() ?? "";


    string dataLaudo = root.GetProperty("dataLaudo").GetString() ?? "";
    string dataPericia = root.GetProperty("dataPericia").GetString() ?? "";

    


    Periculosidade periculo = new Periculosidade(numeroPericia, dataPericia, dataLaudo, new LocalDeTrabalho(localAtividade, paredes, pisos, iluminacao, ventilacao), new Endereco(rua, numero, cidade, bairro), reclamantes, reclamadas, documentos, anexosPericu, presentesFuncao);
    System.Console.WriteLine(periculo);

     if (string.IsNullOrEmpty(periculo.NumeroPericia))
    {
        return Results.BadRequest("NumeroPericia é obrigatório.");
    }
    System.Console.WriteLine(periculo.NumeroPericia);

        foreach(var presentes in periculo.PresentesFuncao) {
                System.Console.WriteLine(presentes);
            }


    var service = new PericulosidadeServices();
    service.GerarDocumentoPericulosidade(periculo, tipo);

    return Results.Ok( );
});



app.MapPost("/api/laudo-insalub", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var json = JsonDocument.Parse(body);
    var root = json.RootElement;

    Console.WriteLine("=== INSALUBRIDADE ===");

    Console.WriteLine($"Tipo: {root.GetProperty("tipo")}");
string numeroPericia = root.TryGetProperty("numeroPericia", out var n)? n.GetString() ?? "": "";


    List<string> reclamantes = new List<string>();
    
    foreach (var item in root.GetProperty("reclamantes").EnumerateArray())
    {
        var valor = item.GetString();
        if (valor != null)
            reclamantes.Add(valor);
    }    
    
    List<string> reclamadas = new List<string>();
    foreach (var item in root.GetProperty("reclamadas").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            reclamadas.Add(valor);
    }

    List<string> anexosInsalub = new List<string>();
    foreach (var item in root.GetProperty("anexos").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            anexosInsalub.Add(valor);
    }

    List<string> documentos = new List<string>();
    foreach (var item in root.GetProperty("documentos").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            documentos.Add(valor);
    }

        var local = root.GetProperty("localTrabalho");
    string localAtividade = local.GetProperty("local").GetString() ?? "";
    string paredes = local.GetProperty("paredes").GetString() ?? "";
    string pisos = local.GetProperty("pisos").GetString() ?? "";
    string iluminacao = local.GetProperty("iluminacao").GetString() ?? "";
    string ventilacao = local.GetProperty("ventilacao").GetString() ?? "";


    var endereco = root.GetProperty("endereco");
    string rua    = endereco.GetProperty("rua").GetString() ?? "";
    string numero = endereco.GetProperty("numero").GetString() ?? "";
    string bairro = endereco.GetProperty("bairro").GetString() ?? "";
    string cidade = endereco.GetProperty("cidade").GetString() ?? "";


    string dataLaudo = root.GetProperty("dataLaudo").GetString() ?? "";
    string dataPericia = root.GetProperty("dataPericia").GetString() ?? "";

    Insalubridade insalub = new Insalubridade(numeroPericia, dataPericia, dataLaudo, new LocalDeTrabalho(localAtividade, paredes, pisos, iluminacao, ventilacao), new Endereco(rua, numero, cidade, bairro), reclamantes, reclamadas, documentos, anexosInsalub);
    System.Console.WriteLine(insalub);
    return Results.Ok();
});



app.MapPost("/api/laudo-ambos", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var json = JsonDocument.Parse(body);
    var root = json.RootElement;

    Console.WriteLine("=== LAUDO AMBOS ===");

string numeroPericia = root.TryGetProperty("numeroPericia", out var n)? n.GetString() ?? "": "";
    List<string> reclamantes = new List<string>();
    
    foreach (var item in root.GetProperty("reclamantes").EnumerateArray())
    {
        var valor = item.GetString();
        if (valor != null)
            reclamantes.Add(valor);
    }
    
    
    
    List<string> reclamadas = new List<string>();
    foreach (var item in root.GetProperty("reclamadas").EnumerateArray()){
        var valor = item.GetString();
        if (valor != null)
            reclamadas.Add(valor);
    }
// ANEXOS PERICULOSIDADE
List<string> anexosPericulosidade = new List<string>();
if (root.TryGetProperty("anexosPericulosidade", out var pericArray) &&
    pericArray.ValueKind == JsonValueKind.Array)
{
    foreach (var item in pericArray.EnumerateArray())
    {
        var valor = item.GetString();
        if (valor != null)
            anexosPericulosidade.Add(valor);
    }
}


// ANEXOS INSALUBRIDADE
List<string> anexosInsalubridade = new List<string>();
if (root.TryGetProperty("anexosInsalubridade", out var insalArray) &&
    insalArray.ValueKind == JsonValueKind.Array)
{
    foreach (var item in insalArray.EnumerateArray())
    {
        var valor = item.GetString();
        if (valor != null)
            anexosInsalubridade.Add(valor);
    }
}


// DOCUMENTOS
List<string> documentos = new List<string>();
if (root.TryGetProperty("documentos", out var docsArray) &&
    docsArray.ValueKind == JsonValueKind.Array)
{
    foreach (var item in docsArray.EnumerateArray())
    {
        var valor = item.GetString();
        if (valor != null)
            documentos.Add(valor);
    }
}

    var local = root.GetProperty("localTrabalho");
    string localAtividade = local.GetProperty("local").GetString() ?? "";
    string paredes = local.GetProperty("paredes").GetString() ?? "";
    string pisos = local.GetProperty("pisos").GetString() ?? "";
    string iluminacao = local.GetProperty("iluminacao").GetString() ?? "";
    string ventilacao = local.GetProperty("ventilacao").GetString() ?? "";


    var endereco = root.GetProperty("endereco");
    string rua    = endereco.GetProperty("rua").GetString() ?? "";
    string numero = endereco.GetProperty("numero").GetString() ?? "";
    string bairro = endereco.GetProperty("bairro").GetString() ?? "";
    string cidade = endereco.GetProperty("cidade").GetString() ?? "";


    string dataLaudo = root.GetProperty("dataLaudo").GetString() ?? "";
    string dataPericia = root.GetProperty("dataPericia").GetString() ?? "";

    Ambos ambos = new Ambos(numeroPericia, dataPericia, dataLaudo, new LocalDeTrabalho(localAtividade, paredes, pisos, iluminacao, ventilacao), new Endereco(rua, numero, cidade, bairro), reclamantes, reclamadas, documentos, anexosInsalubridade, anexosPericulosidade);
    System.Console.WriteLine(ambos);
    return Results.Ok();
});
app.Run();
public record LaudoRequest(string Tipo);