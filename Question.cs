using System.Collections.Generic;

namespace QuizGame
{
    public class Question
    {
        public string Topic { get; set; }
        public string Difficulty { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }
    }
}
