using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quiz()
        {
            List<QuestionVm> questionSet = BookReviews.Quiz.GenerateQuestionSet();
            return View(questionSet);
        }

        [HttpPost]
        public IActionResult Quiz(List<QuestionVm> answers)
        {
            // Only the user answers get sent from the input form.
            // We need to add the questions and right answers again.
            var questions = BookReviews.Quiz.GenerateQuestionSet();
            for (int i = 0; i < questions.Count; i++)
            {
                questions[i].UserAnswer = answers[i].UserAnswer;
            }
            BookReviews.Quiz.CheckAnswers(questions); 
            return View(questions);
        }
    }
}