using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractShopService.ImplementationsList
{
    public class ExecuterServiceList : IExecuterService
    {
        private DataListSingleton source;

        public ExecuterServiceList()
        {
            source = DataListSingleton.GetExample();
        }

        public List<ExecuterViewModel> GetList()
        {
            List<ExecuterViewModel> result = new List<ExecuterViewModel>();
            for (int i = 0; i < source.Executers.Count; ++i)
            {
                result.Add(new ExecuterViewModel
                {
                    Id = source.Executers[i].Id,
                    ExecuterFIO = source.Executers[i].ExecuterFIO
                });
            }
            return result;
        }

        public ExecuterViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Executers.Count; ++i)
            {
                if (source.Executers[i].Id == id)
                {
                    return new ExecuterViewModel
                    {
                        Id = source.Executers[i].Id,
                        ExecuterFIO = source.Executers[i].ExecuterFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ExecuterBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Executers.Count; ++i)
            {
                if (source.Executers[i].Id > maxId)
                {
                    maxId = source.Executers[i].Id;
                }
                if (source.Executers[i].ExecuterFIO == model.ExecuterFIO)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Executers.Add(new Executer
            {
                Id = maxId + 1,
                ExecuterFIO = model.ExecuterFIO
            });
        }

        public void UpdElement(ExecuterBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Executers.Count; ++i)
            {
                if (source.Executers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Executers[i].ExecuterFIO == model.ExecuterFIO && 
                    source.Executers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Executers[index].ExecuterFIO = model.ExecuterFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Executers.Count; ++i)
            {
                if (source.Executers[i].Id == id)
                {
                    source.Executers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
