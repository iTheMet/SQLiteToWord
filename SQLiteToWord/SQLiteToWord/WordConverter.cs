using SQLiteToWord.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace SQLiteToWord
{
    internal class WordConverter
    {
        public List<Products> lp { get; set; }

        public int order_id { get; set; }

        public string date { get; set; }
        public WordConverter(List<Products> lp, int order_id, string date) 
        {
            this.lp = lp;
            this.order_id = order_id;
            this.date = date; 
        }

        private string TemplatePath = $@"R:\Документы\GIT\SQLiteToWord\SQLiteToWord\SQLiteToWord\Tovarnyy-chek-blank.docx";

        private string DocksPath = $@"R:\Документы\GIT\SQLiteToWord\SQLiteToWord\SQLiteToWord/docs/";

        public void CreateDocument()
        {
            string time = DateTime.Now.ToOADate().ToString();

            //File.Delete($@"R:\Документы\GIT\SQLiteToWord\SQLiteToWord\SQLiteToWord/WordDoc.docx");          
            File.Copy(TemplatePath, $@"{DocksPath}WordDoc{time}.docx");

            decimal allCoast = 0;

            TableContent tc = new TableContent("WordTable");

            for (int i = 0; i < lp.Count; i++)
            {
                tc.AddRow(
                    new FieldContent("WordOrder", $"{i+1}"),
                    new FieldContent("WordProductName", $"{lp[i].product_name}"),
                    new FieldContent("WordNumber", $"{lp[i].number}"),
                    new FieldContent("WordPriceOfOne", $"{lp[i].price_per_one}"),
                    new FieldContent("WordPriceOfAll", $"{lp[i].number* lp[i].price_per_one}"));
                allCoast += lp[i].number * lp[i].price_per_one;
            }

            var valuesToFill = new Content(

                new FieldContent("WordChequeId", order_id.ToString()),
                new FieldContent("WordChequeDate", date),
                tc,
                new FieldContent("WordPriceOfAllProducts", allCoast.ToString()));

            using (var outputDocument = new TemplateProcessor($@"{DocksPath}WordDoc{time}.docx")
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
            }
        }
    }
}
