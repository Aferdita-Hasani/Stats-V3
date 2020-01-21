using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Suksesi
    {

        public static List<Suksesi> List()
        {
            var LstL = new List<Suksesi>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Suksesi_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var suksesi = new Suksesi();
                    suksesi.Emertimi = rdr["Emertimi"].ToString();
                    suksesi.Id = int.Parse(rdr["Id"].ToString());
                


                    LstL.Add(suksesi);
                }
                return LstL;
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