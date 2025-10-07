using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Quiz_Game
{
    public partial class FormQuiz : Form
    {
        private List<Question> allQuestions;
        private List<Question> quizQuestions;
        private int currentIndex = 0;
        private int score = 0;
        private int timeLeft;
        private int customTimer;
        private Timer quizTimer = new Timer();

        public FormQuiz(string difficulty, List<string> topics, bool timer, bool neg_mark)
        {
            InitializeComponent();
            LoadQuestions();

            if (difficulty == "Easy")
                customTimer = 20;
            else if (difficulty == "Medium")
                customTimer = 20;
            else
                customTimer = 20;

            quizQuestions = allQuestions.Where(q => topics.Contains(q.Topic) && q.Difficulty == difficulty).ToList();

            timeLeft = customTimer;
            quizTimer.Interval = 1000;
            quizTimer.Tick += QuizTimer_Tick;

            LoadNextQuestion();
        }
        private void LoadQuestions()
        {
            string json = File.ReadAllText("questions.json");
            allQuestions = JsonConvert.DeserializeObject<List<Question>>(json);
        }

        private void LoadNextQuestion()
        {
            if (currentIndex >= quizQuestions.Count)
            {
                quizTimer.Stop();
                MessageBox.Show($"Quiz Finished!\nYour Score: {score}/{quizQuestions.Count}");
                this.Close();
                return;
            }

            var q = quizQuestions[currentIndex];
            label1.Text = q.Text;
            button1.Text = q.Options[0];
            button2.Text = q.Options[1];
            button3.Text = q.Options[2];
            button4.Text = q.Options[3];

            timeLeft = customTimer;
            label3.Text = timeLeft.ToString();
            quizTimer.Start();
        }

        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            label3.Text = timeLeft.ToString();
            if (timeLeft <= 0)
            {
                quizTimer.Stop();
                currentIndex++;
                LoadNextQuestion();
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var correctAnswer = quizQuestions[currentIndex].Answer;

            if (btn.Text == correctAnswer)
                score++;

            currentIndex++;
            LoadNextQuestion();
        }
    }
}
