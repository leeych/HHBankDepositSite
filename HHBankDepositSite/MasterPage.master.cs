using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HHBankDepositSite
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                NavigationMenu.Visible = false;
                NavigationTreeView.Visible = true;
            }
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
                NavigationMenu.DynamicMenuStyle.Width = Unit.Pixel(120);
            }

            if (!IsPostBack)
            {
                AddEventHandlers();
                //AddPageContent();
            }
        }

        private void AddEventHandlers()
        {
            NavigationMenu.MenuItemDataBound += new MenuEventHandler(NavigationMenu_MenuItemDataBound);
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
        }

        private void AddPageContent()
        {
            string pageName = HttpContext.Current.Request.Url.AbsolutePath.Substring(
                HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/") + 1);
            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
            Label label = new Label();
            label.Text = " <br /> Content for page: " + pageName;
            contentPlaceHolder.Controls.Add(label);
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            SiteMapNode tempNode = currentNode;
            tempNode = ReplaceNodeText(tempNode);
            return currentNode;
        }

        internal SiteMapNode ReplaceNodeText(SiteMapNode smn)
        {
            if (smn != null && smn.Title.Contains("<u>"))
            {
                smn.Title = smn.Title.Replace("<u>", "").Replace("</u>", "");
            }

            if (smn.ParentNode != null)
            {
                if (smn.ParentNode.Title.Contains("<u>"))
                {
                    SiteMapNode gpn = smn.ParentNode;
                    smn.ParentNode.Title = smn.ParentNode.Title.Replace("<u>", "").Replace("</u>", "");
                    smn = ReplaceNodeText(gpn);
                }
            }
            return smn;
        }

        protected void NavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            SiteMapNode node = (SiteMapNode)e.Item.DataItem;
            if (node["target"] != null)
            {
                e.Item.Target = node["target"];
            }
            if (node["accesskey"] != null)
            {
                CreateAccessKeyButton(node["accesskey"] as string, node.Url);
            }
        }

        private void CreateAccessKeyButton(string ak, string url)
        {
            HtmlButton inputBtn = new HtmlButton();
            inputBtn.Style.Add("width", "1px");
            inputBtn.Style.Add("height", "1px");
            inputBtn.Style.Add("position", "absolute");
            inputBtn.Style.Add("left", "-255px");
            inputBtn.Style.Add("z-index", "-1");
            inputBtn.Attributes.Add("type", "button");
            inputBtn.Attributes.Add("value", "");
            inputBtn.Attributes.Add("accesskey", ak);
            inputBtn.Attributes.Add("onclick", "navigateTo('" + url + "'):");
            AccessKeyPanel.Controls.Add(inputBtn);
        }
    }
}