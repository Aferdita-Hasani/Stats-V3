using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_SuksesiLendor
    {
        #region Create
        public static bool Create(SuksesiLendor suksesiLendor)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_SuksesiLendor_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FormulariDetajetId", suksesiLendor.FormulariDetajetId);
                cmd.Parameters.AddWithValue("@LendaId", suksesiLendor.LendaId);
                cmd.Parameters.AddWithValue("@SuksesiId", suksesiLendor.SuksesiId);
                cmd.Parameters.AddWithValue("@NrNxeneseveFemra", suksesiLendor.NrNxenesveFemra);
                cmd.Parameters.AddWithValue("@NrNxeneseveMeshkuj", suksesiLendor.NrNxenesveMeshkuj);


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

        #region Full info by Formular Id
        public static List<SuksesiLendor> List( int? id)
        {
            var LstL = new List<SuksesiLendor>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_SuksesiLendor_GetListFullInfoByFormularID", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@FormulariId", id);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   // @FormulariId
                     var suksesiLendor = new SuksesiLendor();
                    suksesiLendor.Id = int.Parse(rdr["SuksesiLendorId"].ToString());

                    suksesiLendor.FormulariDetajetId = int.Parse(rdr["Id"].ToString());
                    suksesiLendor.FormulariDetajet = new FormulariDetajet();
                    suksesiLendor.FormulariDetajet.OretEMbajtura = int.Parse(rdr["OretEMbajtura"].ToString());
                    suksesiLendor.FormulariDetajet.OretEPlanifikuara = int.Parse(rdr["OretEPlanifikuara"].ToString());
                    suksesiLendor.NrNxenesveFemra= int.Parse(rdr["NrNxenesveFemra"].ToString());
                    suksesiLendor.NrNxenesveMeshkuj = int.Parse(rdr["NrNxenesveMeshkuj"].ToString());

                    suksesiLendor.Lenda = new Lenda();
                    suksesiLendor.Lenda.Emertimi = rdr["LendaEmri"].ToString();
                    suksesiLendor.Suksesi = new Suksesi();
                    suksesiLendor.Suksesi.Emertimi = rdr["SuksesiEmri"].ToString();

                    
                    LstL.Add(suksesiLendor);
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