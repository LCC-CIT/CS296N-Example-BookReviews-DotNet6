using BookReviews.Models;

namespace BookReviews
{
    public static class Quiz
    {
        // Use these for answers to true/false questions.
        // QuestionVM.Answer isn't bool because it gets used for other types too.
        public const string TRUE = "true";
        public const string FALSE = "false";

        public static List<QuestionVm> GenerateQuestionSet()
        {
            var set = new List<QuestionVm>();

            set.Add(new QuestionVm
            {
                Type = QuestionType.ShortAnswer,
                Question = "Who wrote Les Misarable?",
                Answer = "Victor Hugo"
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.Numeric,
                Question = "What year was Charles Dickens born?",
                Answer = "1812"
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.TrueFalse,
                Question = "The term Orwellian is a reference to the author Orson Wells.",
                Answer = FALSE
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.Numeric,
                Question = "How many volumes make up The Lord of the Rings by J.R.R. Tolkien?",
                Answer = "3"
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.TrueFalse,
                Question = "War and Peace, by Leo Tolstoy, is over 1300 pages long.",
                Answer = TRUE
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.ShortAnswer,
                Question = "Hamlet, the play by William Shakespere, is set in what country?",
                Answer = "Denmark"
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.ShortAnswer,
                Question = "Who is the headmaster of Hogwarts in the Harry Potter series",
                Answer = "Albus Dumbledore"
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.ShortAnswer,
                Question = "The main character of Great Expectations, by Charles Dickens is",
                Answer = "Pip"
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.TrueFalse,
                Question = "Katniss Everdeen is the main character of Twighlight",
                Answer = FALSE
            });

            set.Add(new QuestionVm
            {
                Type = QuestionType.TrueFalse,
                Question = "The first book of the Bible is Genesis",
                Answer = TRUE
            });

            return set;
        }


        public static void CheckAnswers(List<QuestionVm> answers)
        {
            foreach (QuestionVm set in answers)
            {
                set.IsRight = set.Answer == set.UserAnswer;
            }
        }
    }
}
