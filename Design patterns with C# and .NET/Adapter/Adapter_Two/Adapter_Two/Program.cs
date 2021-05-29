using System;

namespace Adapter_Two
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public int Width { get; }
        public int Height { get; }
        public SquareToRectangleAdapter(Square square)
        {
            Height = square.Side;
            Width = square.Side;
        }
    }
    public class Program
    {
        private static void Main(string[] args)
        {
            var square = new Square { Side = 10 };

            var adapter = new SquareToRectangleAdapter(square);

            var area = adapter.Area();

            Console.WriteLine(area);

            Console.ReadLine();
        }
    }
}
