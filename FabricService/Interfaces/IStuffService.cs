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
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface IStuffService
    {
        [CustomMethod("Метод получения списка изделий")]
        List<StuffViewModel> GetList();

        [CustomMethod("Метод получения изделия по id")]
        StuffViewModel GetElement(int id);

        [CustomMethod("Метод добавления изделия")]
        void AddElement(StuffBindingModel model);

        [CustomMethod("Метод изменения данных по изделию")]
        void UpdElement(StuffBindingModel model);

        [CustomMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
