using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FabricService.ImplementationsBD
{
	public class ExecuterServiceBD : IExecuterService
	{
		private AlexeysDbContext context;

		public ExecuterServiceBD(AlexeysDbContext context)
		{
			this.context = context;
		}

		public List<ExecuterViewModel> GetList()
		{
			List<ExecuterViewModel> result = context.Executers
				.Select(rec => new ExecuterViewModel
				{
					Id = rec.Id,
					ExecuterFIO = rec.ExecuterFIO
				})
				.ToList();
			return result;
		}

		public ExecuterViewModel GetElement(int id)
		{
			Executer element = context.Executers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new ExecuterViewModel
				{
					Id = element.Id,
					ExecuterFIO = element.ExecuterFIO
				};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(ExecuterBindingModel model)
		{
			Executer element = context.Executers.FirstOrDefault(rec => rec.ExecuterFIO == model.ExecuterFIO);
			if (element != null)
			{
				throw new Exception("Уже есть сотрудник с таким ФИО");
			}
			context.Executers.Add(new Executer
			{
				ExecuterFIO = model.ExecuterFIO
			});
			context.SaveChanges();
		}

		public void UpdElement(ExecuterBindingModel model)
		{
			Executer element = context.Executers.FirstOrDefault(rec =>
										rec.ExecuterFIO == model.ExecuterFIO && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть сотрудник с таким ФИО");
			}
			element = context.Executers.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.ExecuterFIO = model.ExecuterFIO;
			context.SaveChanges();
		}

		public void DelElement(int id)
		{
			Executer element = context.Executers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				context.Executers.Remove(element);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}
