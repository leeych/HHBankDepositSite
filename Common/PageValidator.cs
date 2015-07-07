using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public class PageValidator
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]*[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        private static Regex RegDigitAlpha = new Regex("^[a-zA-Z0-9]+$");
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|tv|info)$");
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

        public PageValidator() { }

        #region 数字字符串检查

        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的健值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(inputKey))
            {
                retVal = req.QueryString[inputKey];
                if (retVal == null)
                {
                    retVal = req.Form[inputKey];
                }
                if (retVal != null)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                    {
                        retVal = string.Empty;
                    }
                }
            }
            if (retVal == null)
            {
                retVal = string.Empty;
            }
            return retVal;
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否数字字符串（可带正负号）
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否浮点数
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否浮点数（可带正负号）
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 验证数字和字母字符串
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsDigitAlpha(string inputData)
        {
            Match m = RegDigitAlpha.Match(inputData);
            return m.Success;
        }


        #region 中文检测

        /// <summary>
        /// 是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是邮件地址
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #region 其他

        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns></returns>
        public static string SqlText(string sqlInput, int maxLen)
        {
            sqlInput = sqlInput.Trim();
            if (sqlInput.Length > maxLen)
            {
                sqlInput = sqlInput.Substring(0, maxLen);
            }
            return sqlInput;
        }

        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }

        #endregion

        #endregion

        #endregion
    }
}
