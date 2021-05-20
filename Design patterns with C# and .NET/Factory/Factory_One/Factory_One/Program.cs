using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_One
{
    // In this Class rather than having the logic to initialize the values of x and y based on the type of point (cartesian/polar)
    // the logic is outsourced to the factory mathods and the constructor is made private.
    public class Point
    {
        private double x, y;

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public static Point NewCatesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)} : {x} , {nameof(y)} : {y}";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return (x == p.x) && (y == p.y);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Point point = Point.NewCatesianPoint(5, 6);
            Console.WriteLine(point);

            Console.ReadLine();
        }
    }
}
