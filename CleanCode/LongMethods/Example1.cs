using System;
using System.Web;

namespace CleanCode.LongMethods
{
    public partial class Download : System.Web.UI.Page
    {
        private readonly DataTableToCsvMapper _dataTableToCsvMapper = new DataTableToCsvMapper();
        private readonly TableReader _tableReader = new TableReader();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClearResponse();

            SetCacheability();

            WriteContentToResponse(GetCsv());
        }

        private byte[] GetCsv()
        {
            var map = _dataTableToCsvMapper.Map(_tableReader.GetDataTable("FooFoo"));
            byte[] byteArray = map.ToArray();
            map.Flush();
            map.Close();
            return byteArray;
        }

        private void WriteContentToResponse(byte[] byteArray)
        {
            Response.Charset = System.Text.Encoding.UTF8.WebName;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/comma-separated-values";
            Response.AddHeader("Content-Disposition", "attachment; filename=FooFoo.csv");
            Response.AddHeader("Content-Length", byteArray.Length.ToString());
            Response.BinaryWrite(byteArray);
        }

        private void SetCacheability()
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.CacheControl = "private";
            Response.AppendHeader("Pragma", "cache");
            Response.AppendHeader("Expires", "60");
        }

        private void ClearResponse()
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();
        }
    }
}