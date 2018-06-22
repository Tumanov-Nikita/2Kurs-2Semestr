using FabricService.BindingModels;
using FabricService.Interfaces;
using System;
using System.Web.Http;

namespace FabricRestApi.Controllers
{
	public class ExecuterController : ApiController
	{
		private readonly IExecuterService _service;

		public ExecuterController(IExecuterService service)
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
		public void AddElement(ExecuterBindingModel model)
		{
			_service.AddElement(model);
		}

		[HttpPost]
		public void UpdElement(ExecuterBindingModel model)
		{
			_service.UpdElement(model);
		}

		[HttpPost]
		public void DelElement(ExecuterBindingModel model)
		{
			_service.DelElement(model.Id);
		}
	}
}
