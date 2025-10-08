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
            else if (difficulty == "Hard")
                customTimer = 10;
            else
                customTimer = 15;

            if (difficulty == "All")
                quizQuestions = allQuestions.Where(q => topics.Contains(q.Topic)).ToList();
            else
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

            Shuffle(allQuestions); // Shuffle after loading
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]); // Swap
            }
        }

        private void LoadNextQuestion()
        {
            if (currentIndex >= quizQuestions.Count)
            {
                result_box();
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

        private void result_box()
        {
            quizTimer.Stop();
            MessageBox.Show($"Quiz Finished!\nYour Score: {score}/{quizQuestions.Count}");
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            result_box();
        }
    }
}
