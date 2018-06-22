using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
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
