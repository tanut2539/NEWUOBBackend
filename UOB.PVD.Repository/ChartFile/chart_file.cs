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

namespace UOB.PVD.Repository.ChartFile
{
	public class chart_file
	{
		public virtual int Add(string connections, DataRow newRow)
		{
			string strSql = @"   Insert into chart_file ( 
										   chart_title_th
										  ,chart_title_en
										  ,chart_file_name_th
										  ,chart_file_original_name_th
										  ,chart_file_name_en
										  ,chart_file_original_name_en
										  ,chart_type
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
										  ,order_by
										  ,active
										  ,can_delete
										 ,show_with
										 ,chart_goto_url_th
										 ,chart_goto_url_en
								 ) values (
										   @chart_title_th
										  ,@chart_title_en
										  ,@chart_file_name_th
										  ,@chart_file_original_name_th
										  ,@chart_file_name_en
										  ,@chart_file_original_name_en
										  ,@chart_type
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
										  ,(SELECT max(chart_file.order_by) +1 FROM chart_file)
										  ,@active
										  ,@can_delete
										  ,@show_with
										  ,@chart_goto_url_th
										  ,@chart_goto_url_en
								 )";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("chart_goto_url_th", newRow["chart_goto_url_th"]));
				lParams.Add(new SqlParameter("chart_goto_url_en", newRow["chart_goto_url_en"]));
				lParams.Add(new SqlParameter("show_with", newRow["show_with"]));
				lParams.Add(new SqlParameter("chart_title_th", newRow["chart_title_th"]));
				lParams.Add(new SqlParameter("chart_title_en", newRow["chart_title_en"]));
				lParams.Add(new SqlParameter("chart_file_name_th", newRow["chart_file_name_th"]));
				lParams.Add(new SqlParameter("chart_file_original_name_th", newRow["chart_file_original_name_th"]));
				lParams.Add(new SqlParameter("chart_file_name_en", newRow["chart_file_name_en"]));
				lParams.Add(new SqlParameter("chart_file_original_name_en", newRow["chart_file_original_name_en"]));
				lParams.Add(new SqlParameter("chart_type", newRow["chart_type"]));
				lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
				lParams.Add(new SqlParameter("order_by", newRow["order_by"]));
				lParams.Add(new SqlParameter("active", newRow["active"]));
				lParams.Add(new SqlParameter("can_delete", newRow["can_delete"]));

				return SqlServer.ExecuteNonQuery(connections, CommandType.Text, strSql, lParams.ToArray());
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
		public virtual int Edit(string connections, DataRow updRow)
		{
			string strSql = @"  Update chart_file set  
										 chart_title_th = @chart_title_th
										  ,chart_title_en = @chart_title_en
										  ,chart_file_name_th = @chart_file_name_th
										  ,chart_file_original_name_th = @chart_file_original_name_th
										  ,chart_file_name_en = @chart_file_name_en
										  ,chart_file_original_name_en = @chart_file_original_name_en
										  ,chart_type = @chart_type
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
										  ,order_by = @order_by
										  ,active = @active
										  ,can_delete = @can_delete
										  ,chart_goto_url_th = @chart_goto_url_th
										  ,chart_goto_url_en = @chart_goto_url_en
										  ,show_with = @show_with
									where chart_file_seq = @chart_file_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("chart_goto_url_th", updRow["chart_goto_url_th"]));
				lParams.Add(new SqlParameter("chart_goto_url_en", updRow["chart_goto_url_en"]));
				lParams.Add(new SqlParameter("show_with", updRow["show_with"]));
				lParams.Add(new SqlParameter("chart_file_seq", updRow["chart_file_seq"]));
				lParams.Add(new SqlParameter("chart_title_th", updRow["chart_title_th"]));
				lParams.Add(new SqlParameter("chart_title_en", updRow["chart_title_en"]));
				lParams.Add(new SqlParameter("chart_file_name_th", updRow["chart_file_name_th"]));
				lParams.Add(new SqlParameter("chart_file_original_name_th", updRow["chart_file_original_name_th"]));
				lParams.Add(new SqlParameter("chart_file_name_en", updRow["chart_file_name_en"]));
				lParams.Add(new SqlParameter("chart_file_original_name_en", updRow["chart_file_original_name_en"]));
				lParams.Add(new SqlParameter("chart_type", updRow["chart_type"]));
				lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
				lParams.Add(new SqlParameter("order_by", updRow["order_by"]));
				lParams.Add(new SqlParameter("active", updRow["active"]));
				lParams.Add(new SqlParameter("can_delete", updRow["can_delete"]));

