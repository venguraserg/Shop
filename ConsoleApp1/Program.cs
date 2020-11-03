using AppBLL.Interfaces;
using AppBLL.Services;
using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUI

{
    class Program
    {
        static IShopService shopService = new ShopService();
        static IBuyerService buyerService = new BuyerService();
        static IManagerService managerService = new ManagerService();
        static IUnitService unitService = new UnitService();



        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в ИНТЕРНЕТ МАГАЗИН");
            Console.WriteLine("===================================");
            Console.WriteLine("Для продолжения нажмите любую кнопку");
            Console.ReadKey();
            Console.Clear();

          //Login/registration buyer/quit
            
            while (true)
            {
                switch (UserVM.Role)
                {
                    case "manager":
                        Console.WriteLine($"Вы вошли как {UserVM.Role}");
                        do
                        {
                            Console.ReadKey();
                        }
                        while (UserVM.Role == "manager");
                        
                        break;

                    case "buyer":
                        Console.WriteLine($"Вы вошли как {UserVM.Role}");
                        do
                        {
                            Console.ReadKey();
                        }
                        while (UserVM.Role == "buyer");

                        break;

                    case "admin":
                        Console.WriteLine($"Вы вошли как {UserVM.Role}");
                        do
                        {
                            Menu_admin();
                        }
                        while (UserVM.Role == "admin");

                        break;

                    case null:
                        Authorization();
                        break;
                }
            }
        }

        static void Authorization()
        {
            Console.Clear();
            Console.WriteLine("Пожалуйста пройдите АВТОРИЗАЦИЮ/РЕГИСТРАЦИЮ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1 => Авторизация\n" +
                              "2 => Регистрация\n" +
                              "3 => Выйти из приложения");
            if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
            switch (key)
            {
                //login    
                case 1:
                    Console.Clear();
                    Console.WriteLine("----------ВЫБЕРИТЕ КАТЕГОРИЮ ПОЛЬЗОВАТЕЛЯ----------");
                    Console.WriteLine("===================================================");
                    Console.WriteLine("1 => Покупатель\n" +
                                      "2 => Менеджер\n" +
                                      "3 => Вернутся назад");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");

                    switch (key)
                    {
                        //login buyer
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Приветствую ПОКУПТЕЛЬ, введите свой LOGIN:");
                            string current_login = Console.ReadLine();
                            Console.WriteLine("Ввведите свой ПАРОЛЬ:");
                            string current_passwordHash = Console.ReadLine().GetHashCode().ToString();
                            if (buyerService.LogInBuyer(current_login, current_passwordHash)) 
                            {
                                Console.WriteLine("Вход выполнен, нажмите любую клавишу...");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("НЕ ВЕРНЫЙ ЛОГИН/ПАРОЛЬ, нажмите любую клавишу...");
                                Console.ReadKey();
                                break;
                            }

                        //login manager 
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Приветствую МЕНЕДЖЕР, введите свой LOGIN:");
                            current_login = Console.ReadLine();
                            Console.WriteLine("Введите свой ПАРОЛЬ:");
                            current_passwordHash = Console.ReadLine().GetHashCode().ToString();
                            if (managerService.LogInManager(current_login, current_passwordHash))
                            {
                                Console.WriteLine("Вход выполнен, нажмите любую клавишу...");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("НЕ ВЕРНЫЙ ЛОГИН/ПАРОЛЬ, нажмите любую клавишу...");
                                Console.ReadKey();
                                break;
                            }

                        //back
                        case 3:
                            Console.Clear();
                            break;


                        default://incorrect input
                            Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }

                    break;
                //registration buyer
                case 2:

                    AddBuyer();
                    
                    break;
                //quit
                case 3:
                    QuitApp();
                    break;
                default://incorrect input
                    Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
            
        }
        static DateTime InputData()
        {
            DateTime data; // date 
            string input;
            do
            {
                Console.WriteLine("Введите дату в формате дд.ММ.гггг (день.месяц.год):");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out data));

            return data;
        }
        static void Menu_buyer()
        {

        }
        static void Menu_manager()
        {

        }
        static void Menu_admin()
        {
            Console.Clear();
            Console.WriteLine($"{UserVM.Role} пожалуйста выбеите пункт МЕНЮ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1 => Добавить\n" +
                              "2 => Просмотреть\n" +
                              "3 => Изменить\n" +
                              "4 => Удалить\n" +
                              "5 => Выйти из аккаунта\n" +
                              "6 => Выйти из программы");
            if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
            switch (key)
            {
                //Меню - 1.Добавить
                case 1:
                    Console.Clear();
                    Console.WriteLine("Пожалуйста выбеите пункт МЕНЮ              ");
                    Console.WriteLine("===========================================");
                    Console.WriteLine("1.1 => Зарегистрировать менеджера\n" +
                                      "1.2 => Зарегистрировать покупателя\n" +
                                      "1.3 => Добавить магазин\n" +
                                      "1.4 => Добавить продукт\n" +
                                      "1.5 => Добавить единицы измерения\n" +
                                      "1.6 => Вернутся назад\n");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    switch (key)
                    {
                        //1.1 Регистрация нового менеджера
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Пожалуйста данные нового менеджера:        ");
                            Console.WriteLine("===========================================");
                            Console.Write("Login:  ");
                            var temp_login = Console.ReadLine();
                            Console.Write("\nPassword:  ");
                            var temp_password = Console.ReadLine().GetHashCode().ToString();
                            Console.Write("\nИмя:  ");
                            var temp_name = Console.ReadLine();
                            Console.Write("\nФамилия:  ");
                            var temp_surname = Console.ReadLine();
                            Console.Write("\nНомер телефона:  ");
                            var temp_phonenumb = Console.ReadLine();
                            var temp_Id = ShopView(true);
                            managerService.AddManager(temp_login, temp_password, temp_name, temp_surname, temp_phonenumb, temp_Id);
                            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                        //1.2.Регистрация нового покупателя
                        case 2:
                            AddBuyer();
                            break;
                        //1.3.Регистрация нового магазина
                        case 3:
                            Console.Clear();
                            Console.WriteLine("Пожалуйста внесите данные нового магазина:        ");
                            Console.WriteLine("===========================================");
                            Console.Write("Наименование:  ");
                            var temp_name_shop = Console.ReadLine();
                            Console.Write("\nОписание:  ");
                            var temp_Discr_Shop = Console.ReadLine();
                            shopService.RegisterNewShop(temp_name_shop, temp_Discr_Shop);
                            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                        //1.4.Регистрация нового Продукта
                        case 4:
                            //Console.Clear();
                            //Console.WriteLine("Пожалуйста данные нового менеджера:        ");
                            //Console.WriteLine("===========================================");
                            //Console.Write("Login:  ");
                            //var temp_login = Console.ReadLine();
                            //Console.Write("\nPassword:  ");
                            //var temp_password = Console.ReadLine().GetHashCode().ToString();
                            //Console.Write("\nИмя:  ");
                            //var temp_name = Console.ReadLine();
                            //Console.Write("\nФамилия:  ");
                            //var temp_surname = Console.ReadLine();
                            //Console.Write("\nНомер телефона:  ");
                            //var temp_phonenumb = Console.ReadLine();
                            break;
                        //1.5.Добавить единицу измерения
                        case 5:
                            Console.Clear();
                            Console.WriteLine("Пожалуйста внесите новую единицу измерения:");
                            Console.WriteLine("===========================================");
                            Console.Write("Наименование:  ");
                            var temp_new_unit = Console.ReadLine();
                            unitService.AddUnit(temp_new_unit);
                            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                        //1.5.Back
                        case 6:
                            break;
                        default://incorrect input
                            Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                    
                    break;
                //Меню - 2.Просмотреть
                case 2:
                    Console.Clear();
                    Console.WriteLine("Пожалуйста выбеите пункт МЕНЮ              ");
                    Console.WriteLine("===========================================");
                    Console.WriteLine("2.1 => Просмотреть список менеджеров\n" +
                                      "2.2 => Просмотреть список покупателей\n" +
                                      "2.3 => Просмотреть список магазинов\n" +
                                      "2.4 => Просмотреть список продуктов\n" +
                                      "2.5 => Вернутся назад\n");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    switch (key)
                    {
                        //Просмотреть список менеджеров
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Список Менеджеров");
                            var ListManagers = managerService.GetAllManagersInfo();
                            for (var i = 0; i < ListManagers.Count(); i++)
                            {
                                Console.WriteLine($"{i} -> {ListManagers[i]}");
                            }
                            Console.WriteLine("Для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                            
                        //Просмотреть список покупателей
                        case 2:
                            
                            Console.Clear();
                            var buyerCount = buyerService.GetNumbOfItemBuyer();
                            var numbersOfItem = 5;
                            if (buyerCount >= numbersOfItem)
                            {

                                for (var i = 0; i < buyerCount; i += numbersOfItem)
                                {
                                    var ListShops = buyerService.GetPageBuyersInfo(i, numbersOfItem);
                                    Console.Clear();
                                    Console.WriteLine($"Список магазинов. Страница {1 + i / numbersOfItem}");
                                    for (var j = 0; j < ListShops.Count(); j++)
                                    {
                                        Console.WriteLine($"{j + 1} -> {ListShops[j]}");
                                    }
                                    Console.WriteLine("next page...");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                var ListShops = buyerService.GetPageBuyersInfo(0, buyerCount);
                                for (var i = 0; i < ListShops.Count(); i++)
                                {
                                    Console.WriteLine($"{i + 1} -> {ListShops[i]}");
                                }
                            }
                            Console.WriteLine("Для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;



                        //Просмотреть список магазинов
                        case 3:
                            Console.Clear();
                            var shopCount = shopService.GetNumbOfItemShop();
                            numbersOfItem = 15;
                            if (shopCount >= numbersOfItem)
                            {
                                
                                for(var i = 0; i < shopCount; i += numbersOfItem)
                                {
                                    var ListShops = shopService.GetPageShopInfo(i, numbersOfItem);
                                    Console.Clear();
                                    Console.WriteLine($"Список магазинов. Страница {1+i/numbersOfItem}");
                                    for (var j = 0; j < ListShops.Count(); j++)
                                    {
                                        Console.WriteLine($"{j+1} -> {ListShops[j]}");
                                    }
                                    Console.WriteLine("next page...");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                var ListShops = shopService.GetPageShopInfo(0, shopCount);
                                for (var i = 0; i < ListShops.Count(); i++)
                                {
                                    Console.WriteLine($"{i+1} -> {ListShops[i]}");
                                }
                            }
                            Console.WriteLine("Для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                        case 4:
                            Console.Clear();

                            Console.WriteLine("Для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                        case 5:
                            Console.Clear();

                            Console.WriteLine("Для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
                            break;
                        default://incorrect input
                            Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                    break;
                //ИЗМЕНИТЬ
                case 3:
                
                    break;
                //УДАЛИТЬ
                case 4:

                    break;
                //LOGOUT
                case 5:
                    UserVM.Id = Guid.Empty;
                    UserVM.Role = null;
                    break;
                //QUIT
                case 6:
                    QuitApp();
                    break;
                default://incorrect input
                    Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                    Console.ReadKey();
                    Console.Clear();
                    break;

            }
            

        }
        static void QuitApp()
        {
            Console.Clear();
            Console.WriteLine("ВЫЙТИ ИЗ ПРИЛОЖЕНИЯ  (y/n)?:");
            char yn = Console.ReadLine()[0];

            if (yn == 'y' || yn == 'Y')
            {
                Environment.Exit(0);
            }
            else if (yn == 'n' || yn == 'N')
            {
                Console.WriteLine("Продолжим...");
            }
        }
        static void AddBuyer()
        {
            Console.Clear();
            Console.WriteLine("Регистрация нового пользователя: ");
            Console.Write("Введите ваш LOGIN: ");
            string temp_login = Console.ReadLine();
            Console.Write("\nВведите ваш ПАРОЛЬ: ");
            string temp_passHash = Console.ReadLine().GetHashCode().ToString();
            Console.Write("\nВведите ваше ИМЯ:");
            string temp_name = Console.ReadLine();
            Console.Write("\nВведите вашу ФАМИЛИЮ: ");
            string temp_surname = Console.ReadLine();
            Console.Write("\nВведите ваш НОМЕР ТЕЛЕФОНА: ");
            string temp_numbtel = (Console.ReadLine());
            Console.Write("\nВведите ваш АДРЕС: ");
            string temp_address = Console.ReadLine();
            Console.Write("\nВведите дату вашего рождения\n");
            DateTime temp_date_of_birth = InputData();

            buyerService.RegisterNewBuyer(temp_login, temp_passHash, temp_name, temp_surname, temp_numbtel, temp_address, temp_date_of_birth);

            Console.WriteLine($"\n{temp_name}, вы успешно зарегистрированы\nДля продолжения нажмите любую клавишу ... ");
            Console.ReadKey();

        }
        static Guid ShopView(bool mode)
        {
            Console.Clear();
            var shopCount = shopService.GetNumbOfItemShop();
            var numbersOfItem = 10;
            var temp_Id = Guid.Empty;
            //if (shopCount >= numbersOfItem)
            //{
            do
            {
                for (var i = 0; i < shopCount; i += numbersOfItem)
                {
                    var ListShops = shopService.GetPageShopInfo(i, numbersOfItem);
                    Console.Clear();
                    Console.WriteLine($"Список магазинов. Страница {1 + i / numbersOfItem}");
                    for (var j = 0; j < ListShops.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListShops[j]}");
                    }
                    if (mode)
                    {
                        Console.WriteLine("Выберите номер магазина или нажмите ENTER для перехода к следующей странице");
                        if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (key >= 1 && key <= ListShops.Count())
                        {
                            temp_Id = ListShops[key - 1].Id;
                            return temp_Id;
                        }

                    }
                    else
                    {
                        Console.WriteLine("next page...");
                        Console.ReadKey();
                    }

                }
            } while (mode==true && (temp_Id!=null));
            return Guid.Empty;
            //}
            //else
            //{
            //    var ListShops = shopService.GetPageShopInfo(0, shopCount);
            //    for (var i = 0; i < ListShops.Count(); i++)
            //    {
            //        Console.WriteLine($"{i + 1} -> {ListShops[i]}");
            //    }
            //}
            //Console.WriteLine("Для продолжения нажмите любую клавишу ...");
            //Console.ReadKey();
        }
        



    }



}
