﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Stats_V3.DAL
{
    public class DAL_Formulari
    {
        #region Create
        public static int Create(Formulari formulari)
        {
            var conn = DBHelper.GetConnection();

            try
            {
                var cmd = new SqlCommand("usp_Formulari_Insert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@prmGjenerataId", formulari.GjenerataId);
                cmd.Parameters.AddWithValue("@prmGjysmevjetoriId", formulari.GjysmevjetoriId);
                cmd.Parameters.AddWithValue("@prmMungesaMeArsye", formulari.MungesaMeArsye);
                cmd.Parameters.AddWithValue("@prmMungesaPaArsye", formulari.MungesaPaArsye);
              
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

        #region Read
        public static Formulari Read(int? id)
        {
            var formulari = new Formulari();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Formularet_GetById", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                if (id != null)
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        formulari.Id = int.Parse(rdr["Id"].ToString());
                        formulari.GjenerataId = int.Parse(rdr["GjenerataId"].ToString());
                        formulari.GjysmevjetoriId = int.Parse(rdr["GjysmevjetoriId"].ToString());
                        formulari.ShkollaId = int.Parse(rdr["ShkollaId"].ToString());
                        formulari.MungesaMeArsye = int.Parse(rdr["MungesaMeArsye"].ToString());
                        formulari.MungesaPaArsye = int.Parse(rdr["MungesaPaArsye"].ToString());
                    }
                    return formulari;
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
        #endregion

        #region List
        public static List<Formulari> ListFormular()
        {
            var LstGJ = new List<Formulari>();
            var conn = DBHelper.GetConnection();
            var cmd = new SqlCommand("usp_Formulari_GetList", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var formulari = new Formulari();
                    formulari.GjenerataId = int.Parse(rdr["GjenerataId"].ToString());
                    formulari.Id = int.Parse(rdr["Id"].ToString());
                    formulari.GjysmevjetoriId = int.Parse(rdr["GjysmevjetoriId"].ToString());
                    formulari.ShkollaId = int.Parse(rdr["ShkollaId"].ToString());
                    formulari.MungesaMeArsye = int.Parse(rdr["MungesaMeArsye"].ToString());
                    formulari.MungesaPaArsye = int.Parse(rdr["MungesaPaArsye"].ToString());

                    formulari.Gjysmevjetori = DAL_Gjysmevjetori.Read(formulari.GjysmevjetoriId);
                    formulari.Gjenerata = DAL_Gjenerata.Read(formulari.GjenerataId);

                    LstGJ.Add(formulari);
                }
                return LstGJ;
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
    }
}