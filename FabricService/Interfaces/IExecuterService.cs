using FabricService.Attributies;
using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.Interfaces
{
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface IExecuterService
    {
        [CustomMethod("Метод получения списка работников")]
        List<ExecuterViewModel> GetList();

        [CustomMethod("Метод получения работника по id")]
        ExecuterViewModel GetElement(int id);

        [CustomMethod("Метод добавления работника")]
        void AddElement(ExecuterBindingModel model);

        [CustomMethod("Метод изменения данных по работнику")]
        void UpdElement(ExecuterBindingModel model);

        [CustomMethod("Метод удаления работника")]
        void DelElement(int id);
    }
}
