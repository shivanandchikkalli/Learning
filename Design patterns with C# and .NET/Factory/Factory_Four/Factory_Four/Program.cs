using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Four
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

    // If a factory for a new type of drink is added then the Enum in the below class needs to be altered
    // This is a clear violation of Open-Close principle
    // Refer Factory_Five project for the new implementation.
    public class HotDrinkMachine
    {
        public enum AvailableDrinks
        {
            Coffee,
            Tea
        }

        private Dictionary<AvailableDrinks, IHotDrinkFactory> _factory =
            new Dictionary<AvailableDrinks, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrinks drink in Enum.GetValues(typeof(AvailableDrinks)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("Factory_Four." + Enum.GetName(typeof(AvailableDrinks), drink) + "Factory"));
                _factory.Add(drink, factory);
            }
        }

        public IHotDrink Prepare(AvailableDrinks drink, int amount)
        {
            return _factory[drink].Prepare(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            machine.Prepare(HotDrinkMachine.AvailableDrinks.Coffee, 100).Consume();

            Console.ReadLine();
        }
    }
}
