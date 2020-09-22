using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Paie_Calcul
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"data source = 192.168.1.156 ; initial catalog = Paie ; integrated security = false; User ID = S_Paie; Password = S_Paie0823;");
        /*SqlConnection cn = new SqlConnection(@"data source = 192.168.1.207 ; initial catalog = Paie ; integrated security = true; ");*/


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Excel Files | *.xlsx; *.xls;";

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)

                { this.textBox1.Text = ofd.FileName; }


                string path = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox1.Text + ";Extended Properties = \"Excel 12.0; HDR=YES\" ; ";
                OleDbConnection cn = new OleDbConnection(path);
                cn.Open();

                comboBox1.DataSource = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                comboBox1.DisplayMember = "TABLE_NAME";
                comboBox1.ValueMember = "TABLE_NAME";

                cn.Close();

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                string path = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox1.Text + ";Extended Properties = \"Excel 12.0; HDR=YES\" ; ";
                OleDbConnection cn = new OleDbConnection(path);
                OleDbDataAdapter dta = new OleDbDataAdapter("Select * from [" + comboBox1.SelectedValue + "]", cn);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                MessageBox.Show(dt.Rows.Count.ToString(), "Nbr de Lignes");


                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.DataSource = dt;
                }

            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 0; i++)
            {
                SqlCommand cmd = new SqlCommand("insert into Paie_initial (Id_Table,Matricule,FullName,The_Date,Entree_01,Sortie_01,Entree_02,Sortie_02,Entree_03,Sortie_03,Original_Time_Minutes) values(@Id_Table,@Matricule,@FullName,@The_Date,@Entree_01,@Sortie_01,@Entree_02,@Sortie_02,@Entree_03,@Sortie_03,@Original_Time_Minutes) ", cn);
                cmd.Parameters.AddWithValue("@Id_Table", textBox2.Text);
                cmd.Parameters.AddWithValue("@Matricule", dataGridView1.Rows[i].Cells[0].Value.ToString());
                cmd.Parameters.AddWithValue("@FullName", dataGridView1.Rows[i].Cells[1].Value.ToString());
                cmd.Parameters.AddWithValue("@The_Date", dataGridView1.Rows[i].Cells[2].Value);
                cmd.Parameters.AddWithValue("@Entree_01", dataGridView1.Rows[i].Cells[3].Value);
                cmd.Parameters.AddWithValue("@Sortie_01", dataGridView1.Rows[i].Cells[4].Value);
                cmd.Parameters.AddWithValue("@Entree_02", dataGridView1.Rows[i].Cells[5].Value);
                cmd.Parameters.AddWithValue("@Sortie_02", dataGridView1.Rows[i].Cells[6].Value);
                cmd.Parameters.AddWithValue("@Entree_03", dataGridView1.Rows[i].Cells[7].Value);
                cmd.Parameters.AddWithValue("@Sortie_03", dataGridView1.Rows[i].Cells[8].Value);
                cmd.Parameters.AddWithValue("@Original_Time_Minutes", dataGridView1.Rows[i].Cells[9].Value);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            MessageBox.Show("Sauvegarde à réussi! ", "Saving", MessageBoxButtons.OK);
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            dataGridView1.DataSource = dt;
            dt.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
        }

        private void calculer8HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void calculer7HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt1_form1(object sender, EventArgs e)
        {
            if (textBox2.Text != "") { button3.Enabled = true; }
            else { button3.Enabled = false; }
        }

        private void calculer4HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }

        private void exporterExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }
    }
}
