using AppBLL.Interfaces;
using AppBLL.Services;
using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class ConsoleHelper
    {
        static IShopService shopService = new ShopService();
        static IBuyerService buyerService = new BuyerService();
        static IManagerService managerService = new ManagerService();
        static IUnitService unitService = new UnitService();
        static IProductService productService = new ProductService();


        #region Метод авторизации
        public static void Authorization()
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
                            
                            LogInUser("Покупатель", buyerService.LogInBuyer);
                            break;
                        
                        //login manager 
                        case 2:

                            LogInUser("Mенеджер", managerService.LogInManager);
                            break;

                        //back
                        case 3:
                            Console.Clear();
                            break;

                        //incorrect input
                        default:
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
        #endregion

        #region Метод Работы с пользователем
        public static void LogInUser(string role, Func<string,string,bool>LogInUser)
        {
            Console.Clear();
            Console.WriteLine($"Приветствую {role}, введите свой LOGIN:");
            var current_login = Console.ReadLine();
            Console.WriteLine("Ввведите свой ПАРОЛЬ:");
            var current_passwordHash = Console.ReadLine().GetHashCode().ToString();
            if (LogInUser(current_login,current_passwordHash))
            {
                Console.WriteLine("Вход выполнен, нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("НЕ ВЕРНЫЙ ЛОГИН/ПАРОЛЬ, нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        public static void AddBuyer()
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
            DateTime temp_date_of_birth = InputDate();

            buyerService.RegisterNewBuyer(temp_login, temp_passHash, temp_name, temp_surname, temp_numbtel, temp_address, temp_date_of_birth);

            Console.WriteLine($"\n{temp_name}, вы успешно зарегистрированы\nДля продолжения нажмите любую клавишу ... ");
            Console.ReadKey();

        }
        public static void AddManager() 
        {
            Console.Clear();
            Console.WriteLine("Пожалуйста внесите данные нового менеджера:        ");
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

            //var temp_Id = ShopView(true);

            var temp_id = OpenView<ShopVM>(shopService.GetPageShopInfo(0, 10), true, shopService.GetNumbOfItemShop(), 10);
            if (temp_id.HasValue)
            {

                managerService.AddManager(temp_login, temp_password, temp_name, temp_surname, temp_phonenumb, temp_id.Value);
            }





            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
            Console.ReadKey();
        }
        #endregion

        #region Методы работы с сущностями
        public static void AddShop()
        {
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
        }
        #endregion

        #region Методы Меню
        public static void Menu_buyer()
        {

        }
        public static void Menu_manager()
        {

        }
        public static void Menu_admin()
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
                // Меню - 1.Добавить
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

                            AddManager();
                            break;

                        //1.2.Регистрация нового покупателя
                        case 2:

                            AddBuyer();
                            break;

                        //1.3.Регистрация нового магазина
                        case 3:

                            AddShop();
                            break;

                        //1.4.Регистрация нового Продукта
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Пожалуйста внесите данные товара:        ");
                            Console.WriteLine("===========================================");
                            Console.Write("Наименование:  ");
                            var temp_name_prod = Console.ReadLine();
                            Console.Write("\nОписание:  ");
                            var temp_descr_prod = Console.ReadLine();
                            Console.Write("\nКоличество:  ");
                            var temp_amount_prod = float.Parse(Console.ReadLine());
                            Console.Write("\nЕдиницы измерения:  ");
                            var temp_unit = UnitView(true);
                            Console.Write("\nЦена:  ");
                            var temp_price = decimal.Parse(Console.ReadLine());
                            Console.Write("\nВыберите магазин:  ");
                            var temp_Shop_Id = ShopView(true);
                            productService.AddProduct(temp_name_prod, temp_descr_prod, temp_amount_prod, temp_price, temp_Shop_Id, temp_unit);
                            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
                            Console.ReadKey();
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
                            ManagerView(false);
                            break;
                        //Просмотреть список покупателей
                        case 2:
                            BuyerView(false);
                            break;
                        //Просмотреть список магазинов
                        case 3:
                            ShopView(false);
                            break;
                        //Просмотреть список продуктов
                        case 4:
                            ProductView(false);
                            break;
                        //Назад
                        case 5:

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
                    Console.Clear();
                    Console.WriteLine("Пожалуйста выбеите пункт МЕНЮ              ");
                    Console.WriteLine("===========================================");
                    Console.WriteLine("4.1 => Удалить менеджера\n" +
                                      "4.2 => Удалить покупателей\n" +
                                      "4.3 => Удалить магазинов\n" +
                                      "4.4 => Удалить продуктов\n" +
                                      "4.5 => Вернутся назад\n");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    switch (key)
                    {
                        case 1:
                            //todo: managerService.DeleteManager(ManagerView(true));
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        default:
                            break;
                    }
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
        #endregion




        public static DateTime InputDate()
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
        
        public static void QuitApp()
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
        
        public static Guid ShopView(bool mode)
        {
            //Console.Clear();
            var shopCount = shopService.GetNumbOfItemShop();
            var numbersOfItem = 10;
            var temp_Id = Guid.Empty;
            do
            {
                for (var i = 0; i < shopCount; i += numbersOfItem)
                {
                    var ListShops = shopService.GetPageShopInfo(i, numbersOfItem);
                    //Console.Clear();
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
            } while (mode == true && (temp_Id != null));
            return Guid.Empty;

        }
        public static Guid UnitView(bool mode)
        {
            // Console.Clear();
            var unitCount = unitService.GetNumbOfItem();
            var numbersOfItem = 10;
            var temp_Id = Guid.Empty;
            do
            {
                for (var i = 0; i < unitCount; i += numbersOfItem)
                {
                    var ListUnit = unitService.GetPageUnitInfo(i, numbersOfItem);
                    //Console.Clear();
                    Console.WriteLine($"Список Ед.измерений. Страница {1 + i / numbersOfItem}");
                    for (var j = 0; j < ListUnit.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListUnit[j]}");
                    }
                    if (mode)
                    {
                        Console.WriteLine("Выберите номер или нажмите ENTER для перехода к следующей странице");
                        if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (key >= 1 && key <= ListUnit.Count())
                        {
                            temp_Id = ListUnit[key - 1].Id;
                            return temp_Id;
                        }

                    }
                    else
                    {
                        Console.WriteLine("next page...");
                        Console.ReadKey();
                    }

                }
            } while (mode == true && (temp_Id != null));
            return Guid.Empty;

        }

        public static Guid ProductView(bool mode)
        {
            // Console.Clear();
            var productCount = productService.GetNumbOfItem();
            var numbersOfItem = 10;
            var temp_Id = Guid.Empty;
            do
            {
                for (var i = 0; i < productCount; i += numbersOfItem)
                {
                    var ListProduct = productService.GetPageProductInfo(i, numbersOfItem);
                    //Console.Clear();
                    Console.WriteLine($"Список продуктов. Страница {1 + i / numbersOfItem}");
                    for (var j = 0; j < ListProduct.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListProduct[j]}");
                    }
                    if (mode)
                    {
                        Console.WriteLine("Выберите номер или нажмите ENTER для перехода к следующей странице");
                        if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (key >= 1 && key <= ListProduct.Count())
                        {
                            temp_Id = ListProduct[key - 1].Id;
                            return temp_Id;
                        }

                    }
                    else
                    {
                        Console.WriteLine("next page...");
                        Console.ReadKey();
                    }

                }
            } while (mode == true && (temp_Id != null));
            return Guid.Empty;

        }
        public static Guid BuyerView(bool mode)
        {
            // Console.Clear();
            var buyerCount = buyerService.GetNumbOfItem();
            var numbersOfItem = 10;
            var temp_Id = Guid.Empty;
            do
            {
                for (var i = 0; i < buyerCount; i += numbersOfItem)
                {
                    var ListBuyer = buyerService.GetPageBuyerInfo(i, numbersOfItem);
                    //Console.Clear();
                    Console.WriteLine($"Список покупателей. Страница {1 + i / numbersOfItem}");
                    for (var j = 0; j < ListBuyer.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListBuyer[j]}");
                    }
                    if (mode)
                    {
                        Console.WriteLine("Выберите номер или нажмите ENTER для перехода к следующей странице");
                        if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (key >= 1 && key <= ListBuyer.Count())
                        {
                            temp_Id = ListBuyer[key - 1].Id;
                            return temp_Id;
                        }

                    }
                    else
                    {
                        Console.WriteLine("next page...");
                        Console.ReadKey();
                    }

                }
            } while (mode == true && (temp_Id != null));
            return Guid.Empty;

        }

        public static Guid? ManagerView(bool mode)
        {
            // Console.Clear();
            var managerCount = managerService.GetNumbOfItem();
            var numbersOfItem = 10;
            var temp_Id = Guid.Empty;
            do
            {
                for (var i = 0; i < managerCount; i += numbersOfItem)
                {
                    var ListManager = managerService.GetPageManagerInfo(i, numbersOfItem);
                    //Console.Clear();
                    Console.WriteLine($"Список покупателей. Страница {1 + i / numbersOfItem}");
                    for (var j = 0; j < ListManager.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListManager[j]}");
                    }
                    if (mode)
                    {
                        Console.WriteLine("Выберите номер или нажмите ENTER для перехода к следующей странице");
                        if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (key >= 1 && key <= ListManager.Count())
                        {
                            temp_Id = ListManager[key - 1].Id;
                            return temp_Id;
                        }

                    }
                    else
                    {
                        Console.WriteLine("next page...");
                        Console.ReadKey();
                    }

                }
            } while (mode == true && (temp_Id != null));
            return null;

        }

        public static Guid? OpenView<T>(List<T> list, bool mode, int totalCount, int numbersOfItem = 10) where T:BaseVMmodel
        {


            var temp_Id = Guid.Empty;
            do
            {
                for (var i = 0; i < totalCount; i += numbersOfItem)
                {

                    Console.WriteLine($"Список покупателей. Страница {1 + i / numbersOfItem}");
                    for (var j = 0; j < list.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {list[j]}");
                    }
                    if (mode)
                    {
                        Console.WriteLine("Выберите номер или нажмите ENTER для перехода к следующей странице");
                        if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (key >= 1 && key <= list.Count())
                        {
                            temp_Id = list[key - 1].Id;
                            return temp_Id;
                        }

                    }
                    else
                    {
                        Console.WriteLine("next page...");
                        Console.ReadKey();
                    }

                }
            } while (mode == true && (temp_Id != null));
            return null;

        }


    }
}
