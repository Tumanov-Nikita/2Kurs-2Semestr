using FabricService.Attributies;
using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveStuffPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов с количество компонент на них")]
        List<StoragesLoadViewModel> GetStoragesLoad();

        [CustomMethod("Метод сохранения списка списка складов с количество компонент на них в xls-файл")]
        void SaveStoragesLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<CustomerBookingsModel> GetCustomerBookings(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveCustomerBookings(ReportBindingModel model);
    }
}
