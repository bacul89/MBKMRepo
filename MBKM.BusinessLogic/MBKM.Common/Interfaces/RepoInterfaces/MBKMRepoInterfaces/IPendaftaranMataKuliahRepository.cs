using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IPendaftaranMataKuliahRepository : IGenericRepository<PendaftaranMataKuliah>
    {
        IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search);
        IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search);
        IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search);
        VMKampus GetInformasiKampusByIdProdi(string idProdi);
        IEnumerable<VMFakultas> GetFakultasInternal(string jenjangStudi, string search, string fakultas);
        VMSemester getOngoingSemester(string jenjangStudi);
		VMListPendaftaranMataKuliah GetPendaftaranList(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, int strm);
        VMListPendaftaranMataKuliah GetPendaftaranListFromMahasiswa(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string emailMahasiswa);
        IEnumerable<VMPendaftaranWithInformasipertukaran> GetListPendaftaranAndInformasiPertukaran(long strm);
        IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranNonPertukaran(long strm);
        IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranInternalPertukaran(long strm);
        IEnumerable<VMReportMahasiswaEksternal> GetListPendaftaranEksternalPertukaran(long strm);
        IEnumerable<VMReportMahasiswaInternalKeluar> GetListPendaftaranInternalPertukaranKeluar(long strm);
    }
}
