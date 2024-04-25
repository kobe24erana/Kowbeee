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
    public partial class SubjectScheduleEntry : Form
    {
        public SubjectScheduleEntry()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\PC\\OneDrive\\Documents\\ERANA.accdb";

            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string sql = "SELECT * FROM SUBJECTFILE1";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(sql, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);

            DataSet thisDataSet = new DataSet();
            thisAdapter.Fill(thisDataSet, "SubjectFile1");

            DataRow thisRow = thisDataSet.Tables["SubjectFile1"].NewRow();
            thisRow["SSESUBJECTEDPCODE"] = EDPCodeTextBox.Text;
            thisRow["SSESUBJECTCODE"] = SubjectCodeTextBox.Text;
            thisRow["SSEDESCRIPTION"] = DescriptionLabel.Text;
            thisRow["SSETTS"] = TimeStartTextBox.Text;
            thisRow["SSEAMPM"] = AMPMComboBox.Text;
            thisRow["SSETE"] = TimeEndTextBox.Text;
            thisRow["SSEDAYS"] = DaysTextBox.Text;
            thisRow["SSESECTION"] = SectionTextBox.Text;
            thisRow["SSEROOM"] = RoomTextBox.Text;
            thisRow["SSESCHOOLYEAR"] = SchoolYearTextBox.Text;

            thisDataSet.Tables["SubjectFile1"].Rows.Add(thisRow);
            thisAdapter.Update(thisDataSet, "SubjectFile1");

            MessageBox.Show("Information Save!!!");

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
       