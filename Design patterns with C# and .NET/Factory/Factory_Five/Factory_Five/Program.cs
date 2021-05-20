using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Five
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This is tea is awesome.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This Coffee is good");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"{amount}ml Tea is being prepared...");
            return new Tea();
        }
    }
    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"{amount}ml Coffee is being prepared...");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {

        private List<Tuple<string, IHotDrinkFactory>> _factory = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var type in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(type) && !type.IsInterface)
                {
                    _factory.Add(Tuple.Create(
                        type.Name.Replace("Factory",""), (IHotDrinkFactory)Activator.CreateInstance(type)
                        ));
                }
            }
        }

        public IHotDrink Prepare()
        {
            for (var index = 0; index < _factory.Count; index++)
            {
                var tuple = _factory[index];
                Console.WriteLine($"{index} : {tuple.Item1}");
            }

            while (true)
            {
                Console.WriteLine("Enter your selection : ");
                var input = string.Empty;
                if ((input = Console.ReadLine()) != null && int.TryParse(input, out int userSelection) &&
                    userSelection >= 0 && userSelection < _factory.Count)
                {
                    Console.WriteLine("Enter the amount (in ml) : ");
                    if ((input = Console.ReadLine()) != null && int.TryParse(input, out int amount) &&
                        amount > 0)
                    {
                        return _factory[userSelection].Item2.Prepare(amount);
                    }
                }

                Console.WriteLine("Invalid input, try again... ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            
            machine.Prepare().Consume();

            Console.ReadLine();
        }
    }
}
