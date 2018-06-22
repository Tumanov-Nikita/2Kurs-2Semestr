using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
{
    public interface IStorageService
    {
        List<StorageViewModel> GetList();

        StorageViewModel GetElement(int id);

        void AddElement(StorageBindingModel model);

        void UpdElement(StorageBindingModel model);

        void DelElement(int id);
    }
}
