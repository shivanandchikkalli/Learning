using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Adapter_One
{
    public class Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public class Line
    {
        public readonly Point Start;
        public readonly Point End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        protected bool Equals(Line other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Line)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }
    }

    public class VectorObject : Collection<Line>
    {
    }

    public class RectangleVectorObject : VectorObject
    {
        public RectangleVectorObject(int x, int y, int width, int height)
        {

            /*                             topLine
             *                         ________________
             *                        |               |
             *              leftLine  |               |  rightLine
             *                        |_______________|
             *                             baseLine
             */

            var baseLine = new Line(new Point(x, y), new Point(x + width, y));
            var rightLine = new Line(new Point(x + width, y), new Point(x + width, y + height));
            var leftLine = new Line(new Point(x, y), new Point(x, y + height));
            var topLine = new Line(new Point(x, y + height), new Point(x + width, y + height));

            Add(baseLine);
            Add(rightLine);
            Add(leftLine);
            Add(topLine);
        }
    }


    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int _count;
        private static readonly Dictionary<int, List<Point>> Cache = new Dictionary<int, List<Point>>();
        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();
            if (Cache.ContainsKey(hash))
                return;
            Console.WriteLine($"\n{++_count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");
            var left = Math.Min(line.Start.X, line.End.X);
            var right = Math.Max(line.Start.X, line.End.X);
            var top = Math.Min(line.Start.Y, line.End.Y);
            var bottom = Math.Max(line.Start.Y, line.End.Y);
            var dx = right - left;
            var dy = line.End.Y - line.Start.Y;

            List<Point> points = new List<Point>();

            if (dx == 0)
            {
                for (var y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (var x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            Cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return Cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class Program
    {
        // the interface we have
        public static void DrawPoint(Point point)
        {
            Console.Write(".");
        }

        private static void Main(string[] args)
        {
            var vectorObjects = new List<VectorObject>
            {
                new RectangleVectorObject(1, 1, 10, 10),
                new RectangleVectorObject(3, 3, 6, 6)
            };

            Draw(vectorObjects);
            Draw(vectorObjects);


            Console.ReadLine();
        }

        private static void Draw(List<VectorObject> vectorObjects)
        {
            foreach (var item in vectorObjects)
            {
                foreach (var line in item)
                {
                    // we don't have any interface to draw line
                    // we have interface to draw Point
                    // An adapter will be implemented for this purpose

                    var adapter = new LineToPointAdapter(line);
                    foreach (var point in adapter)
                    {
                        DrawPoint(point);
                    }
                }
            }
        }
    }
}