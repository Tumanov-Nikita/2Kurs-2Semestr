using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
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
