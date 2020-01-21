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

        public static List<Gjenerata> ListGjenerata()
        {
            var LstGJ = new List<Gjenerata>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Gjenerata_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var gjenerata = new Gjenerata();
                    gjenerata.Id = int.Parse(rdr["Id"].ToString());
                    gjenerata.Klasa = int.Parse(rdr["Klasa"].ToString());
                    gjenerata.Paralelja = int.Parse(rdr["Paralelja"].ToString());
                    gjenerata.ShkollaId = int.Parse(rdr["ShkollaId"].ToString());
                    gjenerata.VitiShkollore = new VitiShkollore();
                    gjenerata.VitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();
                    gjenerata.VitiShkollore.Id = int.Parse(rdr["VitiId"].ToString());

                    LstGJ.Add(gjenerata);
                }
                return LstGJ;
            }
            catch (Exception e)
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