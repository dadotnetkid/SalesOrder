using System;
using Autofac;
using Data.Models;
using FluentValidation;
using Services.Interfaces;
using Services.Validators;
using Services.VM;

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
            builder.RegisterType<SalesOrderDetailVM>().AsSelf();
            builder.RegisterType<RegisterViewModelValidator>().As<IValidator<RegisterViewModel>>();

            builder.RegisterType<SalesOrderDetailsValidator>().As<IValidator<SalesOrderDetailVM>>();
            builder.RegisterType<ChequeServices>().As<IChequeService>();

            base.Load(builder);
        }
    }
}
