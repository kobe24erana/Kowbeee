using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrollment_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\PC\\OneDrive\\Documents\\ERANA.accdb";
        private void SaveButton_Click(object sender, EventArgs e)
        {
            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string sql = "SELECT * FROM SUBJECTFILE";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(sql, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "SubjectFile");

            DataRow thisRow = thisDataSet.Tables["SubjectFile"].NewRow();
            thisRow["SFSUBJCODE"] = SubjectCodeTextBox.Text;
            thisRow["SFSUBJDESC"] = DescriptionTextBox.Text;
            thisRow["SFSUBJUNITS"] = Convert.ToInt16(UnitsTextBox.Text);
            thisRow["SFSUBJCATEGORY"] = CategoryComboBox.Text.Substring(0, 3);
            thisRow["SFSUBJOFRNG"] = Convert.ToUInt16(OfferingComboBox.Text.Substring(0, 1));
            thisRow["SFSSUBJCOUCODE"] = CourseCodeComboBox.Text.Substring(0, 2);
            thisRow["SFSSUBJCURRIYEAR"] = CurriculumYearTextBox.Text;


            thisDataSet.Tables["SubjectFile"].Rows.Add(thisRow);
            thisAdapter.Update(thisDataSet, "SubjectFile");




            MessageBox.Show("Entries Recorded");
        }

        private void RequisiteTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OleDbConnection thisConnection = new OleDbConnection(connectionString);
                thisConnection.Open();
                OleDbCommand thisCommand = thisConnection.CreateCommand();

                string sql = "SELECT * FROM SUBJECTFILE";
                thisCommand.CommandText = sql;

                OleDbDataReader thisDataReader = thisCommand.ExecuteReader();

                bool found = false;
                string subjectCode = "";
                string description = "";
                string units = "";
                string category = "";

                while (thisDataReader.Read())
                {
                    // MessageBox.Show(thisDataReader["SFSUBJCODE"].ToString());
                    if (thisDataReader["SFSUBJCODE"].ToString().Trim().ToUpper() == RequisiteTextBox.Text.Trim().ToUpper())
                    {
                        found = true;
                        subjectCode = thisDataReader["SFSUBJCODE"].ToString();
                        description = thisDataReader["SFSUBJDESC"].ToString();
                        units = thisDataReader["SFSUBJUNITS"].ToString();
                        break;
                        //
                    }

                }

                int index;
                if (found == false)
                    MessageBox.Show("Subject Code Not Found");
                else
                {
                    index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells["SubjectCodeColumn"].Value = subjectCode;
                    dataGridView1.Rows[index].Cells["CategoryColumn"].Value = category;
                    dataGridView1.Rows[index].Cells["DescriptionColumn"].Value = description;
                    dataGridView1.Rows[index].Cells["UnitsColumn"].Value = units;
                }
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            SubjectScheduleEntry subject_Schedule_Entry = new SubjectScheduleEntry();
            subject_Schedule_Entry.ShowDialog();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            SubjectCodeTextBox.Text = " ";
            CurriculumYearTextBox.Text = " ";
            DescriptionTextBox.Text = " ";
            CategoryComboBox.Text = " ";
            OfferingComboBox.Text = " ";
            CourseCodeComboBox.Text = " ";
            UnitsTextBox.Text = " ";
        }
    }
}
    