using Quesify.QuestionService.API.Models;
using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;

namespace Quesify.QuestionService.API.Mappers;

public class QuestionMapper
{
    public static CreateQuestionResponse Map(Question question, CreateQuestionResponse response)
    {
        response.Id = question.Id;
        response.Title = question.Title;
        response.Body = question.Body;
        return response;
    }

    public static CreateVoteResponse Map(Vote vote, Question question, CreateVoteResponse response)
    {
        response.QuestionId = vote.QuestionId;
        response.VoteType = vote.VoteType;
        response.NewQuestionScore = question.Score;
        return response;
    }
}
