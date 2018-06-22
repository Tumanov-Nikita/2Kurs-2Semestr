using FabricModel;
using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;

namespace FabricService.ImplementationsList
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
                if(source.Bookings[i].BuilderId.HasValue)
                {
                    for (int j = 0; j < source.Builders.Count; ++j)
                    {
                        if (source.Builders[j].Id == source.Bookings[i].BuilderId.Value)
                        {
                            implementerFIO = source.Builders[j].BuilderFIO;
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
                    BuilderId = source.Bookings[i].BuilderId,
                    BuilderName = implementerFIO,
                    Count = source.Bookings[i].Count,
                    Sum = source.Bookings[i].Sum,
                    DateCreate = source.Bookings[i].DateCreate.ToLongDateString(),
                    DateExecute = source.Bookings[i].DateBuild?.ToLongDateString(),
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
                Count = model.Count,
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
                            countOnStocks += source.StorageParts[j].Count;
                        }
                    }
                    if(countOnStocks < source.StuffParts[i].Count * source.Bookings[index].Count)
                    {
                        for (int j = 0; j < source.Parts.Count; ++j)
                        {
                            if (source.Parts[j].Id == source.StuffParts[i].PartId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Parts[j].PartName + 
                                    " требуется " + source.StuffParts[i].Count + ", в наличии " + countOnStocks);
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
                    int countOnStocks = source.StuffParts[i].Count * source.Bookings[index].Count;
                    for (int j = 0; j < source.StorageParts.Count; ++j)
                    {
                        if (source.StorageParts[j].PartId == source.StuffParts[i].PartId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.StorageParts[j].Count >= countOnStocks)
                            {
                                source.StorageParts[j].Count -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.StorageParts[j].Count;
                                source.StorageParts[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.Bookings[index].BuilderId = model.BuilderId;
            source.Bookings[index].DateBuild = DateTime.Now;
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
                    source.StorageParts[i].Count += model.Count;
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
                Count = model.Count
            });
        }
    }
}
