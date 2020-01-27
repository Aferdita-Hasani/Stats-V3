using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Gjenerata
    {
        public static Gjenerata Read(int? id)
        {
            var gjenerata = new Gjenerata();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Gjenerata_GetById", conn);
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


                        gjenerata.Id = int.Parse(rdr["Id"].ToString());
                        gjenerata.Klasa= int.Parse(rdr["Klasa"].ToString());
                        gjenerata.Paralelja = int.Parse(rdr["Paralelja"].ToString());
                       gjenerata.ShkollaId= int.Parse(rdr["ShkollaId"].ToString());
                        gjenerata.NrFemra = int.Parse(rdr["NrNxenesveFemra"].ToString());
                        gjenerata.NrMeshkuj = int.Parse(rdr["NrNxenesveMeshkuj"].ToString());
                        gjenerata.VitiShkollore = new VitiShkollore();
                        gjenerata.VitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();
                        gjenerata.VitiShkollore.Id = int.Parse(rdr["VitiId"].ToString());

                    }
                    return gjenerata;
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

        public static List<Gjenerata> List()
        {
            var LstGJ = new List<Gjenerata>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Gjenerata_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var gjenerata = new Gjenerata();
                    gjenerata.Id = int.Parse(rdr["Id"].ToString());
                    gjenerata.Klasa = int.Parse(rdr["Klasa"].ToString());
                    gjenerata.Paralelja = int.Parse(rdr["Paralelja"].ToString());
                    gjenerata.ShkollaId = int.Parse(rdr["ShkollaId"].ToString());
                    gjenerata.NrFemra = int.Parse(rdr["NrNxenesveFemra"].ToString());
                    gjenerata.NrMeshkuj = int.Parse(rdr["NrNxenesveMeshkuj"].ToString());
                    gjenerata.VitiShkollore = new VitiShkollore();
                    gjenerata.VitiShkollore.VitiShkollor = rdr["VitiShkollor"].ToString();
                    gjenerata.VitiShkollore.Id = int.Parse(rdr["VitiId"].ToString());

                    LstGJ.Add(gjenerata);
                }
                return LstGJ;
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

        #region Create
        public static bool Create(Gjenerata gjenerata)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Gjenerata_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmKlasa", gjenerata.Klasa);
                cmd.Parameters.AddWithValue("@prmParalelja", gjenerata.Paralelja);
                cmd.Parameters.AddWithValue("@prmVitiShkolloreId", gjenerata.VitiShkolloreId);
                cmd.Parameters.AddWithValue("@prmNrFemra", gjenerata.NrFemra);
                cmd.Parameters.AddWithValue("@prmNrMeshkuj", gjenerata.NrMeshkuj);
                cmd.Parameters.AddWithValue("@prmShkollaId", 1/*gjenerata.ShkollaId*/);



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