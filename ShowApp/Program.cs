using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShowContext>(option =>
                    option.UseSqlite("Data source =show.db"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShowContext>();
    ShowSeeder.SetShowSeeder(context);
}

if(app.Environment.IsDevelopment()){
app.UseSwagger();
app.UseSwaggerUI();
}

app.MapGet("/", () => "Bienvenue sur mon app de gestion de film !");

app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();