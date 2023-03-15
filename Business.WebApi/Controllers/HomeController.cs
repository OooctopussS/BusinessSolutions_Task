using Business.Application.Orders.Commands.CreateOrder;
using Business.Application.Orders.Commands.DeleteOrder;
using Business.Application.Orders.Commands.UpdateOrder;
using Business.Application.Orders.Queries.CheckOrderValid;
using Business.Application.Orders.Queries.FilterOrder;
using Business.Application.Orders.Queries.GetDistinctValues;
using Business.Application.Orders.Queries.GetOrder;
using Business.Application.Orders.Queries.GetOrderList;
using Business.Application.Providers.Queries.GetProviderList;
using Business.Domain;
using Business.WebApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Business.WebApi.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IValidator<OrderVm> _validatorOrderVm;


        public HomeController(IValidator<OrderVm> validatorOrderVm) =>
            _validatorOrderVm = validatorOrderVm;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (Mediator != null)
            {
                var orderList = await Mediator.Send(new GetOrderListQuery());

                var filterList = await Mediator.Send(new GetDistinctValuesQuery());

                var orderFilterVm = new OrderFilterVm()
                {
                    Orders = orderList,
                    DateStart = DateTime.Now.AddMonths(-1),
                    DateEnd = DateTime.Now,
                    Numbers = new MultiSelectList(filterList.Numbers),
                    Providers = new MultiSelectList(filterList.Providers, nameof(Provider.Id), nameof(Provider.Name)),
                    Names = new MultiSelectList(filterList.Names),
                    Units = new MultiSelectList(filterList.Units)
                };

                return View(orderFilterVm);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderFilterVm orderFilterVm)
        {
            if (Mediator != null)
            {
                var filterQuery = new FilterOrderQuery
                {
                    DateStart = orderFilterVm.DateStart,
                    DateEnd = orderFilterVm.DateEnd,
                    NumbersValue = orderFilterVm.NumbersValue,
                    ProvidersValue = orderFilterVm.ProvidersValue,
                    NamesValue = orderFilterVm.NamesValue,
                    UnitsValue = orderFilterVm.UnitsValue
                };

                var orderList = await Mediator.Send(filterQuery);

                var filterList = await Mediator.Send(new GetDistinctValuesQuery());

                orderFilterVm.Orders = orderList;
                orderFilterVm.DateStart = DateTime.Now.AddMonths(-1);
                orderFilterVm.DateEnd = DateTime.Now;
                orderFilterVm.Numbers = new MultiSelectList(filterList.Numbers);
                orderFilterVm.Providers = new MultiSelectList(filterList.Providers, nameof(Provider.Id), nameof(Provider.Name));
                orderFilterVm.Names = new MultiSelectList(filterList.Names);
                orderFilterVm.Units = new MultiSelectList(filterList.Units);

                return View(orderFilterVm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            if (Mediator != null)
            {
                var providerList = await Mediator.Send(new GetProviderListQuery());

                var orderVm = new OrderVm
                {
                    Order = new Order(),

                    Providers = providerList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    })
                };

                if (id == null)
                {
                    // Создание

                    return View(orderVm);
                }
                else
                {
                    // Редактирование

                    var query = new GetOrderQuery()
                    {
                        Id = id.Value
                    };

                    orderVm.Order = await Mediator.Send(query);

                    return View(orderVm);
                }
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(OrderVm orderVm)
        {
            if (Mediator != null)
            {
                
                if (orderVm.Order.OrderItems != null)
                {
                    while (orderVm.Order.OrderItems.Remove(null)) { }
                }

                ModelState.Clear();

                ValidationResult resultOrderVm = await _validatorOrderVm.ValidateAsync(orderVm);


                if (resultOrderVm.IsValid)
                {
                    var query = new CheckOrderValidQuery()
                    {
                        Id = orderVm.Order.Id,
                        Number = orderVm.Order.Number!,
                        ProviderId = orderVm.Order.ProviderId
                    };

                    bool resultOrder = await Mediator.Send(query);

                    if (resultOrder)
                    {
                        if (orderVm.Order.Id == 0)
                        {
                            // Создание

                            var createCommand = new CreateOrderCommand()
                            {
                                Number = orderVm.Order.Number,
                                Date = orderVm.Order.Date,
                                ProviderId = orderVm.Order.ProviderId,
                                OrderItems = orderVm.Order.OrderItems
                            };

                            await Mediator.Send(createCommand);
                        }
                        else
                        {
                            // Редактирование

                            var updateCommand = new UpdateOrderCommand()
                            {
                                Id = orderVm.Order.Id,
                                Number = orderVm.Order.Number,
                                Date = orderVm.Order.Date,
                                ProviderId = orderVm.Order.ProviderId,
                                OrderItems = orderVm.Order.OrderItems
                            };

                            await Mediator.Send(updateCommand);
                        }

                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError(nameof(orderVm.Order), "Такой номер заказа с этим поставщиком уже существует");
                }

                resultOrderVm.AddToModelState(ModelState);

                var providerList = await Mediator.Send(new GetProviderListQuery());

                orderVm.Providers = providerList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                return View(orderVm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult OrderItemEditor()
        {
            OrderItem orderItem = new();

            return PartialView("_OrderItemEditor", orderItem);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (Mediator != null)
            {
                var query = new GetOrderQuery()
                {
                    Id = id
                };

                var order = await Mediator.Send(query);

                return View(order);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (Mediator != null)
            {
                var command = new DeleteOrderCommand()
                {
                    Id = id
                };

                await Mediator.Send(command);
            }

            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}