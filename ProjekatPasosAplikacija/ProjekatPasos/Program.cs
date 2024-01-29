using AplikacioniSloj;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Interfejsi;
using SlojPodataka.Repozitorijumi;
using DomenskiSloj;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IKorisnikRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsKorisnikRepo(stringKonekcije);
});

builder.Services.AddScoped<IZahtevRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsZahtevRepo(stringKonekcije);
});

builder.Services.AddScoped<IPasosRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsPasosRepo(stringKonekcije);
});

builder.Services.AddScoped<ITerminRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsTerminRepo(stringKonekcije);
});

builder.Services.AddScoped<clsKorisnikServis>();
builder.Services.AddScoped<clsZahtevServis>();
builder.Services.AddScoped<clsTerminServis>();
builder.Services.AddScoped<clsPasosServis>();
builder.Services.AddScoped<clsPoslovnaPravila>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Pocetna}/{id?}");

app.Run();
