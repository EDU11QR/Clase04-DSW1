using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication2026.Models;

namespace WebApplication2026.Controllers
{
    public class CompraController : Controller
    {
        public readonly IConfiguration _config;

        //creamos un constructor..
        public CompraController(IConfiguration IConfig) { 
            
            _config = IConfig;
        
        }// fin del constructor

        public IActionResult Index()
        {
            return View();
        } // fin de metodo

        public IActionResult Cotizar() {
            
            return View();
        } // fin del metodo

        [HttpPost]
        public IActionResult CalculoCotizar(ClassCotizar objcotizar) {
            //pequeño algoritmo...
            //aplicamos un switch....
            string vehi = objcotizar.Tvehi;
            string mensaje = "";
            switch (vehi) {

                case "camaro":
                    // asignamos un precio
                    objcotizar.Precio = 14000;
                    objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;
                    break;

                case "camioneta":
                    // asignamos un precio
                    objcotizar.Precio = 20000;
                    objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;
                    break;

                case "comercial":
                    // asignamos un precio
                    objcotizar.Precio = 10000;
                    objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;
                    break;

                case "cybertruck":
                    // asignamos un precio
                    objcotizar.Precio = 35000;
                    objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;
                    break;

                case "pickup":
                    // asignamos un precio
                    objcotizar.Precio = 8000;
                    objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;
                    break;

                case "sedan":
                    // asignamos un precio
                    objcotizar.Precio = 12000;
                    objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;
                    break;

            } //fin del switch

            //******* insertar a la base de datos
            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))
            {


                try {
                    //aperturamos la BD
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_registrar_cotizar", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cli", objcotizar.Cliente);
                    cmd.Parameters.AddWithValue("@con", objcotizar.Conce);
                    cmd.Parameters.AddWithValue("@cant", objcotizar.Cantidad); 
                    cmd.Parameters.AddWithValue("@pre", objcotizar.Precio); 
                    cmd.Parameters.AddWithValue("@tvhi", objcotizar.Tvehi); 
                    cmd.Parameters.AddWithValue("@tot", objcotizar.Total); 
                    //*** realizamos la ejecucion del p.a.
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"registro insertado{c}en bd";

                }
                catch (Exception ex) {

                    mensaje = ex.Message;

                }
            }// fin de using

            // retorna la visra
            return RedirectToAction("listadoCotizaciones");

        } // fin del metodo


        //************** creamos codigos para listar cotizacion

        IEnumerable<ClassCotizar> cotizaciones()
        {
            List<ClassCotizar> cotizaciones = new List<ClassCotizar>();
            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))
            {
                /// aplicamos las base de datos
                cn.Open();
                // aplicamos el SQLCOMMAND
                SqlCommand cmd = new SqlCommand("sp_listar_cotizar",cn);

                // apicamos el sqldatareader
                SqlDataReader dr = cmd.ExecuteReader();
                // aplicamos un bucle

                while (dr.Read())
                {
                    cotizaciones.Add(new ClassCotizar()
                    {
                        idcoti = dr.GetInt32(0),
                        Cliente = dr.GetString(1),
                        Conce = dr.GetString(2),
                        Cantidad = dr.GetInt32(3),
                        Precio = dr.GetInt32(4),
                        Tvehi = dr.GetString(5),
                        Total = dr.GetInt32(6)

                    });// fin del bucle
                }// RETIORNO
                 // retornamos la data recuperar de la bd
                return cotizaciones;

            }
        }

        // Editar cotizacion

        // ===================== EDITAR =====================

        // GET: Editar
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ClassCotizar cot = new ClassCotizar();

            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("sp_buscar_cotizar", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cot.idcoti = Convert.ToInt32(dr["idcoti"]);
                        cot.Cliente = dr["cliente"].ToString();
                        cot.Conce = dr["conce"].ToString();
                        cot.Cantidad = Convert.ToInt32(dr["cantidad"]);
                        cot.Precio = Convert.ToInt32(dr["precio"]);
                        cot.Tvehi = dr["tvhi"].ToString();
                        cot.Total = Convert.ToInt32(dr["total"]);
                    }
                }
            }

            return View(cot);
        }


        // POST: Editar
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit_Post(ClassCotizar objcotizar)
        {
            string vehi = objcotizar.Tvehi;
            string mensaje = "";

            switch (vehi)
            {
                case "camaro":
                    objcotizar.Precio = 14000;
                    break;

                case "camioneta": 
                    objcotizar.Precio = 20000;
                    break;

                case "comercial":
                    objcotizar.Precio = 10000;
                    break;

                case "cybertruck":
                    objcotizar.Precio = 35000;
                    break;

                case "pickup":
                    objcotizar.Precio = 8000;
                    break;

                case "sedan":
                    objcotizar.Precio = 12000;
                    break;

                default:
                    objcotizar.Precio = 0;
                    break;
            }

            objcotizar.Total = objcotizar.Precio * objcotizar.Cantidad;

            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizar_cotizar", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cod", objcotizar.idcoti);
                    cmd.Parameters.AddWithValue("@cli", objcotizar.Cliente);
                    cmd.Parameters.AddWithValue("@con", objcotizar.Conce);
                    cmd.Parameters.AddWithValue("@cant", objcotizar.Cantidad);
                    cmd.Parameters.AddWithValue("@pre", objcotizar.Precio);
                    cmd.Parameters.AddWithValue("@tv", objcotizar.Tvehi);
                    cmd.Parameters.AddWithValue("@tot", objcotizar.Total);

                    cn.Open();
                    int c = cmd.ExecuteNonQuery();

                    mensaje = $"Registro actualizado correctamente: {c}";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }

            TempData["mensaje"] = mensaje;
            return RedirectToAction("listadoCotizaciones");
        }

        // ===================== ELIMINAR =====================

        public IActionResult Delete(int id)
        {
            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_eliminar_cotizar", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod", id);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("listadoCotizaciones");
        } 



        //************** metodo que retorna el listado de cotizaciones
        // registrado en bd.....

        public async Task<IActionResult> listadoCotizaciones(int p = 0)
        {
            int nr = 5;
            int tr = cotizaciones().Count();
            int paginas = tr % nr > 0 ? tr / nr + 1 : tr / nr;

            ViewBag.paginas = paginas;

            return View(await Task.Run(() => cotizaciones().Skip(p * nr).Take(nr)));
        }



    }
}
