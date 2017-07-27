namespace Blog_DotNet_NancyFx_Entities_and_Validations.Validators
{

    using FluentValidation;
    using Blog_DotNet_NancyFx_Entities_and_Validations.Entities;

    public class PersonValidator : AbstractValidator<PersonEntity>
    {
        public PersonValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(m => m.Age).GreaterThanOrEqualTo(1).WithMessage("Age should be greater than 0").LessThanOrEqualTo(130).WithMessage("Age should be less than 130");
        }
    }

}