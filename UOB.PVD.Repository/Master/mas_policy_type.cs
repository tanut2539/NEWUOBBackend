using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Repository.ClassUtility;

namespace UOB.PVD.Repository.Master
{
	public class mas_policy_type
    {
		public virtual DataTable FrontEndGetList(string connect, string fund_seq , string investment_type_id)
		{
			string strSql = $@" select * from mas_policy_type tb
								where tb.investment_type_id = @investment_type_id
								and tb.fund_seq = @fund_seq ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("fund_seq", fund_seq));
				lParams.Add(new SqlParameter("investment_type_id", investment_type_id));
				return SqlServer.ExecuteDataset(connect, CommandType.Text, strSql, lParams.ToArray()).Tables[0];
			}
			catch (SqlException)
			{
				throw;
			}
			finally
			{
				lParams.Clear();
			}
		}
	}
}
