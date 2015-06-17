using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite
{
    public class TGridView : GridView
    {
        #region Properties

        public bool ShowEmptyTable 
        {
            get 
            {
                object obj = ViewState["ShowEmptyTable"];
                return (obj != null ? (bool)obj : true);
            }

            set
            {
                ViewState["ShowEmptyTable"] = value;
            }
        }

        #endregion


        private bool _enableEmptyContentRender = true;

        public bool EnableEmptyContentRender
        {
            get { return _enableEmptyContentRender; }
            set { _enableEmptyContentRender = value; }
        }

        private string _emptyDataCellCssClass;

        public string EmptyDataCellCssClass 
        {
            get { return _emptyDataCellCssClass; }
            set { _emptyDataCellCssClass = value; }
        }

        protected virtual void RenderEmptyContent(HtmlTextWriter writer)
        {
            Table t = new Table();
            t.CssClass = this.CssClass;
            t.GridLines = this.GridLines;
            t.BorderStyle = this.BorderStyle;
            t.CellPadding = this.CellPadding;
            t.CellSpacing = this.CellSpacing;
            t.BackColor = this.BackColor;

            t.HorizontalAlign = this.HorizontalAlign;
            t.Width = this.Width;
            t.CopyBaseAttributes(this);

            TableRow row = new TableRow();
            t.Rows.Add(row);
            foreach (DataControlField f in this.Columns)
            {
                TableCell cell = new TableCell();
                cell.Text = f.HeaderText;
                cell.CssClass = "gridview";
                row.Cells.Add(cell);
            }

            TableRow row2 = new TableRow();
            t.Rows.Add(row2);
            TableCell msgCell = new TableCell();
            msgCell.CssClass = this.EmptyDataCellCssClass;
            if (this.EmptyDataTemplate != null)
            {
                this.EmptyDataTemplate.InstantiateIn(msgCell);
            }
            else
            {
                msgCell.Text = this.EmptyDataText;
            }
            msgCell.HorizontalAlign = HorizontalAlign.Center;
            msgCell.ColumnSpan = this.Columns.Count;
            row2.Cells.Add(msgCell);
            t.RenderControl(writer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (_enableEmptyContentRender &&  (this.Rows.Count == 0 || this.Rows[0].RowType == DataControlRowType.EmptyDataRow))
            {
                RenderEmptyContent(writer);
            }
            else
            {
                base.Render(writer);
            }
        }
    }
}