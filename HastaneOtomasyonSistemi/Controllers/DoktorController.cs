using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneOtomasyonSistemi.Controllers
{
    public class DoktorController : Controller
    {
        // GET: Doktor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string tc_numara, string sifre)
        {
            SqlConnection connection = new SqlConnection("data source=DESKTOP-IHRMNQ9\\MSSQLSERVER01;initial catalog=HastaneOtomasyonSistemi;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connection.Open();
            string sorgu = "SELECT * FROM TBL_DOKTOR INNER JOIN TBL_PERSONEL_HESAP ON TBL_DOKTOR.DOKTORHESAP_ID = TBL_PERSONEL_HESAP.PERSONEL_HESAP_ID WHERE TBL_DOKTOR.TC = @tc_numara AND TBL_PERSONEL_HESAP.SIFRE = @sifre";
            SqlCommand cmd = new SqlCommand(sorgu, connection);
            cmd.Parameters.AddWithValue("@tc_numara", tc_numara);
            cmd.Parameters.AddWithValue("@sifre", sifre);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string doktorAdi = reader["Ad"].ToString();
                string doktorSoyadi = reader["Soyad"].ToString();

                Session["DoktorAdi"] = doktorAdi;
                Session["DoktorSoyadi"] = doktorSoyadi;

                reader.Close();
                connection.Close();
                return RedirectToAction("DoktorDetay");
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
        public ActionResult DoktorDetay()
        {

            return View("DoktorDetay");
        }

    }
}