using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_FormulariDetaje
    {
        #region Create
        public static bool Create(FormulariDetajet formulariDetajet)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_FormulariDetaje_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FormulariId", formulariDetajet.FormulariId);
                cmd.Parameters.AddWithValue("@OretEPlanifikuara", formulariDetajet.OretEPlanifikuara);
                cmd.Parameters.AddWithValue("@OretEMbajtura", formulariDetajet.OretEMbajtura);
                cmd.Parameters.Add("@prmId", SqlDbType.Int).Direction = ParameterDirection.Output;


                conn.Open();
                cmd.ExecuteNonQuery();



                formulariDetajet.Id = Convert.ToInt32(cmd.Parameters["@prmId"].Value);
               
                foreach(var item in formulariDetajet.SuksesiLendors)
                {
                    item.FormulariDetajetId = formulariDetajet.Id;
                    DAL_SuksesiLendor.Create(item);
                }

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