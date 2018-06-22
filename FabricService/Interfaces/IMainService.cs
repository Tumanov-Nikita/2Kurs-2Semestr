using FabricService.BindingModels;
using FabricService.ViewModels;
using System.Collections.Generic;

namespace FabricService.Interfaces
{
    public interface IMainService
    {
        List<BookingViewModel> GetList();

        void CreateBooking(BookingBindingModel model);

        void TakeBookingInWork(BookingBindingModel model);

        void FinishBooking(int id);

        void PayBooking(int id);

        void PutPartOnStorage(StoragePartsBindingModel model);
    }
}
