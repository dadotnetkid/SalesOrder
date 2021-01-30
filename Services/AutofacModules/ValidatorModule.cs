using Autofac;
using FluentValidation;
using Services.Interfaces;
using Services.Validators;
using Services.VM;

namespace Services.AutofacModules
{
    public static class ValidatorModule
    {
        public static ContainerBuilder ValidatorServices(this ContainerBuilder builder)
        {
            builder.RegisterType<RegisterViewModelValidator>().As<IValidator<RegisterViewModel>>();
            builder.RegisterType<SalesOrderDetailsValidator>().As<IValidator<SalesOrderDetailVM>>();
            builder.RegisterType<ChequeServices>().As<IChequeService>();
            return builder;
        }
    }
}
