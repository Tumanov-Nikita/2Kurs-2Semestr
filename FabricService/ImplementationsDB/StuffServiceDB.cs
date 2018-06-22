using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FabricService.ImplementationsBD
{
    public class StuffServiceBD : IStuffService
	{
		private FabricDbContext context;

		public StuffServiceBD(FabricDbContext context)
		{
			this.context = context;
		}

		public List<StuffViewModel> GetList()
		{
			List<StuffViewModel> result = context.Stuffs
				.Select(rec => new StuffViewModel
				{
					Id = rec.Id,
					StuffName = rec.StuffName,
					Cost = rec.Cost,
					StuffParts = context.StuffParts
							.Where(recPC => recPC.StuffId == rec.Id)
							.Select(recPC => new StuffPartViewModel
							{
								Id = recPC.Id,
								StuffId = recPC.StuffId,
								PartId = recPC.PartId,
								PartName = recPC.Part.PartName,
								Count = recPC.Count
							})
							.ToList()
				})
				.ToList();
			return result;
		}

		public StuffViewModel GetElement(int id)
		{
			Stuff element = context.Stuffs.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new StuffViewModel
				{
					Id = element.Id,
					StuffName = element.StuffName,
					Cost = element.Cost,
					StuffParts = context.StuffParts
							.Where(recPC => recPC.StuffId == element.Id)
							.Select(recPC => new StuffPartViewModel
							{
								Id = recPC.Id,
								StuffId = recPC.StuffId,
								PartId = recPC.PartId,
								PartName = recPC.Part.PartName,
								Count = recPC.Count
							})
							.ToList()
				};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(StuffBindingModel model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Stuff element = context.Stuffs.FirstOrDefault(rec => rec.StuffName == model.StuffName);
					if (element != null)
					{
						throw new Exception("Уже есть изделие с таким названием");
					}
					element = new Stuff
					{
						StuffName = model.StuffName,
						Cost = model.Cost
					};
					context.Stuffs.Add(element);
					context.SaveChanges();
					// убираем дубли по компонентам
					var groupParts = model.StuffParts
												.GroupBy(rec => rec.PartId)
												.Select(rec => new
												{
													PartId = rec.Key,
													Count = rec.Sum(r => r.Count)
												});
					// добавляем компоненты
					foreach (var groupPart in groupParts)
					{
						context.StuffParts.Add(new StuffPart
						{
							StuffId = element.Id,
							PartId = groupPart.PartId,
							Count = groupPart.Count
						});
						context.SaveChanges();
					}
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}

		public void UpdElement(StuffBindingModel model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Stuff element = context.Stuffs.FirstOrDefault(rec =>
										rec.StuffName == model.StuffName && rec.Id != model.Id);
					if (element != null)
					{
						throw new Exception("Уже есть изделие с таким названием");
					}
					element = context.Stuffs.FirstOrDefault(rec => rec.Id == model.Id);
					if (element == null)
					{
						throw new Exception("Элемент не найден");
					}
					element.StuffName = model.StuffName;
					element.Cost = model.Cost;
					context.SaveChanges();

					// обновляем существуюущие компоненты
					var compIds = model.StuffParts.Select(rec => rec.PartId).Distinct();
					var updateParts = context.StuffParts
													.Where(rec => rec.StuffId == model.Id &&
														compIds.Contains(rec.PartId));
					foreach (var updatePart in updateParts)
					{
						updatePart.Count = model.StuffParts
														.FirstOrDefault(rec => rec.Id == updatePart.Id).Count;
					}
					context.SaveChanges();
					context.StuffParts.RemoveRange(
										context.StuffParts.Where(rec => rec.StuffId == model.Id &&
																			!compIds.Contains(rec.PartId)));
					context.SaveChanges();
					// новые записи
					var groupParts = model.StuffParts
												.Where(rec => rec.Id == 0)
												.GroupBy(rec => rec.PartId)
												.Select(rec => new
												{
													PartId = rec.Key,
													Count = rec.Sum(r => r.Count)
												});
					foreach (var groupPart in groupParts)
					{
						StuffPart elementPC = context.StuffParts
												.FirstOrDefault(rec => rec.StuffId == model.Id &&
																rec.PartId == groupPart.PartId);
						if (elementPC != null)
						{
							elementPC.Count += groupPart.Count;
							context.SaveChanges();
						}
						else
						{
							context.StuffParts.Add(new StuffPart
							{
								StuffId = model.Id,
								PartId = groupPart.PartId,
								Count = groupPart.Count
							});
							context.SaveChanges();
						}
					}
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}

		public void DelElement(int id)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Stuff element = context.Stuffs.FirstOrDefault(rec => rec.Id == id);
					if (element != null)
					{
						// удаяем записи по компонентам при удалении изделия
						context.StuffParts.RemoveRange(
											context.StuffParts.Where(rec => rec.StuffId == id));
						context.Stuffs.Remove(element);
						context.SaveChanges();
					}
					else
					{
						throw new Exception("Элемент не найден");
					}
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}
	}
}
