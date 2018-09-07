using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IStuffService
    {
        List<StuffViewModel> GetList();

        StuffViewModel GetElement(int id);

        void AddElement(StuffBindingModel model);

        void UpdElement(StuffBindingModel model);

        void DelElement(int id);
    }
}
