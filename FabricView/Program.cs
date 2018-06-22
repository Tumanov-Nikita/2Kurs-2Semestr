using FabricService;
using FabricService.ImplementationsBD;
using FabricService.ImplementationsList;
using FabricService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace FabricView
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var container = BuildUnityContainer();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(container.Resolve<FormGeneral>());
		}

		public static IUnityContainer BuildUnityContainer()
		{
			var currentContainer = new UnityContainer();
			currentContainer.RegisterType<DbContext, FabricDbContext>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ICustomerService, CustomerServiceBD>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IPartService, PartServiceBD>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IExecuterService, ExecuterServiceBD>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IStuffService, StuffServiceBD>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IStorageService, StorageServiceBD>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IGeneralService, GeneralServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
		}
	}
}
