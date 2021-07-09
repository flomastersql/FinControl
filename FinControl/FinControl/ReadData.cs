using System.IO;

namespace FinControl
{
    class ReadData
    {
            public static string[] ReadOrders()
            {
                return File.ReadAllLines(@"D:\Documents\2 курс\Practice\ФоновоеОплаченныеЗаявкиПоКиСВФайл.txt");
            }
    }
}
