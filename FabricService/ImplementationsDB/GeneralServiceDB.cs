using FabricModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using Fabric;
using FabricService.Interfaces;
using FabricService.ViewModels;
using FabricService.BindingModels;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace FabricService.ImplementationsBD
{
	public class GeneralServiceBD : IGeneralService
	{
		private FabricDbContext context;

		public GeneralServiceBD(FabricDbContext context)
		{
			this.context = context;
		}

		public List<BookingViewModel> GetList()
		{
			List<BookingViewModel> result = context.Bookings
				.Select(rec => new BookingViewModel
				{
					Id = rec.Id,
					CustomerId = rec.CustomerId,
					StuffId = rec.StuffId,
					ExecuterId = rec.ExecuterId,
					DateBegin = SqlFunctions.DateName("dd", rec.DateBegin) + " " +
								SqlFunctions.DateName("mm", rec.DateBegin) + " " +
								SqlFunctions.DateName("yyyy", rec.DateBegin),
					DateBuilt = rec.DateBuilt == null ? "" :
										SqlFunctions.DateName("dd", rec.DateBuilt.Value) + " " +
										SqlFunctions.DateName("mm", rec.DateBuilt.Value) + " " +
										SqlFunctions.DateName("yyyy", rec.DateBuilt.Value),
					Status = rec.Status.ToString(),
					Count = rec.Count,
					Cost = rec.Cost,
					CustomerFIO = rec.Customer.CustomerFIO,
					StuffName = rec.Stuff.StuffName,
					ExecuterName = rec.Executer.ExecuterFIO
				})
				.ToList();
			return result;
		}

        public void CreateBooking(BookingBindingModel model)
        {
            var Booking = new Booking
            {
                CustomerId = model.CustomerId,
                StuffId = model.StuffId,
                DateBegin = DateTime.Now,
                Count = model.Count,
                Cost = model.Cost,
                Status = BookingStatus.Принят
            };
            context.Bookings.Add(Booking);
            context.SaveChanges();

            var Customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerId);
            SendEmail(Customer.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} создан успешно", Booking.Id,
                Booking.DateBegin.ToShortDateString()));
        }

        public void TakeBookingInWork(BookingBindingModel model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{

					Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
					if (element == null)
					{
						throw new Exception("Элемент не найден");
					}
					var StuffParts = context.StuffParts
												.Include(rec => rec.Part)
												.Where(rec => rec.StuffId == element.StuffId);
					// списываем
					foreach (var StuffPart in StuffParts)
					{
						int countOnStorages = StuffPart.Count * element.Count;
						var storageParts = context.StorageParts
													.Where(rec => rec.PartId == StuffPart.PartId);
						foreach (var storagePart in storageParts)
						{
							// компонентов на одном слкаде может не хватать
							if (storagePart.Count >= countOnStorages)
							{
								storagePart.Count -= countOnStorages;
								countOnStorages = 0;
								context.SaveChanges();
								break;
							}
							else
							{
								countOnStorages -= storagePart.Count;
								storagePart.Count = 0;
								context.SaveChanges();
							}
						}
						if (countOnStorages > 0)
						{
							throw new Exception("Не достаточно компонента " +
								StuffPart.Part.PartName + " требуется " +
								StuffPart.Count + ", не хватает " + countOnStorages);
						}
					}
					element.ExecuterId = model.ExecuterId;
					element.DateBuilt = DateTime.Now;
					element.Status = BookingStatus.Выполняется;
					context.SaveChanges();
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}

        public void FinishBooking(int id)
        {
            Booking element = context.Bookings.Include(rec => rec.Customer).FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = BookingStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} передан на оплату", element.Id,
                element.DateBegin.ToShortDateString()));
        }

        public void PayBooking(int id)
		{
			Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.Status = BookingStatus.Оплачен;
			context.SaveChanges();
		}

		public void PutPartOnStorage(StoragePartBindingModel model)
		{
			StoragePart element = context.StorageParts
												.FirstOrDefault(rec => rec.StorageId == model.StorageId &&
																	rec.PartId == model.PartId);
			if (element != null)
			{
				element.Count += model.Count;
			}
			else
			{
				context.StorageParts.Add(new StoragePart
				{
					StorageId = model.StorageId,
					PartId = model.PartId,
					Count = model.Count
				});
			}
			context.SaveChanges();
		}

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpCustomer = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpCustomer = new SmtpClient("smtp.gmail.com", 587);
                objSmtpCustomer.UseDefaultCredentials = false;
                objSmtpCustomer.EnableSsl = true;
                objSmtpCustomer.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpCustomer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpCustomer.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpCustomer = null;
            }
        }
    }
}
