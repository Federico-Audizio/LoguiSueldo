using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoguiSueldo.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Threading;
using System.Globalization;
using LoguiSueldo.Models.Administracion;
using Org.BouncyCastle.Crypto.Tls;
using RestSharp;

namespace LoguiSueldo.Controllers
{
    public class EmpresasController : Controller
    {
        private LoguiSueldoContext db = new LoguiSueldoContext();

        public void EmpresaActual(string UsuarioID, Empresa empresa)
        {
            var existeEmpresaOnline = (from usu in db.Empresas where db.Usuario == UsuarioID select usu).ToList();
            if (existeEmpresaOnline.Count == 1)
            {
                var empresaOnline = existeEmpresaOnline[0];

                var empresaActual = (from usu in db.Empresas where usu.EmpresaID == empresaOnline.EmpresaID select usu).Single();

                empresa.EmpresaID = empresaActual.EmpresaID;
                empresa.RazonSocial = empresaActual.RazonSocial;
                //empresa.TipoContribuyentes = empresaActual.TipoContribuyentes;
                empresa.TipoContribuyenteID = empresaActual.TipoContribuyenteID;
                empresa.TipoDocumentoID = empresaActual.TipoDocumentoID;
                empresa.NroTipoDocumento = empresaActual.NroTipoDocumento;
                empresa.CorreoElectronico = empresaActual.CorreoElectronico;

                empresa.NombreFantasia = empresaActual.NombreFantasia;
                empresa.DireccionReal = empresaActual.DireccionReal;
                empresa.LocalidadID = empresaActual.LocalidadID;
                empresa.Localidades = empresaActual.Localidades;
                empresa.Telefono1 = empresaActual.Telefono1;
                empresa.UsuarioTitular = empresaActual.UsuarioTitular;
                empresa.EmpresaIDLoguiGestion = empresaActual.EmpresaIDLoguiGestion;
            }
        }

        public JsonResult EmpresasUsuario()
        {
            var usuarioactual = User.Identity.GetUserId();

            Empresa empresa = new Empresa();
            EmpresaActual(usuarioactual, empresa);

            List<ListadoEmpresaUsuario> listado = new List<ListadoEmpresaUsuario>();

            var personas = db.Personas.Where(p => p.EmpresaID == empresa.EmpresaID).ToList();

            List<ListadoPersonas> personasEmpresa = new List<ListadoPersonas>();

            var tablaPersonasEmpresa = (from cam in db.PermisoUsuarios where cam.EmpresaID == empresa.EmpresaID && cam.Desvinculado == false select cam).ToList();
            foreach (var p in tablaPersonasEmpresa)
            {
                if (p.PersonaID != 0)
                {
                    var persona = (from cam in personas where cam.PersonaID == p.PersonaID select cam).Single();

                    var personaEmpresa = new ListadoPersonas
                    {
                        PersonaID = p.PersonaID,
                        NombreCompleto = persona.NombreCompleto,
                        PermisoID = p.PermisoID,
                        PermisoNombre = p.Permiso.PermisoNombre
                    };
                    personasEmpresa.Add(personaEmpresa);
                }
            }
            var empresaMostrar = (from cam in db.Empresas where cam.EmpresaID == empresa.EmpresaID select cam).Single();
            var itemEmpresa = new ListadoEmpresaUsuario
            {
                EmpresaID = empresaMostrar.EmpresaID,
                RazonSocial = empresaMostrar.RazonSocial,
                //TipoContribuyenteNombre = empresaMostrar.TipoContribuyentes.TipoContribuyenteNombre,
                //TipoDocumentoNombre = empresaMostrar.TipoDocumentos.TipoDocumentoNombre,
                NroTipoDocumento = empresaMostrar.NroTipoDocumento,
                PersonasEmpresa = personasEmpresa
            };
            listado.Add(itemEmpresa);

            return Json(listado);
        }

