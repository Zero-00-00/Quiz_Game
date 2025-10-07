using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Quiz_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string difficulty = comboBox1.SelectedItem.ToString();
            bool timed = false, neg_mark = false;

            if (checkBox4.Checked) timed = true;
            if (checkBox5.Checked) neg_mark = true;

            List<string> selectedTopics = new List<string>();
            if (checkBox8.Checked) selectedTopics.Add("Science");
            if (checkBox7.Checked) selectedTopics.Add("Math");
            if (checkBox6.Checked) selectedTopics.Add("History");
            if (checkBox9.Checked) selectedTopics.Add("GK");
            if (checkBox10.Checked) selectedTopics.Add("Reason");
            if (checkBox11.Checked) selectedTopics.Add("Geo");

            FormQuiz quizForm = new FormQuiz(difficulty, selectedTopics, timed, neg_mark);
            quizForm.Show();
            this.Hide();
        }
    }
}
