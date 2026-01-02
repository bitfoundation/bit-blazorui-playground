using Bit.BlazorUIPlayground.Components;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment() is false)
{
    builder.Services.AddResponseCompression(opts =>
        opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/wasm", "application/octet-stream"]));
}

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

builder.Services.AddBitBlazorUIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment() is false)
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (builder.Environment.IsDevelopment() is false)
{
    app.UseHttpsRedirection();
    app.UseResponseCompression();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
