using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Lenda
    {

        #region Create
        public static bool Create(Lenda lenda)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Lenda_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmEmertimi", lenda.Emertimi);
                cmd.Parameters.AddWithValue("@prmKlasa", lenda.Klasa);



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
        public static List<Lenda> List()
        {
            var LstL = new List<Lenda>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Lenda_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var lenda = new Lenda();
                    lenda.Emertimi = rdr["Emertimi"].ToString();
                    lenda.Id = int.Parse(rdr["Id"].ToString());
                    lenda.Klasa = rdr["Klasa"].ToString();


                    LstL.Add(lenda);
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