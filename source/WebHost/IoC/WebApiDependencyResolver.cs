using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebHost.IoC
{
    public class WebApiDependencyResolver
        : IDependencyResolver
    {
        IKernel _Kernel;

        public WebApiDependencyResolver(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            _Kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependencyScope(_Kernel.BeginBlock());
        }

        public object GetService(Type serviceType)
        {
            return _Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _Kernel = null;
                GC.SuppressFinalize(this);
            }
        }
    }

    public class WebApiDependencyScope
        : IDependencyScope
    {
        IResolutionRoot _Resolver;

        public WebApiDependencyScope(IResolutionRoot resolver)
        {
            _Resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (_Resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed.");
            }

            return _Resolver.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_Resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed.");
            }

            return _Resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _Resolver = null;
                GC.SuppressFinalize(this);
            }
        }
    }
}