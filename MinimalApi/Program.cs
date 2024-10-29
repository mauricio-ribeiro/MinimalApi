using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Adicionando servi�os para o cont�iner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurando o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint para calcular a soma
app.MapGet("/sum", ([FromQuery] double? a, [FromQuery] double? b) =>
{
    // Verificar se ambos os par�metros est�o presentes
    if (!a.HasValue || !b.HasValue)
    {
        return Results.BadRequest(new { error = "Os par�metros 'a' e 'b' s�o obrigat�rios." });
    }

    // Calcular a soma
    var result = a.Value + b.Value;

    // Retornar o resultado como JSON
    return Results.Ok(new { result });
})
.WithName("GetSum") // Nome do endpoint
.Produces(200) // Indica que este endpoint retorna um c�digo de status 200
.Produces(400); // Indica que este endpoint pode retornar um c�digo de status 400

app.Run();
