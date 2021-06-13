using System;

/*
 *        ________________                             _________________
 *       | abstract View  |                           |     IResource   |
 *       |________________|                           |_________________|
 *       | IResource _res;|-------------------------->|                 |
 *       |________________|                           |_________________|
 *               ^                                             ^
 *               |                                             |
 *               |                                             |
 *       ________|________                             ________|_________
 *      |                 |                           |                  |_ 
 *     _|                 |                           |                  | |_
 *   _| |     View 1      |                           |   Resource 1     | | |
 *  | | |                 |                           |                  | | |
 *  | | |_________________|                           |__________________| | |
 *  | |_________________|                                |_________________| |
 *  |_________________|                                    |_________________|
 *
 */

namespace Bridge_1
{
    public interface IResource
    {
        string Show();
    }

    public class Button : IResource
    {
        public string Show()
        {
            return "Button";
        }
    }

    public class Image : IResource
    {
        public string Show()
        {
            return "Image";
        }
    }

    public class Navigation : IResource
    {
        public string Show()
        {
            return "Navigation";
        }
    }

    public abstract class View
    {
        protected readonly IResource Resource;

        protected View(IResource resource)
        {
            Resource = resource;
        }

        public abstract string Show();

    }

    public class LongView : View
    {
        public LongView(IResource resource) : base(resource)
        {
        }

        public override string Show()
        {
            return $"Long View {Resource.Show()}";
        }
    }

    public class ShortView : View
    {
        public ShortView(IResource resource) : base(resource)
        {
        }

        public override string Show()
        {
            return $"Short View {Resource.Show()}";
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var resource = new Button();

            // Same button is used for long view and short view.
            // Long view class displays the button with its own styles applied on top it so does the short view.
            // IResource is responsible to provide a concrete button(resource), showing on view is View class responsibility.
            // In this way the IResource and View are allowed for the modifications independently and solves the cartesian product issue.
            var longView = new LongView(resource);
            var shortView = new ShortView(resource);

            Console.WriteLine(longView.Show());
            Console.WriteLine(shortView.Show());
        }
    }
}
