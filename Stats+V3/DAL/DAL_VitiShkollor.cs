using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_VitiShkollor
    {
        #region List
        public static List<VitiShkollore> List()
        {
            var LstV = new List<VitiShkollore>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_VitiShkollor_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var vitiShkollore = new VitiShkollore();
                    vitiShkollore.Id = int.Parse(rdr["Id"].ToString());
                    vitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();


                    LstV.Add(vitiShkollore);
                }
                return LstV;
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
        public static bool Create(VitiShkollore vitiSh)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_VitiShkollor_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmNVitiShkollor", vitiSh.VitiShkollor);


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