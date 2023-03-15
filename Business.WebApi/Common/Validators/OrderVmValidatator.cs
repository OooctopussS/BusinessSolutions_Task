#nullable disable
using Business.Domain;
using Business.WebApi.Models;
using FluentValidation;

namespace Business.WebApi.Common.Validators
{
    public class OrderVmValidatator : AbstractValidator<OrderVm>
    {
        public OrderVmValidatator()
        {
            RuleFor(o => o.Order.Number)
                .NotEmpty()
                .WithMessage("Это поле обязательноe");

            RuleFor(o => o.Order.Date)
                .Must(BeAValidDate)
                .WithMessage("Некорректная дата");


            RuleFor(o => o.Order)
                .Must(BeAValidNumberWithName)
                .WithMessage("Имя элемента заказа не может быть таким же как и номер заказа");

            RuleForEach(o => o.Order.OrderItems).SetValidator(new OrderItemValidatator());
        }

        private bool BeAValidDate(DateTime? date)
        {
            return !date.Equals(default);
        }

        private bool BeAValidNumberWithName(Order order)
        {
            if (order.OrderItems != null)
            {
                foreach (var item in order.OrderItems)
                {
                    if (item != null && item.Name != null)
                    {
                        if (order.Number == item.Name)
                        {
                            return false;
                        }
                    }
                }
            }
            
            return true;
            
        }
    }

    public class OrderItemValidatator : AbstractValidator<OrderItem>
    {
        public OrderItemValidatator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage("Это поле обязательноe");

            RuleFor(i => i.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Значение не должно быть отрицательным");

            RuleFor(i => i.Quantity)
                .NotNull()
                .WithMessage("Некорректное значение");

            RuleFor(i => i.Unit)
                .NotEmpty()
                .WithMessage("Это поле обязательноe");
        }
    }
}
