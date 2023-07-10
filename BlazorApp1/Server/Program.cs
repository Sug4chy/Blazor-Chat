using BlazorApp1.Server.Data;
using BlazorApp1.Server.Extensions;
using BlazorApp1.Server.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite("Data source=app.db");
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

#region Cookies Auth

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromHours(1);
		options.SlidingExpiration = true;
	});

#endregion
#region MyRegion

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//	.AddJwtBearer(options =>
//	{
//		options.RefreshInterval = TimeSpan.FromMinutes(30);
//	});

#endregion

builder.Services.AddDefaultServices();
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
    app.UseHsts();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();