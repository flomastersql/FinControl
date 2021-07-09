using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace FinControl
{
    class DB
    {
        static string str = "uid=Grafana;pwd=8emQ5NsG;Initial Catalog=FinControl;Data Source=10.0.20.8";

        public static void FindCreatedOrders () {
            ExecSqlCommand(
            "	update MAIN_TAB set [CHECK_STAT] = 1 where id in (	 " +
            "		 " +
            "	select mt.id	 " +
            "	from 	 " +
            "	[dbo].[MAIN_TAB] mt 	 " +
            "	join [dbo].[OBJ] o on mt.ID_OBJ = o.ID 	 " +
            "	join [dbo].[PERIOD_CTRL] pc on mt.ID_PERIOD_CTRL = pc.ID	 " +
            "	join [dbo].[PROV] p on mt.ID_PROV = p.ID 	 " +
            "	join [dbo].[SERV] s on mt.ID_SERV = s.ID  	 " +
            "	join [dbo].[STAT_ORDERS] so on mt.ID_STAT = so.ID 	 " +
            "	join BIT_DATA bd on bd.BIT_FRC = o.NAME and bd.BIT_PROVIDER = p.NAME  	 " +
            "	where 	 " +
            //"	 взяли заявки которые нужно проверить на существование в БИТ	 " +
            "	so.name = 'Заведен'	 " +
            //"	 и при этом только те что надо проверять на текущий день	 " +
            "	and mt.DAY_CTRL <= datepart(day, GETDATE()+3)	 " +
            //"	 и которые до настоящего момент не проверены	 " +
            "	and mt.CHECK_STAT <> 1 	 " +
            //"	 и которые вобще нужно в этом месяце проверять	 " +
            "	and pc.PATTERN like '%,' + CONVERT(varchar, datepart(month, GETDATE())) + ',%'	 " +
            //"	 сравниваем с данными загруженными из БИТ только за нужный месяц (т.к. там все подряд) НЕ ЗАБЫТЬ УБРАТЬ РАЗНОСТЬ В  datepart(month, GETDATE()-10)  В ПРДАКШН	 " +
            "	and datepart(month, GETDATE()) = datepart(month,  CONVERT(date, bd.bit_date))	 " +
            //"	 определяем что мы нашли именно ту заявку которую нужно по диапазону цены, сформированному из примерной цены и степени допуска в main_tab	 " +
            "	and convert(float, replace(replace(bd.bit_sum, ',', '.'), ' ', '')) 	 " +
            "	between convert(int, SUBSTRING(convert(varchar,mt.price), 1, len(convert(varchar,mt.price)) - mt.tolerance_of_price) + REPLICATE('0', mt.tolerance_of_price)) and	 " +
            "	convert(int, SUBSTRING(convert(varchar,mt.price), 1, len(convert(varchar,mt.price)) - mt.tolerance_of_price) + REPLICATE('0', mt.tolerance_of_price))+ convert(int, '1' + REPLICATE('0', mt.tolerance_of_price))	 " +
            "		 " +
            "	)	 " 


                );

         }


        public static void ExecSqlCommand(string comm)
        {
            SqlConnection scon = new SqlConnection(str);
            scon.Open();
            SqlCommand scom = new SqlCommand(comm, scon);

            scom.ExecuteNonQuery();

            scon.Close();

        }
    }
}