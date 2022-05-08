using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;
using UOB.PVD.Repository.ClassUtility;

namespace UOB.PVD.Repository.policy
{
	public class policy
	{
		public virtual DataTable Add(string connection, DataRow newRow)
		{
			
			string strSql = @"   Insert into policy ( 
										   policy_id
										  ,policy_type_id
										  ,policy_title_th
										  ,policy_title_en
										  ,policy_detail_th
										  ,policy_detail_en
										  ,show_with
										  ,policy_pdf_name_th
										  ,policy_pdf_original_name_th
										  ,policy_pdf_name_en
										  ,policy_pdf_original_name_en
										  ,policy_goto_url_th
										  ,policy_goto_url_en
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
										  ,order_by
										  ,active
								 ) values (
										   @policy_id
										  ,@policy_type_id
										  ,@policy_title_th
										  ,@policy_title_en
										  ,@policy_detail_th
										  ,@policy_detail_en
										  ,@show_with
										  ,@policy_pdf_name_th
										  ,@policy_pdf_original_name_th
										  ,@policy_pdf_name_en
										  ,@policy_pdf_original_name_en
										  ,@policy_goto_url_th
										  ,@policy_goto_url_en
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
										  ,@order_by
										  ,@active
								 )";
			strSql += @" select * from policy 
                                  where policy_seq = SCOPE_IDENTITY();";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
 
				lParams.Add(new SqlParameter("policy_id", newRow["policy_id"]));
				lParams.Add(new SqlParameter("policy_type_id", newRow["policy_type_id"]));
				lParams.Add(new SqlParameter("policy_title_th", newRow["policy_title_th"]));
				lParams.Add(new SqlParameter("policy_title_en", newRow["policy_title_en"]));
				lParams.Add(new SqlParameter("policy_detail_th", newRow["policy_detail_th"]));
				lParams.Add(new SqlParameter("policy_detail_en", newRow["policy_detail_en"]));
				lParams.Add(new SqlParameter("show_with", newRow["show_with"]));
				lParams.Add(new SqlParameter("policy_pdf_name_th", newRow["policy_pdf_name_th"]));
				lParams.Add(new SqlParameter("policy_pdf_original_name_th", newRow["policy_pdf_original_name_th"]));
				lParams.Add(new SqlParameter("policy_pdf_name_en", newRow["policy_pdf_name_en"]));
				lParams.Add(new SqlParameter("policy_pdf_original_name_en", newRow["policy_pdf_original_name_en"]));
				lParams.Add(new SqlParameter("policy_goto_url_th", newRow["policy_goto_url_th"]));
				lParams.Add(new SqlParameter("policy_goto_url_en", newRow["policy_goto_url_en"]));
				lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
				lParams.Add(new SqlParameter("order_by", newRow["order_by"]));
				lParams.Add(new SqlParameter("active", newRow["active"]));

