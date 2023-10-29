using HastaneOtomasyonSistemi.Models.Entity;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class SekreterController : Controller
    {
        // GET: Sekreter
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string tc_numara, string sifre)
        {
            // Sekreter giriş işlemleri için gerekli veritabanı bağlantısını yapın
            SqlConnection connection = new SqlConnection("data source=DESKTOP-IHRMNQ9\\MSSQLSERVER01;initial catalog=HastaneOtomasyonSistemi;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

            connection.Open();

            // Veritabanında sekreterin doğruluğunu kontrol eden sorguyu oluşturun
            string sorgu = "SELECT * FROM TBL_SEKRETER INNER JOIN TBL_PERSONEL_HESAP ON TBL_SEKRETER.SEKRETERHESAP_ID = TBL_PERSONEL_HESAP.PERSONEL_HESAP_ID WHERE TBL_SEKRETER.TC = @tc_numara AND TBL_PERSONEL_HESAP.SIFRE = @sifre";

            SqlCommand cmd = new SqlCommand(sorgu, connection);

            cmd.Parameters.AddWithValue("@tc_numara", tc_numara);
            cmd.Parameters.AddWithValue("@sifre", sifre);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Eğer giriş başarılıysa, gerekli işlemleri yapın
                // Örneğin, oturum açma veya yönlendirme

                reader.Close();
                connection.Close();

                return RedirectToAction("SekreterDetay");
            }
            else
            {
                // Kullanıcı doğrulanmadı, hata mesajı göster
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre yanlış.";

                reader.Close();
                connection.Close();

                return View("Index");
            }
        }
        public ActionResult SearchPatient(string hasta_adi, string hasta_soyadi, string tc_kimlik)
        {
            SqlConnection connection = new SqlConnection("data source=DESKTOP-IHRMNQ9\\MSSQLSERVER01;initial catalog=HastaneOtomasyonSistemi;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connection.Open();
            string sqlQuery = "SELECT Hasta.AD AS HastaAd, Hasta.SOYAD AS HastaSoyad, Hasta.TC AS TC, " +
                   "Randevu.TARIH AS RandevuTarihi, Randevu.SAAT AS Saat, " +
                   "Poliklinik.ISIM AS PoliklinikAdi, Doktor.AD AS DoktorAdi " +
                   "FROM TBL_HASTA AS Hasta " +
                   "INNER JOIN TBL_RANDEVU AS Randevu ON Hasta.HASTA_HESAP_ID = Randevu.HASTA_HESAP_ID " +
                   "INNER JOIN TBL_POLİKİNLİK AS Poliklinik ON Randevu.POLIKINLIK_ID = Poliklinik.POLIKINLIK_ID " +
                   "INNER JOIN TBL_DOKTOR AS Doktor ON Randevu.DOKTOR_HESAP_ID = Doktor.DOKTORHESAP_ID " +
                   "WHERE Hasta.TC = @tc_numara AND Hasta.AD=@hasta_ad AND Hasta.SOYAD=@hasta_soyad"; 
            SqlCommand cmd = new SqlCommand(sqlQuery,connection);
            cmd.Parameters.AddWithValue("@tc_numara", tc_kimlik);
            cmd.Parameters.AddWithValue("@hasta_ad", hasta_adi);
            cmd.Parameters.AddWithValue("@hasta_soyad", hasta_soyadi);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                HastaViewModel hasta = new HastaViewModel()
                {
                    HastaAd = dr["HastaAd"].ToString(),
                    HastaSoyad = dr["HastaSoyad"].ToString(),
                    TC = dr["TC"].ToString(),
                    RandevuTarihi = dr["RandevuTarihi"].ToString(),
                    Saat = dr["Saat"].ToString(),
                    PoliklinikAdi = dr["PoliklinikAdi"].ToString(),
                    DoktorAdi = dr["DoktorAdi"].ToString(),
                };
                connection.Close();
                return View("SekreterDetay", hasta); // Hasta bulunduysa, detay sayfasını hasta verisiyle birlikte döndür

            }
            else
            {
                ViewBag.ErrorMessage = "Girilen kişiye ait randevu bulunamadı.";
                return View("SekreterDetay");
            }
            connection.Close();

        }
        public ActionResult SekreterDetay()
        {
            return View("SekreterDetay");
        }
    }
}
