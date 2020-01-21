using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Shkolla
    {
        #region Create
        public static bool Create(Shkolla shkolla)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Shkolla_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmEmertimi", shkolla.Emertimi);
          



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
        #endregion



        #region List
        public static List<Shkolla> List()
        {
            var LstL = new List<Shkolla>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Shkolla_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var shkolla = new Shkolla();
                    shkolla.Emertimi = rdr["Emertimi"].ToString();
                    shkolla.Id = int.Parse(rdr["Id"].ToString());
                   


                    LstL.Add(shkolla);
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
        #endregion
    }
}