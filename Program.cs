using AspNet_MVC.Models.Entidades;
using Models.Data;
using Models.Entidades;
using Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<PacientesRepository>();
builder.Services.AddScoped<ConsultasRepository>();
builder.Services.AddScoped<MedicosRepository>();
builder.Services.AddScoped<AmbulatoriosRepository>();
builder.Services.AddScoped<FuncionariosRepository>();
builder.Services.AddScoped<ConsultasServices>();
builder.Services.AddScoped<AmbulatoriosServices>();
builder.Services.AddScoped<FuncionariosServices>();
builder.Services.AddScoped<MedicosServices>();
builder.Services.AddScoped<PacientesServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
