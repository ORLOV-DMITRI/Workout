Market market = new Market();
bool isOpen = true;
int _count = 1;

while (isOpen)
{
    Console.WriteLine("Администрация супермаркета");
    Console.WriteLine("1.Создать очередь клиентов\n2.Обслужить очередь\n3.Выйти\n4. Посмотреть очередь\n5. Очистить очередь");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Сколько клиентов будет создано : ");
            int count = Convert.ToInt32(Console.ReadLine());
            while (count != _count - 1)
            {
                Console.WriteLine("Что бы создать клиента введите его имя: ");
                string _name = Console.ReadLine();


                market.CreatClientsQueue(_name);
                _count++;
            }
            _count = 1;



            Console.WriteLine("Очередь создана, посмотреть её можно в соответсвующем меню");
            Console.ReadKey();
            Console.Clear();


            break;
        case "2":
            market.ServiceClient();
            break;
        case "3":
            isOpen = false;
            break;
        case "4":
            Console.Clear();
            market.ShowClients();
            

            Console.ReadKey();
            Console.Clear();
            break;
        case "5":
            market.Clear();
            _count = 1;
            break;
    }



}






class Market
{
    List<Product> _products = new List<Product>();
    Queue<Client> clients = new Queue<Client>();
    Random rand = new Random();

    public Market()
    {
        _products.Add(new Product("Водка", GetPriceProduct()));
        _products.Add(new Product("Пиво", GetPriceProduct()));
        _products.Add(new Product("Шпек", GetPriceProduct()));
        _products.Add(new Product("Насик", GetPriceProduct()));
        _products.Add(new Product("Орехи", GetPriceProduct()));
        _products.Add(new Product("Хлеб", GetPriceProduct()));
        _products.Add(new Product("Вода", GetPriceProduct()));
        _products.Add(new Product("Шоколадки", GetPriceProduct()));
        _products.Add(new Product("Конфеты", GetPriceProduct()));
    }
    public void Clear()
    {
        clients.Clear();
    }
    public void ShowClients()
    {

        foreach (var item in clients)
        {

            string name = item.Name;
            int money = item._money;
            List<Product> products = item._productInBasket;
            Console.Write("Клиент:  ");
            Console.Write($"Имя - {name}. Деньги - {money}\n");
            Console.WriteLine("Корзина: ");

            for (int i = 0; i < products.Count; i++)
            {

                products[i].Sho();
            }
            Console.WriteLine();


        }
        Console.WriteLine();
    }
   


    public int GetPriceProduct()
    {
        int minPrice = 100;
        int maxPrice = 200;
        int PriceProd = rand.Next(minPrice, maxPrice);
        return PriceProd;

    }
    public void CreatClientsQueue(string name)
    {
        clients.Enqueue(CreateClients(name));
    }


    public Client CreateClients(string name) // метод создания клиентов, возвращаемое тип Client. Создает рандомное количество продуктов и помещает их в список.
    {
        List<Product> products = new List<Product>();

        int MinCountProd = 3;
        int MaxCountProd = 7;
        int MinCountMoney = 400;
        int MaxCountMoney = 600;
        int CountProd = rand.Next(MinCountProd, MaxCountProd);
        int CountMoney = rand.Next(MinCountMoney, MaxCountMoney);

        for (int i = 0; i < CountProd; i++)
        {
            products.Add(_products[rand.Next(0, _products.Count - 1)]);
        }

        return new Client(products, CountMoney, name);


    }
    public void ServiceClient()
    {
        while (clients.Count > 0)
        {
            clients.Dequeue().SellProduct();
        }
    }

}
class Client
{
    public List<Product> _productInBasket;  // В конструктор класса можно вставить лист.
    public int _money;
    public string Name;

    public Client()
    {

    }
    public Client(List<Product> productInBasket, int money, string name)
    {
        _productInBasket = productInBasket;
        _money = money;
        Name = name;
    }
    public void SellProduct()
    {
        int FinalPrice = GetAllPriceBasket();
        ShowBasket();
        Console.WriteLine($"Сумма товаров - {FinalPrice}. У клиента - {Name}, на счету - {_money} Рублей");
        if (_money > FinalPrice)
        {
            Console.WriteLine($"Клиент - {Name}, оплатил товары на сумму {FinalPrice}, остаток на счету {_money - FinalPrice}. Клиент покинул магазин");
        }
        else
        {
            
            DelProduct();
            FinalPrice = GetAllPriceBasket();
            if(_money > FinalPrice)
            {
                Console.WriteLine($"Клиент - {Name}, оплатил товары на сумму {FinalPrice}, остаток на счету {_money - FinalPrice}. Клиент покинул магазин");
            }
           
        }
       
        Console.WriteLine();
        Console.ReadKey();
        Console.Clear();

    }
    public void DelProduct()
    {
        while(GetAllPriceBasket() > _money)
        {
            RemovProduct();
        }
    }
    public void RemovProduct()
    {
        Random rand = new Random();
        int index = rand.Next(0, _productInBasket.Count);
        Product productdel = _productInBasket[index];
        Console.WriteLine($"Клиент отказался от товара {productdel.Name}, стоймостью {productdel.Price}");
        _productInBasket.Remove(productdel);


    }
    public int GetAllPriceBasket()
    {
        int finalPrice = 0;
        foreach (var item in _productInBasket)
        {
            finalPrice += item.Price;
        }
        return finalPrice;
    }
    public void ShowBasket()
    {
        Console.WriteLine("Корзина клиента: " + Name + "\n");
        foreach (var item in _productInBasket )
        {
            Console.WriteLine("Продукт - [" + item.Name + ". Цена - " + item.Price + "]");
        }
        Console.WriteLine();
    }



}
class Product
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
    public void Sho()
    {
        Console.Write($"[ Товар - {Name} | Цена - {Price}]\n");
       

    }

}