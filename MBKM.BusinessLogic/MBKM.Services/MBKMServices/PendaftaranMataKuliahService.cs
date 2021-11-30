using MBKM.Common.Helpers;
using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services.MBKMServices
{
    public interface IPendaftaranMataKuliahService : IEntityService<PendaftaranMataKuliah>
    {
        IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search);
        IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search);
        IEnumerable<VMFakultas> GetFakultasInternal(string jenjangStudi, string search, string fakultas);
        IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search);
        VMSemester getOngoingSemester(string jenjangStudi);
        VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTable(DataTableAjaxPostModel model, int strm, string prodi, string role);
        VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTableByMahasiswa(DataTableAjaxPostModel model, string emailMahasiswa);
        IEnumerable<VMPendaftaranWithInformasipertukaran> GetListPendaftaranAndInformasiPertukaran(long strm);
        IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranNonPertukaran(long strm);
        IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranInternalPertukaran(long strm);
        IEnumerable<VMReportMahasiswaEksternal> GetListPendaftaranEksternalPertukaran(long strm);
        IEnumerable<VMReportMahasiswaInternalKeluar> GetListPendaftaranInternalPertukaranKeluar(long strm);
        VMKampus GetInformasiKampusByIdProdi(string idProdi);
        IEnumerable<VMReportMahasiswaEksternal> GetListPendaftaranEksternalPertukaranWithoutNilai(long strm);
    }
    public class PendaftaranMataKuliahService : EntityService<PendaftaranMataKuliah>, IPendaftaranMataKuliahService
    {
        IUnitOfWork _unitOfWork;
        IPendaftaranMataKuliahRepository _pmkRepository;

        public PendaftaranMataKuliahService(IUnitOfWork unitOfWork, IPendaftaranMataKuliahRepository PMKRepository)
            : base(unitOfWork, PMKRepository)
        {
            _unitOfWork = unitOfWork;
            _pmkRepository = PMKRepository;
        }

        public IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search)
        {
            return _pmkRepository.GetFakultas(jenjangStudi, search);
        }
        public IEnumerable<VMFakultas> GetFakultasInternal(string jenjangStudi, string search, string fakultas)
        {
            return _pmkRepository.GetFakultasInternal(jenjangStudi, search, fakultas);
        }
        public IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            return _pmkRepository.GetProdiByFakultas(jenjangStudi, idFakultas, search);
        }
        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search)
        {
            return _pmkRepository.GetLokasiByProdi(jenjangStudi, namaProdi, search);
        }

        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _pmkRepository.getOngoingSemester(jenjangStudi);
        }
        public VMKampus GetInformasiKampusByIdProdi(string idProdi)
        {
            return _pmkRepository.GetInformasiKampusByIdProdi(idProdi);
        }
        public VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTable(DataTableAjaxPostModel model, int strm, string prodi, string role)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _pmkRepository.GetPendaftaranList(skip, take, searchBy, sortBy, sortDir, strm, prodi, role);
        }

        public VMListPendaftaranMataKuliah GetPendaftaranMahasiswaDataTableByMahasiswa(DataTableAjaxPostModel model, string emailMahasiswa)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _pmkRepository.GetPendaftaranListFromMahasiswa(skip, take, searchBy, sortBy, sortDir, emailMahasiswa);
        }

        public IEnumerable<VMPendaftaranWithInformasipertukaran> GetListPendaftaranAndInformasiPertukaran(long strm)
        {
            return _pmkRepository.GetListPendaftaranAndInformasiPertukaran(strm);
        }

        public IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranNonPertukaran(long strm)
        {
            return _pmkRepository.GetListPendaftaranNonPertukaran(strm);
        }

        public IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranInternalPertukaran(long strm)
        {
            return _pmkRepository.GetListPendaftaranInternalPertukaran(strm);
        }

        public IEnumerable<VMReportMahasiswaEksternal> GetListPendaftaranEksternalPertukaran(long strm)
        {
            return _pmkRepository.GetListPendaftaranEksternalPertukaran(strm);
        }

        public IEnumerable<VMReportMahasiswaInternalKeluar> GetListPendaftaranInternalPertukaranKeluar(long strm)
        {
            return _pmkRepository.GetListPendaftaranInternalPertukaranKeluar(strm);
        }

        public IEnumerable<VMReportMahasiswaEksternal> GetListPendaftaranEksternalPertukaranWithoutNilai(long strm)
        {
            return _pmkRepository.GetListPendaftaranEksternalPertukaranWithoutNilai(strm);
        }
    }
}
