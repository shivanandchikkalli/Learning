using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factory_Two
{
    // Here in this class , if an object needs to be initialized asynchronously, constructor cannot have async and await , so can be achieved as follows 
    public class Foo
    {
        private Foo()
        {
        }

        private async Task<Foo> Initialize()
        {
            await Task.Delay(2000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            Foo oFoo = new Foo();
            return oFoo.Initialize();
        }

        public override string ToString()
        {
            return "Object Initialized Asynchronously";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var v = Foo.CreateAsync();

            int counter = 1;
            while (!v.IsCompleted)
            {
                Thread.Sleep(1000);
                Console.WriteLine(counter++);
            }

            Console.WriteLine(v.Result);

            Console.ReadLine();
        }
    }
}
