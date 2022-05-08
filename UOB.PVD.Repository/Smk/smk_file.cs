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

namespace UOB.PVD.Repository.Smk
{
	public class smk_file
	{
		public virtual int Add(string connection, DataRow newRow)
		{
			string strSql = @"   Insert into smk_file ( 
										  smk_title_th
										  ,smk_title_en
										  ,smk_file_name_th
										  ,smk_file_name_en
										  ,smk_type
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
										  ,order_by
										  ,active
										,smk_file_original_name_th
										,smk_file_original_name_en
								 ) values (
										  @smk_title_th
										  ,@smk_title_en
										  ,@smk_file_name_th
										  ,@smk_file_name_en
										  ,@smk_type
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
										  ,@order_by
										  ,@active
										  ,@smk_file_original_name_th
										  ,@smk_file_original_name_en
								 )";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("smk_file_original_name_th", newRow["smk_file_original_name_th"]));
				lParams.Add(new SqlParameter("smk_file_original_name_en", newRow["smk_file_original_name_en"]));
				lParams.Add(new SqlParameter("smk_file_seq", newRow["smk_file_seq"]));
				lParams.Add(new SqlParameter("smk_title_th", newRow["smk_title_th"]));
				lParams.Add(new SqlParameter("smk_title_en", newRow["smk_title_en"]));
				lParams.Add(new SqlParameter("smk_file_name_th", newRow["smk_file_name_th"]));
				lParams.Add(new SqlParameter("smk_file_name_en", newRow["smk_file_name_en"]));
				lParams.Add(new SqlParameter("smk_type", newRow["smk_type"]));
				lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
				lParams.Add(new SqlParameter("order_by", newRow["order_by"]));
				lParams.Add(new SqlParameter("active", newRow["active"]));

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
		public virtual int Edit(string connection, DataRow updRow)
		{
			string strSql = @"  Update smk_file set  
										 smk_title_th = @smk_title_th
										  ,smk_title_en = @smk_title_en
										  ,smk_file_name_th = @smk_file_name_th
										  ,smk_file_name_en = @smk_file_name_en
										  ,smk_type = @smk_type
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
										  ,order_by = @order_by
										  ,active = @active
										 ,smk_file_original_name_th = @smk_file_original_name_th
										 ,smk_file_original_name_en = @smk_file_original_name_en
									where smk_file_seq = @smk_file_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("smk_file_original_name_th", updRow["smk_file_original_name_th"]));
				lParams.Add(new SqlParameter("smk_file_original_name_en", updRow["smk_file_original_name_en"]));
				lParams.Add(new SqlParameter("smk_file_seq", updRow["smk_file_seq"]));
				lParams.Add(new SqlParameter("smk_title_th", updRow["smk_title_th"]));
				lParams.Add(new SqlParameter("smk_title_en", updRow["smk_title_en"]));
				lParams.Add(new SqlParameter("smk_file_name_th", updRow["smk_file_name_th"]));
				lParams.Add(new SqlParameter("smk_file_name_en", updRow["smk_file_name_en"]));
				lParams.Add(new SqlParameter("smk_type", updRow["smk_type"]));
				lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
				lParams.Add(new SqlParameter("order_by", updRow["order_by"]));
				lParams.Add(new SqlParameter("active", updRow["active"]));

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
		public virtual int Delete(string connection, string smk_file_seq)
		{
			string strSql = @"  Delete from smk_file
							where (1=1) ";

			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(smk_file_seq))
				{
					strSql += " and (1 = 2) ";
				}
				else
				{
					strSql += " and smk_file_seq = @smk_file_seq ";
					lParams.Add(new SqlParameter("smk_file_seq", smk_file_seq));
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

		public DataTable GetList(string connection, string smk_file_seq )
		{
			try
			{
				string strSql = @"   select tb.* 
  ,'{0}' + tb.smk_file_name_th as smk_file_name_th_url
  ,'{0}' + tb.smk_file_name_en as smk_file_name_en_url
							 from smk_file tb
							 where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					string sqlWhere = string.Empty;

					if (string.IsNullOrEmpty(smk_file_seq))
					{
						strSql += " and (1=2)";
					}
					else
					{
						if (!string.IsNullOrEmpty(smk_file_seq))
						{
							strSql += " and tb.smk_file_seq = @smk_file_seq";
							lParams.Add(new SqlParameter("smk_file_seq", smk_file_seq));
						}
					}

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

		public DataTable GetList(string connection, string env, string smk_type = null)
		{
			try
			{
				string strSql = @"   select tb.* 
  ,'{0}' + tb.smk_file_name_th as smk_file_name_th_url
  ,'{0}' + tb.smk_file_name_en as smk_file_name_en_url
							 from smk_file tb
							 where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					string sqlWhere = string.Empty;

					if (!string.IsNullOrEmpty(smk_type))
					{
						strSql += " and tb.smk_type = @smk_type";
						lParams.Add(new SqlParameter("smk_type", smk_type));
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

		public DataTable GetDetail(string connection, string env, string smk_file_seq)
		{
			try
			{
				string strSql = @"   select tb.* 
								  ,'{0}' + tb.smk_file_name_th as smk_file_name_th_url
								  ,'{0}' + tb.smk_file_name_en as smk_file_name_en_url
							 from smk_file tb
							 where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					string sqlWhere = string.Empty;

					if (string.IsNullOrEmpty(smk_file_seq))
					{
						strSql += " and (1=2)";
					}
					else
					{
						if (!string.IsNullOrEmpty(smk_file_seq))
						{
							strSql += " and tb.smk_file_seq = @smk_file_seq";
							lParams.Add(new SqlParameter("smk_file_seq", smk_file_seq));
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

		public DataRow SetData(string connections, SmkFileDTO data)
		{
			DataTable dt = GetList(connections, ConvertTo.String(data.smk_file_seq));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["smk_title_th"] = ConvertTo.StringForDatabase(data.smk_title_th);
			row["smk_title_en"] = ConvertTo.StringForDatabase(data.smk_title_en);
			row["smk_file_name_th"] = ConvertTo.StringForDatabase(data.smk_file_name_th);
			row["smk_file_name_en"] = ConvertTo.StringForDatabase(data.smk_file_name_en);
			row["smk_file_original_name_th"] = ConvertTo.StringForDatabase(data.smk_file_original_name_th);
			row["smk_file_original_name_en"] = ConvertTo.StringForDatabase(data.smk_file_original_name_en);
			row["smk_type"] = ConvertTo.StringForDatabase(data.smk_type);
			return row;
		}

	}
}
