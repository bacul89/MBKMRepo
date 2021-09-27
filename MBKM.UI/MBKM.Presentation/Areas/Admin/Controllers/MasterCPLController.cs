using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class MasterCPLController : Controller
    {
        // GET: Admin/MasterCPL
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ModaladdCPL()
        {
            //IEnumerable<VMLookup> listJabatan = _roleService.getLookupRole();
            //ViewBag.listJabatan = new SelectList(listJabatan, "Nilai", "Nama");

            //IEnumerable<VMListProdi> listProdi = _lookupService.getListProdi();
            //ViewBag.listProdi = new SelectList(listProdi, "IDProdi", "NamaProdi");

            //IEnumerable<VMListProdi> listNProdi = _lookupService.getListProdi();
            //ViewBag.listNProdi = new SelectList(listNProdi, "NamaProdi", "IDProdi");

            //return RedirectToAction("Index", "Home", new { area = "Portal" });
            //Debug.WriteLine("akses index");
            return View("ModalCreateCPL");
        }
    }
}