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
	public class mas_investment_type
    {
		public virtual DataTable GetList(string connect, string fund_seq)
		{
			string strSql = $@"   select * from mas_fund tb
							     where (1=1) ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				string sqlWhere = string.Empty;
				if (!string.IsNullOrEmpty(fund_seq))
				{
					strSql += " and tb.fund_seq = @fund_seq";
					lParams.Add(new SqlParameter("fund_seq", fund_seq));
				}

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
