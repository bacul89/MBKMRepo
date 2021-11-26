using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Presentation.Helper;
using MBKM.Entities.ViewModel;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class NimDigitalController : Controller
    {

        private IMahasiswaService _mahasiswaService;
        private IAttachmentService _attachmentService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IJadwalKuliahMahasiswaService _jkMhsService;

        public NimDigitalController(IMahasiswaService mahasiswaService, IAttachmentService attachmentService, IInformasiPertukaranService informasiPertukaranService, IJadwalKuliahMahasiswaService jkMhsService)
        {
            _mahasiswaService = mahasiswaService;
            _attachmentService = attachmentService;
            _informasiPertukaranService = informasiPertukaranService;
            _jkMhsService = jkMhsService;
        }

        public ActionResult Index()
        {
            //Session["nama"] = "smitty werben man jensen";
            //Session["email"] = "abayyina@gmail.com";
            return View();
        }


        public ActionResult GetNim()
        {

            string email = Session["emailMahasiswa"] as string;

            var ma = _mahasiswaService.Find(x => x.Email == email).First();

            
            VMSemester semester = _jkMhsService.getOngoingSemester(ma.JenjangStudi);


            var photoprofile = "none";
            try
            {
                var attachment = _attachmentService.Find(x => x.MahasiswaID == ma.ID && x.FileType == "FotoDiri").First();
                photoprofile = "Upload/" + ma.ID + "/" + attachment.FileName;
            }
            catch (Exception ex)
            {
                photoprofile = "Asset/default-photo-profile.png";
            }

            var infoPertukaran = false;
            try
            {
                var Pertukaran = _informasiPertukaranService.Find(m => m.MahasiswaID == ma.ID && m.STRM == semester.ID).First();
                infoPertukaran = true;
            }
            catch
            {
                infoPertukaran = false;
            }


            return Json(new {
                Nama = ma.Nama,
                NIM = ma.NIM,
                Prodi = ma.ProdiAsal,
                PhotoProfile = photoprofile,
                NamaUniversitas = "Universitas Katolik Atma Jaya",
                pertukaran = infoPertukaran
            }, JsonRequestBehavior.AllowGet);

        }


        /*        public Mahasiswa GetMahasiswaByEmail(string email)
                {
                    return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault().Attachments.Select(x => new Attachment
                    {
                        ID = x.ID
                    });
                }*/
    }
}