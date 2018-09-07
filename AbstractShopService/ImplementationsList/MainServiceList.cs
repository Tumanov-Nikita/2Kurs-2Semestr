using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractShopService.ImplementationsList
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetExample();
        }

        public List<BookingViewModel> GetList()
        {
            List<BookingViewModel> result = new List<BookingViewModel>();
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.Customers.Count; ++j)
                {
                    if(source.Customers[j].Id == source.Bookings[i].CustomerId)
                    {
                        clientFIO = source.Customers[j].CustomerFIO;
                        break;
                    }
                }
                string productName = string.Empty;
                for (int j = 0; j < source.Stuffs.Count; ++j)
                {
                    if (source.Stuffs[j].Id == source.Bookings[i].StuffId)
                    {
                        productName = source.Stuffs[j].StuffName;
                        break;
                    }
                }
                string implementerFIO = string.Empty;
                if(source.Bookings[i].ExecuterId.HasValue)
                {
                    for (int j = 0; j < source.Executers.Count; ++j)
                    {
                        if (source.Executers[j].Id == source.Bookings[i].ExecuterId.Value)
                        {
                            implementerFIO = source.Executers[j].ExecuterFIO;
                            break;
                        }
                    }
                }
                result.Add(new BookingViewModel
                {
                    Id = source.Bookings[i].Id,
                    CustomerId = source.Bookings[i].CustomerId,
                    CustomerFIO = clientFIO,
                    StuffId = source.Bookings[i].StuffId,
                    StuffName = productName,
                    ExecuterId = source.Bookings[i].ExecuterId,
                    ExecuterName = implementerFIO,
                    Amount = source.Bookings[i].Count,
                    Sum = source.Bookings[i].Sum,
                    DateCreate = source.Bookings[i].DateCreate.ToLongDateString(),
                    DateExecute = source.Bookings[i].DateExecute?.ToLongDateString(),
                    Status = source.Bookings[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateBooking(BookingBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
            }
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                StuffId = model.StuffId,
                DateCreate = DateTime.Now,
                Count = model.Amount,
                Sum = model.Sum,
                Status = BookingStatus.Принят
            });
        }

        public void TakeBookingInWork(BookingBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Bookings[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for(int i = 0; i < source.StuffParts.Count; ++i)
            {
                if(source.StuffParts[i].StuffId == source.Bookings[index].StuffId)
                {
                    int countOnStocks = 0;
                    for(int j = 0; j < source.StorageParts.Count; ++j)
                    {
                        if(source.StorageParts[j].PartId == source.StuffParts[i].PartId)
                        {
                            countOnStocks += source.StorageParts[j].Amount;
                        }
                    }
                    if(countOnStocks < source.StuffParts[i].Amount * source.Bookings[index].Count)
                    {
                        for (int j = 0; j < source.Parts.Count; ++j)
                        {
                            if (source.Parts[j].Id == source.StuffParts[i].PartId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Parts[j].PartName + 
                                    " требуется " + source.StuffParts[i].Amount + ", в наличии " + countOnStocks);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.StuffParts.Count; ++i)
            {
                if (source.StuffParts[i].StuffId == source.Bookings[index].StuffId)
                {
                    int countOnStocks = source.StuffParts[i].Amount * source.Bookings[index].Count;
                    for (int j = 0; j < source.StorageParts.Count; ++j)
                    {
                        if (source.StorageParts[j].PartId == source.StuffParts[i].PartId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.StorageParts[j].Amount >= countOnStocks)
                            {
                                source.StorageParts[j].Amount -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.StorageParts[j].Amount;
                                source.StorageParts[j].Amount = 0;
                            }
                        }
                    }
                }
            }
            source.Bookings[index].ExecuterId = model.ExecuterId;
            source.Bookings[index].DateExecute = DateTime.Now;
            source.Bookings[index].Status = BookingStatus.Выполняется;
        }

        public void FinishBooking(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Customers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Bookings[index].Status = BookingStatus.Готов;
        }

        public void PayBooking(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Bookings.Count; ++i)
            {
                if (source.Customers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Bookings[index].Status = BookingStatus.Оплачен;
        }

        public void PutPartOnStorage(StoragePartsBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.StorageParts.Count; ++i)
            {
                if(source.StorageParts[i].StorageId == model.StorageId && 
                    source.StorageParts[i].PartId == model.PartId)
                {
                    source.StorageParts[i].Amount += model.Amount;
                    return;
                }
                if (source.StorageParts[i].Id > maxId)
                {
                    maxId = source.StorageParts[i].Id;
                }
            }
            source.StorageParts.Add(new StorageParts
            {
                Id = ++maxId,
                StorageId = model.StorageId,
                PartId = model.PartId,
                Amount = model.Amount
            });
        }
    }
}
