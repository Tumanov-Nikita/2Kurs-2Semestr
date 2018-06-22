using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
{
    public interface IReportService
    {
        void SaveArticlePrice(ReportBindingModel model);

        List<StoragesLoadViewModel> GetStoragesLoad();

        void SaveStoragesLoad(ReportBindingModel model);

        List<CustomerContractsModel> GetCustomerContracts(ReportBindingModel model);

        void SaveCustomerContracts(ReportBindingModel model);
    }
}
