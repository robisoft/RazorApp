using crossPublisher;
using crossPublisherRazor;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Security.Policy;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();  // ho aggiunto HttpContext per poter ottenere URL. NavigationManager è di Blazor.
// la riga sopra serve per rendere disponibile HttpContext al fuori dei controllers o dei pagemodels. non ha senso iniettarlo come ho fatto io
// poichè è HttpContext è già presente nelle classi ereditate 'ControllerBase' e 'PageModel'.
// Quindi in questi contesti si può usare direttamente HttpContext senza iniettarlo


var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
if (appSettings != null && !String.IsNullOrEmpty(appSettings.Store))
    appSettings.Store = "admin";

builder.Services.AddSingleton(appSettings!);
builder.Services.AddScoped<WebAppSession>();


// non so se ha senso, ma potrei ricavare la lista delle pages con le relative proprietà qui, per evitare di ricalcolarela ogni volta.
// così sarebbe disponibile tramite DI. Mi servirebbe il valore di host, che potrebbe essere passato direttamete da qui.



// se volessi usare le sessioni, ad esempio per memorizzare le preferenze dell'utente come ad esempio 'lang', devo configurare il supporto per le sessioni:
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
