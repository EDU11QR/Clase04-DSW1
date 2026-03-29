using Microsoft.AspNetCore.Mvc;
using WebApplication2026.Models;

namespace WebApplication2026.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } // fin de metodo

        public IActionResult Cotizar() {
            
            return View();
        } // fin del metodo

        public IActionResult CalculoCotizar(ClassCotizar objcotizar) {
            //pequeño algoritmo...
            //aplicamos un switch....
            string vehi = objcotizar.Tvehi;
            switch (vehi) {

                case "camaro":
                    // asignamos un precio
                    objcotizar.Precio = 14000;
                    objcotizar.Total  = objcotizar.Precio * objcotizar.Cantidad;
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

            // retorna la visra
            return View(objcotizar);

        } // fin del metodo
    }
}
