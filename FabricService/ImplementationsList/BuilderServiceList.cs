using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;

namespace FabricService.ImplementationsList
{
    public class BuilderServiceList : IBuilderService
    {
        private DataListSingleton source;

        public BuilderServiceList()
        {
            source = DataListSingleton.GetExample();
        }

        public List<BuilderViewModel> GetList()
        {
            List<BuilderViewModel> result = new List<BuilderViewModel>();
            for (int i = 0; i < source.Builders.Count; ++i)
            {
                result.Add(new BuilderViewModel
                {
                    Id = source.Builders[i].Id,
                    BuilderFIO = source.Builders[i].BuilderFIO
                });
            }
            return result;
        }

        public BuilderViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Builders.Count; ++i)
            {
                if (source.Builders[i].Id == id)
                {
                    return new BuilderViewModel
                    {
                        Id = source.Builders[i].Id,
                        BuilderFIO = source.Builders[i].BuilderFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BuilderBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Builders.Count; ++i)
            {
                if (source.Builders[i].Id > maxId)
                {
                    maxId = source.Builders[i].Id;
                }
                if (source.Builders[i].BuilderFIO == model.BuilderFIO)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Builders.Add(new Builder
            {
                Id = maxId + 1,
                BuilderFIO = model.BuilderFIO
            });
        }

        public void UpdElement(BuilderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Builders.Count; ++i)
            {
                if (source.Builders[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Builders[i].BuilderFIO == model.BuilderFIO && 
                    source.Builders[i].Id != model.Id)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Builders[index].BuilderFIO = model.BuilderFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Builders.Count; ++i)
            {
                if (source.Builders[i].Id == id)
                {
                    source.Builders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
