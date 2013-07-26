﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Logging;
using ScriptCs.Package;

namespace ScriptCs
{
    public interface IInitializationContainerFactory
    {
        IContainer Container { get; }
    }

    public class InitializationContainerFactory : ScriptContainerFactory, IInitializationContainerFactory
    {
        public InitializationContainerFactory(ILog logger, IDictionary<Type, object> overrides)
            : base(logger, overrides)
        {
        }

        protected override IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<ILog>(_logger);
            RegisterOverrideOrDefault<IFileSystem>(builder, b => b.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance());
            RegisterOverrideOrDefault<IAssemblyUtility>(builder, b => b.RegisterType<AssemblyUtility>().As<IAssemblyUtility>().SingleInstance());
            RegisterOverrideOrDefault<IPackageContainer>(builder, b => b.RegisterType<PackageContainer>().As<IPackageContainer>().SingleInstance());
            RegisterOverrideOrDefault<IPackageAssemblyResolver>(builder, b => b.RegisterType<PackageAssemblyResolver>().As<IPackageAssemblyResolver>().SingleInstance());
            RegisterOverrideOrDefault<IAssemblyResolver>(builder, b => b.RegisterType<AssemblyResolver>().As<IAssemblyResolver>().SingleInstance());
            return builder.Build();
        }
    }
}
