using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMDNR
    {
        public Int64 ID { get; set; }
        public Int64 KodeDosen { get; set; }
        public string NamaDosen { get; set; }
        public int STRM { get; set; }
        public string NamaMatakuliah { get; set; }
        public string KodeMatakuliah { get; set; }
        public string Lokasi { get; set; }
        public string Fakultas { get; set; }
        public string Prodi { get; set; }
        public string Seksi { get; set; }
        public string A { get; set; }
        public string Amin { get; set; }
        public string Bplus { get; set; }
        public string B { get; set; }
        public string Bmin { get; set; }
        public string Cplus { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string M { get; set; }
        public int Total { get; set; }
        public int ATotal { get; set; }
        public int AminTotal { get; set; }
        public int BplusTotal { get; set; }
        public int BTotal { get; set; }
        public int BminTotal { get; set; }
        public int CplusTotal { get; set; }
        public int CTotal { get; set; }
        public int DTotal { get; set; }
        public int ETotal { get; set; }
        public int MTotal { get; set; }
        public int TMCoursework { get; set; }
        public int TMMidterm { get; set; }
        public int TMFinal { get; set; }
        public double PctCoursework { get; set; }
        public double PctMidterm { get; set; }
        public double PctFinal { get; set; }
        public double AveragePoint { get; set; }
        public double StddevPoint { get; set; }
        public int NumberOfStudent { get; set; }
        public int NumberOfR { get; set; }
        public int NumberOfS { get; set; }
        public double Stddev { get; set; }
        public double Variance { get; set; }
        public double Average { get; set; }
        public double MaxMarks { get; set; }
        public double MinMarks { get; set; }
        public List<VMMahasiswa> mahasiswas { get; set; }
    }

    public class VMMahasiswa
    {
        public string StudentID { get; set; }
        public string CampusID { get; set; }
        public string Nama { get; set; }
        public string Supp { get; set; }
        public int MT { get; set; }
        public int CW1 { get; set; }
        public int CW2 { get; set; }
        public int CW3 { get; set; }
        public int CW4 { get; set; }
        public int CW5 { get; set; }
        public int Final { get; set; }
        public int NilaiTotal { get; set; }
        public string Grade { get; set; }
    }
}
