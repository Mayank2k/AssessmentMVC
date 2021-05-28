using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AssessmentMVC
{
    public partial class Convert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            if (this.FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(this.FileUpload1.PostedFile.FileName);
                Path.GetExtension(this.FileUpload1.PostedFile.FileName);
                string text = ConfigurationManager.AppSettings["FolderPath"];
                string text2 = base.Server.MapPath(text + fileName);
                string path = base.Server.MapPath(text);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                this.FileUpload1.SaveAs(text2);
                this.dosomething(text2);
            }
        }

        public void Save(DataTable dt, string filepath)
        {
            string fileName = Path.GetFileName(filepath);
            using (XLWorkbook xLWorkbook = new XLWorkbook())
            {
                xLWorkbook.Worksheets.Add(dt, "Data");
                base.Response.Clear();
                base.Response.Buffer = true;
                base.Response.Charset = "";
                base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                base.Response.AddHeader("content-disposition", "attachment;filename=" + fileName.Replace(".docx", ".xlsx"));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    xLWorkbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(base.Response.OutputStream);
                    base.Response.Flush();
                    base.Response.End();
                }
            }
        }

        public void dosomething(string filepath)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("QuestionID");
            dataTable.Columns.Add("QuestionText");
            dataTable.Columns.Add("QuestionImage");
            dataTable.Columns.Add("AnswerOption1Text");
            dataTable.Columns.Add("AnswerOption1Image");
            dataTable.Columns.Add("AnswerOption2Text");
            dataTable.Columns.Add("AnswerOption2Image");
            dataTable.Columns.Add("AnswerOption3Text");
            dataTable.Columns.Add("AnswerOption3Image");
            dataTable.Columns.Add("AnswerOption4Text");
            dataTable.Columns.Add("AnswerOption4Image");
            dataTable.Columns.Add("CorrectAnswerOption");
            dataTable.Columns.Add("Explanationforcorrectoption");
            dataTable.Columns.Add("IncorrectAnswerOption1");
            dataTable.Columns.Add("Categoryofmisconception1");
            dataTable.Columns.Add("Explanationforincorrectoption1");
            dataTable.Columns.Add("IncorrectAnswerOption2");
            dataTable.Columns.Add("Categoryofmisconception2");
            dataTable.Columns.Add("Explanationforincorrectoption2");
            dataTable.Columns.Add("IncorrectAnswerOption3");
            dataTable.Columns.Add("Categoryofmisconception3");
            dataTable.Columns.Add("Explanationforincorrectoption3");
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filepath, true))
            {
                string str = "";
                try
                {
                    foreach (DocumentFormat.OpenXml.Wordprocessing.Table current in wordprocessingDocument.MainDocumentPart.Document.Body.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= current.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>().Count<DocumentFormat.OpenXml.Wordprocessing.TableRow>() - 1; i++)
                        {
                            DocumentFormat.OpenXml.Wordprocessing.TableCell tableCell = current.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>().ElementAt(i).Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>().ElementAt(1);
                            dataRow[i] = tableCell.InnerText;
                            if (i == 0)
                            {
                                str = tableCell.InnerText;
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
                catch (Exception ex)
                {
                    this.lblMessage.ForeColor = System.Drawing.Color.Red;
                    this.lblMessage.Text = "Error on Question Code: " + str + ".\n Detail Error: " + ex.Message;
                }
            }
            if (this.lblMessage.Text.Length == 0)
            {
                this.Save(dataTable, filepath);
            }
        }
    }
}