using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Gjenerata
    {
        public static Gjenerata Read(int? id)
        {
            var gjenerata = new Gjenerata();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Gjenerata_GetById", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                if (id != null)
                {
                    cmd.Parameters.AddWithValue("@prmId", id);

                    conn.Open();
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {


                        gjenerata.Id = int.Parse(rdr["Id"].ToString());
                        gjenerata.Klasa= int.Parse(rdr["Klasa"].ToString());
                        gjenerata.Paralelja = int.Parse(rdr["Paralelja"].ToString());
                       gjenerata.ShkollaId= int.Parse(rdr["ShkollaId"].ToString());
                        gjenerata.VitiShkollore = new VitiShkollore();
                        gjenerata.VitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();
                        gjenerata.VitiShkollore.Id = int.Parse(rdr["VitiId"].ToString());

                    }
                    return gjenerata;
                }
                else
                {
                    return null;
                }
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