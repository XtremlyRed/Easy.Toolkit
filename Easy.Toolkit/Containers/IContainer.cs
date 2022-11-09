using System;

namespace Easy.Toolkit
{
    #region Public interfaces
    /// <summary>
    /// Represents a scope in which per-scope objects are instantiated a single time
    /// </summary>
    public interface IContainer : IDisposable, IServiceProvider
    {
    }


    #endregion
}