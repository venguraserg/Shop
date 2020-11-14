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
        List<ShopVM> GetPageInfo(int start_items, int amount_items);
        string FindShop(Guid Id);
        int GetNumbOfItem();
        ShopVM GetShop(Guid Id);

        //Update Обновление данных по магазину
        void UpdateShop(Guid id, string shopname, string discription);
        //Delete удаление магазина
        bool DeleteShop(Guid Id);
    }
}
