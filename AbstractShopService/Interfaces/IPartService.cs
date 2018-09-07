using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IPartService
    {
        List<PartViewModel> GetList();

        PartViewModel GetElement(int id);

        void AddElement(PartBindingModel model);

        void UpdElement(PartBindingModel model);

        void DelElement(int id);
    }
}
