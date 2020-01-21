using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Gjysmevjetori
    {
        public static Gjysmevjetori Read(int? id)
        {
            var gjysmevjetori = new Gjysmevjetori();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Gjysmevjetori_GetById", conn);
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

                        gjysmevjetori.Gjysmevjetori1 = int.Parse(rdr["Gjysmevjetori"].ToString());
                        gjysmevjetori.Id = int.Parse(rdr["Id"].ToString());
                        gjysmevjetori.VitiShkollore = new VitiShkollore();
                        gjysmevjetori.VitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();
                        gjysmevjetori.VitiShkollore.Id = int.Parse(rdr["VitiId"].ToString());

                    }
                    return gjysmevjetori;
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

        #region List
        public static List<Gjysmevjetori> List()
        {
            var LstGJ = new List<Gjysmevjetori>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Gjysmevjetori_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var gjysmevjetori = new Gjysmevjetori();
                    gjysmevjetori.Id = int.Parse(rdr["Id"].ToString());
                    gjysmevjetori.Gjysmevjetori1 = int.Parse(rdr["Gjysmevjetori"].ToString());

                    gjysmevjetori.VitiShkollore = new VitiShkollore();
                    gjysmevjetori.VitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();
                    gjysmevjetori.VitiShkollore.Id = int.Parse(rdr["VitiId"].ToString());

                    LstGJ.Add(gjysmevjetori);
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
        #endregion

        #region Create
        public static bool Create(Gjysmevjetori Gjysmevjetori)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Gjysmevjetori_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmGjysmevjetori", Gjysmevjetori.Gjysmevjetori1);
                cmd.Parameters.AddWithValue("@prmVitishkolloreId", Gjysmevjetori.VitiShkolloreId);


                conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion
    }
}