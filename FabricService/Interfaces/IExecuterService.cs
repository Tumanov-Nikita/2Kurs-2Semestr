using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
