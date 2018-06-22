using FabricRestApi.Services;
using FabricService.BindingModels;
using FabricService.Interfaces;
using System;
using System.Web.Http;

namespace FabricRestApi.Controllers
{
	public class GeneralController : ApiController
	{
		private readonly IGeneralService _service;

		public GeneralController(IGeneralService service)
		{
			_service = service;
		}

		[HttpGet]
		public IHttpActionResult GetList()
		{
			var list = _service.GetList();
			if (list == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(list);
		}

		[HttpPost]
		public void CreateBooking(BookingBindingModel model)
		{
			_service.CreateBooking(model);
		}

		[HttpPost]
		public void TakeBookingInWork(BookingBindingModel model)
		{
			_service.TakeBookingInWork(model);
		}

		[HttpPost]
		public void FinishBooking(BookingBindingModel model)
		{
			_service.FinishBooking(model.Id);
		}

		[HttpPost]
		public void PayBooking(BookingBindingModel model)
		{
			_service.PayBooking(model.Id);
		}

		[HttpPost]
		public void PutPartOnStorage(StoragePartBindingModel model)
		{
			_service.PutPartOnStorage(model);
		}

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}
