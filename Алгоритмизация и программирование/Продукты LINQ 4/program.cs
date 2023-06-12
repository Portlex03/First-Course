class Product
{
    public int id;
    public string name;

    public Product(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

class Provider
{
    public int id;
    public string name;

    public Provider(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

class Delivery
{
    public int id_of_provider;
    public int id_of_product;
    public string date;
    public int count;

    public Delivery(int id_of_provider, int id_of_product,string date, int count)
    {
        this.id_of_provider = id_of_provider;
        this.id_of_product = id_of_product;
        this.date = date;
        this.count = count;
    }
}

class Program
{
    static void Main()
    {
        var products = new List<Product>()
        {
            new Product(1, "Сметана"),
            new Product(2, "Лаваш"),
            new Product(3, "Молоко"),
            new Product(4, "Капуста"),
            new Product(5, "Колбаса"),
            new Product(6, "Картошка"),
            new Product(7, "Хлопья")
        };

        var providers = new List<Provider>()
        {
            new Provider(1, "Поставщик А"),
            new Provider(2, "Поставщик Б"),
            new Provider(3, "Поставщик В")
        };

        var delivers = new List<Delivery>()
        {
            new Delivery(1,2,"16.05",50),
            new Delivery(3,6,"12.03", 51),
            new Delivery(1,6,"28.05",30),
            new Delivery(2,7,"23.04",78),
            new Delivery(3,2,"2.07", 32),
            new Delivery(2,3,"1.09",100),

        };

        //группировка по дате
        var group_for_date = from delivery in delivers
                             group delivery by delivery.date into g
                             select new
                             {
                                 date = g.Key,
                                 provider = from provider in providers
                                            where provider.id == g.First().id_of_provider
                                            select provider.name,
                                 product = from product in products
                                           where product.id == g.First().id_of_product
                                           select product.name,
                                 g.First().count
                             };

        foreach (var item in group_for_date)
        {
            Console.WriteLine($"Дата: {item.date}");
            Console.WriteLine($"Поставщик: {item.provider.First()}");
            Console.WriteLine($"Товар: {item.product.First()}");
            Console.WriteLine($"Количество: {item.count}\n");
        }

        //товар и его поставщики
        var products_query = from delivery in delivers
                             group delivery by delivery.id_of_product into g
                             select new
                             {
                                 name = from product in products
                                        where product.id == g.Key
                                        select product.name,
                                 providers = from p in g
                                             join provider in providers
                                             on p.id_of_provider equals provider.id
                                             select provider.name
                             };

        foreach (var product in products_query)
        {
            Console.WriteLine($"Продукт *{product.name.First()}*, поставщики:"); ;
            foreach (var provider in product.providers)
            {
                Console.WriteLine(provider);
            }
            Console.WriteLine();
        }

        // поставщик и какие товары он поставил
        var providers_query = from delivery in delivers
                              group delivery by delivery.id_of_provider into g
                              select new
                              {
                                  name = from provider in providers
                                         where provider.id == g.Key
                                         select provider.name,
                                  products = from item in g
                                             join product in products
                                             on item.id_of_product equals product.id
                                             select product.name
                              };
        foreach(var provider in providers_query)
        {
            Console.WriteLine($"Поставщик *{provider.name.First()}*, Продукты:");
            foreach(var product in provider.products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
        }
    }
}
