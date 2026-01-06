// Eu preciso contruir as APIs
// o que eu preciso para construir apis?
// eu preciso de um servidor web
// como eu crio um servidor web?
// eu posso usar o ASP.NET Core para criar um servidor web

// Eu preciso descobrir como armazenar os dados dinamicamente e depois envia-los para um database

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();