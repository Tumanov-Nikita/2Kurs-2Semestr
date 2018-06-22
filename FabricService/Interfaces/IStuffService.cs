using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricService.BindingModels;
using FabricService.ViewModels;

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
