using Quesify.AnswerService.API.Models;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Questions;
using Quesify.AnswerService.Core.Aggregates.Votes;

namespace Quesify.AnswerService.API.Mappers;

public class AnswerMapper
{
    public static CreateAnswerResponse Map(Answer answer, CreateAnswerResponse response)
    {
        response.Id = answer.Id;
        response.QuestionId = answer.QuestionId;
        response.Body = answer.Body;
        return response;
    }

    public static CreateVoteResponse Map(Vote vote, Answer answer, CreateVoteResponse response)
    {
        response.AnswerId = answer.Id;
        response.QuestionId = answer.QuestionId;
        response.VoteType = vote.VoteType;
        response.NewAnswerScore = answer.Score;
        return response;
    }
}
