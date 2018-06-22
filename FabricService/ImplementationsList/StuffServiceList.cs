using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;

namespace FabricService.ImplementationsList
{
    public class StuffServiceList : IStuffService
    {
        private DataListSingleton source;

        public StuffServiceList()
        {
            source = DataListSingleton.GetExample();
        }

        public List<StuffViewModel> GetList()
        {
            List<StuffViewModel> result = new List<StuffViewModel>();
            for (int i = 0; i < source.Stuffs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<StuffPartsViewModel> productComponents = new List<StuffPartsViewModel>();
                for (int j = 0; j < source.StuffParts.Count; ++j)
                {
                    if (source.StuffParts[j].StuffId == source.Stuffs[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.StuffParts[j].PartId == source.Parts[k].Id)
                            {
                                componentName = source.Parts[k].PartName;
                                break;
                            }
                        }
                        productComponents.Add(new StuffPartsViewModel
                        {
                            Id = source.StuffParts[j].Id,
                            StuffId = source.StuffParts[j].StuffId,
                            PartId = source.StuffParts[j].PartId,
                            PartName = componentName,
                            Amount = source.StuffParts[j].Amount
                        });
                    }
                }
                result.Add(new StuffViewModel
                {
                    Id = source.Stuffs[i].Id,
                    StuffName = source.Stuffs[i].StuffName,
                    Cost = source.Stuffs[i].Cost,
                    StuffParts = productComponents
                });
            }
            return result;
        }

        public StuffViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Stuffs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<StuffPartsViewModel> productComponents = new List<StuffPartsViewModel>();
                for (int j = 0; j < source.StuffParts.Count; ++j)
                {
                    if (source.StuffParts[j].StuffId == source.Stuffs[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.StuffParts[j].PartId == source.Parts[k].Id)
                            {
                                componentName = source.Parts[k].PartName;
                                break;
                            }
                        }
                        productComponents.Add(new StuffPartsViewModel
                        {
                            Id = source.StuffParts[j].Id,
                            StuffId = source.StuffParts[j].StuffId,
                            PartId = source.StuffParts[j].PartId,
                            PartName = componentName,
                            Amount = source.StuffParts[j].Amount
                        });
                    }
                }
                if (source.Stuffs[i].Id == id)
                {
                    return new StuffViewModel
                    {
                        Id = source.Stuffs[i].Id,
                        StuffName = source.Stuffs[i].StuffName,
                        Cost = source.Stuffs[i].Cost,
                        StuffParts = productComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(StuffBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Stuffs.Count; ++i)
            {
                if (source.Stuffs[i].Id > maxId)
                {
                    maxId = source.Stuffs[i].Id;
                }
                if (source.Stuffs[i].StuffName == model.StuffName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Stuffs.Add(new Stuff
            {
                Id = maxId + 1,
                StuffName = model.StuffName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.StuffParts.Count; ++i)
            {
                if (source.StuffParts[i].Id > maxPCId)
                {
                    maxPCId = source.StuffParts[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.StuffParts.Count; ++i)
            {
                for (int j = 1; j < model.StuffParts.Count; ++j)
                {
                    if(model.StuffParts[i].PartId ==
                        model.StuffParts[j].PartId)
                    {
                        model.StuffParts[i].Amount +=
                            model.StuffParts[j].Amount;
                        model.StuffParts.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.StuffParts.Count; ++i)
            {
                source.StuffParts.Add(new StuffParts
                {
                    Id = ++maxPCId,
                    StuffId = maxId + 1,
                    PartId = model.StuffParts[i].PartId,
                    Amount = model.StuffParts[i].Amount
                });
            }
        }

        public void UpdElement(StuffBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Stuffs.Count; ++i)
            {
                if (source.Stuffs[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Stuffs[i].StuffName == model.StuffName && 
                    source.Stuffs[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Stuffs[index].StuffName = model.StuffName;
            source.Stuffs[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.StuffParts.Count; ++i)
            {
                if (source.StuffParts[i].Id > maxPCId)
                {
                    maxPCId = source.StuffParts[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.StuffParts.Count; ++i)
            {
                if (source.StuffParts[i].StuffId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.StuffParts.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.StuffParts[i].Id == model.StuffParts[j].Id)
                        {
                            source.StuffParts[i].Amount = model.StuffParts[j].Amount;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if(flag)
                    {
                        source.StuffParts.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for(int i = 0; i < model.StuffParts.Count; ++i)
            {
                if(model.StuffParts[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.StuffParts.Count; ++j)
                    {
                        if (source.StuffParts[j].StuffId == model.Id &&
                            source.StuffParts[j].PartId == model.StuffParts[i].PartId)
                        {
                            source.StuffParts[j].Amount += model.StuffParts[i].Amount;
                            model.StuffParts[i].Id = source.StuffParts[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.StuffParts[i].Id == 0)
                    {
                        source.StuffParts.Add(new StuffParts
                        {
                            Id = ++maxPCId,
                            StuffId = model.Id,
                            PartId = model.StuffParts[i].PartId,
                            Amount = model.StuffParts[i].Amount
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.StuffParts.Count; ++i)
            {
                if (source.StuffParts[i].StuffId == id)
                {
                    source.StuffParts.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Stuffs.Count; ++i)
            {
                if (source.Stuffs[i].Id == id)
                {
                    source.Stuffs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
