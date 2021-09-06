﻿using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Map.MBKMMap
{
    public class NilaiMap : EntityTypeConfiguration<NilaiKuliah>
    {
        public NilaiMap()
        {
            ToTable("Nilai");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Nilai).IsRequired();
            Property(t => t.Persentase).IsRequired();
        }
    }
}
