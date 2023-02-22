using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookReviews.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quiz()
        {
            List<QuestionVM> questionSet = BookReviews.Quiz.GenerateQuestionSet();
            return View(questionSet);
        }

        [HttpPost]
        public IActionResult Quiz(List<QuestionVM> answers)
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