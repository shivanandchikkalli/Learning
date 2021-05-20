using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Three
{
    // Instead of having all the factory methods in the main class we could have a separate class for the factory methods, but to
    // achieve this the Point constructor needs to be made public.
    // => If Point constructor is made public the whole idea of creating factories is not achieved.
    // => If you are creating a library then the Point constructor can be made internal
    // => to hide the constructor for everyone, the factory class is implemented inside the Point class as inner class
    // This is called the Inner factory pattern.
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Point({x}, {y})";
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Point newCartesianPoint = Point.Factory.NewCartesianPoint(3, 5);

            Console.WriteLine(newCartesianPoint);

            Console.ReadLine();
        }
    }
}
