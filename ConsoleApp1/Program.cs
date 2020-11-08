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
            Console.ReadKey();
            Console.Clear();

          //Login/registration buyer/quit
            
            while (true)
            {
                switch (UserVM.Role)
                {
                    case "manager":
                        do
                        {
                            ConsoleHelper.Menu_manager();
                        }
                        while (UserVM.Role == "manager");
                        
                        break;

                    case "buyer":
                       do
                        {
                            ConsoleHelper.Menu_buyer();
                        }
                        while (UserVM.Role == "buyer");

                        break;

                    case "admin":
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
