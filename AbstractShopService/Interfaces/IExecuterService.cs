using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IExecuterService
    {
        List<ExecuterViewModel> GetList();

        ExecuterViewModel GetElement(int id);

        void AddElement(ExecuterBindingModel model);

        void UpdElement(ExecuterBindingModel model);

        void DelElement(int id);
    }
}
