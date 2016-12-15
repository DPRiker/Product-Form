using DAL.Abstract;
using DAL.Concrete;
using DAL.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
	
		public class ServiceModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IRepository<Product>>().To<LiteDbRepository<Product>>();
				Bind<IRepository<Category>>().To<LiteDbRepository<Category>>();
			}
		}
	}

