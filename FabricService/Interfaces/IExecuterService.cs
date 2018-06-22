using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
{
    public interface IBuilderService
    {
        List<BuilderViewModel> GetList();

        BuilderViewModel GetElement(int id);

        void AddElement(BuilderBindingModel model);

        void UpdElement(BuilderBindingModel model);

        void DelElement(int id);
    }
}