				return SqlServer.ExecuteDataset(connection, CommandType.Text, strSql, lParams.ToArray()).Tables[0];
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				lParams.Clear();
			}
		}
		public virtual int Edit(string connection, DataRow updRow)
		{
			
			string strSql = @"  Update policy set  
										 policy_id = @policy_id
										  ,policy_type_id = @policy_type_id
										  ,policy_title_th = @policy_title_th
										  ,policy_title_en = @policy_title_en
										  ,policy_detail_th = @policy_detail_th
										  ,policy_detail_en = @policy_detail_en
										  ,show_with = @show_with
										  ,policy_pdf_name_th = @policy_pdf_name_th
										  ,policy_pdf_original_name_th = @policy_pdf_original_name_th
										  ,policy_pdf_name_en = @policy_pdf_name_en
										  ,policy_pdf_original_name_en = @policy_pdf_original_name_en
										  ,policy_goto_url_th = @policy_goto_url_th
										  ,policy_goto_url_en = @policy_goto_url_en
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
										  ,order_by = @order_by
										  ,active = @active
									where policy_seq = @policy_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("policy_seq", updRow["policy_seq"]));
				lParams.Add(new SqlParameter("policy_id", updRow["policy_id"]));
				lParams.Add(new SqlParameter("policy_type_id", updRow["policy_type_id"]));
				lParams.Add(new SqlParameter("policy_title_th", updRow["policy_title_th"]));
				lParams.Add(new SqlParameter("policy_title_en", updRow["policy_title_en"]));
				lParams.Add(new SqlParameter("policy_detail_th", updRow["policy_detail_th"]));
				lParams.Add(new SqlParameter("policy_detail_en", updRow["policy_detail_en"]));
				lParams.Add(new SqlParameter("show_with", updRow["show_with"]));
				lParams.Add(new SqlParameter("policy_pdf_name_th", updRow["policy_pdf_name_th"]));
				lParams.Add(new SqlParameter("policy_pdf_original_name_th", updRow["policy_pdf_original_name_th"]));
				lParams.Add(new SqlParameter("policy_pdf_name_en", updRow["policy_pdf_name_en"]));
				lParams.Add(new SqlParameter("policy_pdf_original_name_en", updRow["policy_pdf_original_name_en"]));
				lParams.Add(new SqlParameter("policy_goto_url_en", updRow["policy_goto_url_en"]));
				lParams.Add(new SqlParameter("policy_goto_url_th", updRow["policy_goto_url_th"]));
				lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
				lParams.Add(new SqlParameter("order_by", updRow["order_by"]));
				lParams.Add(new SqlParameter("active", updRow["active"]));

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
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
		public virtual int Delete(string connection, string policy_seq)
		{
			
			string strSql = @"  Delete from policy
							where (1=1) ";

			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(policy_seq))
				{
					strSql += " and (1 = 2) ";
				}
				else
				{
					strSql += " and policy_seq = @policy_seq ";
					lParams.Add(new SqlParameter("policy_seq", policy_seq));
				}

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				lParams.Clear();
			}
		}

		public DataTable GetListForInvestorChoice(string connection, InvestorChoiceSearchListDTO search)
		{
			try
			{
				string sqlWhere = $@"  and mpt.fund_seq = 1 
									   and mpt.investment_type_id = '{search.InvestorPage}'";
				string sql = $@"select 
								tb.policy_seq,
								tb.policy_id,
								tb.policy_type_id,
								mpt.policy_type_name_th,
								mpt.investment_type_id ,
								mit.investment_type_name,
								mpt.fund_seq,
								mf.fund_name_th,
								tb.policy_title_th,
								tb.policy_title_en
 
							from policy tb
								left join mas_policy_type mpt on mpt.policy_type_id = tb.policy_type_id
								left join mas_investment_type mit on mit.investment_type_id = mpt.investment_type_id
								left join mas_fund mf on mf.fund_seq = mpt.fund_seq where (1=1) {sqlWhere}";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
 
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
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
			catch (SqlException ex)
			{
				throw;
			}
		}
		public DataTable GetListForMasterFund(string connection, MasterFundSearchListDTO search)
		{
			try
			{
				string sqlWhere = $@"  and mpt.fund_seq = 2 
									   and mpt.investment_type_id = '{search.InvestorPage}'";
				string sql = $@"select 
								tb.policy_seq,
								tb.policy_id,
								tb.policy_type_id,
								mpt.policy_type_name_th,
								mpt.investment_type_id ,
								mit.investment_type_name,
								mpt.fund_seq,
								mf.fund_name_th,
								tb.policy_title_th,
								tb.policy_title_en
 
							from policy tb
								left join mas_policy_type mpt on mpt.policy_type_id = tb.policy_type_id
								left join mas_investment_type mit on mit.investment_type_id = mpt.investment_type_id
								left join mas_fund mf on mf.fund_seq = mpt.fund_seq where (1=1) {sqlWhere}";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{

					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
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
			catch (SqlException ex)
			{
				throw;
			}
		}
		public DataTable GetList(string connection, string policy_seq)
		{
			try
			{
				string sql = "select tb.* from policy tb where tb.policy_seq = @policy_seq ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					lParams.Add(new SqlParameter("@policy_seq", policy_seq));
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
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
				throw;
			}
		}
		public DataTable FrontEndGetList(string connection, string fund_seq, string investment_type_id, string policy_type_id, string basePathFile)
		{
			try
			{
				string sql = @"select 
									tb.policy_seq,
									tb.policy_id,
									tb.policy_type_id,
									mpt.policy_type_name_th,
									mpt.investment_type_id ,
									mit.investment_type_name,
									mpt.fund_seq,
									mf.fund_name_th,
									tb.policy_title_th,
									tb.policy_title_en,
									tb.policy_detail_th,
									tb.policy_detail_en,
									tb.show_with,
									tb.policy_pdf_name_th,
									'{0}' + tb.policy_pdf_name_th as policy_pdf_url_th,
									tb.policy_pdf_original_name_th,
									tb.policy_pdf_name_en,
									'{0}' + tb.policy_pdf_name_en as policy_pdf_url_en,
									tb.policy_pdf_original_name_en,
									tb.policy_goto_url_th ,
									tb.policy_goto_url_en 
								from policy tb
									left join mas_policy_type mpt on mpt.policy_type_id = tb.policy_type_id
									left join mas_investment_type mit on mit.investment_type_id = mpt.investment_type_id
									left join mas_fund mf on mf.fund_seq = mpt.fund_seq 
								where (1=1)
									and mpt.fund_seq = @fund_seq
									and mit.investment_type_id = @investment_type_id
									and mpt.policy_type_id = @policy_type_id";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
				
					lParams.Add(new SqlParameter("@fund_seq", fund_seq));
					lParams.Add(new SqlParameter("@investment_type_id", investment_type_id));
					lParams.Add(new SqlParameter("@policy_type_id", policy_type_id));
					sql = string.Format(sql, basePathFile);
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
		 
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
			catch (SqlException)
			{
				throw;
			}
		}
		public DataTable FrontEndGetList(string connection,  string fund_seq,  string basePathFile)
		{
			try
			{
				string sql = @"select 
									tb.policy_seq,
									tb.policy_id,
									tb.policy_type_id,
									mpt.policy_type_name_th,
									mpt.investment_type_id ,
									mit.investment_type_name,
									mpt.fund_seq,
									mf.fund_name_th,
									tb.policy_title_th,
									tb.policy_title_en,
									tb.policy_detail_th,
									tb.policy_detail_en,
									tb.show_with,
									tb.policy_pdf_name_th,
									'{0}' + tb.policy_pdf_name_th as policy_pdf_url_th,
									tb.policy_pdf_original_name_th,
									tb.policy_pdf_name_en,
									'{0}' + tb.policy_pdf_name_en as policy_pdf_url_en,
									tb.policy_pdf_original_name_en,
									tb.policy_goto_url_en ,
									tb.policy_goto_url_th 
								from policy tb
									left join mas_policy_type mpt on mpt.policy_type_id = tb.policy_type_id
									left join mas_investment_type mit on mit.investment_type_id = mpt.investment_type_id
									left join mas_fund mf on mf.fund_seq = mpt.fund_seq 
								where (1=1)
									and mpt.fund_seq  = @fund_seq ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					lParams.Add(new SqlParameter("@fund_seq", fund_seq));
					sql = string.Format(sql, basePathFile);
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];

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
			catch (SqlException)
			{
				throw;
			}
		}
		public DataRow GetData(string connection, string policy_seq,string policy_type_id, string basePathFile)
		{
			try
			{
				string sql = @"select 
									tb.policy_seq,
									tb.policy_id,
									tb.policy_type_id,
									mpt.policy_type_name_th,
									mpt.investment_type_id ,
									mit.investment_type_name,
									mpt.fund_seq,
									mf.fund_name_th,
									tb.policy_title_th,
									tb.policy_title_en,
									tb.policy_detail_th,
									tb.policy_detail_en,
									tb.show_with,
									tb.policy_pdf_name_th,
									'{0}' + tb.policy_pdf_name_th as policy_pdf_url_th,
									tb.policy_pdf_original_name_th,
									tb.policy_pdf_name_en,
									'{0}' + tb.policy_pdf_name_en as policy_pdf_url_en,
									tb.policy_pdf_original_name_en,
									tb.policy_goto_url_en ,
policy_goto_url_th
								from policy tb
									left join mas_policy_type mpt on mpt.policy_type_id = tb.policy_type_id
									left join mas_investment_type mit on mit.investment_type_id = mpt.investment_type_id
									left join mas_fund mf on mf.fund_seq = mpt.fund_seq 
								where tb.policy_seq = @policy_seq and tb.policy_type_id = @policy_type_id";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					sql = string.Format(sql,basePathFile);
					lParams.Add(new SqlParameter("@policy_seq", policy_seq));
					lParams.Add(new SqlParameter("@policy_type_id", policy_type_id));
					DataTable dt = SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
                    if (dt is not null && dt.Rows.Count > 0)
                    {
						return dt.Rows[0];
					}
                    else
                    {
						return null;
                    }
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
			catch (SqlException)
			{
				throw;
			}
		}
		public DataRow SetData(string connections, InvestorChoiceDTO data)
		{
			DataTable dt = GetList(connections, ConvertTo.String(data.policy_seq));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["policy_type_id"] = ConvertTo.StringForDatabase(data.policy_type_id);
			row["policy_title_th"] = ConvertTo.StringForDatabase(data.policy_title_th);
			row["policy_title_en"] = ConvertTo.StringForDatabase(data.policy_title_en);
			row["policy_detail_th"] = ConvertTo.StringForDatabase(data.policy_detail_th);
			row["policy_detail_en"] = ConvertTo.StringForDatabase(data.policy_detail_en);
			row["show_with"] = ConvertTo.StringForDatabase(data.show_with);
			row["policy_pdf_name_th"] = ConvertTo.StringForDatabase(data.policy_pdf_name_th);
			row["policy_pdf_original_name_th"] = ConvertTo.StringForDatabase(data.policy_pdf_original_name_th);
			row["policy_pdf_name_en"] = ConvertTo.StringForDatabase(data.policy_pdf_name_en);
			row["policy_pdf_original_name_en"] = ConvertTo.StringForDatabase(data.policy_pdf_original_name_en);
			row["policy_goto_url_th"] = ConvertTo.StringForDatabase(data.policy_goto_url_th);
			row["policy_goto_url_en"] = ConvertTo.StringForDatabase(data.policy_goto_url_en);
			return row;
		}
		public DataRow SetData(string connections, MasterFundDTO data)
		{
			DataTable dt = GetList(connections, ConvertTo.String(data.policy_seq));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["policy_type_id"] = ConvertTo.StringForDatabase(data.policy_type_id);
			row["policy_title_th"] = ConvertTo.StringForDatabase(data.policy_title_th);
			row["policy_title_en"] = ConvertTo.StringForDatabase(data.policy_title_en);
			row["policy_detail_th"] = ConvertTo.StringForDatabase(data.policy_detail_th);
			row["policy_detail_en"] = ConvertTo.StringForDatabase(data.policy_detail_en);
			row["show_with"] = ConvertTo.StringForDatabase(data.show_with);
			row["policy_pdf_name_th"] = ConvertTo.StringForDatabase(data.policy_pdf_name_th);
			row["policy_pdf_original_name_th"] = ConvertTo.StringForDatabase(data.policy_pdf_original_name_th);
			row["policy_pdf_name_en"] = ConvertTo.StringForDatabase(data.policy_pdf_name_en);
			row["policy_pdf_original_name_en"] = ConvertTo.StringForDatabase(data.policy_pdf_original_name_en);
			row["policy_goto_url_en"] = ConvertTo.StringForDatabase(data.policy_goto_url_en);
			row["policy_goto_url_th"] = ConvertTo.StringForDatabase(data.policy_goto_url_th);
			return row;
		}
	}
}
