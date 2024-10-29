using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços para o contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurando o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint para calcular a soma
app.MapGet("/sum", ([FromQuery] double? a, [FromQuery] double? b) =>
{
    // Verificar se ambos os parâmetros estão presentes
    if (!a.HasValue || !b.HasValue)
    {
        return Results.BadRequest(new { error = "Os parâmetros 'a' e 'b' são obrigatórios." });
    }

    // Calcular a soma
    var result = a.Value + b.Value;

    // Retornar o resultado como JSON
    return Results.Ok(new { result });
})
.WithName("GetSum") // Nome do endpoint
.Produces(200) // Indica que este endpoint retorna um código de status 200
.Produces(400); // Indica que este endpoint pode retornar um código de status 400

app.Run();
