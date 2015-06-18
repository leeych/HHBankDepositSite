using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace HHBankDepositSite.Data
{
    public class TestDatas
    {
        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("age", typeof(string));
            dt.Columns.Add("salary", typeof(string));
            dt.Columns.Add("group", typeof(string));

            for (int i = 0; i < 666; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "ID：" + i.ToString().PadLeft(4, '0');
                dr[1] = "名字：" + i.ToString().PadLeft(4, '0');
                dr[2] = "年龄：" + new Random(i).Next(20, 30);
                dr[3] = "周薪：" + new Random(i).Next(10000, 99999);
                dr[4] = "分组：" + (int)(i / 7);

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}