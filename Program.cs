using System.Xml.Linq;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Skapa ett nytt objekt av Restaurant-klassen.
            Resturant McD = new Resturant();
    
            //2. Lägg till fyra olika rätter i menyn.
            MenuItems pasta = new MenuItems("pasta", 135);
            MenuItems nuggets = new MenuItems("nuggets", 119);
            MenuItems kebab = new MenuItems("kebab", 145);
            MenuItems bigmac = new MenuItems("bigmac", 95);
    
            McD.AddToMenu(pasta);
            McD.AddToMenu(nuggets);
            McD.AddToMenu(kebab);
            McD.AddToMenu(bigmac);
    
            //3. Skriv ut menyn.
            McD.ShowMenu();
    
            //4. Skapa 3 st nya ordrar, med minst två olika rätter i varje,
            //och lägg till dem i orderkön (du kan hitta på bordsnummer för beställningarna).
            Order bord1 = new Order();
            bord1.Add(pasta);
            bord1.Add(nuggets);
            bord1.Add(nuggets);
    
    
            Order bord2 = new Order();
            bord2.Add(pasta);
            bord2.Add(kebab);
            bord2.Add(nuggets);
            bord2.Add(kebab);
            bord2.Add(bigmac);
    
            Order bord3 = new Order();
            bord3.Add(pasta);
            bord3.Add(bigmac);
            bord3.Add(bigmac);
            bord3.Add(bigmac);
    
            McD.CreateOrder(bord1);
            McD.CreateOrder(bord2);
            McD.CreateOrder(bord3);
    
            //5. Visa alla aktuella ordrar.
            McD.ShowOrder();
    
            //6. Visa antalet ordrar i kön.
            McD.ShowOrderCount();
    
            //7. Visa nästa order på kö.
            McD.ShowNextOrder();
    
            //8. Hantera en order.
            McD.HandleOrder();
    
            //9. Visa antalet ordrar i kön.
            McD.ShowOrderCount();
    
            //10 Lägg till en ny order.
            Order bord4 = new Order();
            bord4.Add(pasta);
            bord4.Add(pasta);
            bord4.Add(kebab);
            bord4.Add(bigmac);
    
            McD.CreateOrder(bord4);
            //11. Visa antalet ordrar i kön.
            McD.ShowOrder();
    
            //12. Hantera två ordrar.
            McD.HandleOrder();
            McD.HandleOrder();
    
            //13. Visa antalet ordrar i kön.
            McD.ShowOrderCount();
    
            //14. Visa nästa order på kö.
            McD.ShowNextOrder();
    
            //15. Hantera en order.
            McD.HandleOrder();
    
            //16. Visa antalet ordrar i kön.
            McD.ShowOrderCount();
    
            Console.ReadLine();
    
        }
    }
    
    class MenuItems
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    
        public MenuItems(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    
        public void Show()
        {
            Console.Write($"{Name} - {Price:C}\n");
        }
    
    }
    
    class Resturant
    {
        public List<MenuItems> Menu = new List<MenuItems>();
        public Queue<Order> CurrentOrders = new Queue<Order>();
    
        public void AddToMenu(MenuItems menuItem)
        {
            Console.WriteLine();
            Console.WriteLine($"{menuItem.Name} was added to the menu. ");
            Menu.Add(menuItem);
        }
        public void CreateOrder(Order order)
        {
            Console.WriteLine();
            Console.WriteLine($"Order nr {order.OrderId} was added to the queue");
            CurrentOrders.Enqueue(order);
        }
        public void HandleOrder()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            if (CurrentOrders.Count()>0)
            {
                Console.WriteLine($"Order {CurrentOrders.Peek().OrderId} is ready.");
                CurrentOrders.Dequeue();
                
            }
            else
            {
                Console.WriteLine("There are no current orders.");
            }
         }
    
        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            int count = 0;
            Header(" Menu");
            foreach(var m in Menu)
            {
                count++;
                Console.Write($"{count}. ");
                m.Show();
            }
            Console.WriteLine();
        }
        public void ShowOrder()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            Header(" Orders: ");
            foreach(var ordr in CurrentOrders)
            {
                Console.WriteLine($"Order {ordr.OrderId}");
                ordr.Show();
            }
        }
        public void ShowNextOrder()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            if (CurrentOrders.Count()>0)
            {
                Header(" Next order: ");
                CurrentOrders.Peek().Show();
            }
            else
            {
                Header(" There are no current orders.");
            }
    
            
        }
        public void ShowOrderCount()
        {
            Console.WriteLine();
            Header(" Current amount of orders: ");
            Console.WriteLine(CurrentOrders.Count());
        }
        public void Header(string s)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
    
    
    class Order
    {
        public List<MenuItems> TableOrder = new List<MenuItems>();
        private static int currentOrderId = 0;
        public int OrderId { get; private set; }
        public decimal TotalCost { get; set; }
    
        public Order()
        {
            OrderId = ++currentOrderId;
        }
    
        public void Add(MenuItems menuItems)
        {
            TableOrder.Add(menuItems);
            TotalCost += menuItems.Price;
        }
        public void Show()
        {
            List<string> skip = new List<string>();
            foreach (var k in TableOrder)
            {
                if(skip.Contains(k.Name))
                {
                    continue;
                }
                Console.WriteLine($"{TableOrder.Count(order => order == k)}st {k.Name}");
                skip.Add(k.Name);
            }
            Console.WriteLine($"Total cost {TotalCost}");
            Console.WriteLine();
            
        }
    }
}
