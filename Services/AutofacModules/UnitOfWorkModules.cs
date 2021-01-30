using Autofac;
using Data.Models;
using Services.Interfaces;
using Services.Repository;

namespace Services.AutofacModules
{
    static class UnitOfWorkModules
    {
        public static ContainerBuilder UnitOfWokService(this ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>();
            builder.RegisterType<GenericRepository<SalesOrders>>().As<IGenericRepository<SalesOrders>>();
            builder.RegisterType<GenericRepository<SalesOrderDetails>>().As<IGenericRepository<SalesOrderDetails>>();
            builder.RegisterType<GenericRepository<SalesOrderPayments>>().As < IGenericRepository<SalesOrderPayments>>();
            builder.RegisterType<GenericRepository<Cheques>>().As<IGenericRepository<Cheques>>();
            builder.RegisterType<GenericRepository<Inventory>>().As<IGenericRepository<Inventory>>();
            builder.RegisterType<GenericRepository<TransformationMaps>>().As<IGenericRepository<TransformationMaps>>();
            return builder;
        }
    }
}
