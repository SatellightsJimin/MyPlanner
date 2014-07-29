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

        public PlanView(int id, string subject, string important, DateTime date, string plan)
        {
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
    }
}