				return SqlServer.ExecuteNonQuery(connections, CommandType.Text, strSql, lParams.ToArray());
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
		public virtual int Delete(string connections, string chart_file_seq)
		{

			string strSql = @"  Delete from chart_file
							where (1=1) ";

			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(chart_file_seq))
				{
					strSql += " and (1 = 2) ";
				}
				else
				{
					strSql += " and chart_file_seq = @chart_file_seq ";
					lParams.Add(new SqlParameter("chart_file_seq", chart_file_seq));
				}

				return SqlServer.ExecuteNonQuery(connections, CommandType.Text, strSql, lParams.ToArray());
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
		public DataTable GetList(string connection, string chart_file_seq)
		{
			try
			{
				string strSql = @"   select tb.* 
									 from chart_file tb
									 where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					if (!string.IsNullOrWhiteSpace(chart_file_seq))
					{
						strSql += " and tb.chart_file_seq = @chart_file_seq ";
						lParams.Add(new SqlParameter("chart_file_seq", chart_file_seq));

					}

					strSql += "order by tb.order_by ";
					return SqlServer.ExecuteDataset(connection, CommandType.Text, strSql, lParams.ToArray()).Tables[0];
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
		public DataTable GetList(string connection, string env, string chart_type = null)
		{
			try
			{
				string strSql = @"   select tb.* 
  ,'{0}' + tb.chart_file_name_th as chart_file_name_th_url
  ,'{0}' + tb.chart_file_name_en as chart_file_name_en_url
							 from chart_file tb
							 where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					string sqlWhere = string.Empty;

					if (!string.IsNullOrEmpty(chart_type))
					{
						strSql += " and tb.smk_type = @smk_type";
						lParams.Add(new SqlParameter("smk_type", chart_type));
					}

					strSql = string.Format(strSql, env);
					strSql += "order by tb.order_by ";

					return SqlServer.ExecuteDataset(connection, CommandType.Text, strSql, lParams.ToArray()).Tables[0];
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

		public DataTable GetDetail(string connection, string env, string chart_file_seq)
		{
			try
			{
				string strSql = @"   select tb.* 
  ,'{0}' + tb.chart_file_name_th as chart_file_name_th_url
  ,'{0}' + tb.chart_file_name_en as chart_file_name_en_url
							 from chart_file tb
							 where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					string sqlWhere = string.Empty;

					if (string.IsNullOrEmpty(chart_file_seq))
					{
						strSql += " and (1=2)";
					}
					else
					{
						if (!string.IsNullOrEmpty(chart_file_seq))
						{
							strSql += " and tb.chart_file_seq = @chart_file_seq";
							lParams.Add(new SqlParameter("chart_file_seq", chart_file_seq));
						}
					}

					strSql = string.Format(strSql, env);


					return SqlServer.ExecuteDataset(connection, CommandType.Text, strSql, lParams.ToArray()).Tables[0];
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

		public DataRow SetData(string connections, ChartFileDTO data)
		{
			DataTable dt = GetList(connections, ConvertTo.String(data.chart_file_seq));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["show_with"] = ConvertTo.StringForDatabase(data.show_with);
			row["chart_goto_url_th"] = ConvertTo.StringForDatabase(data.chart_goto_url_th);
			row["chart_goto_url_en"] = ConvertTo.StringForDatabase(data.chart_goto_url_en);
			row["chart_title_th"] = ConvertTo.StringForDatabase(data.chart_title_th);
			row["chart_title_en"] = ConvertTo.StringForDatabase(data.chart_title_en);
			row["chart_file_name_th"] = ConvertTo.StringForDatabase(data.chart_file_name_th);
			row["chart_file_original_name_th"] = ConvertTo.StringForDatabase(data.chart_file_original_name_th);
			row["chart_file_name_en"] = ConvertTo.StringForDatabase(data.chart_file_name_en);
			row["chart_file_original_name_en"] = ConvertTo.StringForDatabase(data.chart_file_original_name_en);
			row["chart_type"] = ConvertTo.StringForDatabase(data.chart_type);

			return row;
		}
	}
}
