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
        private Timer quizTimer = new Timer();

        public FormQuiz(string difficulty, List<string> topics, bool timer, bool neg_mark)
        {
            InitializeComponent();
        }
    }
}
