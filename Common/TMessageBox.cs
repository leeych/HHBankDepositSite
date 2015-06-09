using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Common
{
    public class TMessageBox
    {
        private TMessageBox() { }

        /// <summary>
        /// 显示消息对话框
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="msg">提示信息</param>
        public static void Show(Page page, string tag, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), tag, "alert('" + msg + "');", true);
        }

        public static void ShowMsg(Page page, string key, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), key, "<script language='javascript' defer>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// 控件点击消息确认提示框
        /// </summary>
        /// <param name="control">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(WebControl control, string msg)
        {
            control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标url</param>
        public static void ShowAndRedirect(Page page, string msg, string url)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' defer>");
            builder.AppendFormat("alert('{0}');", msg);
            builder.AppendFormat("top.location.href='{0}'", url);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        public static void ShowConfirmAndRedirect(Page page, string tag, string msg, string url)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script lanuage='javascript' defer>");
            builder.AppendFormat("confirm('{0}');", msg);
            builder.AppendFormat("top.location.href='{0}'", url);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), tag, builder.ToString());
        }

        /// <summary>
        /// 显示页面自定义脚本消息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public static void ResponseScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", script, true);
        }


        public static void SetFocus(Control ctrl, Page page)
        {
            string s = "<script language='javascript' defer>document.getElementById('" + ctrl.ID + "').focus() </script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", s);
        }
    }
}
