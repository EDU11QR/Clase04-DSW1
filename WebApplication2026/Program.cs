using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 🗺️ CREAMOS UNA RUTA DE PRUEBA (puedes entrar desde tu navegador)
app.MapGet("/probar-conexion", () =>
{
    // 1. Leemos la cadena de conexión del appsettings.json
    string connectionString = builder.Configuration.GetConnectionString("cn");

// 2. Intentamos conectar
using (SqlConnection conexion = new SqlConnection(connectionString))
{
    try
    {
        conexion.Open();
        // Si todo sale bien, el navegador mostrará este mensaje:
        return "¡Conexión CORRECTA con la base de datos SQL SERVER !!!!!!!!!!!!";
        //por consola
        //Console.WriteLine("¡Conexión exitosa a la base de datos bdt1!");
    }
    catch (SqlException ex)
    {
        // Si hay un error (por ejemplo, que no creaste la base de datos bdt1 aún)
        return $"Error al conectar: {ex.Message}";
    }
}
});

app.Run();
