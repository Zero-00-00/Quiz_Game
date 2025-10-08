﻿using System;
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
        private int timeLeft = 0, timeLeft_pq;
        private Timer quizTimer = new Timer();
        private bool timer, neg_mark;
        private int ques;

        public FormQuiz(string difficulty, List<string> topics, bool timed, bool neg_markd, decimal questions, decimal customTime)
        {
            InitializeComponent();
            LoadQuestions();
            timer = timed; neg_mark = neg_markd;
            ques = (int)questions;

            if (timer) time(customTime, difficulty);

            if (difficulty == "All")
                quizQuestions = allQuestions.Where(q => topics.Contains(q.Topic)).ToList();
            else
                quizQuestions = allQuestions.Where(q => topics.Contains(q.Topic) && q.Difficulty == difficulty).ToList();

            if (quizQuestions.Count < ques) ques = quizQuestions.Count;
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
            if (currentIndex == (ques))
            {
                result_box();
                return;
            }

            label4.Text = $"Q {currentIndex + 1} / {ques}";

            var q = quizQuestions[currentIndex];
            label1.Text = q.Text;
            button1.Text = q.Options[0];
            button2.Text = q.Options[1];
            button3.Text = q.Options[2];
            button4.Text = q.Options[3];

            if (timer)
            {
                label2.Visible = true;
                label3.Visible = true;
                timeLeft_pq = timeLeft;
                label3.Text = timeLeft_pq.ToString();
                quizTimer.Start();
            }
        }

        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            if (timer)
            {
                timeLeft_pq--;
                label3.Text = timeLeft_pq.ToString();
                if (timeLeft_pq <= 0)
                {
                    quizTimer.Stop();
                    currentIndex++;
                    LoadNextQuestion();
                }
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var correctAnswer = quizQuestions[currentIndex].Answer;

            if (btn.Text != correctAnswer && neg_mark) score--;
            if (btn.Text == correctAnswer) score++;

            currentIndex++;
            LoadNextQuestion();
        }

        private void result_box()
        {
            quizTimer.Stop();
            MessageBox.Show($"Quiz Finished!\nYour Score: {score}/{ques}");
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            result_box();
        }

        private void time(decimal customTime, string difficulty)
        {

            quizTimer.Interval = 1000;
            quizTimer.Tick += QuizTimer_Tick;

            if (customTime == 0)
            {
                if (difficulty == "Easy")
                    timeLeft = 20;
                else if (difficulty == "Hard")
                    timeLeft = 10;
                else
                    timeLeft = 15;
            }
            else 
            { 
                timeLeft = (int)customTime;
            }
        }
    }
}
