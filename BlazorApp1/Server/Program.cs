using BlazorApp1.Server;
using BlazorApp1.Server.Data;
using BlazorApp1.Server.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite("Data source=app.db");
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddTransientServices();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var app = builder.Build();
using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();