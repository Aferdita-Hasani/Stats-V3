using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Lenda
    {

        #region Create
        public static int Create(Lenda lenda)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Lenda_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmEmertimi", lenda.Emertimi);

                cmd.Parameters.Add("@prmId", SqlDbType.Int).Direction = ParameterDirection.Output;

                conn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.Parameters["@prmId"].Value);
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


        #region CreateLendaKlasa
        public static bool CreateLendaKlasa(int Lendaid, int klasa)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_LendaKlasa_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmLendaId", Lendaid);
                cmd.Parameters.AddWithValue("@prmKlasa", klasa);



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


    }
}