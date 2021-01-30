using Autofac;
using Data.Models;
using FluentValidation;
using Services.Interfaces;
using Services.Validators;
using Services.VM;

namespace Services.AutofacModules
{
    public class AutofacService : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<SalesOrderService>().As<ISalesOrderService>();
            builder.RegisterType<SalesOrderDetailService>().As<ISalesOrderDetailService>();
            builder.RegisterType<SalesOrderPaymentService>().As<ISalesOrderPaymentService>();
            builder.RegisterType<InventoryService>().As<IInventoryService>();
            builder.RegisterType<TransformMapService>().As<ITransformMapService>();
            builder.RegisterType<ModelDb>();
            builder.RegisterType<SalesOrderDetailVM>().AsSelf();



            //Load all the repository
            builder.UnitOfWokService();
            //Load all the validator
            builder.ValidatorServices();

            base.Load(builder);
        }




    }
}
