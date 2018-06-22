using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();

        CustomerViewModel GetElement(int id);

        void AddElement(CustomerBindingModel model);

        void UpdElement(CustomerBindingModel model);

        void DelElement(int id);

    }
}
