using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
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
