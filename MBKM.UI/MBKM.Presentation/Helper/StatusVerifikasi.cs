﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBKM.Presentation.Helper
{
    public class StatusVerifikasi
    {
        public static string Terima { get; set; }
        public static string Tolak { get; set; }

        static StatusVerifikasi()
        {
            Terima = "AKTIF";
            Tolak = "DITOLAK";

        }

    }
}