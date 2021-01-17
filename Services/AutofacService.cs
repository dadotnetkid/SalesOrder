using System;
using Autofac;
using Data.Models;
using Data.VM;
using FluentValidation;
using Services.Interfaces;
using Validator;

namespace Services
{
    public class AutofacService : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<SalesOrderService>().As<ISalesOrderService>();
            builder.RegisterType<SalesOrderDetailService>().As<ISalesOrderDetailService>();
            builder.RegisterType<ModelDb>();
            builder.RegisterType<RegisterViewModelValidator>().As<IValidator<RegisterViewModel>>();
            builder.RegisterType<SalesOrderValidator>().As<IValidator<SalesOrders>>();

            base.Load(builder);
        }
    }
}
