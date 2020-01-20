using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Formulari
    {
        public static bool Create(Formulari formulari)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Formulari_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmGjenerataId", formulari.GjenerataId);
                cmd.Parameters.AddWithValue("@prmGjysmevjetoriId", formulari.GjysmevjetoriId);
                cmd.Parameters.AddWithValue("@prmMungesaMeArsye", formulari.MungesaMeArsye);
                cmd.Parameters.AddWithValue("@prmMungesaPaArsye", formulari.MungesaPaArsye);



                conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public static List<Formulari> ListFormular()
        {
            var LstGJ = new List<Formulari>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Formulari_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var formulari = new Formulari();
                    formulari.GjenerataId = int.Parse(rdr["GjenerataId"].ToString());
                    formulari.Id = int.Parse(rdr["Id"].ToString());
                    formulari.GjysmevjetoriId = int.Parse(rdr["GjysmevjetoriId"].ToString());
                    formulari.ShkollaId = int.Parse(rdr["ShkollaId"].ToString());
                    formulari.MungesaMeArsye = int.Parse(rdr["MungesaMeArsye"].ToString());
                    formulari.MungesaPaArsye = int.Parse(rdr["MungesaPaArsye"].ToString());

                    formulari.Gjysmevjetori = DAL_Gjysmevjetori.Read(formulari.GjysmevjetoriId);
                    formulari.Gjenerata = DAL_Gjenerata.Read(formulari.GjenerataId);

                    LstGJ.Add(formulari);
                }
                return LstGJ;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}