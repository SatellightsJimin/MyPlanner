using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPlanner
{
    public partial class MyPlanner : Form
    {
        int cur_method=0;
        SQLManager sqlM;
        public MyPlanner()
        {    
            sqlM = new SQLManager();
            InitializeComponent();
            impBoxRight.Items.Add("상");
            impBoxRight.Items.Add("중");
            impBoxRight.Items.Add("하");
            impBoxLeft.Items.Add("날짜 내림차순");
            impBoxLeft.Items.Add("날짜 오름차순");
            impBoxLeft.Items.Add("중요도 내림차순");
            impBoxLeft.Items.Add("중요도 오름차순");
            UpdateDatagridView(cur_method);      
        }

        private void UpdateDatagridView(int method)
        {
            DataSet ds = sqlM.SortResult(method);
            dataGridView1.DataSource = ds.Tables[0];

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (dataGridView1.Columns[i].Name == "Id")
                {
                    dataGridView1.Columns[i].Visible = false;
                }
                if (dataGridView1.Columns[i].Name == "Date")
                {
                    dataGridView1.Columns[i].HeaderText = "날짜";
                    dataGridView1.Columns[i].FillWeight = 20;
                }
                if (dataGridView1.Columns[i].Name == "Subject")
                {
                    dataGridView1.Columns[i].HeaderText = "제목";
                    dataGridView1.Columns[i].FillWeight = 30;
                }
                if (dataGridView1.Columns[i].Name == "Important")
                {
                    dataGridView1.Columns[i].HeaderText = "중요도";
                    dataGridView1.Columns[i].FillWeight = 10;
                }

            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            TimePicker.Format = DateTimePickerFormat.Time;
            TimePicker.ShowUpDown = true;
        }


        private void SortDataGrid(object sender, EventArgs e)
        {
            ComboBox s = (ComboBox)sender;
            cur_method = s.SelectedIndex;
            UpdateDatagridView(cur_method);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(impBoxRight.SelectedItem.ToString().Length==0 
                || planText.Text.ToString().Length == 0 
                || subjectText.Text.ToString().Length == 0)
            {
                MessageBox.Show("일정을 제대로 입력하세요");
            }
            else
            {
                DateTime dateTime = Convert.ToDateTime(DatePicker.Value.ToShortDateString() + " " + TimePicker.Value.ToLongTimeString());
                MessageBox.Show(dateTime.ToString());
                if (sqlM.InsertQuery(subjectText.Text.ToString(), planText.Text.ToString(), dateTime, impBoxRight.SelectedItem.ToString()) == true)
                {
                    UpdateDatagridView(cur_method);
                }
            }
        }

        private void ShowPlan(object sender, DataGridViewCellEventArgs e)
        {

            ViewPlan();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewPlan();
        }

        private void ViewPlan()
        {
            int id;
            string subject;
            string important;
            string plan;
            DateTime date;


            id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[0].Value);
            date = Convert.ToDateTime(dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[1].Value);
            subject = dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[2].Value.ToString();
            important = dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[3].Value.ToString();
            plan = sqlM.getPlan(id);

            new PlanView(id, subject, important, date, plan).Show();
        }
    }
}
