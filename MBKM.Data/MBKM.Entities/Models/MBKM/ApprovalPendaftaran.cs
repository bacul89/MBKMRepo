using MBKM.Entities.Basentities;
using System;

namespace MBKM.Entities.Models.MBKM
{
    public class ApprovalPendaftaran : BaseEntity
    {
        public string StatusPendaftaran { get; set; }
        public string Approval { get; set; }
        public string Catatan { get; set; }
        public Int64 PendaftaranMataKuliahID { get; set; }
        public virtual PendaftaranMataKuliah PendaftaranMataKuliahs { get; set; }
    }
}
