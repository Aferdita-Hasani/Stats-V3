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
    
    }
}