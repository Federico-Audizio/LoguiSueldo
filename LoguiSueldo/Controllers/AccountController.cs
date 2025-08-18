using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LoguiSueldo.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using LoguiSueldo.Models.Administracion;
using LoguiSueldo.Models.PlanSistema;

namespace LoguiSueldo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext dbc = new ApplicationDbContext();
        private LoguiContableContext db = new LoguiContableContext();

        public EmpresasController EmpresasController = new EmpresasController();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("IngresoSistema", "Account");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("Email", "Intento de inicio de sesión no válido.");
                    ViewBag.ModelError = "Intento de inicio de sesión no válido.";
                    return View(model);
            }
        }

        [Authorize]
        public ActionResult IngresoSistema()
        {
            var usuarioactual = User.Identity.GetUserId();

            var empresasUsuario = (from emp in db.PermisoUsuarios where emp.UsuarioID == usuarioactual && emp.Desvinculado == false select emp).ToList();
            if (empresasUsuario.Count == 1)//si ese usuario tiene una sola empresa, entre directamente
            {
                if (empresasUsuario[0].EmpresaID == 0)
                {
                    return RedirectToAction("Index", "Manage");
                }

                return RedirectToAction("SeleccionaEmpresa", "Manage", new { id = empresasUsuario[0].EmpresaID });
            }
            else
            {
                //PORQUE TIENE MAS DE UNA EMPRESA
                //REDIRIGIMOS AL INDEX DE MANAGE PARA MOSTRAR LAS EMPRESAS RELACIONADAS Y SI QUIERE CAMBIAR CONTRASEÑA
                return RedirectToAction("Index", "Manage");
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    try
                    {
                        string emailA = "loguisoft@gmail.com";
                        string emailDe = "loguisoftcomunica@hotmail.com";

                        string emailCredencial = "loguisoftcomunica@hotmail.com";
                        string contraseñaCredencial = "comunica89";

                        MailMessage msg = new MailMessage();
                        msg.To.Add(new MailAddress(emailA));

                        msg.From = new MailAddress(emailDe, "Nuevo registro de Usuario en Logui Contable", System.Text.Encoding.UTF8);

                        msg.Subject = "Mensaje de " + emailDe;
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;

                        msg.Body = "<p>" + "El usuario: <b>" + model.Email + "</b> se ha registrado en el sistema.</p>";

                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.IsBodyHtml = true;

                        SmtpClient clienteSmtp = new SmtpClient();
                        clienteSmtp.Host = "smtp-mail.outlook.com";
                        clienteSmtp.Port = 587;
                        clienteSmtp.UseDefaultCredentials = false;
                        clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        clienteSmtp.Credentials = new System.Net.NetworkCredential(emailCredencial, contraseñaCredencial);
                        clienteSmtp.EnableSsl = true;
                        clienteSmtp.Send(msg);
                    }
                    catch (Exception ex)
                    {

                    }

                    return RedirectToAction("RegistrarCliente", "Account");
                }
                ViewBag.Error = "Ya existe un usuario con el mismo email.";
            }
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }



        [Authorize]
        public ActionResult RegistrarCliente()
        {
            var usuarioactual = User.Identity.GetUserId();

            //VALIDAR QUE EL USUARIO ACTUAL NO TENGA CLIENTE ASOCIADO
            var existeCliente = (from o in db.ClientesLoguiSoft where o.UsuarioID == usuarioactual select o).Count();
            if (existeCliente > 0)
            {
                return RedirectToAction("RegistrarEmpresa");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCliente([Bind(Include = "ClienteID, RazonSocial, Telefono, Domicilio, NroTipoDocumento")] ClientesLogui clientesLogui)
        {
            var usuarioactual = User.Identity.GetUserId();

            //FIJAR INFORMACION DE CULTURA PARA FECHA Y DECIMALES
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            ZonaHorariaServidor zonaHorariaServidor = new ZonaHorariaServidor();

            // SI O SI DEBE INGRESAR NRO DE DOCUMENTO / CUIT Y VALIDAR SI EXISTE
            bool registrar = true;

            if (clientesLogui.NroTipoDocumento == null || clientesLogui.NroTipoDocumento == "" || clientesLogui.NroTipoDocumento == "0")
            {
                //DAR MENSAJE QUE DEBE INGRESAR UN NRO DOCUMENTO VALIDO
                ModelState.AddModelError("NroTipoDocumento", "Debe ingresar un Nº Documento / CUIT.");
                registrar = false;
            }
            else
            {
                //ARMADO DE MASCARA NRO DOCUMENTO/CUIT 
                String remplazado = clientesLogui.NroTipoDocumento.Replace("-", "");
                remplazado = remplazado.Replace("_", "");
                long number = Convert.ToInt64(remplazado);
                string fmt = "";

                clientesLogui.NroTipoDocumento = number.ToString(fmt);
                //ARMADO DE MASCARA NRO DOCUMENTO/CUIT 

                if (db.ClientesLoguiSoft.Any(x => x.UsuarioID == usuarioactual))
                {
                    ModelState.AddModelError("RazonSocial", "Ya existe un cliente asociado a su cuenta de Correo Electrónico.");
                    registrar = false;
                }

                if (db.ClientesLoguiSoft.Any(x => x.NroTipoDocumento == clientesLogui.NroTipoDocumento))
                {
                    ModelState.AddModelError("NroTipoDocumento", "Ya existe un cliente con el mismo Nº Documento / CUIT.");
                    registrar = false;
                }
            }

            if (ModelState.IsValid)
            {
                if (clientesLogui.RazonSocial != null)
                {
                    clientesLogui.RazonSocial = clientesLogui.RazonSocial.ToUpper();
                }
                if (clientesLogui.Domicilio != null)
                {
                    clientesLogui.Domicilio = clientesLogui.Domicilio.ToUpper();
                }
                if (clientesLogui.NroTipoDocumento == null || clientesLogui.NroTipoDocumento == "")
                {
                    clientesLogui.NroTipoDocumento = "0";
                }

                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        var clienteRegistrar = new ClientesLogui
                        {
                            RazonSocial = clientesLogui.RazonSocial,
                            Telefono = clientesLogui.Telefono,
                            Domicilio = clientesLogui.Domicilio,
                            NroTipoDocumento = clientesLogui.NroTipoDocumento,
                            FechaAlta = DateTime.Now.AddHours(zonaHorariaServidor.HorasRestar),
                            FechaBaja = Convert.ToDateTime("01/01/2000"),
                            UsuarioID = usuarioactual,
                            Activo = true
                        };
                        db.ClientesLoguiSoft.Add(clienteRegistrar);
                        db.SaveChanges();

                        transaccion.Commit();

                        return RedirectToAction("RegistrarEmpresa");

                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();

                        ViewBag.Error = "Error: " + ex.Message;
                    }
                }

            }

            return View(clientesLogui);
        }



        public JsonResult EditarCliente(int ClienteID, string RazonSocial, string Telefono, string Domicilio, int ContribuyenteID, int DocumentoID, string DNI)
        {
            //FIJAR INFORMACION DE CULTURA PARA FECHA Y DECIMALES
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");


            // SI O SI DEBE INGRESAR NRO DE DOCUMENTO / CUIT Y VALIDAR SI EXISTE
            bool registrar = true;

            //ARMADO DE MASCARA NRO DOCUMENTO/CUIT 
            String remplazado = DNI.Replace("-", "");
            remplazado = remplazado.Replace("_", "");
            long number = Convert.ToInt64(remplazado);
            string fmt = "";
            if (DocumentoID == 96)
            {
                fmt = "00000000";
            }
            else
            {
                fmt = "00000000000";
            }
            DNI = number.ToString(fmt);
            //ARMADO DE MASCARA NRO DOCUMENTO/CUIT 

            if (db.ClientesLoguiSoft.Any(x => x.NroTipoDocumento == DNI && x.ClienteID != ClienteID))
            {
                registrar = false;
            }

            if (registrar == true)
            {
                if (RazonSocial != null)
                {
                    RazonSocial = RazonSocial.ToUpper();
                }
                if (Domicilio != null)
                {
                    Domicilio = Domicilio.ToUpper();
                }

                var clienteLogui = (from cam in db.ClientesLoguiSoft where cam.ClienteID == ClienteID select cam).Single();

                clienteLogui.RazonSocial = RazonSocial;
                clienteLogui.Telefono = Telefono;
                clienteLogui.Domicilio = Domicilio;
                clienteLogui.TipoContribuyenteID = ContribuyenteID;
                clienteLogui.TipoDocumentoID = DocumentoID;
                clienteLogui.NroTipoDocumento = DNI;
                db.SaveChanges();
            }

            return Json(registrar, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        public ActionResult RegistrarEmpresa()
        {
            var usuarioactual = User.Identity.GetUserId();

            Empresa empresa = new Empresa();
            EmpresasController.EmpresaActual(usuarioactual, empresa);

            //VALIDAR ANTES DE CREAR EMPRESA QUE EL USUARIO ACTUAL ESTE ASOCIADO A UN CLIENTE
            var esCliente = (from act in db.ClientesLoguiSoft where act.UsuarioID == usuarioactual select act).ToList();
            if (esCliente.Count == 0)
            {
                return RedirectToAction("RegistrarCliente", "Account");
            }

            var planEmpresaUsuario = (from act in db.PlanEmpresa where act.UsuarioID == usuarioactual select act).ToList();
            ViewBag.CantEmpresasCreadas = planEmpresaUsuario.Count;

            //BUSCAMOS SI YA TIENE ALGUNA EMPRESA CREADA
            var empresasDemo = (from act in planEmpresaUsuario where act.EmpresaID == empresa.EmpresaID select act).Count();
            if (empresasDemo > 0)
            {
                //MENSAJE DE QUE YA TIENE UNA EMPRESA CREADA
                return RedirectToAction("Index", "Manage", new { Error = "2" });
            }

            var resultado = "";
            var registrarEmpresa = true;

            var cliente = esCliente[0];

            if (cliente.Activo == false)
            {
                //MENSAJE DE QUE DEBE ABONAR SU PLAN PORQUE ESTA BLOQUEADO
                resultado = "1";
                registrarEmpresa = false;
            }

            //Empresa empresa = new Empresa();
            empresa.RazonSocial = cliente.RazonSocial;
            empresa.NombreFantasia = cliente.RazonSocial;
            empresa.DireccionReal = cliente.Domicilio;
            empresa.Telefono1 = cliente.Telefono;
            empresa.NroTipoDocumento = cliente.NroTipoDocumento;

            if (registrarEmpresa == true && resultado != "1")
            {
                var localidad = (from cam in db.Localidades where cam.LocalidadID == 1 select cam).Single();
                ViewBag.LocalidadID = localidad.LocalidadID;
                ViewBag.LocalidadNombre = localidad.NombreVista;

                var tiposContribuyentes = (from o in db.TipoContribuyentes where o.Visible == true select o).ToList();
                ViewBag.TipoContribuyenteID = new SelectList(tiposContribuyentes.OrderBy(t => t.TipoContribuyenteNombre), "TipoContribuyenteID", "TipoContribuyenteNombre", cliente.TipoContribuyenteID);

                var tipoDocumentos = (from o in db.TipoDocumentos where o.Visible == true select o).ToList();
                ViewBag.TipoDocumentoID = new SelectList(tipoDocumentos.OrderBy(t => t.TipoDocumentoNombre), "TipoDocumentoID", "TipoDocumentoNombre", cliente.TipoDocumentoID);

                return View(empresa);
            }

            return RedirectToAction("Index", "Manage", new { Error = resultado });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarEmpresa([Bind(Include = "EmpresaID,RazonSocial,NombreFantasia,DireccionReal,LocalidadID, NroTipoDocumento,Telefono1,CorreoElectronico,TipoContribuyenteID,TipoDocumentoID,Email,Password,ConfirmPassword")] Empresa empresa)
        {
            var usuarioactual = User.Identity.GetUserId();

            //FIJAR INFORMACION DE CULTURA PARA FECHA Y DECIMALES
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            ZonaHorariaServidor zonaHorariaServidor = new ZonaHorariaServidor();

            // SI O SI DEBE INGRESAR NRO DE DOCUMENTO / CUIT Y VALIDAR SI EXISTE
            bool registrar = true;

            if (empresa.NroTipoDocumento == null || empresa.NroTipoDocumento == "" || empresa.NroTipoDocumento == "0")
            {
                //DAR MENSAJE QUE DEBE INGRESAR UN NRO DOCUMENTO VALIDO
                ModelState.AddModelError("NroTipoDocumento", "Debe ingresar un Nº Documento / CUIT.");
                registrar = false;
            }
            else
            {
                //ARMADO DE MASCARA NRO DOCUMENTO/CUIT 
                String remplazado = empresa.NroTipoDocumento.Replace("-", "");
                remplazado = remplazado.Replace("_", "");
                long number = Convert.ToInt64(remplazado);
                string fmt = "";
                if (empresa.TipoDocumentoID == 96)
                {
                    fmt = "00000000";
                }
                else
                {
                    fmt = "00000000000";
                }
                empresa.NroTipoDocumento = number.ToString(fmt);
                //ARMADO DE MASCARA NRO DOCUMENTO/CUIT   
            }

            if (ModelState.IsValid)
            {
                if (empresa.RazonSocial != null)
                {
                    empresa.RazonSocial = empresa.RazonSocial.ToUpper();
                }
                if (empresa.DireccionReal != null)
                {
                    empresa.DireccionReal = empresa.DireccionReal.ToUpper();
                }
                if (empresa.CorreoElectronico != null)
                {
                    empresa.CorreoElectronico = empresa.CorreoElectronico.ToLower();
                }
                if (empresa.NroTipoDocumento == null || empresa.NroTipoDocumento == "")
                {
                    empresa.NroTipoDocumento = "0";
                }


                if (empresa.TipoDocumentoID != 0 && empresa.TipoContribuyenteID != 0 && registrar == true)
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var empresaRegistrar = new Empresa
                            {
                                RazonSocial = empresa.RazonSocial,
                                NombreFantasia = empresa.NombreFantasia,
                                DireccionReal = empresa.DireccionReal,
                                LocalidadID = empresa.LocalidadID, //SIN LOCALIDAD POR DEFECTO
                                TipoContribuyenteID = empresa.TipoContribuyenteID,
                                TipoDocumentoID = empresa.TipoDocumentoID,
                                NroTipoDocumento = empresa.NroTipoDocumento,
                                Telefono1 = empresa.Telefono1,
                                CorreoElectronico = empresa.CorreoElectronico,
                                Cargado = DateTime.Now.AddHours(zonaHorariaServidor.HorasRestar),
                                UsuarioTitular = usuarioactual
                            };
                            db.Empresas.Add(empresaRegistrar);
                            db.SaveChanges();

                            var clienteLogui = db.ClientesLoguiSoft.Where(p => p.UsuarioID == usuarioactual).Single();

                            var nuevoPlanEmpresa = new PlanEmpresa
                            {
                                PlanID = 1,
                                UsuarioID = usuarioactual,
                                FechaCreado = DateTime.Today.Date,
                                EstadoPlan = EstadoPlan.Activo,
                                ClienteID = clienteLogui.ClienteID,
                                EmpresaID = empresaRegistrar.EmpresaID
                            };
                            db.PlanEmpresa.Add(nuevoPlanEmpresa);
                            db.SaveChanges();

                            var persona = new Persona
                            {
                                NombreCompleto = empresa.RazonSocial,
                                NombreFantasia = empresa.NombreFantasia,
                                DireccionReal = empresa.DireccionReal,
                                LocalidadID = empresaRegistrar.LocalidadID,
                                TipoContribuyenteID = empresa.TipoContribuyenteID,
                                TipoDocumentoID = empresa.TipoDocumentoID,
                                NroTipoDocumento = empresa.NroTipoDocumento,
                                Telefono1 = empresa.Telefono1,
                                CorreoElectronico = empresa.CorreoElectronico,
                                EmpresaID = empresaRegistrar.EmpresaID
                            };
                            db.Personas.Add(persona);
                            db.SaveChanges();

                            var usuarioSuperusuario = new PermisoUsuario
                            {
                                UsuarioID = usuarioactual,
                                PersonaID = persona.PersonaID,
                                PermisoID = 1,
                                UsuarioCarga = usuarioactual,
                                Digitos = 0,
                                CodigoSeguridad = "",
                                SolicitarCodigo = 0,
                                EmpresaID = empresaRegistrar.EmpresaID
                            };
                            db.PermisoUsuarios.Add(usuarioSuperusuario);
                            db.SaveChanges();

                            transaccion.Commit();

                            return RedirectToAction("SeleccionaEmpresa", "Manage", new { id = empresaRegistrar.EmpresaID });

                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();

                            ViewBag.Error = "Error: " + ex.Message;
                        }
                    }
                }
                else
                {
                    if (empresa.TipoDocumentoID == 0)
                    {
                        ModelState.AddModelError("TipoDocumentoID", "Seleccione un tipo de documento.");
                    }
                    if (empresa.TipoContribuyenteID == 0)
                    {
                        ModelState.AddModelError("TipoContribuyenteID", "Seleccione un tipo de contribuyente.");
                    }
                }
            }

            var localidad = (from cam in db.Localidades where cam.LocalidadID == 1 select cam).Single();
            ViewBag.LocalidadID = localidad.LocalidadID;
            ViewBag.LocalidadNombre = localidad.NombreVista;

            var tiposContribuyentes = (from o in db.TipoContribuyentes where o.Visible == true select o).ToList();
            ViewBag.TipoContribuyenteID = new SelectList(tiposContribuyentes.OrderBy(t => t.TipoContribuyenteNombre), "TipoContribuyenteID", "TipoContribuyenteNombre", empresa.TipoContribuyenteID);

            var tipoDocumentos = (from o in db.TipoDocumentos where o.Visible == true select o).ToList();
            ViewBag.TipoDocumentoID = new SelectList(tipoDocumentos.Where(t => t.TipoDocumentoID == empresa.TipoDocumentoID).OrderBy(t => t.TipoDocumentoNombre), "TipoDocumentoID", "TipoDocumentoNombre", empresa.TipoDocumentoID);

            return View(empresa);
        }

        [AllowAnonymous]
        public ActionResult RecuperarContraseña()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperarContraseña(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbc));

                //borramos contraseña vieja
                var usuario = userManager.FindByEmail(model.Email);
                if (usuario != null)
                {
                    usuario.PasswordHash = null;
                    dbc.SaveChanges();

                    //armamos el objeto random
                    Random obj = new Random();

                    string posibles = "1234567890";

                    int longitud = posibles.Length;

                    char letra;

                    int longitudnuevacadena = 6;

                    string nuevacadena = "";

                    for (int i = 0; i < longitudnuevacadena; i++)
                    {
                        letra = posibles[obj.Next(longitud)];
                        nuevacadena += letra.ToString();
                    }

                    var result = await UserManager.AddPasswordAsync(usuario.Id, nuevacadena);
                    if (result.Succeeded)
                    {
                        try
                        {
                            string emailA = model.Email;

                            string emailDe = "loguisoftcomunica@hotmail.com";

                            string emailCredencial = "loguisoftcomunica@hotmail.com";
                            string contraseñaCredencial = "comunica89";

                            MailMessage msg = new MailMessage();
                            msg.To.Add(new MailAddress(emailA));

                            msg.From = new MailAddress(emailDe, "Recuperación de contraseña", System.Text.Encoding.UTF8);

                            msg.Subject = "Mensaje de " + emailDe;
                            msg.SubjectEncoding = System.Text.Encoding.UTF8;

                            msg.Body = "<p>" + "Su nueva contraseña es: <b>" + nuevacadena + "</b> . Se recomienda que al ingresar al sistema modifique nuevamente su contraseña. Muchas gracias Logui Soft. </p>";

                            msg.BodyEncoding = System.Text.Encoding.UTF8;
                            msg.IsBodyHtml = true;

                            SmtpClient clienteSmtp = new SmtpClient();
                            clienteSmtp.Host = "smtp-mail.outlook.com";
                            clienteSmtp.Port = 587;
                            clienteSmtp.UseDefaultCredentials = false;
                            clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            clienteSmtp.Credentials = new System.Net.NetworkCredential(emailCredencial, contraseñaCredencial);
                            clienteSmtp.EnableSsl = true;
                            clienteSmtp.Send(msg);

                            return RedirectToAction("Index", "Home");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = "No se puedo enviar el email. Intente mas tarde o consulte a su desarrollador.";
                            return View(model);
                        }
                    }
                    AddErrors(result);
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        [AllowAnonymous]
        public async Task<JsonResult> AjaxLogin(LoginViewModel model, string returnUrl)
        {

            // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    //return RedirectToAction("IngresoSistema", "Account");
                    return Json(true);
                //case SignInStatus.LockedOut:
                //    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                    return Json(false);
                default:
                    //ModelState.AddModelError("Email", "Intento de inicio de sesión no válido.");
                    //ViewBag.ModelError = "Intento de inicio de sesión no válido.";
                    return Json(false);
            }
        }

        [AllowAnonymous]
        public JsonResult GuardarNuevoUsuario(string NombreCompleto, string Telefono, string Documento, string Email, string Password, string ConfirmPassword)
        {
            //FIJAR INFORMACION DE CULTURA PARA FECHA Y DECIMALES
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            ZonaHorariaServidor zonaHorariaServidor = new ZonaHorariaServidor();

            int resultado = 0;
            //RESULTADO 1 = EL EMAIL YA EXISTE
            //RESULTADO 2 = EL DOCUMENTO DEL CLIENTE YA EXISTE

            //VALIDAR SI YA EXISTE EL CLIENTE
            if (db.ClientesLoguiSoft.Any(x => x.NroTipoDocumento == Documento))
            {
                resultado = 2; // "YA EXISTE UN CLIENTE CON EL MISMO NÚMERO DE DOCUMENTO";
            }
            else
            {
                var user = new ApplicationUser { UserName = Email, Email = Email };
                var result = UserManager.Create(user, Password);

                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    // SETEAMOS EL NOMBRE EN MAYUSCULAS.
                    if (NombreCompleto != null)
                    {
                        NombreCompleto = NombreCompleto.ToUpper();
                    }
                    var nombreCliente = NombreCompleto;

                    var clienteRegistrar = new ClientesLogui
                    {
                        RazonSocial = nombreCliente,
                        Telefono = Telefono,
                        Domicilio = "",
                        TipoContribuyenteID = 5,
                        TipoDocumentoID = 96,
                        NroTipoDocumento = Documento,
                        FechaAlta = DateTime.Now.AddHours(zonaHorariaServidor.HorasRestar),
                        FechaBaja = Convert.ToDateTime("01/01/2000"),
                        UsuarioID = user.Id,
                        Activo = true
                    };
                    db.ClientesLoguiSoft.Add(clienteRegistrar);
                    db.SaveChanges();

                    // ACA HACEMOS EL ENVIO DEL EMAIL PARA EL NUEVO USUARIO REGISTRADO.
                    try
                    {
                        string emailA = "loguisoft@gmail.com";
                        string emailDe = "loguisoftcomunica@hotmail.com";

                        string emailCredencial = "loguisoftcomunica@hotmail.com";
                        string contraseñaCredencial = "comunica89";

                        MailMessage msg = new MailMessage();
                        msg.To.Add(new MailAddress(emailA));

                        msg.From = new MailAddress(emailDe, "Nuevo registro de Usuario en Logui Contable", System.Text.Encoding.UTF8);

                        msg.Subject = "Mensaje de " + emailDe;
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;

                        msg.Body = "<p>" + "El usuario: <b>" + Email + "</b> se ha registrado en el sistema.</p>";

                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.IsBodyHtml = true;

                        SmtpClient clienteSmtp = new SmtpClient();
                        clienteSmtp.Host = "smtp-mail.outlook.com";
                        clienteSmtp.Port = 587;
                        clienteSmtp.UseDefaultCredentials = false;
                        clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        clienteSmtp.Credentials = new System.Net.NetworkCredential(emailCredencial, contraseñaCredencial);
                        clienteSmtp.EnableSsl = true;
                        clienteSmtp.Send(msg);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    resultado = 1;//El email ya se encuentra registrado en el sistema.
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}