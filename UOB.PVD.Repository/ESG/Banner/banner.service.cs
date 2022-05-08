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

namespace UOB.PVD.Repository.Banner
{
    public class banner
    {
        //===== CMS =====//
        public virtual int Add(string connection, DataRow newRow)
        {
            string strSql = @"   Insert into banner ( 
										   title_th
										  ,title_en
										  ,text_th
										  ,text_en
										  ,link
										  ,image_th
										  ,image_en
										  ,image_mobile_th
										  ,image_mobile_en
										  ,status
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
,vdo
								 ) values (
										   @title_th
										  ,@title_en
										  ,@text_th
										  ,@text_en
										  ,@link
										  ,@image_th
										  ,@image_en
										  ,@image_mobile_th
										  ,@image_mobile_en
										  ,@status
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
 ,@vdo
								 )";
            List<SqlParameter> lParams = new List<SqlParameter>();
            try
            {
 
                lParams.Add(new SqlParameter("title_th", newRow["title_th"]));
                lParams.Add(new SqlParameter("title_en", newRow["title_en"]));
                lParams.Add(new SqlParameter("text_th", newRow["text_th"]));
                lParams.Add(new SqlParameter("text_en", newRow["text_en"]));
                lParams.Add(new SqlParameter("link", newRow["link"]));
                lParams.Add(new SqlParameter("image_th", newRow["image_th"]));
                lParams.Add(new SqlParameter("image_en", newRow["image_en"]));
                lParams.Add(new SqlParameter("image_mobile_th", newRow["image_mobile_th"]));
                lParams.Add(new SqlParameter("image_mobile_en", newRow["image_mobile_en"]));
                lParams.Add(new SqlParameter("status", newRow["status"]));
                lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
                lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
                lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
                lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
                lParams.Add(new SqlParameter("vdo", newRow["vdo"]));

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
            string strSql = @"  Update banner set  
										 title_th = @title_th
										  ,title_en = @title_en
										  ,text_th = @text_th
										  ,text_en = @text_en
										  ,link = @link
										  ,image_th = @image_th
										  ,image_en = @image_en
										  ,image_mobile_th = @image_mobile_th
										  ,image_mobile_en = @image_mobile_en
										  ,status = @status
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
 ,vdo = @vdo
									where banner_seq = @banner_seq  ";
            List<SqlParameter> lParams = new List<SqlParameter>();
            try
            {
                lParams.Add(new SqlParameter("banner_seq", updRow["banner_seq"]));
                lParams.Add(new SqlParameter("title_th", updRow["title_th"]));
                lParams.Add(new SqlParameter("title_en", updRow["title_en"]));
                lParams.Add(new SqlParameter("text_th", updRow["text_th"]));
                lParams.Add(new SqlParameter("text_en", updRow["text_en"]));
                lParams.Add(new SqlParameter("link", updRow["link"]));
                lParams.Add(new SqlParameter("image_th", updRow["image_th"]));
                lParams.Add(new SqlParameter("image_en", updRow["image_en"]));
                lParams.Add(new SqlParameter("image_mobile_th", updRow["image_mobile_th"]));
                lParams.Add(new SqlParameter("image_mobile_en", updRow["image_mobile_en"]));
                lParams.Add(new SqlParameter("status", updRow["status"]));
                lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
                lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
                lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
                lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
                lParams.Add(new SqlParameter("vdo", updRow["vdo"]));

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
        public virtual int Delete(string connection, string banner_seq)
        {
            string strSql = @"  Delete from banner
							where (1=1) ";

            List<SqlParameter> lParams = new List<SqlParameter>();
            try
            {
                if (string.IsNullOrEmpty(banner_seq))
                {
                    strSql += " and (1 = 2) ";
                }
                else
                {
                    strSql += " and banner_seq = @banner_seq ";
                    lParams.Add(new SqlParameter("banner_seq", banner_seq));
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
        public virtual DataRow GetData(string connection ,string banner_seq)
		{

			string strWhere = string.Empty;
			string strSql = @" select  
								  tb.banner_seq
								 ,tb.title_th
								 ,tb.title_en
								 ,tb.text_th
								 ,tb.text_en
								 ,tb.link
								 ,tb.image_th
								 ,tb.image_en
								 ,tb.image_mobile_th
								 ,tb.image_mobile_en
                                 ,tb.vdo
								 ,tb.status
                                 ,st.status_name
								 ,st.status_color
							 from banner tb
                                 left join [status] st on  st.status_id = tb.status
							 where (1=1) {0}";
			DataTable dtResult = null;
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(banner_seq))
				{
					strWhere += " and (1=2)";
				}
				else
				{
					if (!string.IsNullOrEmpty(banner_seq))
					{
						strWhere += " and tb.banner_seq = @banner_seq";
						lParams.Add(new SqlParameter("banner_seq", banner_seq));
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
		public DataRow GetItem(string connection, string banner_seq)
        {
            try
            {
                string sql = "select tb.* from banner tb where tb.banner_seq = @banner_seq ";
                List<SqlParameter> lParams = new List<SqlParameter>();
                try
                {
                    lParams.Add(new SqlParameter("@banner_seq", banner_seq));
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

            }
            return null;
        }
        public DataTable GetList(string connection, string banner_seq)
        {
            try
            {
                string sql = "select tb.* from banner tb where tb.banner_seq = @banner_seq ";
                List<SqlParameter> lParams = new List<SqlParameter>();
                try
                {
                    lParams.Add(new SqlParameter("@banner_seq", banner_seq));
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
        public DataTable GetAllList(string connection,string base_picture_url, DatatableSearchBanner param)
        {
            try
            {
                string sqlWhere = "";
                string sql = @"   with tmp as
								(
									select ROW_NUMBER() OVER (order by tb.create_date desc,tb.create_date desc) as rowindex
										,tb.banner_seq AS Id
									from banner tb
									where (1=1) {0} 
								)  
								select tmp.rowindex
										,(select count(*) from banner where (1=1) {0}  )  as maxrow
										,tb.banner_seq
										,tb.title_th
										,tb.title_en
                                        ,'{1}' + tb.vdo as vdo
										,'{1}' + tb.image_th as image_th  
										,tb.status  
										,st.status_name  
								from tmp, banner tb
								join [status] st on st.status_id = tb.status
								where tmp.id = tb.banner_seq  {0}   ";
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
        public virtual int Publish(string connection,string banner_seq,string username)
        {
   
            string strSql = @"  Update banner set  
										 status = @status
                                         ,publish_by = @publish_by
                                         ,publish_date = @publish_date
									where banner_seq = @banner_seq  ";
            List<SqlParameter> lParams = new List<SqlParameter>();
            try
            {
                lParams.Add(new SqlParameter("banner_seq", banner_seq));
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
        public virtual int UnPublish(string connection, string banner_seq, string username)
        {

            string strSql = @"  Update banner set  
										 status = 0
                                         ,unpublish_by = @unpublish_by
                                         ,unpublish_date = getdate()
									where banner_seq =   " + banner_seq;
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
        public DataRow SetData(string connection, BannerModel data)
        {
            DataTable dt =  GetList(connection, ConvertTo.String(data.banner_seq));
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
            row["text_th"] = ConvertTo.StringForDatabase(data.text_th);
            row["text_en"] = ConvertTo.StringForDatabase(data.text_en);
            row["link"] = ConvertTo.StringForDatabase(data.link);
            row["image_th"] = ConvertTo.StringForDatabase(data.image_th);
            row["image_en"] = ConvertTo.StringForDatabase(data.image_en);
            row["image_mobile_th"] = ConvertTo.StringForDatabase(data.image_mobile_th);
            row["image_mobile_en"] = ConvertTo.StringForDatabase(data.image_mobile_en);
            row["vdo"] = ConvertTo.StringForDatabase(data.vdo);
            return row;
        }

        //===== CMS =====//

        //===== FRONT =====//
        public DataTable FrontEndGetListTH(string connection, string cms_url)
        {
            try
            {
                string sql = @"select tb.banner_seq
                                    , tb.title_th as title
	                                ,tb.text_th as text
	                                ,tb.link as link
	                                ,tb.image_th as image
                                    ,'{0}' + tb.image_th as url_image
	                                ,tb.image_mobile_th as image_mobile
                                    ,'{0}' + tb.image_mobile_th as url_image_mobile
                                    ,'{0}' + tb.vdo as url_vdo
                                from banner tb
                                where tb.status = 1
                                order by publish_date desc";
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
                string sql = @"select tb.banner_seq
                                    , tb.title_en as title
	                                ,tb.text_en as text
	                                ,tb.link as link
	                                ,tb.image_en as image
                                    ,'{0}' + tb.image_en as url_image
	                                ,tb.image_mobile_en as image_mobile
                                    ,'{0}' + tb.image_mobile_en as url_image_mobile
                                    ,'{0}' + tb.vdo as url_vdo
                                from banner tb
                                where tb.status = 1
                                order by publish_date desc";
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
        //===== FRONT =====//
    }
}
 