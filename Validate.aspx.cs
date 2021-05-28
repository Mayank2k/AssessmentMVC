using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AssessmentMVC
{
    public partial class Validate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string com = "Select * from School where active=1 Order by SchoolName";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddlSchools.Items.Clear();
                ddlSchools.Items.Add(new ListItem("--Select School--", ""));
                ddlSchools.DataSource = dt;
                ddlSchools.DataTextField = "SchoolName";
                ddlSchools.DataValueField = "SchoolID";
                ddlSchools.DataBind();
                con.Close();
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(this.FileUpload1.PostedFile.FileName);
                string extension = Path.GetExtension(this.FileUpload1.PostedFile.FileName);
                string text = ConfigurationManager.AppSettings["FolderPath"];
                string text2 = base.Server.MapPath(text + fileName);
                string path = base.Server.MapPath(text);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                this.FileUpload1.SaveAs(text2);
                this.GetExcelSheets(text2, extension.ToLower(), "Yes");
            }
        }

        private void GetExcelSheets(string FilePath, string Extension, string isHDR)
        {
            string text = "";
            if (!(Extension == ".xls"))
            {
                if (Extension == ".xlsx")
                {
                    text = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                }
            }
            else
            {
                text = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
            }
            text = string.Format(text, FilePath, isHDR);
            OleDbConnection oleDbConnection = new OleDbConnection(text);
            OleDbCommand arg_4C_0 = new OleDbCommand();
            new OleDbDataAdapter();
            arg_4C_0.Connection = oleDbConnection;
            oleDbConnection.Open();
            this.ddlSheets.Items.Clear();
            this.ddlSheets.Items.Add(new ListItem("--Select Sheet--", ""));

            //this.ddlSheets.DataSource = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            DataTable schemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            foreach (DataRow row in schemaTable.Rows)
            {
                string sheetName = row["TABLE_NAME"].ToString();
                if (!sheetName.EndsWith("$") && !sheetName.EndsWith("$'"))
                    continue;
                this.ddlSheets.Items.Add(new ListItem(sheetName, sheetName));
            }

            this.ddlSheets.DataTextField = "TABLE_NAME";
            this.ddlSheets.DataValueField = "TABLE_NAME";
            this.ddlSheets.DataBind();
            oleDbConnection.Close();
            this.lblFileName.Text = Path.GetFileName(FilePath);
            this.Panel2.Visible = true;
            this.Panel1.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlSchools.SelectedValue) || string.IsNullOrEmpty(ddlSheets.SelectedValue))
            {
                this.lblMessage.ForeColor = Color.Red;
                this.lblMessage.Text = "No school selected";
                this.btnDownload.Visible = true;
                return;
            }
            string text = this.lblFileName.Text;
            Path.GetExtension(text);
            string str = base.Server.MapPath(ConfigurationManager.AppSettings["FolderPath"]);
            string commandText = "sp_ValidateDataFromExcel";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = commandText;
            sqlCommand.Parameters.Add("@SheetName", SqlDbType.VarChar).Value = this.ddlSheets.SelectedItem.Text;
            sqlCommand.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = str + text;
            sqlCommand.Parameters.Add("@SchoolID", SqlDbType.Int).Value = this.ddlSchools.SelectedValue; 
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandTimeout = 0;
            try
            {
                sqlConnection.Open();
                object obj = sqlCommand.ExecuteNonQuery();
                this.btnDownload.Visible = false;
                this.lblMessage.ForeColor = Color.Green;                
                this.lblMessage.Text = obj.ToString() + " records validated.";
            }
            catch (Exception ex)
            {
                this.lblMessage.ForeColor = Color.Red;
                this.lblMessage.Text = ex.Message;
                this.btnDownload.Visible = true;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                this.Panel1.Visible = true;
                this.Panel2.Visible = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM TempData"))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        using (DataTable dataTable = new DataTable())
                        {
                            sqlDataAdapter.Fill(dataTable);
                            using (XLWorkbook xLWorkbook = new XLWorkbook())
                            {
                                xLWorkbook.Worksheets.Add(dataTable, this.ddlSheets.SelectedItem.Text.Replace("'", "").Replace("$", ""));
                                base.Response.Clear();
                                base.Response.Buffer = true;
                                base.Response.Charset = "";
                                base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                base.Response.AddHeader("content-disposition", "attachment;filename=" + this.lblFileName.Text.Replace(".xlsx", "") + "_Error.xlsx");
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    xLWorkbook.SaveAs(memoryStream);
                                    memoryStream.WriteTo(base.Response.OutputStream);
                                    base.Response.Flush();
                                    base.Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}