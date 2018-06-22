using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricService.Attributies;
using FabricService.BindingModels;
using FabricService.ViewModels;

namespace FabricService.Interfaces
{
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IPartService
    {
        [CustomMethod("Метод получения списка компонент")]
        List<PartViewModel> GetList();

        [CustomMethod("Метод получения компонента по id")]
        PartViewModel GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(PartBindingModel model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(PartBindingModel model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
