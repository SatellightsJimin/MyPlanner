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
    public partial class PlanView : Form
    {
        private int id;
        private DateTime date;
        private string subject;
        private string important;
        private string plan;
        private SQLManager sqlM;
        private MyPlanner myPlanner;
        public PlanView(int id, string subject, string important, DateTime date, string plan, MyPlanner myPlanner)
        {
            sqlM = new SQLManager();
            this.myPlanner = myPlanner;
            this.id = id;
            this.date = date;
            this.subject = subject;
            this.important = important;
            this.plan = plan;
            InitializeComponent();
        }


        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlanView_Load(object sender, EventArgs e)
        {
            subjectBox.Text = subject;
            dateBox.Text = date.ToString();
            impBox.Text = important;
            planBox.Text = plan;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("\'" + subject.Trim() + "\' 계획을 삭제합니까?",
                      "경고", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    if (sqlM.deletePlan(id))
                    {
                        MessageBox.Show("정삭 삭제 되었습니다");
                        myPlanner.UpdateDatagridView(myPlanner.cur_method);
                        myPlanner.Focus();
                        this.Close();
                    }
                    break;
                case DialogResult.No:
                    break;
            }
        }
    }
}
