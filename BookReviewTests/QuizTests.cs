using BookReviews;
using Xunit;

namespace BookReviewTests
{
    public class QuizTests
    {
        [Fact]
        public void CheckAnswerRightTest()
        {
            // arrange: 
            var set = Quiz.GenerateQuestionSet();
            // set all the answers to the right answer
            foreach(var answer in set)
            {
                answer.UserAnswer = answer.Answer;
            }

            // act
            Quiz.CheckAnswers(set);

            // assert
            bool result = true;  // assume all the answers were right
            // change result if ANY answer is wrong
            foreach(var answer in set)
            {   
                result = result && (answer.IsRight ?? false);  // count null as false
            }
            Assert.True(result);
        }

        [Fact]
        public void CheckAnswerWrongTest()
        {
            // arrange: 
            var set = Quiz.GenerateQuestionSet();
            // set all the answers to the wrong answer
            foreach (var answer in set)
            {
                answer.UserAnswer = "Something random";
            }

            // act
            Quiz.CheckAnswers(set);

            // assert
            bool result = false;  // assume all the answers were wrong
            // change result if ANY answer is right
            foreach (var answer in set)
            {
                result = result || (answer.IsRight ?? false);  // count null as false
            }
            Assert.False(result);
        }
    }
}
