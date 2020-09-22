using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Paie_Calcul
{
    public partial class Form3 : Form
    {
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Paie ; integrated security = false; User ID = S_Paie; Password = S_Paie0823;");
        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Paie ; integrated security = true; ");*/


        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            SqlCommand cmd = new SqlCommand(" select distinct Matricule,left(The_Date,10)[The_Date],left(Temps_Sortie,5)[Temps_Sortie]  from Paie_total where Id_Table = '" + textBox1.Text + "' ", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), textBox2.Text);
            }
            cn.Close();
            MessageBox.Show(dataGridView1.Rows.Count.ToString(), "Nbr de Lignes");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportTOExcel(dataGridView1);
        }

        void ExportTOExcel(DataGridView dataGridView1)
        {


            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //add data 
            int StartCol = 1;
            int StartRow = 1;
            int j = 0, i = 0;

            //Write Headers
            for (j = 0; j < dataGridView1.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView1.Columns[j].HeaderText;
            }

            StartRow++;

            //Write datagridview content
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    try
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    }
                    catch
                    {
                        ;
                    }
                }
            }

            xlApp.Visible = true;

        }

        private void précedentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "") { textBox2.Enabled = true; }
            else { textBox2.Enabled = false; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "") { button1.Enabled = true; }
            else { button1.Enabled = false; }
        }

        /*private void txt1_form3(object sender, EventArgs e)
        {
            //Textbox
            if (textBox1.Text != "") { textBox2.Enabled = true; }
            else { textBox2.Enabled = false; }
            if (textBox1.Text != "" && textBox2.Text != "") { button1.Enabled = true; }
            else { button1.Enabled = false; }


            cn.Open();
            SqlCommand cmd = new SqlCommand("select Id_Table from Paie_total", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            AutoCompleteStringCollection autodata = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                autodata.Add(dr.GetString(0));
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = autodata;
            cn.Close();
        }*/

    }
}
