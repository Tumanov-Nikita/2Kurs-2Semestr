using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricService.BindingModels;
using FabricService.ViewModels;

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
