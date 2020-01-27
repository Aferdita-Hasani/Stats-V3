using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Stats_V3.Models;

namespace Stats_V3.DAL
{
    public class DAL_Raporti
    {
        #region List
        public static List<RaportiIGjeneratesViewModel> List(int klasa, int gjysmevjetoriId)
        {
            var LstL = new List<RaportiIGjeneratesViewModel>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_RaportetESuksesitLendorPerGjysmevjetor", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                //                @prmKlasa bit,
                //@prmGjysmevjetoriId int


                cmd.Parameters.AddWithValue("@prmKlasa", 1);
                cmd.Parameters.AddWithValue("@prmGjysmevjetoriId", gjysmevjetoriId);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var raporti = new RaportiIGjeneratesViewModel();
                    raporti.Klasa = int.Parse(rdr["Klasa"].ToString());
                    raporti.Paralelja = int.Parse(rdr["Paralelja"].ToString());
                    raporti.NrNxeneseFemra = int.Parse(rdr["NrNxenesveFemra"].ToString());
                    raporti.NrNxenesM = int.Parse(rdr["NrNxenesveMeshkuj"].ToString());
                    raporti.SuksesiEmertimi = rdr["Emertimi"].ToString();
                    raporti.SuksesiNrF = int.Parse(rdr["SuksesiNrFemra"].ToString());
                    raporti.SuksesiNrM = int.Parse(rdr["SuksesiNrMeshkuj"].ToString());
                    raporti.MungesaMeArsye = int.Parse(rdr["MungesaMeArsye"].ToString());
                    raporti.MungesaPaArsye = int.Parse(rdr["MungesaPaArsye"].ToString());
                    
                    LstL.Add(raporti);
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