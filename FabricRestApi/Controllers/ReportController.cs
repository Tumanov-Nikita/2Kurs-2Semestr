using FabricService.BindingModels;
using FabricService.Interfaces;
using System;
using System.Web.Http;

namespace FabricRestApi.Controllers
{
	public class ReportController : ApiController
	{
		private readonly IReportService _service;

		public ReportController(IReportService service)
		{
			_service = service;
		}

		[HttpGet]
		public IHttpActionResult GetStoragesLoad()
		{
			var list = _service.GetStoragesLoad();
			if (list == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(list);
		}

		[HttpPost]
		public IHttpActionResult GetCustomerBookings(ReportBindingModel model)
		{
			var list = _service.GetCustomerBookings(model);
			if (list == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(list);
		}

		[HttpPost]
		public void SaveStuffPrice(ReportBindingModel model)
		{
			_service.SaveStuffPrice(model);
		}

		[HttpPost]
		public void SaveStoragesLoad(ReportBindingModel model)
		{
			_service.SaveStoragesLoad(model);
		}

		[HttpPost]
		public void SaveCustomerBookings(ReportBindingModel model)
		{
			_service.SaveCustomerBookings(model);
		}
	}
}
