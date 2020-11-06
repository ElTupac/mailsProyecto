using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mailProyecto.Models;
using System.Net.Mail;
using System.Net;

namespace mailProyecto.Controllers
{
    


    public class HomeController : Controller
    {
        private readonly NotasContext db;
       /*  public HomeController(NotasContext context)
        {
            db = context;
        } */

        [HttpPost]
        public IActionResult AgregarNota(string titulo, string cuerpo)
        {
            Nota nuevaNota = new Nota(){
                Titulo = titulo,
                Cuerpo = cuerpo
            };
            db.Notas.Add(nuevaNota);
            db.SaveChanges();

            ViewBag.tituloNota = titulo;
            ViewBag.cuerpoNota = cuerpo;

            return View("NotaCreada");
        }

        [HttpGet]
        public IActionResult ConsultarNotas(long ID)
        {
            Nota notaConsulta = db.Notas.FirstOrDefault(n => n.ID == ID);

            if(notaConsulta == null)
            {
                return View("NotaInexistente");
            } 
            else
            {
                ViewBag.titulo = notaConsulta.Titulo;
                ViewBag.cuerpo = notaConsulta.Cuerpo;

                return View("NotaConsultada");
            }
        }

        public string myMail = "juanzinhoperezinho@gmail.com";
        public string myPassword = "grupo3FTW";
        private readonly ILogger<HomeController> _logger;

        public string NombreContacto(string nombre)
        {
            return $"Gracias por el mensaje {nombre}";
        }

        [HttpPost]
        public IActionResult EnviarContacto(string nombre, string mail, string mensaje)
        {
            ViewBag.nombre = nombre;
            ViewBag.mail = mail;
            ViewBag.mensaje = mensaje;

            var smtpClient = new SmtpClient("smtp.gmail.com"){
                Port = 587,
                Credentials = new NetworkCredential(myMail, myPassword),
                EnableSsl = true,
            };

            string mensajeMail = $"{nombre}, tu mensaje fue recibido. Nos pondremos en contacto con usted.\n Su mensaje fue: {mensaje}";

            smtpClient.Send(myMail, mail, $"{nombre}, gracias por tu mensaje", mensajeMail);
            smtpClient.Send(myMail, myMail, $"Llego un mail de {mail}", $"{mensaje}");
            
            return View("Saludo");
        }
        public HomeController(ILogger<HomeController> logger, NotasContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult MisNotas()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
