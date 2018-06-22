using Fabric;
using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.ImplementationsList
{
    public class GeneralServiceList : IGeneralService
    {
        private DataListSingleton source;

        public GeneralServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<BookingViewModel> GetList()
        {
            List<BookingViewModel> result = source.Bookings
                .Select(rec => new BookingViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    StuffId = rec.StuffId,
                    ExecuterId = rec.ExecuterId,
                    DateBegin = rec.DateBegin.ToLongDateString(),
                    DateBuilt = rec.DateBuilt?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Cost = rec.Cost,
                    CustomerFIO = source.Customers
                                    .FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                    StuffName = source.Stuffs
                                    .FirstOrDefault(recP => recP.Id == rec.StuffId)?.StuffName,
                    ExecuterName = source.Executers
                                    .FirstOrDefault(recI => recI.Id == rec.ExecuterId)?.ExecuterFIO
                })
                .ToList();
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            int maxId = source.Bookings.Count > 0 ? source.Bookings.Max(rec => rec.Id) : 0;
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                StuffId = model.StuffId,
                DateBegin = DateTime.Now,
                Count = model.Count,
                Cost = model.Cost,
                Status = BookingStatus.Принят
            });
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            var articleParts = source.StuffParts.Where(rec => rec.StuffId == element.StuffId);
            foreach (var articlePart in articleParts)
            {
                int countOnStorages = source.StorageParts
                                            .Where(rec => rec.PartId == articlePart.PartId)
                                            .Sum(rec => rec.Count);
                if (countOnStorages < articlePart.Count * element.Count)
                {
                    var partName = source.Parts
                                    .FirstOrDefault(rec => rec.Id == articlePart.PartId);
                    throw new Exception("Не достаточно детали " + partName?.PartName +
                        " требуется " + articlePart.Count + ", в наличии " + countOnStorages);
                }
            }
            foreach (var articlePart in articleParts)
            {
                int countOnStorages = articlePart.Count * element.Count;
                var stockParts = source.StorageParts
                                            .Where(rec => rec.PartId == articlePart.PartId);
                foreach (var stockPart in stockParts)
                {
                    if (stockPart.Count >= countOnStorages)
                    {
                        stockPart.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= stockPart.Count;
                        stockPart.Count = 0;
                    }
                }
            }
            element.ExecuterId = model.ExecuterId;
            element.DateBuilt = DateTime.Now;
            element.Status = BookingStatus.Выполняется;
        }

        public void FinishBooking(int id)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BookingStatus.Готов;
        }

        public void PayBooking(int id)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BookingStatus.Оплачен;
        }

        public void PutPartOnStorage(StoragePartBindingModel model)
        {
            StoragePart element = source.StorageParts
                                                .FirstOrDefault(rec => rec.StorageId == model.StorageId &&
                                                                    rec.PartId == model.PartId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageParts.Count > 0 ? source.StorageParts.Max(rec => rec.Id) : 0;
                source.StorageParts.Add(new StoragePart
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    PartId = model.PartId,
                    Count = model.Count
                });
            }
        }
    }
}
