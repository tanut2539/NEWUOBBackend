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

namespace UOB.PVD.Repository.Article
{
    public class article
    {
		public virtual int Add(string connection, DataRow newRow)
		{
			string strSql = @"   Insert into article ( 
										   title_th
										  ,title_en
										  ,content_th
										  ,content_en
										  ,author_name
										  ,image_th
										  ,image_en
										  ,image_thumbnail_th
										  ,image_thumbnail_en
										  ,status
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
										  ,publish_by
										  ,publish_date
										  ,unpublish_by
										  ,unpublish_date
								 ) values (
										   @title_th
										  ,@title_en
										  ,@content_th
										  ,@content_en
										  ,@author_name
										  ,@image_th
										  ,@image_en
										  ,@image_thumbnail_th
										  ,@image_thumbnail_en
										  ,@status
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
										  ,@publish_by
										  ,@publish_date
										  ,@unpublish_by
										  ,@unpublish_date
								 )";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
 
				lParams.Add(new SqlParameter("title_th", newRow["title_th"]));
				lParams.Add(new SqlParameter("title_en", newRow["title_en"]));
				lParams.Add(new SqlParameter("content_th", newRow["content_th"]));
				lParams.Add(new SqlParameter("content_en", newRow["content_en"]));
				lParams.Add(new SqlParameter("author_name", newRow["author_name"]));
				lParams.Add(new SqlParameter("image_th", newRow["image_th"]));
				lParams.Add(new SqlParameter("image_en", newRow["image_en"]));
				lParams.Add(new SqlParameter("image_thumbnail_th", newRow["image_thumbnail_th"]));
				lParams.Add(new SqlParameter("image_thumbnail_en", newRow["image_thumbnail_en"]));
				lParams.Add(new SqlParameter("status", newRow["status"]));
				lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
				lParams.Add(new SqlParameter("publish_by", newRow["publish_by"]));
				lParams.Add(new SqlParameter("publish_date", newRow["publish_date"]));
				lParams.Add(new SqlParameter("unpublish_by", newRow["unpublish_by"]));
				lParams.Add(new SqlParameter("unpublish_date", newRow["unpublish_date"]));

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
			string strSql = @"  Update article set  
										 title_th = @title_th
										  ,title_en = @title_en
										  ,content_th = @content_th
										  ,content_en = @content_en
										  ,author_name = @author_name
										  ,image_th = @image_th
										  ,image_en = @image_en
										  ,image_thumbnail_th = @image_thumbnail_th
										  ,image_thumbnail_en = @image_thumbnail_en
										  ,status = @status
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
										  ,publish_by = @publish_by
										  ,publish_date = @publish_date
										  ,unpublish_by = @unpublish_by
										  ,unpublish_date = @unpublish_date
									where article_seq = @article_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("article_seq", updRow["article_seq"]));
				lParams.Add(new SqlParameter("title_th", updRow["title_th"]));
				lParams.Add(new SqlParameter("title_en", updRow["title_en"]));
				lParams.Add(new SqlParameter("content_th", updRow["content_th"]));
				lParams.Add(new SqlParameter("content_en", updRow["content_en"]));
				lParams.Add(new SqlParameter("author_name", updRow["author_name"]));
				lParams.Add(new SqlParameter("image_th", updRow["image_th"]));
				lParams.Add(new SqlParameter("image_en", updRow["image_en"]));
				lParams.Add(new SqlParameter("image_thumbnail_th", updRow["image_thumbnail_th"]));
				lParams.Add(new SqlParameter("image_thumbnail_en", updRow["image_thumbnail_en"]));
				lParams.Add(new SqlParameter("status", updRow["status"]));
				lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
				lParams.Add(new SqlParameter("publish_by", updRow["publish_by"]));
				lParams.Add(new SqlParameter("publish_date", updRow["publish_date"]));
				lParams.Add(new SqlParameter("unpublish_by", updRow["unpublish_by"]));
				lParams.Add(new SqlParameter("unpublish_date", updRow["unpublish_date"]));

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
		public virtual int Delete(string connection, string article_seq)
		{
			string strSql = @"  Delete from article
							where (1=1) ";

			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(article_seq))
				{
					strSql += " and (1 = 2) ";
				}
				else
				{
					strSql += " and article_seq = @article_seq ";
					lParams.Add(new SqlParameter("article_seq", article_seq));
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

		public DataTable GetList(string connection, string article_seq)
		{
			try
			{
				string sql = "select tb.* from article tb where tb.article_seq = @article_seq ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					lParams.Add(new SqlParameter("@article_seq", article_seq));
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
		public virtual DataRow GetData(string connection, string article_seq)
		{

			string strWhere = string.Empty;
			string strSql = @" select  
								  tb.article_seq
								 ,tb.title_th
								 ,tb.title_en
								 ,tb.content_th
								 ,tb.content_en
								 ,tb.author_name
								 ,tb.image_th
								 ,tb.image_en
								 ,tb.image_thumbnail_th
								 ,tb.image_thumbnail_en
								 ,tb.status
                                 ,st.status_name
								 ,st.status_color
							 from article tb
                                 left join [status] st on  st.status_id = tb.status
							 where (1=1) {0}";
			DataTable dtResult = null;
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(article_seq))
				{
					strWhere += " and (1=2)";
				}
				else
				{
					if (!string.IsNullOrEmpty(article_seq))
					{
						strWhere += " and tb.article_seq = @article_seq";
						lParams.Add(new SqlParameter("article_seq", article_seq));
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

		public DataTable GetAllList(string connection, string base_picture_url, DatatableSearchArticle param)
		{
			try
			{
				string sqlWhere = "";
				string sql = @"   with tmp as
								(
									select ROW_NUMBER() OVER (order by tb.create_date desc,tb.create_date desc) as rowindex
										,tb.article_seq AS Id
									from article tb
									where (1=1) {0} 
								)  
								select tmp.rowindex
										,(select count(*) from article where (1=1) {0}   )  as maxrow
										,tb.article_seq
										,tb.title_th
										,tb.title_en
										,'{1}' + tb.image_th as image_th  
										,tb.status  
										,st.status_name  
								from tmp, article tb
								join [status] st on st.status_id = tb.status
								where tmp.id = tb.article_seq {0}   ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					if (!string.IsNullOrEmpty(param.search_text))
					{
						sqlWhere += " and isnull(tb.title_th,'') +' '+ isnull(tb.title_en,'') like '%'+@search_text+'%' ";
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
		public virtual int Publish(string connection, string article_seq, string username)
		{

			string strSql = @"  Update article set  
										 status = @status
                                         ,publish_by = @publish_by
                                         ,publish_date = @publish_date
									where article_seq = @article_seq  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("article_seq", article_seq));
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
		public virtual int UnPublish(string connection, string article_seq, string username)
		{

			string strSql = @"  Update article set  
										 status = 0
                                         ,unpublish_by = @unpublish_by
                                         ,unpublish_date = getdate()
									where article_seq =   " + article_seq;
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

		public DataRow SetData( string connections, ArticleModel data)
		{
			DataTable dt = GetList(connections, ConvertTo.String(data.article_seq));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["title_th"] = ConvertTo.StringForDatabase(data.title_th);
			row["title_en"] = ConvertTo.StringForDatabase(data.title_en);
			row["content_th"] = ConvertTo.StringForDatabase(data.content_th);
			row["content_en"] = ConvertTo.StringForDatabase(data.content_en);
			row["author_name"] = ConvertTo.StringForDatabase(data.author_name);
			row["image_th"] = ConvertTo.StringForDatabase(data.image_th);
			row["image_en"] = ConvertTo.StringForDatabase(data.image_en);
			row["image_thumbnail_th"] = ConvertTo.StringForDatabase(data.image_thumbnail_th);
			row["image_thumbnail_en"] = ConvertTo.StringForDatabase(data.image_thumbnail_en);
			return row;
		}

		//===== FRONT =====//
		public DataTable FrontEndGetListTH(string connection, string cms_url ,int size)
		{
			try
			{
				string sql = @"select top {1} tb.article_seq
                                    , tb.title_th as title
	                                ,tb.content_th as text
	                                ,tb.image_th as image
                                    ,'{0}' + tb.image_th as url_image
	                                ,tb.image_thumbnail_th as image_mobile
                                    ,'{0}' + tb.image_thumbnail_th as url_image_mobile
									,(SELECT COUNT(*) FROM article) as maxrow   
                                from article tb
                                where tb.status = 1   
								order by publish_date desc";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					sql = string.Format(sql, cms_url, size);
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
		public DataTable FrontEndGetListEN(string connection, string cms_url, int size)
		{
			try
			{
				string sql = @"select top {1} tb.article_seq
                                    , tb.title_en as title
	                                ,tb.content_en as text
	                                ,tb.image_en as image
                                    ,'{0}' + tb.image_en as url_image
	                                ,tb.image_thumbnail_en as image_mobile
                                    ,'{0}' + tb.image_thumbnail_en as url_image_mobile
									,(SELECT COUNT(*) FROM article) as maxrow	
                                from article tb
                                where tb.status = 1   
								order by publish_date desc";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					sql = string.Format(sql, cms_url, size);
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
		public DataTable FrontEndGetAllListTH(string connection, string cms_url, int start,int end)
		{
			try
			{
				string sqlWhere = "";
				string OrderBy = "";
				string sql = @" with temp as (
									select row_number() over ( order by publish_date desc ) as rownum
									,tb.article_seq AS id
									from article tb
									where (1=1)  {1}
								)
								select   
									temp.rownum
									, tb.article_seq
                                    , tb.title_th as title
	                                ,tb.content_th as text
	                                ,tb.image_th as image
                                    ,'{0}' + tb.image_th as url_image
	                                ,tb.image_thumbnail_th as image_mobile
                                    ,'{0}' + tb.image_thumbnail_th as url_image_mobile
                                from   temp ,article tb
                                where   temp.id = tb.article_seq     ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					OrderBy += "";
					sqlWhere += " and tb.status = 1  ";

					sql = string.Format(sql, cms_url, sqlWhere, OrderBy);

					if (start > 0 && end > 0)
					{
						sql += string.Format(" and temp.rownum between {0} and {1} ", start + 1,  end);
		 
					}
					sql += " order by rownum";
 
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
		public DataTable FrontEndGetAllListEn(string connection, string cms_url, int start, int end)
		{
			try
			{
				string sqlWhere = "";
				string OrderBy = "";
				string sql = @" with temp as (
									select row_number() over ( order by publish_date desc ) as rownum
									,tb.article_seq AS id
									from article tb
									where (1=1)  {1}
								)
								select   
									temp.rownum
									, tb.article_seq
                                    , tb.title_en as title
	                                ,tb.content_en as text
	                                ,tb.image_en as image
                                    ,'{0}' + tb.image_en as url_image
	                                ,tb.image_thumbnail_en as image_mobile
                                    ,'{0}' + tb.image_thumbnail_en as url_image_mobile
                                from   temp ,article tb
                                where   temp.id = tb.article_seq     ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					OrderBy += "";
					sqlWhere += " and tb.status = 1  ";

					sql = string.Format(sql, cms_url, sqlWhere, OrderBy);

					if (start > 0 && end > 0)
					{
						sql += string.Format(" and temp.rownum between {0} and {1} ", start + 1, end);

					}
					sql += " order by rownum";

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
		public DataRow FrontEndGetDataTH(string connection, string article_seq)
		{
			try
			{
				string sqlWhere = "";
				string sql = @"select tb.article_seq
                                     , tb.title_th as title
	                                 , tb.content_th as text
								     , tb.publish_date as publish_date
									 , tb.create_by as create_by
									 , a.user_fname + ' ' + a.user_lname as create_by_name
                                from article tb
								left join account a on a.username = tb.create_by
                                where tb.status = 1 {0}";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
                    if (!string.IsNullOrEmpty(article_seq))
                    {
						sqlWhere += string.Format(" and tb.article_seq = '{0}'" , article_seq); 
					}
                    else
                    {
						sqlWhere += " and (1=2)";
					}
					sql = string.Format(sql, sqlWhere);
					DataTable dt = SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
						return dt.Rows[0];
					}
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
			catch (SqlException ex)
			{
				throw;
			}
		}
		public DataRow FrontEndGetDataEN(string connection, string article_seq)
		{
			try
			{
				string sqlWhere = "";
				string sql = @"select tb.article_seq
                                     , tb.title_en as title
	                                 , tb.content_en as text
								     , tb.publish_date as publish_date
									 , tb.create_by as create_by
									 , a.user_fname + ' ' + a.user_lname as create_by_name
                                from article tb
								left join account a on a.username = tb.create_by
                                where tb.status = 1 {0}";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					if (!string.IsNullOrEmpty(article_seq))
					{
						sqlWhere += string.Format(" and tb.article_seq = '{0}'", article_seq);
					}
					else
					{
						sqlWhere += " and (1=2)";
					}
					sql = string.Format(sql, sqlWhere);
					DataTable dt = SqlServer.ExecuteDataset(connection, CommandType.Text, sql, lParams.ToArray()).Tables[0];
					if (dt != null && dt.Rows.Count > 0)
					{
						return dt.Rows[0];
					}
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
			catch (SqlException ex)
			{
				throw;
			}
		}
		//===== FRONT =====//


	}
}
