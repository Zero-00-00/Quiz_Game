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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quiz_Game
{
    public partial class Form1 : Form
    {
        bool will_check = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string difficulty = "";
                decimal customTime = 0;
                if (comboBox1.SelectedItem != null)
                {
                    difficulty = comboBox1.SelectedItem.ToString();
                }
                else
                {
                    label1.Text = "Please select at least one topic and a difficulty level";
                    return; // stop execution
                }
                decimal questions = numericUpDown1.Value;
                bool timed = false, neg_mark = false;

                if (checkBox4.Checked) timed = true;
                if (radioButton1.Checked) customTime = numericUpDown2.Value;
                if (checkBox5.Checked) neg_mark = true;

                List<string> selectedTopics = new List<string>();
                if (checkBox8.Checked) selectedTopics.Add("Science");
                if (checkBox7.Checked) selectedTopics.Add("Math");
                if (checkBox6.Checked) selectedTopics.Add("History");
                if (checkBox9.Checked) selectedTopics.Add("GK");
                if (checkBox10.Checked) selectedTopics.Add("Reason");
                if (checkBox11.Checked) selectedTopics.Add("Geo");

                if (selectedTopics.Count == 0)
                {
                    label1.Text = "Please select at least one topic and a difficulty level.";
                }
                else
                {
                    Quiz quizForm = new Quiz(difficulty, selectedTopics, timed, neg_mark, questions, customTime);
                    quizForm.Show();
                    this.Hide();
                    quizForm.FormClosed += quizForm_FormClosed;
                }
            }
            catch (System.NullReferenceException)
            {
                label1.Text = "Please select at least one topic and a difficulty level.";
            }
        }
        private void quizForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            //this.Close();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Visible)
                radioButton1.Visible = false;
            else
                radioButton1.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Visible)
                numericUpDown2.Visible = false;
            else
                numericUpDown2.Visible = true;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (will_check)
            {
                radioButton1.Checked = false;
                will_check = false;
            }
            else
            {
                radioButton1.Checked = true;
                will_check = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
