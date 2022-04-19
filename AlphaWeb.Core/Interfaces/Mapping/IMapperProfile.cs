using System;
using AutoMapper;

namespace AlphaWeb.Core.Interfaces.Mapping
{
    /// <summary>
    /// Mapper configuration registrar interface
    /// </summary>
    public interface IMapperProfile
    {
        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        int Order { get; }
    }
}
