using FluentValidation;

namespace Application.Cities.Queries.GetCitiesInDecapolis
{
    public class GetCitiesInDecapolisQueryValidator : AbstractValidator<GetCitiesInDecapolisQuery>
    {
        public GetCitiesInDecapolisQueryValidator()
        {
            RuleFor(q => q.Decapolis)
                .NotNull();
        }
    }
}