using System;
using OrderSystem.Domain.Delivery;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.ValueObjects;

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Создание нового заказа ---");
        Console.Write("Номер заказа: ");
        int orderNumber = int.Parse(Console.ReadLine()!);

        // Выбор типа доставки
        Delivery delivery = ChooseDelivery();

        var order = new Order<Delivery, int>(orderNumber, delivery, "Новый заказ");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("3. Показать все товары");
            Console.WriteLine("4. Показать адрес доставки");
            Console.WriteLine("5. Показать общую сумму");
            Console.WriteLine("6. Изменить статус заказа");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    AddProduct(order);
                    break;
                case "2":
                    RemoveProduct(order);
                    break;
                case "3":
                    ShowProducts(order);
                    break;
                case "4":
                    order.DisplayAddress();
                    break;
                case "5":
                    Console.WriteLine($"Общая сумма: {order.TotalPrice}");
                    break;
                case "6":
                    ChangeStatus(order);
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    static Delivery ChooseDelivery()
    {
        Console.WriteLine("Выберите тип доставки:");
        Console.WriteLine("1. Домашняя (HomeDelivery)");
        Console.WriteLine("2. Пункт выдачи (PickPointDelivery)");
        Console.WriteLine("3. Магазин (ShopDelivery)");
        Console.Write("Ваш выбор: ");

        string choice = Console.ReadLine()!;
        Delivery delivery;

        switch (choice)
        {
            case "1":
                var home = new HomeDelivery();
                Console.WriteLine("Введите адрес:");
                home.SetAddress(InputAddress());
                delivery = home;
                break;

            case "2":
                var pick = new PickPointDelivery();
                Console.Write("Компания пункта выдачи: ");
                pick.PickupCompany = Console.ReadLine()!;
                Console.Write("ID пункта выдачи: ");
                pick.PickupPointId = Console.ReadLine()!;
                pick.SetAddress(InputAddress());
                delivery = pick;
                break;

            case "3":
                var shop = new ShopDelivery();
                Console.Write("ID магазина: ");
                shop.ShopId = Console.ReadLine()!;
                shop.SetAddress(InputAddress());
                delivery = shop;
                break;

            default:
                Console.WriteLine("Неверный выбор, будет HomeDelivery по умолчанию");
                var defaultHome = new HomeDelivery();
                defaultHome.SetAddress(InputAddress());
                delivery = defaultHome;
                break;
        }

        return delivery;
    }

    static Address InputAddress()
    {
        Console.Write("Страна: ");
        string country = Console.ReadLine()!;
        Console.Write("Город: ");
        string city = Console.ReadLine()!;
        Console.Write("Улица: ");
        string street = Console.ReadLine()!;
        Console.Write("Дом: ");
        string house = Console.ReadLine()!;
        Console.Write("Квартира (если есть): ");
        string? apt = Console.ReadLine();
        return new Address(country, city, street, house, string.IsNullOrWhiteSpace(apt) ? null : apt);
    }

    static void AddProduct(Order<Delivery, int> order)
    {
        Console.Write("ID товара: ");
        int id = int.Parse(Console.ReadLine()!);
        Console.Write("Название товара: ");
        string name = Console.ReadLine()!;
        Console.Write("Цена: ");
        decimal price = decimal.Parse(Console.ReadLine()!);
        Console.Write("Количество: ");
        int qty = int.Parse(Console.ReadLine()!);

        var product = new Product(id, name, price);
        order.AddItem(product, qty);
        Console.WriteLine("Товар добавлен!");
    }

    static void RemoveProduct(Order<Delivery, int> order)
    {
        Console.Write("ID товара для удаления: ");
        int id = int.Parse(Console.ReadLine()!);
        try
        {
            order.RemoveItem(id);
            Console.WriteLine("Товар удалён!");
        }
        catch
        {
            Console.WriteLine("Товар с таким ID не найден.");
        }
    }

    static void ShowProducts(Order<Delivery, int> order)
    {
        Console.WriteLine("Товары в заказе:");
        foreach (var item in order.Items)
        {
            Console.WriteLine($"ID: {item.Product.Id}, Название: {item.Product.Name}, Цена: {item.Product.Price}, Кол-во: {item.Quantity}, Сумма: {item.TotalPrice}");
        }
    }

    static void ChangeStatus(Order<Delivery, int> order)
    {
        Console.WriteLine("Выберите статус:");
        foreach (var s in Enum.GetValues(typeof(OrderStatus)))
        {
            Console.WriteLine($"{(int)s}. {s}");
        }

        int status = int.Parse(Console.ReadLine()!);
        if (Enum.IsDefined(typeof(OrderStatus), status))
        {
            order.SetStatus((OrderStatus)status);
            Console.WriteLine("Статус обновлён!");
        }
        else
        {
            Console.WriteLine("Неверный статус");
        }
    }
}
