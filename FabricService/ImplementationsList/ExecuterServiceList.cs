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
    public class ExecuterServiceList : IExecuterService
    {
        private DataListSingleton source;

        public ExecuterServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ExecuterViewModel> GetList()
        {
            List<ExecuterViewModel> result = source.Executers
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
            Executer element = source.Executers.FirstOrDefault(rec => rec.Id == id);
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
            Executer element = source.Executers.FirstOrDefault(rec => rec.ExecuterFIO == model.ExecuterFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            int maxId = source.Executers.Count > 0 ? source.Executers.Max(rec => rec.Id) : 0;
            source.Executers.Add(new Executer
            {
                Id = maxId + 1,
                ExecuterFIO = model.ExecuterFIO
            });
        }

        public void UpdElement(ExecuterBindingModel model)
        {
            Executer element = source.Executers.FirstOrDefault(rec =>
                                        rec.ExecuterFIO == model.ExecuterFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = source.Executers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ExecuterFIO = model.ExecuterFIO;
        }

        public void DelElement(int id)
        {
            Executer element = source.Executers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Executers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
