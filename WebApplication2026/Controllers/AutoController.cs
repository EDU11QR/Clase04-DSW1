using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Drawing;
using WebApplication2026.Models;

namespace WebApplication2026.Controllers
{
    public class AutoController : Controller
    {

        public readonly IConfiguration _config;

        public AutoController(IConfiguration IConfig) { 
        
            _config = IConfig;
        
        }// fin del constructor....

        public IActionResult Index()
        {
            return View();
        } // fin del index

        //****** codigo para listar autos y marcas
        IEnumerable<ClassAuto> autos() { 
        
            List<ClassAuto> aut = new List<ClassAuto>();
            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"])){
                SqlCommand cmd = new SqlCommand("sp_listar_auto", cn);
                // Aperturamos la BD....
                cn.Open();
                //realizamos la respectiva ejecucion
                SqlDataReader dr=cmd.ExecuteReader();
                // aplicamos un bucle.....
                while (dr.Read()) {

                    aut.Add(new ClassAuto()
                    {
                        // recuperamos lo que viene de la BD....
                        // y almacenamos en las propiedades
                        Idauto = dr.GetInt32(0),
                        Modelo = dr.GetString(1),
                        Color = dr.GetString(2),
                        Motor = dr.GetString(3),
                        Precio = dr.GetDecimal(4),
                        Nommarca = dr.GetString(5),
                        Idmarca = dr.GetInt32(6)
                    }); // fin de agregar listado autos ..........
                } // fin del bucle

            } // fin del using......
            // retornamos listado
            return aut;
        } // fin del metodo
        // retornamos la lista de todos los autos registrados en la bd.......

        public async Task<IActionResult> ListadoAutos() {

            // retornamos hacia la vista
            return View(await Task.Run(() => autos()));
        
        }

        //listado para cargar el select de marca...

        //codigo para recuperar registros de marca...

        IEnumerable<ClassMarca> marcas()

        {

            List<ClassMarca> mar = new List<ClassMarca>();

            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))

            {

                SqlCommand cmd = new SqlCommand("sp_listar_marca", cn);

                //aperturamos la base de datos...

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                //aplicamos un while...

                while (dr.Read())

                {

                    //recupermos los datos de la base de datos

                    //y almacenamos en las propiedades...

                    mar.Add(new ClassMarca()

                    {

                        Idmarca = dr.GetInt32(0),

                        Nommarca = dr.GetString(1)



                    }); //fin de agregar al listado



                } //fin del bucle while...

            } //fin de using...



            //retornamos la data recuperada

            return mar;

        } //fin del Ienumerable marca...



        [HttpGet]



        //siver para cargar el select de datos idmarca,marca....

        public async Task<IActionResult> Create()

        {

            ViewBag.marcas = new SelectList(await Task.Run(() => marcas()), "Idmarca", "Nommarca");

            ClassAuto auto = new ClassAuto();

            //retornamos los valores

            return View(auto);



        } //fin del metodo...



        [HttpPost]

        public async Task<IActionResult> Create(ClassAuto model)

        {

            //aplicamos un if....



            if (!ModelState.IsValid)

            {

                ViewBag.marcas = new SelectList(await Task.Run(() => marcas()), "Idmarca", "Nommarca", model.Idmarca);

                return View(model);

            }  //fin del if...



            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]))

            {

                //aplicamos un try catch..

                try

                {

                    //aperturamos la base de datos...

                    cn.Open();

                    SqlCommand cmd = new SqlCommand("sp_merge_auto", cn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //agregamos los parametros...

                    cmd.Parameters.AddWithValue("@cod", 0);

                    cmd.Parameters.AddWithValue("@mod", model.Modelo);

                    cmd.Parameters.AddWithValue("@col", model.Color);

                    cmd.Parameters.AddWithValue("@mot", model.Motor);

                    cmd.Parameters.AddWithValue("@pre", model.Precio);

                    cmd.Parameters.AddWithValue("@codmar", model.Idmarca);

                    //realizamos la ejecucion...

                    int c = cmd.ExecuteNonQuery();

                    mensaje = $"registro insertado {c} de auto";

                }

                catch (Exception ex)

                {

                    mensaje = ex.Message;

                } //fin del catch....



            } //fin del using....

            //enviamos el mensaje a la vista...

            ViewBag.mensaje = mensaje;

            ViewBag.marcas = new SelectList(await Task.Run(() => marcas()), "Idmarca", "Nommarca", model.Idmarca);

            //redireccionamos

            return RedirectToAction("ListadoAutos", "Auto");



        } //fin del metodo create POST...

    } // fin de la clase
}
