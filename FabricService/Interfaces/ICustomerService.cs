using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

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
