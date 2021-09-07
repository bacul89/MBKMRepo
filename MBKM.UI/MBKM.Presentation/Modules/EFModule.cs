using System;
using System.Collections.Generic;
using System.Data.Entity;
using Autofac;
using MBKM.Common.Interfaces;
using MBKM.Repository.BaseRepository;

namespace MBKM.Presentation.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(MBKMContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

        }
    }
}