using FabricService.BindingModels;
using FabricService.Interfaces;
using System;
using System.Web.Http;

namespace FabricRestApi.Controllers
{
	public class StuffController : ApiController
	{
		private readonly IStuffService _service;

		public StuffController(IStuffService service)
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

		[HttpGet]
		public IHttpActionResult Get(int id)
		{
			var element = _service.GetElement(id);
			if (element == null)
			{
				InternalServerError(new Exception("Нет данных"));
			}
			return Ok(element);
		}

		[HttpPost]
		public void AddElement(StuffBindingModel model)
		{
			_service.AddElement(model);
		}

		[HttpPost]
		public void UpdElement(StuffBindingModel model)
		{
			_service.UpdElement(model);
		}

		[HttpPost]
		public void DelElement(StuffBindingModel model)
		{
			_service.DelElement(model.Id);
		}
	}
}
