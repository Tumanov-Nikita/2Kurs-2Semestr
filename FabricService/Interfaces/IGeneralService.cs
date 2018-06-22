using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.Interfaces
{
    public interface IGeneralService
    {
        List<ContractViewModel> GetList();

        void CreateContract(ContractBindingModel model);

        void TakeContractInWork(ContractBindingModel model);

        void FinishContract(int id);

        void PayContract(int id);

        void PutPartOnStorage(StoragePartBindingModel model);

    }
}
