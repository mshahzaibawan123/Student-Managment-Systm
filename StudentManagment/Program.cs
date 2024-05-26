using DataAccessLayerDAL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using StudentManagment.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
/*
builder.Services.AddSingleton<AttendanceDAL>();
builder.Services.AddScoped<CourseRepository>(_ => new CourseRepository("Data Source=DESKTOP-573DLSK\\SQLEXPRESS;Initial Catalog=StudentManagment;Integrated Security=True"));
    }
*/










var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
