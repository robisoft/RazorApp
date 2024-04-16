using crossPublisher;
using crossPublisherRazor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RazorApp.Services;
using RazorApp.ViewComponents;
using System.Net.NetworkInformation;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();  // ho aggiunto HttpContext per poter ottenere URL. NavigationManager è di Blazor.
// la riga sopra serve per rendere disponibile HttpContext al fuori dei controllers o dei pagemodels. non ha senso iniettarlo come ho fatto io
// poichè è HttpContext è già presente nelle classi ereditate 'ControllerBase' e 'PageModel'.
// Quindi in questi contesti si può usare direttamente HttpContext senza iniettarlo


var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
if (appSettings != null && !System.String.IsNullOrEmpty(appSettings.Store))
    appSettings.Store = "admin";

// Tutti condividono il settaggio del sito web (connString, hostname, ecc)
builder.Services.AddSingleton(appSettings!);

builder.Services.AddScoped<WebAppSession>();
// WebAppSession contiene la currPage, currUser, ecc e quindi è personale x ogni utente che si collega al sito
// Deve pertanto essere Scoped

// Da eliminare! Ogni file deve avere il suo codice, altrimenti diventa ingestibile il sito
// Ad esempio tutta la logica per costruire l'header deve stare in header.cshtml
builder.Services.AddScoped<IPageService, PageService>();


////se volessi usare le sessioni, ad esempio per memorizzare le preferenze dell'utente come ad esempio 'lang', devo configurare il supporto per le sessioni:
//builder.Services.AddSession(options =>
//{
//    // Configura le opzioni della sessione qui, se necessario
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // Esempio: timeout della sessione di 30 minuti
//});

// se poi voglio usare le sessioni, nella pagina devo fare così:
//@using Microsoft.AspNetCore.Http

//@{
//    HttpContext.Session.SetString("Key", "Value");
//    var value = HttpContext.Session.GetString("Key");
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
// app.UseSession(); //sempre se voglio usare le sessioni
app.Run();
