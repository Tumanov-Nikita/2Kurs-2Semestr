using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
{
    public interface IReportService
    {
        void SaveStuffPrice(ReportBindingModel model);

        List<StoragesLoadViewModel> GetStoragesLoad();

        void SaveStoragesLoad(ReportBindingModel model);

        List<CustomerBookingsModel> GetCustomerBookings(ReportBindingModel model);

        void SaveCustomerBookings(ReportBindingModel model);
    }
}
