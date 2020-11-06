using AppBLL.VMs;
using ConsoleApp1;
using System;


namespace AppUI

{
    class Program
    {
        



        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в ИНТЕРНЕТ МАГАЗИН");
            Console.WriteLine("===================================");
            Console.WriteLine("Для продолжения нажмите любую кнопку");
            //Console.WriteLine("a".GetHashCode().ToString());
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
                            ConsoleHelper.Menu_manager();
                        }
                        while (UserVM.Role == "manager");
                        
                        break;

                    case "buyer":
                        Console.WriteLine($"Вы вошли как {UserVM.Role}");
                        do
                        {
                            ConsoleHelper.Menu_buyer();
                        }
                        while (UserVM.Role == "buyer");

                        break;

                    case "admin":
                        Console.WriteLine($"Вы вошли как {UserVM.Role}");
                        do
                        {
                            ConsoleHelper.Menu_admin();
                        }
                        while (UserVM.Role == "admin");

                        break;

                    case null:
                        ConsoleHelper.Authorization();
                        break;
                }
            }
        }

        

       
    }



}
