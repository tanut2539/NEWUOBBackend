using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Repository.ClassUtility;

namespace UOB.PVD.Repository.Account
{
    public class account
    {
        public DataRow Login(string connection, string username, string password)
        {
            try
            {
                string sql = "select * from account where username=@username and password=@password ";

                List<SqlParameter> lParams = new List<SqlParameter>();
                try
                {
                    lParams.Add(new SqlParameter("@username", username));
                    lParams.Add(new SqlParameter("@password", password));
                    DataSet Ds = SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray());
                    if (Ds != null)
                    {
                        DataTable dt = Ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt.Rows[0];
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
                catch (SqlException  )
                {
                    throw;
                }
                finally
                {
                    lParams.Clear();
                }


            }
            catch (SqlException ex)
            {

            }
            return null;
        }
    }
}
