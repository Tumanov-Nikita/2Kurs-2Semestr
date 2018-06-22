using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;

namespace FabricService.ImplementationsList
{
    public class StorageServiceList : IStorageService
    {
        private DataListSingleton source;

        public StorageServiceList()
        {
            source = DataListSingleton.GetExample();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<StoragePartsViewModel> StockComponents = new List<StoragePartsViewModel>();
                for (int j = 0; j < source.StorageParts.Count; ++j)
                {
                    if (source.StorageParts[j].StorageId == source.Storages[i].Id)
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
                        StockComponents.Add(new StoragePartsViewModel
                        {
                            Id = source.StorageParts[j].Id,
                            StorageId = source.StorageParts[j].StorageId,
                            PartId = source.StorageParts[j].PartId,
                            PartName = componentName,
                            Amount = source.StorageParts[j].Amount
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageParts = StockComponents
                });
            }
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                // требуется дополнительно получить список компонентов на складе и их количество
                List<StoragePartsViewModel> StockComponents = new List<StoragePartsViewModel>();
                for (int j = 0; j < source.StorageParts.Count; ++j)
                {
                    if (source.StorageParts[j].StorageId == source.Storages[i].Id)
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
                        StockComponents.Add(new StoragePartsViewModel
                        {
                            Id = source.StorageParts[j].Id,
                            StorageId = source.StorageParts[j].StorageId,
                            PartId = source.StorageParts[j].PartId,
                            PartName = componentName,
                            Amount = source.StorageParts[j].Amount
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageParts = StockComponents
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(StorageBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName && 
                    source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            // при удалении удаляем все записи о компонентах на удаляемом складе
            for (int i = 0; i < source.StorageParts.Count; ++i)
            {
                if (source.StorageParts[i].StorageId == id)
                {
                    source.StorageParts.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
