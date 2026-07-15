using System.Globalization;
using Estacionamento.Dados.Interfaces;
using Estacionamento.Dados.Repositorios;
using Estacionamento.Negocio.Interfaces;
using Estacionamento.Negocio.Services;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRegistroEstacionamentoRepository>(_ => new RegistroEstacionamentoRepository(connectionString));
builder.Services.AddScoped<ITabelaPrecoRepository>(_ => new TabelaPrecoRepository(connectionString));
builder.Services.AddScoped<IEstacionamentoService, EstacionamentoService>();
builder.Services.AddScoped<ITabelaPrecoService, TabelaPrecoService>();

var app = builder.Build();

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = new[] { new CultureInfo("en-US") },
    SupportedUICultures = new[] { new CultureInfo("en-US") }
};
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Estacionamento}/{action=Index}/{id?}");

app.Run();
