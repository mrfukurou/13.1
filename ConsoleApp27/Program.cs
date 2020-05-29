using System;
using System.IO;
using System.Collections.Generic;


namespace ConsoleApp27
{
 public abstract class Tovar
    {
        public abstract void info();
        public abstract bool actual();
    }
    public class Product : Tovar

    {
        private string name;
        private string price;
        private DateTime datepr;
        private DateTime srok;

        public Product(string name1, string price1, DateTime datepr1, DateTime srok1)
        {
            name = name1;
            price = price1;
            datepr = datepr1;
            srok = srok1;
        }

        public override void info()
        {
            Console.WriteLine();
            Console.WriteLine($"Название:{name}\nЦена:{price}\nДата производства:{datepr.ToShortDateString()}\nСрок годности:{srok.ToShortDateString()}");
        }

        public override bool actual()
        {
            if (srok > DateTime.Now) return true;
            else return false;
        }
    }

    public class Part : Tovar
    {
        private string name;
        private string price;
        private int number;
        private DateTime datepr;
        private DateTime srok;

        public Part(string name1, string price1, int number1, DateTime datepr1, DateTime srok1)
        {
            name = name1;
            price = price1;
            number = number1;
            datepr = datepr1;
            srok = srok1;
        }

        public override void info()
        {
            Console.WriteLine();
            Console.WriteLine($"Название:{name}\nЦена:{price}\nКоличество:{number}\nДата производства:{datepr.ToShortDateString()}\nСрок годности:{srok.ToShortDateString()}");
        }

        public override bool actual()
        {
            if (srok > DateTime.Now) return true;
            else return false;
        }
    }

    class Compl : Tovar
    {
        private string name;
        private string price;
        private string component;

        public Compl() { }

        public Compl(string name1, string price1, string component1)
        {
            name = name1;
            price = price1;
            component = component1;
        }

        public override void info()
        {
            Console.WriteLine();
            Console.WriteLine($"Название комплекта: {name}\nЦена: {price}\nПеречень продуктов:{component}");
        }
        public override bool actual() => true;
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите путь к файлу: ");
                string fileName = Console.ReadLine();
                List<Tovar> product_list = new List<Tovar>();
                string[] line = File.ReadAllLines(fileName);
                for (int i = 0; i < line.Length; i++)
                {
                    string[] position = line[i].Split(';');
                    if (position.Length == 4)
                    {
                        Product product = new Product(position[0].ToString(), position[1].ToString(), Convert.ToDateTime(position[2]), Convert.ToDateTime(position[3]));
                        product_list.Add(product);
                    }
                    else
                    {
                        if (position.Length == 5)
                        {
                            Part part = new Part(position[0], position[1], Convert.ToInt32(position[2]), Convert.ToDateTime(position[3]), Convert.ToDateTime(position[4]));
                            product_list.Add(part);
                        }
                        if (position.Length == 3)
                        {
                            Compl compl = new Compl(position[0], position[1], position[2]);
                            product_list.Add(compl);
                        }
                    }
                }

                foreach (Tovar var in product_list)
                {
                    var.info();
                }

                List<Tovar> not_actual = new List<Tovar>();
                int count = 0;
                foreach (Tovar var in product_list)
                {
                    if (!var.actual()) count++;
                }

                if (count != 0)
                {
                    foreach (Tovar var in product_list)
                    {
                        if (!var.actual()) not_actual.Add(var);
                    }
                    Console.WriteLine("____________________________________________"+"\nПросроченные товары:");
                    foreach (Tovar var in not_actual)
                    {
                        var.info();
                    }
                }
                else 
                {
                    Console.WriteLine();
                    Console.WriteLine("Просроченных товаров нет!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}