using Api.Contracts.V1.Requests;
using FluentValidation;

namespace Api.Validators
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9]*$");
        }
    }
}
