using Helper;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;
using UOB.PVD.Repository.ClassUtility;

namespace UOB.PVD.Repository.Fund
{
    public class fund
    {
		public virtual int Add(string connection, DataRow newRow)
		{
			string strSql = @"   Insert into Fund ( 
										  image_th
										  ,image_en
										  ,link
										  ,status
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
										  ,publish_by
										  ,publish_date
										  ,unpublish_by
										  ,unpublish_date
,title_th
,title_en
,text_en
,text_th
,order_by
								 ) values (
										  @image_th
										  ,@image_en
										  ,@link
										  ,@status
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
										  ,@publish_by
										  ,@publish_date
										  ,@unpublish_by
										  ,@unpublish_date
,@title_th
,@title_en
,@text_en
,@text_th
,(SELECT isnull (MAX(Fund.order_by),0) +1 FROM Fund)
								 )";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("image_th", newRow["image_th"]));
				lParams.Add(new SqlParameter("image_en", newRow["image_en"]));
				lParams.Add(new SqlParameter("link", newRow["link"]));
				lParams.Add(new SqlParameter("status", newRow["status"]));
				lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
				lParams.Add(new SqlParameter("publish_by", newRow["publish_by"]));
				lParams.Add(new SqlParameter("publish_date", newRow["publish_date"]));
				lParams.Add(new SqlParameter("unpublish_by", newRow["unpublish_by"]));
				lParams.Add(new SqlParameter("unpublish_date", newRow["unpublish_date"]));
				lParams.Add(new SqlParameter("title_th", newRow["title_th"]));
				lParams.Add(new SqlParameter("title_en", newRow["title_en"]));
				lParams.Add(new SqlParameter("text_en", newRow["text_en"]));
				lParams.Add(new SqlParameter("text_th", newRow["text_th"]));

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
                throw;
            }
			finally
			{
				lParams.Clear();
			}
		}
		public virtual int Edit(string connection, DataRow updRow)
		{
			string strSql = @"  Update Fund set  
										 image_th = @image_th
										  ,image_en = @image_en
										  ,link = @link
										  ,status = @status
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
										  ,publish_by = @publish_by
										  ,publish_date = @publish_date
										  ,unpublish_by = @unpublish_by
										  ,unpublish_date = @unpublish_date
,title_th = @title_th
,title_en = @title_en
,text_en = @text_en
,text_th = @text_th
									where fund_seq = @fund_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{ 
				lParams.Add(new SqlParameter("fund_seq", updRow["fund_seq"]));
				lParams.Add(new SqlParameter("image_th", updRow["image_th"]));
				lParams.Add(new SqlParameter("image_en", updRow["image_en"]));
				lParams.Add(new SqlParameter("link", updRow["link"]));
				lParams.Add(new SqlParameter("status", updRow["status"]));
				lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
				lParams.Add(new SqlParameter("publish_by", updRow["publish_by"]));
				lParams.Add(new SqlParameter("publish_date", updRow["publish_date"]));
				lParams.Add(new SqlParameter("unpublish_by", updRow["unpublish_by"]));
				lParams.Add(new SqlParameter("unpublish_date", updRow["unpublish_date"]));
				lParams.Add(new SqlParameter("title_th", updRow["title_th"]));
				lParams.Add(new SqlParameter("title_en", updRow["title_en"]));
				lParams.Add(new SqlParameter("text_en", updRow["text_en"]));
				lParams.Add(new SqlParameter("text_th", updRow["text_th"]));

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
                throw;
            }
			finally
			{
				lParams.Clear();
			}
		}
		public virtual int Delete(string connection, string fund_seq)
		{
			string strSql = @"  Delete from Fund
							where (1=1) ";

			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(fund_seq))
				{
					strSql += " and (1 = 2) ";
				}
				else
				{
					strSql += " and fund_seq = @fund_seq ";
					lParams.Add(new SqlParameter("fund_seq", fund_seq));
				}

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
                throw;
            }
			finally
			{
				lParams.Clear();
			}
		}
		public virtual int EditOrderBy(string connection, int order_by , int fund_seq)
		{
			string strSql = @"  Update Fund set  order_by = @order_by where fund_seq = @fund_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("order_by", order_by));
				lParams.Add(new SqlParameter("fund_seq", fund_seq));

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
				throw;
			}
			finally
			{
				lParams.Clear();
			}
		}
		public DataTable GetList(string connection, string fund_seq)
		{
			try
			{
				string sql = "select tb.* from Fund tb where tb.fund_seq = @fund_seq order by tb.order_by ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					lParams.Add(new SqlParameter("@fund_seq", fund_seq));
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
				}
				catch (SqlException ex)
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
		public DataTable GetAllList(string connection, string base_picture_url, DatatableSearchFund param)
		{
			try
			{
				string sqlWhere = "";
				string sql = @"   with tmp as
								(
									select ROW_NUMBER() OVER (order by tb.order_by) as rowindex
										,tb.fund_seq AS Id
									from Fund tb
									where (1=1)  {0} 
								)  
								select tmp.rowindex
										,(select count(*) from Fund where (1=1)  {0}   )  as maxrow
										,tb.fund_seq
										,tb.link
										,'{1}' + tb.image_th as image_th 
										,'{1}' + tb.image_en as image_en  
										,tb.status  
										,st.status_name  
								from tmp, Fund tb
								join [status] st on st.status_id = tb.status
								where tmp.id = tb.fund_seq {0}   ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					if (!string.IsNullOrEmpty(param.search_text))
					{
						sqlWhere += " and isnull(tb.link,'')  like '%'+@search_text+'%' ";
						lParams.Add(new SqlParameter("search_text", ConvertTo.String(param.search_text)));
					}
					sql = string.Format(sql, sqlWhere, base_picture_url);
					if (param.Length != -1)
						sql += string.Format(" and tmp.rowindex between {0} and {1} ", param.Start + 1, param.Start + param.Length);
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
				}
				catch (SqlException ex)
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
		public virtual DataRow GetData(string connection, string fund_seq)
		{
			string strWhere = string.Empty;
			string strSql = @" select  
								  tb.fund_seq
,tb.title_th
,tb.title_en
,tb.text_en
,tb.text_th
								 ,tb.image_th
								 ,tb.image_en
								 ,tb.link					 
								 ,tb.status
                                 ,st.status_name
								 ,st.status_color
							 from Fund tb
                                 left join [status] st on  st.status_id = tb.status
							 where (1=1) {0}";
			DataTable dtResult = null;
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(fund_seq))
				{
					strWhere += " and (1=2)";
				}
				else
				{
					if (!string.IsNullOrEmpty(fund_seq))
					{
						strWhere += " and tb.fund_seq = @fund_seq";
						lParams.Add(new SqlParameter("fund_seq", fund_seq));
					}
				}
				strSql = string.Format(strSql, strWhere);
				dtResult = SqlServer.ExecuteDataset(connection, CommandType.Text, strSql, lParams.ToArray()).Tables[0];
				if (dtResult != null && dtResult.Rows.Count > 0)
					return dtResult.Rows[0];
				else
					return null;
			}
			catch (SqlException ex)
			{
				throw;
			}
			finally
			{
				lParams.Clear();
			}
		}
		public virtual int Publish(string connection, string fund_seq, string username)
		{

			string strSql = @"  Update Fund set  
										 status = @status
                                         ,publish_by = @publish_by
                                         ,publish_date = @publish_date
									where fund_seq = @fund_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("fund_seq", fund_seq));
				lParams.Add(new SqlParameter("status", "1"));
				lParams.Add(new SqlParameter("publish_by", username));
				lParams.Add(new SqlParameter("publish_date", DateTime.Now));

				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
				throw;
			}
			finally
			{
				lParams.Clear();
			}
		}
		public virtual int UnPublish(string connection, string fund_seq, string username)
		{

			string strSql = @"  Update Fund set  
										 status = 0
                                         ,unpublish_by = @unpublish_by
                                         ,unpublish_date = getdate()
									where fund_seq =   " + fund_seq;
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("unpublish_by", username));



				return SqlServer.ExecuteNonQuery(connection, CommandType.Text, strSql, lParams.ToArray());
			}
			catch (SqlException ex)
			{
				throw;
			}
			finally
			{
				lParams.Clear();
			}
		}
		public DataTable FrontEndGetListTH(string connection, string cms_url )
		{
			try
			{
				string sql = @"select tb.fund_seq
									,tb.image_th as image
									,'{0}' + tb.image_th as url_image
									,link
									,title_th as  title
									,text_th as text
,order_by
								from Fund tb
								where tb.status = 1  
								order by order_by  ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					sql = string.Format(sql, cms_url);
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
				}
				catch (SqlException ex)
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
		public DataTable FrontEndGetListEN(string connection, string cms_url)
		{
			try
			{
				string sql = @"select tb.fund_seq
									,tb.image_EN as image
									,'{0}' + tb.image_EN as url_image
									,link
									,title_en as  title
									,text_en as text
,order_by
								from Fund tb
								where tb.status = 1  
								order by order_by  ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					sql = string.Format(sql, cms_url);
					return SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
				}
				catch (SqlException ex)
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
		public DataRow SetData(string connection,  FundModel data)
		{
			DataTable dt = GetList(connection, ConvertTo.String(data.fund_seq));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["link"] = ConvertTo.StringForDatabase(data.link);
			row["title_th"] = ConvertTo.StringForDatabase(data.title_th);
			row["title_en"] = ConvertTo.StringForDatabase(data.title_en);
			row["text_en"] = ConvertTo.StringForDatabase(data.text_en);
			row["text_th"] = ConvertTo.StringForDatabase(data.text_th);
			row["image_th"] = ConvertTo.StringForDatabase(data.image_th);
			row["image_en"] = ConvertTo.StringForDatabase(data.image_en);

			return row;
		}

	}
}
