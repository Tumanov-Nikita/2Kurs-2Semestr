using System;
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
        List<BookingViewModel> GetList();

        void CreateBooking(BookingBindingModel model);

        void TakeBookingInWork(BookingBindingModel model);

        void FinishBooking(int id);

        void PayBooking(int id);

        void PutPartOnStorage(StoragePartBindingModel model);

    }
}