        public ActionResult Edit(int? id)
        {
            var usuarioactual = User.Identity.GetUserId();

            Empresa empresaActual = new Empresa();
            PermisoUsuario permisoUsuario = new PermisoUsuario();
            //InformacionEmpresaActual(usuarioactual, empresaActual);

            if (empresaActual.EmpresaID != id)
            {
                return RedirectToAction("Index");
            }

            if (permisoUsuario.PermisoID != 1)
            {
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Empresa empresa = db.Empresas.Find(id);

            if (empresa == null)
            {
                return HttpNotFound();
            }

            var localidad = (from cam in db.Localidades where cam.LocalidadID == empresa.LocalidadID && (cam.EmpresaID == 0 || cam.EmpresaID == empresa.EmpresaID) select cam).Single();
            ViewBag.LocalidadID = localidad.LocalidadID;
            ViewBag.LocalidadNombre = localidad.NombreVista;

            //var tiposContribuyentes = (from o in db.TipoContribuyentes where o.Visible == true select o).ToList();
            //ViewBag.TipoContribuyenteID = new SelectList(tiposContribuyentes.OrderBy(t => t.TipoContribuyenteNombre), "TipoContribuyenteID", "TipoContribuyenteNombre", empresa.TipoContribuyenteID);

            //var tipoDocumentos = (from o in db.TipoDocumentos where o.Visible == true select o).ToList();
           // if (empresa.TipoContribuyenteID == 5)// CONSUMIDOR FINAL.
            //{
                // DNI.
            //    tipoDocumentos = (from o in tipoDocumentos where o.TipoDocumentoID == 96 select o).ToList();
            //}
            //else
            //{
                // CUIT.
              //  tipoDocumentos = (from o in tipoDocumentos where o.TipoDocumentoID == 80 select o).ToList();
            //}
            //ViewBag.TipoDocumentoID = new SelectList(tipoDocumentos.OrderBy(t => t.TipoDocumentoNombre), "TipoDocumentoID", "TipoDocumentoNombre", empresa.TipoDocumentoID);

            //BUSCAR PROVINCIAS CON EMPRESA 0 Y EMPRESA ACTUAL
            var provincias = (from o in db.Provincias where o.EmpresaID == 0 || o.EmpresaID == empresa.EmpresaID select o).ToList();
            ViewBag.ProvinciaID = new SelectList(provincias.Where(p => p.PaisID == 200).OrderBy(p => p.ProvinciaNombre), "ProvinciaID", "ProvinciaNombre");

            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaID,RazonSocial,NombreFantasia,DireccionReal,LocalidadID,TipoDocumentoID,NroTipoDocumento,Telefono1,CorreoElectronico")] Empresa empresa)
        {
            //FIJAR INFORMACION DE CULTURA PARA FECHA Y DECIMALES
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-AR");

            // SI O SI DEBE INGRESAR NRO DE DOCUMENTO / CUIT Y VALIDAR SI EXISTE
            bool registrar = true;

            ZonaHorariaServidor zonaHorariaServidor = new ZonaHorariaServidor();

            //VALIDAR SI INGRESA NRO DOCUMENTO 
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

            if (registrar == true)
            {
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

                    if (empresa.LocalidadID != 0 && empresa.TipoDocumentoID != 0 && empresa.TipoContribuyenteID != 0)
                    {

                        empresa.UsuarioTitular = (from o in db.Empresas where o.EmpresaID == empresa.EmpresaID select o.UsuarioTitular).Single();
                        empresa.Cargado = (from o in db.Empresas where o.EmpresaID == empresa.EmpresaID select o.Cargado).Single();
                        db.Entry(empresa).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    registrar = false;
                }
            }

            if (registrar == false)
            {
                Empresa empresaEditar = db.Empresas.Find(empresa.EmpresaID);

                if (empresa.LocalidadID == 0)
                {
                    ModelState.AddModelError("LocalidadID", "Seleccione una Localidad.");
                }
                if (empresa.TipoDocumentoID == 0)
                {
                    ModelState.AddModelError("TipoDocumentoID", "Seleccione un tipo de documento.");
                }
                if (empresa.TipoContribuyenteID == 0)
                {
                    ModelState.AddModelError("TipoContribuyenteID", "Seleccione un tipo de contribuyente.");
                }

                empresa.LocalidadID = empresaEditar.LocalidadID;
                empresa.TipoDocumentoID = empresaEditar.TipoDocumentoID;
                empresa.TipoContribuyenteID = empresaEditar.TipoContribuyenteID;
                empresa.NroTipoDocumento = empresaEditar.NroTipoDocumento;
            }

            var localidad = (from cam in db.Localidades where cam.LocalidadID == empresa.LocalidadID && (cam.EmpresaID == 0 || cam.EmpresaID == empresa.EmpresaID) select cam).Single();
            ViewBag.LocalidadID = localidad.LocalidadID;
            ViewBag.LocalidadNombre = localidad.NombreVista;

            //var tiposContribuyentes = (from o in db.TipoContribuyentes where o.Visible == true select o).ToList();
            //ViewBag.TipoContribuyenteID = new SelectList(tiposContribuyentes.OrderBy(t => t.TipoContribuyenteNombre), "TipoContribuyenteID", "TipoContribuyenteNombre", empresa.TipoContribuyenteID);

            //var tipoDocumentos = (from o in db.TipoDocumentos where o.Visible == true select o).ToList();
            //if (empresa.TipoContribuyenteID == 5)//CONSUMIDOR FINAL
            //{
                //DNI
             //   tipoDocumentos = (from o in tipoDocumentos where o.TipoDocumentoID == 96 select o).ToList();
            //}
            //else
            //{
                //CUIT
              //  tipoDocumentos = (from o in tipoDocumentos where o.TipoDocumentoID == 80 select o).ToList();
            //}
            //ViewBag.TipoDocumentoID = new SelectList(tipoDocumentos.OrderBy(t => t.TipoDocumentoNombre), "TipoDocumentoID", "TipoDocumentoNombre", empresa.TipoDocumentoID);

            //BUSCAR PROVINCIAS CON EMPRESA 0 Y EMPRESA ACTUAL
            var provincias = (from o in db.Provincias where o.EmpresaID == 0 || o.EmpresaID == empresa.EmpresaID select o).ToList();
            ViewBag.ProvinciaID = new SelectList(provincias.Where(p => p.PaisID == 200).OrderBy(p => p.ProvinciaNombre), "ProvinciaID", "ProvinciaNombre");

            return View(empresa);
        }

        //[OutputCache(Duration = 1, Location = System.Web.UI.OutputCacheLocation.Client)]
        //public void ArmarCookie(Empresa empresa, int diasRestantes, string prueba)
        //{
         //   HttpCookie aCookie = new HttpCookie("userInfo");

         //   if (empresa.NombreFantasia != null && empresa.NombreFantasia != "")
           // {
             //   aCookie.Values["userName"] = empresa.NombreFantasia.ToString();
            //}
            //else
            //{
              //  aCookie.Values["userName"] = empresa.RazonSocial.ToString();
            //}
            //aCookie.Values["diasRestantes"] = diasRestantes.ToString();
            //aCookie.Values["prueba"] = prueba.ToString();
            //aCookie.Expires = DateTime.Now.AddDays(1);
            //aCookie.Expires = DateTime.Now.AddMinutes(1440);
            //Response.Cookies.Add(aCookie);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
