using System.IO;

namespace FinControl
{
    class ReadData
    {
            public static string[] ReadOrders()
            {
                return File.ReadAllLines(@"\\prog_1c.priminvestor.lan\КопииБазОбмен\ФоновоеОплаченныеЗаявкиПоКиСВФайл.TXT");
            }
    }
}
