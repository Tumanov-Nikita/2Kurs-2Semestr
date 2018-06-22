using FabricService.Attributies;
using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IGeneralService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<BookingViewModel> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateBooking(BookingBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeBookingInWork(BookingBindingModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishBooking(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayBooking(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutPartOnStorage(StoragePartBindingModel model);
    }
}
