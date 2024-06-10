using Quesify.Web.Enums;

namespace Quesify.Web.Models.Questions.CreateVoteForQuestionModels.Requests;

public class CreateVoteForQuestionRequest
{
    public VoteType VoteType { get; set; }
}
