using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.EVT;
using UOB.PVD.Repository.ClassUtility;

namespace UOB.PVD.Repository.EVT
{
	public class evevt
    {
		public virtual int Add(string connection, DataRow newRow)
		{
			string strSql = @"   Insert into evevt ( 
										  event_id
										  ,event_code
										  ,event_name_th
										  ,event_name_en
										  ,event_date_from
										  ,event_time_from
										  ,event_date_to
										  ,event_time_to
										  ,event_type_th
										  ,event_type_en
										  ,event_at_th
										  ,event_at_en
										  ,register_total
										  ,register_schedule_from
										  ,register_schedule_to
										  ,fee
										  ,create_by
										  ,create_date
										  ,modify_by
										  ,modify_date
										  ,is_register_by_qrcode
										  ,qr_code
										  ,event_status
										  ,active
										  ,event_detail_th
										  ,event_detail_en
										  ,event_banner_th
										  ,event_banner_en
								 ) values (
										  @event_id
										  ,@event_code
										  ,@event_name_th
										  ,@event_name_en
										  ,@event_date_from
										  ,@event_time_from
										  ,@event_date_to
										  ,@event_time_to
										  ,@event_type_th
										  ,@event_type_en
										  ,@event_at_th
										  ,@event_at_en
										  ,@register_total
										  ,@register_schedule_from
										  ,@register_schedule_to
										  ,@fee
										  ,@create_by
										  ,@create_date
										  ,@modify_by
										  ,@modify_date
										  ,@is_register_by_qrcode
										  ,@qr_code
										  ,@event_status
										  ,@active
										  ,@event_detail_th
										  ,@event_detail_en
										  ,@event_banner_th
										  ,@event_banner_en
								 )";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("event_id", newRow["event_id"]));
				lParams.Add(new SqlParameter("event_code", newRow["event_code"]));
				lParams.Add(new SqlParameter("event_name_th", newRow["event_name_th"]));
				lParams.Add(new SqlParameter("event_name_en", newRow["event_name_en"]));
				lParams.Add(new SqlParameter("event_date_from", newRow["event_date_from"]));
				lParams.Add(new SqlParameter("event_time_from", newRow["event_time_from"]));
				lParams.Add(new SqlParameter("event_date_to", newRow["event_date_to"]));
				lParams.Add(new SqlParameter("event_time_to", newRow["event_time_to"]));
				lParams.Add(new SqlParameter("event_type_th", newRow["event_type_th"]));
				lParams.Add(new SqlParameter("event_type_en", newRow["event_type_en"]));
				lParams.Add(new SqlParameter("event_at_th", newRow["event_at_th"]));
				lParams.Add(new SqlParameter("event_at_en", newRow["event_at_en"]));
				lParams.Add(new SqlParameter("register_total", newRow["register_total"]));
				lParams.Add(new SqlParameter("register_schedule_from", newRow["register_schedule_from"]));
				lParams.Add(new SqlParameter("register_schedule_to", newRow["register_schedule_to"]));
				lParams.Add(new SqlParameter("fee", newRow["fee"]));
				lParams.Add(new SqlParameter("create_by", newRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", newRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", newRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", newRow["modify_date"]));
				lParams.Add(new SqlParameter("is_register_by_qrcode", newRow["is_register_by_qrcode"]));
				lParams.Add(new SqlParameter("qr_code", newRow["qr_code"]));
				lParams.Add(new SqlParameter("event_status", newRow["event_status"]));
				lParams.Add(new SqlParameter("active", newRow["active"]));
				lParams.Add(new SqlParameter("event_detail_th", newRow["event_detail_th"]));
				lParams.Add(new SqlParameter("event_detail_en", newRow["event_detail_en"]));
				lParams.Add(new SqlParameter("event_banner_th", newRow["event_banner_th"]));
				lParams.Add(new SqlParameter("event_banner_en", newRow["event_banner_en"]));

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
			string strSql = @"  Update evevt set  
										 event_code = @event_code
										  ,event_name_th = @event_name_th
										  ,event_name_en = @event_name_en
										  ,event_date_from = @event_date_from
										  ,event_time_from = @event_time_from
										  ,event_date_to = @event_date_to
										  ,event_time_to = @event_time_to
										  ,event_type_th = @event_type_th
										  ,event_type_en = @event_type_en
										  ,event_at_th = @event_at_th
										  ,event_at_en = @event_at_en
										  ,register_total = @register_total
										  ,register_schedule_from = @register_schedule_from
										  ,register_schedule_to = @register_schedule_to
										  ,fee = @fee
										  ,create_by = @create_by
										  ,create_date = @create_date
										  ,modify_by = @modify_by
										  ,modify_date = @modify_date
										  ,is_register_by_qrcode = @is_register_by_qrcode
										  ,qr_code = @qr_code
										  ,event_status = @event_status
										  ,active = @active
										  ,event_detail_th = @event_detail_th
										  ,event_detail_en = @event_detail_en
										  ,event_banner_th = @event_banner_th
										  ,event_banner_en = @event_banner_en
									where event_id = @event_id  ";
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				lParams.Add(new SqlParameter("event_id", updRow["event_id"]));
				lParams.Add(new SqlParameter("event_code", updRow["event_code"]));
				lParams.Add(new SqlParameter("event_name_th", updRow["event_name_th"]));
				lParams.Add(new SqlParameter("event_name_en", updRow["event_name_en"]));
				lParams.Add(new SqlParameter("event_date_from", updRow["event_date_from"]));
				lParams.Add(new SqlParameter("event_time_from", updRow["event_time_from"]));
				lParams.Add(new SqlParameter("event_date_to", updRow["event_date_to"]));
				lParams.Add(new SqlParameter("event_time_to", updRow["event_time_to"]));
				lParams.Add(new SqlParameter("event_type_th", updRow["event_type_th"]));
				lParams.Add(new SqlParameter("event_type_en", updRow["event_type_en"]));
				lParams.Add(new SqlParameter("event_at_th", updRow["event_at_th"]));
				lParams.Add(new SqlParameter("event_at_en", updRow["event_at_en"]));
				lParams.Add(new SqlParameter("register_total", updRow["register_total"]));
				lParams.Add(new SqlParameter("register_schedule_from", updRow["register_schedule_from"]));
				lParams.Add(new SqlParameter("register_schedule_to", updRow["register_schedule_to"]));
				lParams.Add(new SqlParameter("fee", updRow["fee"]));
				lParams.Add(new SqlParameter("create_by", updRow["create_by"]));
				lParams.Add(new SqlParameter("create_date", updRow["create_date"]));
				lParams.Add(new SqlParameter("modify_by", updRow["modify_by"]));
				lParams.Add(new SqlParameter("modify_date", updRow["modify_date"]));
				lParams.Add(new SqlParameter("is_register_by_qrcode", updRow["is_register_by_qrcode"]));
				lParams.Add(new SqlParameter("qr_code", updRow["qr_code"]));
				lParams.Add(new SqlParameter("event_status", updRow["event_status"]));
				lParams.Add(new SqlParameter("active", updRow["active"]));
				lParams.Add(new SqlParameter("event_detail_th", updRow["event_detail_th"]));
				lParams.Add(new SqlParameter("event_detail_en", updRow["event_detail_en"]));
				lParams.Add(new SqlParameter("event_banner_th", updRow["event_banner_th"]));
				lParams.Add(new SqlParameter("event_banner_en", updRow["event_banner_en"]));

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
		public virtual int Delete(string connection, string event_id)
		{
			string strSql = @"  Delete from evevt
							where (1=1) ";

			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(event_id))
				{
					strSql += " and (1 = 2) ";
				}
				else
				{
					strSql += " and event_id = @event_id ";
					lParams.Add(new SqlParameter("event_id", event_id));
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

		/*------------------------------------------------------------------------------------------*/

		public virtual DataRow GetData(string connection, string baseUrl, string event_id )
		{
			string strWhere = string.Empty;
			string strSql = @"   with tmp as 
							 (
								 select row_number() over (order by event_id desc) as rowindex
										,tb.event_id as id
								 from evevt tb
								 where (1=1) 
							 )
							 select top 1  
								 tmp.rowindex
								 ,(select count(*) from evevt where (1=1)) maxrowindex 
								 ,tmp.id
								 ,tb.event_id
								 ,tb.event_code
								 ,tb.event_name_th
								 ,tb.event_name_en
								 ,tb.event_date_from
								 ,tb.event_time_from
								 ,tb.event_date_to
								 ,tb.event_time_to
								 ,tb.event_type_th
								 ,tb.event_type_en
								 ,tb.event_at_th
								 ,tb.event_at_en
								 ,tb.register_total
								 ,tb.register_schedule_from
								 ,tb.register_schedule_to
								 ,tb.fee
								 ,tb.create_by
								 ,tb.create_date
								 ,tb.modify_by
								 ,tb.modify_date
								 ,tb.is_register_by_qrcode
								 ,tb.qr_code
								 ,tb.event_status
								 ,tb.active
								 ,tb.event_detail_th
								 ,tb.event_detail_en
								 ,tb.event_banner_th
								 ,tb.event_banner_en
							 from tmp, evevt tb
							 where tmp.id = tb.event_id  
								{0}
						 ";
			DataTable dtResult = null;
			List<SqlParameter> lParams = new List<SqlParameter>();
			try
			{
				if (string.IsNullOrEmpty(event_id))
				{
					strWhere += "";
				}
				else
				{
					if (!string.IsNullOrEmpty(event_id))
					{
						strWhere += " and tb.event_id = @event_id";
						lParams.Add(new SqlParameter("event_id", event_id));
					}
				}
				strSql = string.Format(strSql, strWhere);

				strSql += " order by tmp.rowindex";

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

		/*------------------------------------------------------------------------------------------*/

		public DataTable GetList(string connection, string event_id)
		{
			try
			{
				string sql = "select * from  [evevt] tb where tb.event_id = @event_id ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					lParams.Add(new SqlParameter("@event_id", event_id));
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

		public DataTable GetListEventInCalendar(string connection)
		{
			try
			{
				string sql = @"select 
									convert(varchar, tb.event_date_from, 23) as start  
									,(convert(varchar, count(tb.event_date_from))  + ' Event ' ) as title
									,'#00CC00' as color
									,'#FFFFFF' as textColor
								from  [evevt] tb  
								group by convert(varchar, tb.event_date_from, 23) ";
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

		public DataTable GetListEventInDay(string connection,string baseUrl,DateTime date)
		{
			try
			{
				string sql = @"select 
								tb.event_id 
								,tb.event_name_th
								,tb.event_name_en
								,'{0}'+ isnull(tb.event_banner_th,'')  as banner
								,convert(date,tb.event_date_from) as event_date_from
							from [evevt] tb 
							where (1=1) ";
				List<SqlParameter> lParams = new List<SqlParameter>();
				try
				{
					sql += " and convert(date,tb.event_date_from) = convert(date,@date)  ";
					string.Format(sql, baseUrl);
					lParams.Add(new SqlParameter("date", date));
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

		public DataRow SetData(string connections, EventModel data)
		{
			DataTable dt = GetList(connections, ConvertTo.String(data.event_id));
			DataRow row = null;
			if (dt != null && dt.Rows.Count > 0)
			{
				row = dt.Rows[0];
			}
			else
			{
				row = dt.NewRow();
			}
			row["event_id"] = ConvertTo.StringForDatabase(data.event_id);
			row["event_code"] = ConvertTo.StringForDatabase(data.event_code);
			row["event_name_th"] = ConvertTo.StringForDatabase(data.event_name_th);
			row["event_name_en"] = ConvertTo.StringForDatabase(data.event_name_en);
			row["event_date_from"] = ConvertTo.CustomDateTime(data.event_date_from);
			row["event_time_from"] = ConvertTo.CustomDateTime(data.event_date_from).TimeOfDay;
			row["event_date_to"] = ConvertTo.CustomDateTime(data.event_date_to);
			row["event_time_to"] = ConvertTo.CustomDateTime(data.event_date_to).TimeOfDay;
			row["event_type_th"] = ConvertTo.StringForDatabase(data.event_type_th);
			row["event_type_en"] = ConvertTo.StringForDatabase(data.event_type_en);
			row["event_at_th"] = ConvertTo.StringForDatabase(data.event_at_th);
			row["event_at_en"] = ConvertTo.StringForDatabase(data.event_at_en);
			row["register_total"] = ConvertTo.IntForDatabase(data.register_total);
			row["register_schedule_from"] = ConvertTo.CustomDateTime(data.register_schedule_from);
			row["register_schedule_to"] = ConvertTo.CustomDateTime(data.register_schedule_to);
			row["fee"] = ConvertTo.DecimalForDatabase(data.fee);
			row["is_register_by_qrcode"] = data.is_register_by_qrcode;
			row["qr_code"] = ConvertTo.StringForDatabase(data.qr_code);
			row["event_detail_th"] = ConvertTo.StringForDatabase(data.event_detail_th);
			row["event_detail_en"] = ConvertTo.StringForDatabase(data.event_detail_en);
			row["event_banner_en"] = ConvertTo.StringForDatabase(data.event_banner_en);
			row["event_banner_th"] = ConvertTo.StringForDatabase(data.event_banner_th);
			return row;
		}
	}
}
