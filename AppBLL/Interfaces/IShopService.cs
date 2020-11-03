using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Interfaces
{
    public interface IShopService
    {
        //Create Создание нового магазина
        void RegisterNewShop(string shopname, string discription);
        //Read Получение списка магазинов
        List<ShopVM> GetAllShopInfo();
        List<ShopVM> GetPageShopInfo(int start_items, int amount_items);
        string FindShop(Guid Id);
        int GetNumbOfItemShop();

        Guid ChooiseOneFromListShop(); //выбор одного из списка

        //Update Обновление данных по магазину
        void UpdateShop();
        //Delete удаление магазина
        bool DeleteShop(Guid Id);
    }
}
