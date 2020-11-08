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


        /// <summary>
        /// Метод Авторизации. Закончен
        /// </summary>
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

                    AddBuyer(true);

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
        

        /// <summary>
        /// Метод ЛОГИН для любого Юзера!! при помощи делегата Func
        /// </summary>
        /// <param name="role"></param>
        /// <param name="LogInUser"></param>
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

        /// <summary>
        /// Метод добавления покупателя . закончен
        /// </summary>
        public static void AddBuyer(bool mode)
        {
            Console.Clear();
            Console.WriteLine("Регистрация нового пользователя: ");

            var temp_login = CheckMatchUser(buyerService.SearchBuyer);

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

            buyerService.RegisterNewBuyer(temp_login, temp_passHash, temp_name, temp_surname, temp_numbtel, temp_address, temp_date_of_birth,mode);

            Console.WriteLine($"\nПользователь {temp_name} успешно зарегистрирован\nДля продолжения нажмите любую клавишу ... ");
            Console.ReadKey();

        }
        /// <summary>
        /// Метод ввода логина с проверкой предидущей регистрации
        /// </summary>
        /// <param name="searchMethod"></param>
        /// <returns></returns>
        public static string CheckMatchUser(Func<string,bool>searchMethod) 
        {
            bool loginMatch;
            string temp_login;
            do
            {
                Console.Write("Введите ваш LOGIN: ");
                temp_login = Console.ReadLine();
                if (searchMethod(temp_login))
                {
                    loginMatch = false;
                    Console.WriteLine("Пользователь с таким LOGIN существует...\n" +
                                  "попробуйте другой LOGIN.\n" +
                                  "для продолжения нажмите любую кнопку... ");
                    Console.ReadKey();
                }
                else
                {
                    loginMatch = true;
                }
            } while (!loginMatch);
            return temp_login;

        }


        /// <summary>
        /// Метод добавления Менеджера. Закончен
        /// </summary>
        public static void AddManager() 
        {
            Console.Clear();
            Console.WriteLine("Пожалуйста внесите данные нового менеджера:        ");
            Console.WriteLine("===========================================");
            Console.Write("Login:  ");
            var temp_login = CheckMatchUser(managerService.SearchManager);
            Console.Write("\nPassword:  ");
            var temp_password = Console.ReadLine().GetHashCode().ToString();
            Console.Write("\nИмя:  ");
            var temp_name = Console.ReadLine();
            Console.Write("\nФамилия:  ");
            var temp_surname = Console.ReadLine();
            Console.Write("\nНомер телефона:  ");
            var temp_phonenumb = Console.ReadLine();

            Guid? temp_id;
            do
            {
                temp_id = EntityView<ShopVM>(shopService.GetPageShopInfo, true, shopService.GetNumbOfItemShop(), 10);
            }
            while (!temp_id.HasValue) ;

            managerService.AddManager(temp_login, temp_password, temp_name, temp_surname, temp_phonenumb, temp_id.Value);


            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
            Console.ReadKey();
        }
        

        
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

        public static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Пожалуйста внесите данные товара:");
            Console.WriteLine("===========================================");
            Console.Write("Наименование:  ");
            var temp_name_prod = Console.ReadLine();
            Console.Write("\nОписание:  ");
            var temp_descr_prod = Console.ReadLine();
            Console.Write("\nКоличество:  ");
            var temp_amount_prod = float.Parse(Console.ReadLine());
            Console.Write("\nЕдиницы измерения:  ");
            Guid? temp_unit;
            do
            {
                temp_unit = EntityView<UnitVM>(unitService.GetPageUnitInfo, true, unitService.GetNumbOfItem(), 10);
            }
            while (!temp_unit.HasValue);
            
            Console.Write("\nЦена:  ");
            var temp_price = decimal.Parse(Console.ReadLine());
            Console.Write("\nВыберите магазин:  ");
            Guid? temp_id;
            do
            {
                temp_id = EntityView<ShopVM>(shopService.GetPageShopInfo, true, shopService.GetNumbOfItemShop(), 10);
            }
            while (!temp_id.HasValue);
            
            productService.AddProduct(temp_name_prod, temp_descr_prod, temp_amount_prod, temp_price, temp_id.Value, temp_unit.Value);
            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
            Console.ReadKey();
        }
       

        
        public static void Menu_buyer()
        {
            Console.Clear();
            Console.WriteLine($"{UserVM.Role} пожалуйста выбеите пункт МЕНЮ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1 => -----\n" +
                              "2 => -----\n" +
                              "5 => Выйти из аккаунта\n" +
                              "6 => Выйти из программы");
            if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
            switch (key)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    UserVM.Id = Guid.Empty;
                    UserVM.Role = null;
                    break;
                //QUIT
                case 6:
                    QuitApp();
                    break;
                //incorrect input
                default:
                    Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }

        }
        public static void Menu_manager()
        {
            Console.Clear();
            Console.WriteLine($"{UserVM.Role} пожалуйста выбеите пункт МЕНЮ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1 => Добавить новый продукт в каталог\n" +
                              "2 => -----\n" +
                              "5 => Выйти из аккаунта\n" +
                              "6 => Выйти из программы");
            if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
            switch (key)
            {
                case 1:

                    AddProduct();

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    UserVM.Id = Guid.Empty;
                    UserVM.Role = null;
                    break;
                //QUIT
                case 6:
                    QuitApp();
                    break;
                //incorrect input
                default:
                    Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
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

                            AddBuyer(false);
                            break;

                        //1.3.Регистрация нового магазина
                        case 3:

                            AddShop();
                            break;

                        //1.4.Регистрация нового Продукта
                        case 4:

                            AddProduct();
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

                            EntityView<ManagerVM>(managerService.GetPageManagerInfo, false, managerService.GetNumbOfItem(), 10);
                            break;
                        //Просмотреть список покупателей
                        case 2:
                            EntityView<BuyerVM>(buyerService.GetPageBuyerInfo, false, buyerService.GetNumbOfItem(), 10);
                            break;
                        //Просмотреть список магазинов
                        case 3:

                            EntityView<ShopVM>(shopService.GetPageShopInfo, false, shopService.GetNumbOfItemShop(), 10);
                            break;
                        //Просмотреть список продуктов
                        case 4:
                            EntityView<ProductVM>(productService.GetPageProductInfo, false, productService.GetNumbOfItem(), 10);
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
        



        /// <summary>
        /// Метод корректного ввода даты с проверкой Диапазона
        /// </summary>
        /// <returns></returns>
        public static DateTime InputDate()
        {
            DateTime data; // date 
            string input;
            bool result;
            do
            {
                Console.WriteLine("Введите дату в формате дд.ММ.гггг (день.месяц.год):");
                input = Console.ReadLine();
                result = DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out data);
                if (data > DateTime.Now || data.Year < 1930)
                {
                    result = false;
                    Console.WriteLine("Не верно введена дата, повторите ввод");
                }
            }
            while (!result);

            return data;
        }
        
        /// <summary>
        /// Метод выход из приложения. Закончен
        /// </summary>
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

        


        public static Guid? EntityView<T>(Func<int,int,List<T>>list, bool mode, int totalCount, int numbersOfItem = 10) where T : BaseVMmodel
        {
            var numberPages = (int)Math.Ceiling((double)totalCount / numbersOfItem);
            bool resultParse;

            Console.Clear();
            Console.WriteLine($"Список состоит из {numberPages} страниц по {numbersOfItem} строк");
            if (numberPages <= 1)
            {
                var ListShops = list(0, numbersOfItem);

                for (var j = 0; j < ListShops.Count(); j++)
                {
                    Console.WriteLine($"{j + 1} -> {ListShops[j]}");
                }
                if (mode) 
                {
                    int numbItem;
                    do
                    {
                        Console.Write("Выберите номер из списка:");
                        resultParse = int.TryParse(Console.ReadLine(), out numbItem);
                        if (!resultParse || numbItem < 1 || numbItem > ListShops.Count())
                        {
                            Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                            resultParse = false;
                        }

                    } while (!resultParse);

                    return ListShops[numbItem - 1].Id;
                }
                else
                {
                    Console.Write("Что бы вернутся нажмите любую кнопку");
                    Console.ReadKey();
                }
            }
            //Когда много страниц
            else
            {
                int watchOfChoose;
                do
                {
                   
                    int page;

                    do
                    {
                        Console.Write($"Введите номер страницы...(1-{numberPages})"); 
                        if(!mode)Console.Write(" (0 - выйти из просмотра)");
                        resultParse = int.TryParse(Console.ReadLine(), out page);
                        if (!resultParse || page < 0 || page > numberPages)
                        {
                            Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                            resultParse = false;
                        }

                    } while (!resultParse);
                    if (page == 0) return null;

                    var ListShops = list((page - 1) * numbersOfItem, numbersOfItem);
                    Console.Clear();
                    for (var j = 0; j < ListShops.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListShops[j]}");
                    }

                    do
                    {
                        Console.WriteLine("1 - посмотреть еще.");
                        if (mode) Console.WriteLine("2 - Выбрать из списка");
                        if (!mode) Console.WriteLine("0 - выйти из просмотра:");

                                               
                        resultParse = int.TryParse(Console.ReadLine(), out watchOfChoose);
                        if (mode)
                        {
                            if (!resultParse || watchOfChoose > 2 || watchOfChoose < 0)
                            {
                                Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                                resultParse = false;
                            }
                        }
                        else
                        {
                            if (!resultParse || watchOfChoose > 1 || watchOfChoose < 0)
                            {
                                Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                                resultParse = false;
                            }

                        }
                        
                    } while (!resultParse);

                    if (watchOfChoose == 2) 
                    {
                        int numbItem;
                        do
                        {
                            Console.Write("Выберите номер из списка:");
                            resultParse = int.TryParse(Console.ReadLine(), out numbItem);
                            if (!resultParse || numbItem < 1 || numbItem > ListShops.Count())
                            {
                                Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                                resultParse = false;
                            }

                        } while (!resultParse);

                        return ListShops[numbItem - 1].Id;

                    }

                } while (watchOfChoose!=0);
            }
            return null;

        }




    }
}
