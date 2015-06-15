using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HHBankDepositSite
{
    public class BizValidator
    {   
        /// <summary>
        /// 验证协议编号
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="protocolId"></param>
        /// <returns></returns>
        public static bool IsProtocolId(string orgCode, string protocolId)
        {
            string pattern = orgCode.Substring(6) + DateTime.Now.Year.ToString();
            Regex reg = new Regex(@"^" + pattern + @"\d{6}$");
            Match m = reg.Match(protocolId);
            return m.Success;
        }

        /// <summary>
        /// 验证存单凭证号码是否是以50开始
        /// </summary>
        /// <param name="billCode"></param>
        /// <returns></returns>
        public static bool CheckBillCode(string billCode)
        {
            string pattern = @"^50\d{10}$";
            Regex reg = new Regex(pattern);
            Match m = reg.Match(billCode);
            return m.Success;
        }

        /// <summary>
        /// 验证对私存单账号
        /// </summary>
        /// <param name="billAccount"></param>
        /// <returns></returns>
        public static bool CheckBillAccount(string billAccount)
        { 
            string pattern = @"^1\d{11}102\d{8}";
            Regex reg = new Regex(pattern);
            Match m = reg.Match(billAccount);
            return m.Success;
        }

        /*
         * 理论部分：
         * 15位身份证号码=6位地区代码+6位生日+3位编号
         * 18位身份证号码=6位地区代码+8位生日+3位编号+1位检验码
         *
         * 各省市地区国家代码前两位代码是：     
         * 北京 11 吉林 22 福建 35 广东 44 云南 53 天津 12 黑龙江 23 江西 36 广西 45 西藏 54 河北 13 上海 31 山东 37 海南 46 陕西 61 山西 14 江苏 32 河南 41 重庆 50
        甘肃 62 内蒙古 15 浙江 33 湖北 42 四川 51 青海 63 辽宁 21 安徽 34 湖南 43 贵州 52 宁夏 64 新 疆 65 台湾 71 香港 81 澳门 82 国外 91   
        *18位身份证标准在国家质量技术监督局于1999年7月1日实施的GB11643-1999《公民身份号码》中做了明确规定。
        *GB11643-1999《公民身份号码》为GB11643-1989《社会保障号码》的修订版，其中指出将原标准名称“社会保障号码”更名为“公民身份号码”，另外GB11643-1999《公民身份号码》从实施之日起代替GB11643-1989。
        *公民身份号码是特征组合码，由十七位数字本体码和一位校验码组成。排列顺序从左至右依次为：六位数字地址码，八位数字出生日期码，三位数字顺序码和一位校验码。其含义如下：
        *1. 地址码：表示编码对象常住户口所在县(市、旗、区)的行政区划代码，按GB/T2260的规定执行。
        *2. 出生日期码：表示编码对象出生的年、月、日，按GB/T7408的规定执行，年、月、日分别用4位、2位、2位数字表示，之间不用分隔符。
        *3. 顺序码：表示在同一地址码所标识的区域范围内，对同年、同月、同日出生的人编定的顺序号，顺序码的奇数分配给男性，偶数分配给女性。
        *校验的计算方式：
        *1. 对前17位数字本体码加权求和
        *公式为：S = Sum(Ai * Wi), i = 0, ... , 16
        *其中Ai表示第i位置上的身份证号码数字值，Wi表示第i位置上的加权因子，其各位对应的值依次为：
        *7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2
        *2. 以11对计算结果取模
        *Y = mod(S, 11)
        *3. 根据模的值得到对应的校验码对应关系为：
        *Y值： 0 1 2 3 4 5 6 7 8 9 10
        *校验码： 1 0 X 9 8 7 6 5 4 3 2
        */

        public static bool CheckIDCard(string id)
        {
            if (id.Length == 18)
            {
                bool check = CheckIDCard18(id);
                return check;
            }
            else if (id.Length == 15)
            {
                bool check = CheckIDCard15(id);
                return check;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckIDCard18(string id)
        {
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return false;
            }
            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }
            string[] arrVerifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVerifyCode[y] != id.Substring(17,1).ToLower())
            {
                return false;
            }
            return true;
        }

        private static bool CheckIDCard15(string id)
        {
            long n = 0;
            if (long.TryParse(id, out n) == false || n < Math.Pow(10,14))
            {
                return false;
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return false;
            }
            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }
            return true;
        }
    }
}
