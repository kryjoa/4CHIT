using SwAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

SwEndpoints.Map(app);

app.Run();