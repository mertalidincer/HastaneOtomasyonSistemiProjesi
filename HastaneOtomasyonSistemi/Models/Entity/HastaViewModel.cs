using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneOtomasyonSistemi.Models.Entity
{
    public class HastaViewModel
    {
        public string HastaAd { get; set; }
        public string HastaSoyad { get; set; }
        public string TC { get; set; }
        public string RandevuTarihi { get; set; }
        public string Saat { get; set; }
        public string PoliklinikAdi { get; set; }
        public string DoktorAdi { get; set; }

    }
}