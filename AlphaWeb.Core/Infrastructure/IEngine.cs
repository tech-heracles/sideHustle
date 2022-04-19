using System;
using AlphaWeb.Core.Configuration;
using AlphaWeb.Core.Infrastructure.DependencyManagement;
using AlphaWeb.Core.Interfaces.Infrastructure;

namespace AlphaWeb.Core.Infrastructure
{
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the various services composing the AlphaWeb engine. 
    /// Edit functionality, modules and implementations access most AlphaWeb functionality through this interface.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Container manager
        /// </summary>
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Initialize components and plugins in the AlphaWeb environment.
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize(AlphaWebConfig config, IRequestLifetimeScopeManager requestLifetimeScopeManager);

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();
    }


}
