using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinControl
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 загружаем массив исходных данных БИТ из текстовика, который каждое утро выгружает 1С
            string[] orders =
            ReadData.ReadOrders();
                
            // 2 чистим хранилище исходных данных БИТ от данных предудущей загрузки
            DB.ExecSqlCommand("delete from BIT_DATA");
            Console.WriteLine("Очистка старых данных BIT_DATA - ok.");

            // 3 загружаем свежие данные БИТ из массива
            int sch = 0;
            string comm = "";
            char[] separator = { ';' };
            for (int i = 1; i < orders.Length; i++)
            {
                if (sch == 0)
                {
                    comm = "INSERT INTO BIT_DATA VALUES ";
                } 
                else 
                {
                    comm += ", ";
                }

                comm += string.Format("('{0}')", orders[i].Replace(";", "','"));
                sch++;

                if (sch == 100 || i == orders.Length-1)
                {
                    sch = 0;
                    Console.WriteLine(i.ToString());
                    DB.ExecSqlCommand(comm);
                } 
            }

            // 4 фиксируем обнаруженные заведенные заяки (ставим update MAIN_TAB set [CHECK_STAT] = 1 на основе данных загруженных в пунктах 1, 3)
            DB.FindCreatedOrders();

            Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}
