using Quesify.Web.Enums;

namespace Quesify.Web.Models.Answers.CreateVoteForAnswerModels.Requests;

public class CreateVoteForAnswerRequest
{
    public VoteType VoteType { get; set; }
}
