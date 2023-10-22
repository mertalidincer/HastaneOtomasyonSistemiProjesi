using System;
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
        public ActionResult SearchPatient(string tc_numara)
        {
            SqlConnection connection = new SqlConnection("data source=DESKTOP-IHRMNQ9\\MSSQLSERVER01;initial catalog=HastaneOtomasyonSistemi;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * From TBL_HASTA WHERE TC=@tc_numara");

            cmd.Parameters.AddWithValue("@tc_numara", tc_numara);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {

            }
            connection.Close();
        }
        public ActionResult SekreterDetay()
        {
            return View("SekreterDetay");
        }
    }
}
