﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricService.ViewModels;
using FabricService.BindingModels;

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
