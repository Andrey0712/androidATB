using FluentValidation;
using WebAtb.Model;

namespace WebAtb.Validators
{
    public class ValidatorCreateCategoryViewModel : AbstractValidator<CreateCategoryViewModel>
    {
        public ValidatorCreateCategoryViewModel()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле ім'я є обов'язковим!")
                .MinimumLength(2).WithMessage("Мінімальна довжина імені 2 символи!");
        }
       /* RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Поле ціна є обов'язковим!")
                .Matches(@"\d{1,20}(\,\d{1,2})?").WithMessage("Формат ціни xx..xx,xx!");*/

    }
}
