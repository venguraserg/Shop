using AppBLL.Interfaces;
using AppBLL.Services;
using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleApp1
{
    public static class ConsoleHelper
    {
        static IShopService shopService = new ShopService();
        static IBuyerService buyerService = new BuyerService();
        static IManagerService managerService = new ManagerService();
        static IUnitService unitService = new UnitService();
        static IProductService productService = new ProductService();
        static IShopCartService shopCartService = new ShopCartService(); 

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
        /// Метод ввода логина с проверкой предидущей регистрации
        /// </summary>
        /// <param name="searchMethod"></param>
        /// <returns></returns>
        public static string CheckMatchUser(Func<string, bool> searchMethod)
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
        /// Метод добавления покупателя, 
        /// mode - True входим по новым пользователем в систему
        /// </summary>
        /// <param name="mode"></param>
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
                temp_id = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);
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
                temp_unit = EntitySelection<UnitVM>(unitService.GetPageInfo, unitService.GetNumbOfItem(), 10);
            }
            while (!temp_unit.HasValue);
            
            Console.Write("\nЦена:  ");
            var temp_price = decimal.Parse(Console.ReadLine());
            Console.Write("\nВыберите магазин:  ");
            Guid? temp_id;
            do
            {
                temp_id = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);
            }
            while (!temp_id.HasValue);
            
            productService.AddProduct(temp_name_prod, temp_descr_prod, temp_amount_prod, temp_price, temp_id.Value, temp_unit.Value);
            Console.Write("\nДанные внесены успешно, для продолжения нажмите любую клавишу ...");
            Console.ReadKey();
        }
       

        //********************************************************

        public static void Menu_buyer()
        {
            Console.Clear();
            Console.WriteLine($"{UserVM.Role} пожалуйста выбеите пункт МЕНЮ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1 => Посмотреть список продуктов в каталоге\n" +
                              "2 => Посмотреть подробную информацию о продукте\n" +
                              "3 => Посмотреть корзину\n" +
                              "4\n" +
                              "5\n" +
                              "6 => Выйти из аккаунта\n" +
                              "7 => Выйти из программы");
            if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
            switch (key)
            {
                case 1:
                    EntityView<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10);
                    break;
                case 2:
                    Console.Clear();
                    var temp_product = productService.GetProduct(EntitySelection<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10).Value);
                    Console.Clear();
                    Console.WriteLine("***** ДЕТАЛЬНОЕ ОПИСАНИЕ ПРОДУКТА *****");
                    Console.WriteLine($"Имя:        {temp_product.Name}\n" +
                                      $"Описание:   {temp_product.Description}\n" +
                                      $"Цена:       {temp_product.Price}\n" +
                                      $"Количество: {temp_product.Amount} {temp_product.Unit}\n" +
                                      $"Магазин:    {temp_product.Shop}\n" +
                                      $"\n\nВернутмя назад - 0\n" +
                                      $"Добавить товар в корзину - 1");
                    int key1;
                    do
                    {
                        if (!int.TryParse(Console.ReadLine(), out  key1)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                        if (!(key1 == 1 || key1 == 0)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    }
                    while (!(key1 == 1 || key1 == 0));
                    
                    switch (key1)
                    {
                        case 0:
                            break;
                        case 1:
                            Console.WriteLine("Выберте количество");
                            float temp_amount;
                            bool parse_result;
                            do
                            {
                                parse_result = float.TryParse(Console.ReadLine(), out temp_amount);
                                if (!parse_result) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");

                            } while (!parse_result);

                            shopCartService.AddInShopCart(shopCartService.GetShopCart(UserVM.Id), temp_product.Id, temp_amount, temp_product.Price);

                            break;
                    }
                    break;
                    // просмотр корзины
                case 3:
                    Console.Clear();
                    //EntityView<ShopCartItemVM>(shopCartService.GetPageInfo, shopCartService.GetNumbOfItem(), 10);
                    var shopCartItems = shopCartService.GetAllItems(shopCartService.GetShopCart(UserVM.Id));
                    Console.WriteLine("Ваша Корзина");
                    foreach (var i in shopCartItems) 
                    {
                        Console.WriteLine(i);
                    }
                    Console.ReadKey();
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
                              "2 => Редактировать продукт\n" +
                              "3 => Удалить продукт\n" +
                              "4 => Посмотреть список продуктов в каталоге\n" +
                              "5 => Посмотреть подробную информацию о продукте\n" +
                              "9 => Выйти из аккаунта\n" +
                              "10 => Выйти из программы");
            if (!int.TryParse(Console.ReadLine(), out int key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
            switch (key)
            {
                case 1:

                    AddProduct();

                    break;
                case 2:
                    var tempProdId = EntitySelection<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10);

                    var editableProduct = productService.GetProduct(tempProdId.Value);

                    Console.Clear();
                    Console.WriteLine(editableProduct);
                    Console.WriteLine("********************************************************");
                    Console.WriteLine($"Продукт: {editableProduct.Name}");
                    Console.Write("Введите новое название: ");
                    var edit_name = Console.ReadLine();
                    Console.WriteLine($"Старое описание: {editableProduct.Description}");
                    Console.Write("Введите новое описание: ");
                    var edit_description = Console.ReadLine();
                    Console.WriteLine($"Старая цена: {editableProduct.Price}");
                    Console.Write("Введите новую цену: ");
                    var edit_price = decimal.Parse(Console.ReadLine());
                    Console.WriteLine($"Старое количество: {editableProduct.Amount}");
                    Console.Write("Введите количество: ");
                    var edit_amount = float.Parse(Console.ReadLine());
                    var edit_unitId = EntitySelection<UnitVM>(unitService.GetPageInfo, unitService.GetNumbOfItem(), 10);
                    var edit_shopId = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);

                    productService.UpdateProduct(tempProdId.Value, edit_name, edit_description, edit_amount, edit_price, edit_unitId.Value, edit_shopId.Value);

                    Console.WriteLine("Данные успешно изменены, для продолжения нажмите любую клавишу");
                    Console.ReadKey();
                    break;
                case 3:
                    tempProdId = EntitySelection<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10);

                    if (productService.DeleteProduct(tempProdId.Value))
                    {
                        Console.WriteLine("Удаление успешно...");
                    }
                    else
                    {
                        Console.WriteLine("Удаление не удалось...");
                    }
                    Console.WriteLine("Для продолжения нажмите любую кнопку...");
                    Console.ReadKey();
                    break;
                case 4:
                    EntityView<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10);
                    break;
                case 5:
                    var temp_product = productService.GetProduct(EntitySelection<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10).Value);
                    Console.Clear();
                    Console.WriteLine("***** ДЕТАЛЬНОЕ ОПИСАНИЕ ПРОДУКТА *****");
                    Console.WriteLine($"Имя:        {temp_product.Name}\n" +
                                      $"Описание:   {temp_product.Description}\n" +
                                      $"Цена:       {temp_product.Price}\n" +
                                      $"Количество: {temp_product.Amount} {temp_product.Unit}\n" +
                                      $"Магазин:    {temp_product.Shop}\n" +
                                      $"\n\nДля продолжения нажмите любую клавишу...");
                    Console.ReadKey();
                    break;
                case 6:
                    break;




                case 9:
                    UserVM.Id = Guid.Empty;
                    UserVM.Role = null;
                    break;
                //QUIT
                case 10:
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
                                      "2.5 => Просмотреть список единц измерения\n" +
                                      "2.6 => Вернутся назад\n");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    switch (key)
                    {
                        //Просмотреть список менеджеров
                        case 1:

                            EntityView<ManagerVM>(managerService.GetPageInfo, managerService.GetNumbOfItem(), 10);
                            break;
                        //Просмотреть список покупателей
                        case 2:
                            EntityView<BuyerVM>(buyerService.GetPageInfo, buyerService.GetNumbOfItem(), 10);
                            break;
                        //Просмотреть список магазинов
                        case 3:

                            EntityView<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);
                            break;
                        //Просмотреть список продуктов
                        case 4:
                            EntityView<ProductVM>(productService.GetPageInfo,  productService.GetNumbOfItem(), 10);
                            break;
                        //Просмотреть список единиц измерения
                        case 5:
                            EntityView<UnitVM>(unitService.GetPageInfo, unitService.GetNumbOfItem(), 10);
                            break;
                        //Назад
                        case 6:

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
                    Console.Clear();
                    Console.WriteLine("Пожалуйста выбеите пункт МЕНЮ              ");
                    Console.WriteLine("===========================================");
                    Console.WriteLine("3.1 => Редактировать менеджера\n" +
                                      "3.2 => Редактировать покупателя\n" +
                                      "3.3 => Редактировать магазин\n" +
                                      "3.4 => Редактировать продукт\n" +
                                      "3.5 => Редактировать единцу измерения\n" +
                                      "3.6 => Вернутся назад\n");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    switch (key)
                    {
                        //Редактировать менеджера
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Меню редактирования МЕНЕДЖЕРА\n" +
                                              "Выберите необходимого менеджера по номеру из списка.\n" +
                                              "Для продолжения нажмите любую клавишу...");
                            Console.ReadLine();
                            var tempManagerId = EntitySelection<ManagerVM>(managerService.GetPageInfo, managerService.GetNumbOfItem(), 10);

                            var editableManager = managerService.GetManager(tempManagerId.Value);

                            Console.Clear();
                            Console.WriteLine(editableManager);
                            Console.WriteLine("********************************************************");
                            Console.WriteLine($"Старый LOGIN: {editableManager.Login}");
                            Console.Write("Введите новый LOGIN: ");
                            var edit_login = CheckMatchUser(managerService.SearchManager);
                            Console.Write("Введите новый пароль: ");
                            var edit_pass_hash=Console.ReadLine().GetHashCode().ToString();
                            Console.WriteLine($"Старое имя : {editableManager.Name}");
                            Console.Write("Введите новое имя: ");
                            var edit_name = Console.ReadLine();
                            Console.WriteLine($"Старая фамилия : {editableManager.Surname}");
                            Console.Write("Введите новую фамилию: ");
                            var edit_surname = Console.ReadLine();
                            Console.WriteLine($"Старый номер телефона: {editableManager.PhoneNumber}");
                            Console.Write("Введите новый номер телефона: ");
                            var edit_phonenumb = Console.ReadLine();
                            Console.WriteLine($"Старый магазин: {editableManager.Shop}");
                            Console.Write("Выберите новый магазин: ");
                            var edit_shopId = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);
                            managerService.UpdateManager(tempManagerId.Value, edit_login, edit_pass_hash, edit_name, edit_surname, edit_phonenumb, edit_shopId.Value);
                            Console.WriteLine("Данные успешно изменены, для продолжения нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        //Редактировать покупателя
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Меню редактирования ПОКУПАТЕЛЯ\n" +
                                              "Выберите необходимого менеджера по номеру из списка.\n" +
                                              "Для продолжения нажмите любую клавишу...");
                            Console.ReadLine();
                            var tempBuyerId = EntitySelection<BuyerVM>(buyerService.GetPageInfo, buyerService.GetNumbOfItem(), 10);

                            var editableBuyer = buyerService.GetBuyer(tempBuyerId.Value);

                            Console.Clear();
                            Console.WriteLine(editableBuyer);
                            Console.WriteLine("********************************************************");
                            Console.WriteLine($"Старый LOGIN: {editableBuyer.Login}");
                            Console.Write("Введите новый LOGIN: ");
                            edit_login = CheckMatchUser(buyerService.SearchBuyer);
                            Console.Write("Введите новый пароль: ");
                            edit_pass_hash = Console.ReadLine().GetHashCode().ToString();
                            Console.WriteLine($"Старое имя : {editableBuyer.Name}");
                            Console.Write("Введите новое имя: ");
                            edit_name = Console.ReadLine();
                            Console.WriteLine($"Старая фамилия : {editableBuyer.Surname}");
                            Console.Write("Введите новую фамилию: ");
                            edit_surname = Console.ReadLine();
                            Console.WriteLine($"Старый номер телефона: {editableBuyer.PhoneNumber}");
                            Console.Write("Введите новый номер телефона: ");
                            edit_phonenumb = Console.ReadLine();
                            Console.WriteLine($"Старый адресс: {editableBuyer.Address}");
                            Console.Write("Выберите новый магазин: ");
                            var edit_address = Console.ReadLine();
                            Console.WriteLine($"Старая дата рождения: {editableBuyer.DateOfBirth}");
                            Console.Write("Введите новую дату рождения: ");
                            var edit_data_birth = InputDate();
                            buyerService.UpdateBuyer(tempBuyerId.Value, edit_login, edit_pass_hash, edit_name, edit_surname, edit_phonenumb, edit_address, edit_data_birth);
                            Console.WriteLine("Данные успешно изменены, для продолжения нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        //Редактировать магазин
                        case 3:

                            var tempShopId = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);

                            var editableShop = shopService.GetShop(tempShopId.Value);

                            Console.Clear();
                            Console.WriteLine(editableShop);
                            Console.WriteLine("********************************************************");
                            Console.WriteLine($"Старое название: {editableShop.Name}");
                            Console.Write("Введите новое название: ");
                            edit_name = Console.ReadLine();
                            Console.WriteLine($"Старое описание: {editableShop.Description}");
                            Console.Write("Введите новое описание: ");
                            var edit_description = Console.ReadLine();
                            shopService.UpdateShop(tempShopId.Value, edit_name, edit_description);
                            Console.WriteLine("Данные успешно изменены, для продолжения нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        //Редактировать продукт
                        case 4:
                            var tempProdId = EntitySelection<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10);

                            var editableProduct = productService.GetProduct(tempProdId.Value);

                            Console.Clear();
                            Console.WriteLine(editableProduct);
                            Console.WriteLine("********************************************************");
                            Console.WriteLine($"Продукт: {editableProduct.Name}");
                            Console.Write("Введите новое название: ");
                            edit_name = Console.ReadLine();
                            Console.WriteLine($"Старое описание: {editableProduct.Description}");
                            Console.Write("Введите новое описание: ");
                            edit_description = Console.ReadLine();
                            Console.WriteLine($"Старая цена: {editableProduct.Price}");
                            Console.Write("Введите новую цену: ");
                            var edit_price = decimal.Parse(Console.ReadLine());
                            Console.WriteLine($"Старое количество: {editableProduct.Amount}");
                            Console.Write("Введите количество: ");
                            var edit_amount = float.Parse(Console.ReadLine()); 
                            var edit_unitId = EntitySelection<UnitVM>(unitService.GetPageInfo, unitService.GetNumbOfItem(), 10);
                            edit_shopId = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);

                            productService.UpdateProduct(tempProdId.Value, edit_name, edit_description, edit_amount, edit_price, edit_unitId.Value, edit_shopId.Value);

                            Console.WriteLine("Данные успешно изменены, для продолжения нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        //Редактировать единцу измерения
                        case 5:
                            var tempUnitId = EntitySelection<UnitVM>(unitService.GetPageInfo, unitService.GetNumbOfItem(), 10);
                            var editableUnit = unitService.GetUnit(tempUnitId.Value);

                            Console.Clear();
                            Console.WriteLine(editableUnit);
                            Console.WriteLine("********************************************************");
                            Console.WriteLine($"Единица измерения: {editableUnit.Name}");
                            Console.Write("Введите новое название: ");
                            edit_name = Console.ReadLine();
                            unitService.UpdateUnit(tempUnitId.Value, edit_name);
                            Console.WriteLine("Данные успешно изменены, для продолжения нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        //Назад
                        case 6:

                            break;
                        default://incorrect input
                            Console.WriteLine("------НЕКОРРЕКТНЫЙ ВВОД, НАЖМИТЕ ЛЮБУЮ КЛАВИШУ-----");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                    break;
                //УДАЛИТЬ
                case 4:
                    Console.Clear();
                    Console.WriteLine("Пожалуйста выбеите пункт МЕНЮ              ");
                    Console.WriteLine("===========================================");
                    Console.WriteLine("4.1 => Удалить менеджера\n" +
                                      "4.2 => Удалить покупателя\n" +
                                      "4.3 => Удалить магазин\n" +
                                      "4.4 => Удалить продукт\n" +
                                      "4.5 => Вернутся назад\n");
                    if (!int.TryParse(Console.ReadLine(), out key)) Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                    switch (key)
                    {
                        case 1:
                            var tempManagerId = EntitySelection<ManagerVM>(managerService.GetPageInfo, managerService.GetNumbOfItem(), 10);
                            if (managerService.DeleteManager(tempManagerId.Value))
                            {
                                Console.WriteLine("Удаление успешно...");
                            }
                            else
                            {
                                Console.WriteLine("Удаление не удалось...");
                            }
                            Console.WriteLine("Для продолжения нажмите любую кнопку...");
                            Console.ReadKey();

                            break;
                        case 2:
                            var tempBuyerId = EntitySelection<BuyerVM>(buyerService.GetPageInfo, buyerService.GetNumbOfItem(), 10);
                            
                            if (buyerService.DeleteBuyer(tempBuyerId.Value))
                            {
                                Console.WriteLine("Удаление успешно...");
                            }
                            else
                            {
                                Console.WriteLine("Удаление не удалось...");
                            }
                            Console.WriteLine("Для продолжения нажмите любую кнопку...");
                            Console.ReadKey();
                            break;
                        case 3:
                            var tempShopId = EntitySelection<ShopVM>(shopService.GetPageInfo, shopService.GetNumbOfItem(), 10);
                            
                            if (shopService.DeleteShop(tempShopId.Value))
                            {
                                Console.WriteLine("Удаление успешно...");
                            }
                            else
                            {
                                Console.WriteLine("Удаление не удалось...");
                            }
                            Console.WriteLine("Для продолжения нажмите любую кнопку...");
                            Console.ReadKey();

                            break;
                        case 4:
                            var tempProdId = EntitySelection<ProductVM>(productService.GetPageInfo, productService.GetNumbOfItem(), 10);
                            
                            if (productService.DeleteProduct(tempProdId.Value))
                            {
                                Console.WriteLine("Удаление успешно...");
                            }
                            else
                            {
                                Console.WriteLine("Удаление не удалось...");
                            }
                            Console.WriteLine("Для продолжения нажмите любую кнопку...");
                            Console.ReadKey();
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
        
        //*********************************************************


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
        
        //**********************************************************
        /// <summary>
        /// Метод просмотра сущностей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="totalCount"></param>
        /// <param name="numbersOfItem"></param>
        public static void EntityView<T>(Func<int,int,List<T>>list, int totalCount, int numbersOfItem = 10) where T : BaseVMmodel
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
                
                Console.Write("Что бы вернутся нажмите любую кнопку");
                Console.ReadKey();
                
            }
            //Когда много страниц
            else
            {
                int watch;
                do
                {
                   
                    int page;

                    do
                    {
                        Console.Write($"Введите номер страницы...(1-{numberPages})"); 
                        Console.Write(" (0 - выйти из просмотра)");
                        resultParse = int.TryParse(Console.ReadLine(), out page);
                        if (!resultParse || page < 0 || page > numberPages)
                        {
                            Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                            resultParse = false;
                        }

                    } while (!resultParse);
                    if (page == 0) break;

                    var ListShops = list((page - 1) * numbersOfItem, numbersOfItem);
                    //Console.Clear();
                    for (var j = 0; j < ListShops.Count(); j++)
                    {
                        Console.WriteLine($"{j + 1} -> {ListShops[j]}");
                    }

                    do
                    {
                        Console.WriteLine("1 - посмотреть еще.");
                        Console.WriteLine("0 - выйти из просмотра:");
                       
                        resultParse = int.TryParse(Console.ReadLine(), out watch);
                        if (!resultParse || watch > 1 || watch < 0)
                        {
                            Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                            resultParse = false;
                        }
                    } while (!resultParse);

                } while (watch!=0);
            }
            

        }
        /// <summary>
        /// Метод ВЫБОРА сущности из списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="totalCount"></param>
        /// <param name="numbersOfItem"></param>
        /// <returns></returns>
        public static Guid? EntitySelection<T>(Func<int, int, List<T>> list, int totalCount, int numbersOfItem = 10) where T : BaseVMmodel
        {
            var numberPages = (int)Math.Ceiling((double)totalCount / numbersOfItem);
            bool resultParse;

           // Console.Clear();
            Console.WriteLine($"Список состоит из {numberPages} страниц по {numbersOfItem} строк");
            if (numberPages <= 1)
            {
                var ListShops = list(0, numbersOfItem);

                for (var j = 0; j < ListShops.Count(); j++)
                {
                    Console.WriteLine($"{j + 1} -> {ListShops[j]}");
                }
                
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
            //Когда много страниц
            else
            {
                int select;
                do
                {

                    int page;

                    do
                    {
                        Console.Write($"Введите номер страницы...(1-{numberPages})");
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
                        Console.WriteLine("\n\n1 - посмотреть еще.");
                        Console.WriteLine("2 - Выбрать из списка");
                        

                        resultParse = int.TryParse(Console.ReadLine(), out select);
                        
                        if (!resultParse || select > 2 || select < 0)
                        {
                            Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                            resultParse = false;
                        }
                        
                    } while (!resultParse);

                    if (select == 2)
                    {
                        int numbItem;
                        do
                        {
                            Console.Write("\nВыберите номер из списка:");
                            resultParse = int.TryParse(Console.ReadLine(), out numbItem);
                            if (!resultParse || numbItem < 1 || numbItem > ListShops.Count())
                            {
                                Console.WriteLine("-----------ВВЕДЕНЫ НЕДОПУСТИМЫЕ СИМВОЛЫ------------");
                                resultParse = false;
                            }

                        } while (!resultParse);

                        return ListShops[numbItem - 1].Id;

                    }

                } while (select != 0);
            }
            return null;

        }




    }
}
