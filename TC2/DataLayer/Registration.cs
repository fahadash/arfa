using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace TC.DataLayer
{
    public class Registration
    {
        public static bool CheckForDuplicateUsername(string userName)
        {
            return (bool) SqlHelper.ExecuteScalar(ConnectionHelper.GetConnectionString(), CommandType.StoredProcedure, "CheckForDuplicateUsername", new SqlParameter () { ParameterName="@Username", Value=userName, SqlDbType = System.Data.SqlDbType.VarChar });
            
        }

        public static bool CreateUser(string userName, string password, string email, string firstName,
                                        string lastName, int? age)
        {

            SqlParameter [] paramarr = new SqlParameter[] 
            {
                new SqlParameter() { ParameterName = "@Username", Value = userName, SqlDbType= SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Password", Value = password, SqlDbType= SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Email", Value = email, SqlDbType= SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Firstname", Value = firstName, SqlDbType= SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Lastname", Value = lastName, SqlDbType= SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Age", Value = age, SqlDbType= SqlDbType.Int},
            };


            try
            {
                SqlHelper.ExecuteNonQuery(ConnectionHelper.GetConnectionString(), CommandType.StoredProcedure, "CreateUser", paramarr);
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public static bool Logon (string userName, string password)
        {
            SqlParameter [] paramarr = new SqlParameter[] 
            {
                new SqlParameter() { ParameterName = "@Username", Value = userName, SqlDbType= SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Password", Value = password, SqlDbType= SqlDbType.VarChar},
            };


            try
            {
              return (bool)  SqlHelper.ExecuteScalar(ConnectionHelper.GetConnectionString(), CommandType.StoredProcedure, "Logon", paramarr);
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
