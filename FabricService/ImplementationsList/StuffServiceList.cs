using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.ImplementationsList
{
    public class StuffServiceList : IStuffService
    {
        private DataListSingleton source;

        public StuffServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StuffViewModel> GetList()
        {
            List<StuffViewModel> result = source.Stuffs
                .Select(rec => new StuffViewModel
                {
                    Id = rec.Id,
                    StuffName = rec.StuffName,
                    Cost = rec.Cost,
                    StuffParts = source.StuffParts
                            .Where(recPC => recPC.StuffId == rec.Id)
                            .Select(recPC => new StuffPartViewModel
                            {
                                Id = recPC.Id,
                                StuffId = recPC.StuffId,
                                PartId = recPC.PartId,
                                PartName = source.Parts
                                    .FirstOrDefault(recC => recC.Id == recPC.PartId)?.PartName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public StuffViewModel GetElement(int id)
        {
            Stuff element = source.Stuffs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StuffViewModel
                {
                    Id = element.Id,
                    StuffName = element.StuffName,
                    Cost = element.Cost,
                    StuffParts = source.StuffParts
                            .Where(recPC => recPC.StuffId == element.Id)
                            .Select(recPC => new StuffPartViewModel
                            {
                                Id = recPC.Id,
                                StuffId = recPC.StuffId,
                                PartId = recPC.PartId,
                                PartName = source.Parts
                                        .FirstOrDefault(recC => recC.Id == recPC.PartId)?.PartName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StuffBindingModel model)
        {
            Stuff element = source.Stuffs.FirstOrDefault(rec => rec.StuffName == model.StuffName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Stuffs.Count > 0 ? source.Stuffs.Max(rec => rec.Id) : 0;
            source.Stuffs.Add(new Stuff
            {
                Id = maxId + 1,
                StuffName = model.StuffName,
                Cost = model.Cost
            });
            int maxPCId = source.StuffParts.Count > 0 ?
                                    source.StuffParts.Max(rec => rec.Id) : 0;
            var groupParts = model.StuffParts
                                        .GroupBy(rec => rec.PartId)
                                        .Select(rec => new
                                        {
                                            PartId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupPart in groupParts)
            {
                source.StuffParts.Add(new StuffPart
                {
                    Id = ++maxPCId,
                    StuffId = maxId + 1,
                    PartId = groupPart.PartId,
                    Count = groupPart.Count
                });
            }
        }

        public void UpdElement(StuffBindingModel model)
        {
            Stuff element = source.Stuffs.FirstOrDefault(rec =>
                                        rec.StuffName == model.StuffName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Stuffs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StuffName = model.StuffName;
            element.Cost = model.Cost;

            int maxPCId = source.StuffParts.Count > 0 ? source.StuffParts.Max(rec => rec.Id) : 0;
            var compIds = model.StuffParts.Select(rec => rec.PartId).Distinct();
            var updateParts = source.StuffParts
                                            .Where(rec => rec.StuffId == model.Id &&
                                           compIds.Contains(rec.PartId));
            foreach (var updatePart in updateParts)
            {
                updatePart.Count = model.StuffParts
                                                .FirstOrDefault(rec => rec.Id == updatePart.Id).Count;
            }
            source.StuffParts.RemoveAll(rec => rec.StuffId == model.Id &&
                                       !compIds.Contains(rec.PartId));
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
                StuffPart elementPC = source.StuffParts
                                        .FirstOrDefault(rec => rec.StuffId == model.Id &&
                                                        rec.PartId == groupPart.PartId);
                if (elementPC != null)
                {
                    elementPC.Count += groupPart.Count;
                }
                else
                {
                    source.StuffParts.Add(new StuffPart
                    {
                        Id = ++maxPCId,
                        StuffId = model.Id,
                        PartId = groupPart.PartId,
                        Count = groupPart.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Stuff element = source.Stuffs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.StuffParts.RemoveAll(rec => rec.StuffId == id);
                source.Stuffs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }

}
