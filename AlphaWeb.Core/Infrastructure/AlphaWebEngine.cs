using System;
using System.Collections.Generic;
using System.Linq;
using AlphaWeb.Core.Common;
using AlphaWeb.Core.Configuration;
using AlphaWeb.Core.Infrastructure.DependencyManagement;
using AlphaWeb.Core.Interfaces.Infrastructure;
using AlphaWeb.Core.Interfaces.Mapping;
using Autofac;
using AutoMapper;


namespace AlphaWeb.Core.Infrastructure
{
    public class AlphaWebEngine : IEngine
    {

        #region Fields

        private ContainerManager _containerManager;
        private IAlphaWebFileProvider _fileProvider;
        private IRequestLifetimeScopeManager _requestLifetimeScopeManager;
        #endregion
        #region Methods

        /// <summary>
        /// Initialize components and plugins in the AlphaWeb environment.
        /// </summary>
        /// <param name="config">Config</param>
        /// <param name="fileProvider"></param>
        public void Initialize(AlphaWebConfig config, IRequestLifetimeScopeManager requestLifetimeScopeManager)
        {
            _requestLifetimeScopeManager = requestLifetimeScopeManager;
            //TODO GETSON find a better approach
            _fileProvider = new AlphaWebFileProvider(AppContext.BaseDirectory, AppContext.BaseDirectory);
            //register dependencies
            RegisterDependencies(config);

            //register mapper configurations
            RegisterMapperConfiguration(config);
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Container manager
        /// </summary>
        public virtual ContainerManager ContainerManager => _containerManager;

        #endregion
        #region Utilities

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterDependencies(AlphaWebConfig config)
        {
            var builder = new ContainerBuilder();

            //dependencies
            var typeFinder = new WebAppTypeFinder(_fileProvider);
            builder.RegisterInstance(config).As<AlphaWebConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //register dependencies provided by other assemblies
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder, config);

            var container = builder.Build();
            _containerManager = new ContainerManager(container,_requestLifetimeScopeManager);
        }

        /// <summary>
        /// Register mapping
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterMapperConfiguration(AlphaWebConfig config)
        {
            //dependencies
            var typeFinder = new WebAppTypeFinder(_fileProvider);

            //register mapper configurations provided by other assemblies
            var mcTypes = typeFinder.FindClassesOfType<IMapperProfile>();
            var mpInstances = new List<IMapperProfile>();
            foreach (var mcType in mcTypes)
                mpInstances.Add((IMapperProfile)Activator.CreateInstance(mcType));
            //sort
            mpInstances = mpInstances.AsQueryable().OrderBy(t => t.Order).ToList();

            //create AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                foreach (var instance in mpInstances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            //register
            AutoMapperConfiguration.Init(mapperConfig);
        }

        #endregion
    }
}
