using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Realization;
using BLL.DTOModels;

namespace WebUI.Util
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;
		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			AddBindings();
		}
		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
		private void AddBindings()
		{
			kernel.Bind<IService<ProductDTO>>().To<ProductService>();
		}
	}
}