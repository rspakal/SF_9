using System;
using System.Collections.Generic;


namespace SF_9._6_1
{
    delegate void HandlePressedButton(object sender, SortArg e);
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("Smith");
            list.Add("Winston");
            list.Add("London");
            list.Add("Hill");
            list.Add("Johnson");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("");
            PressedButtonHandler handler = new PressedButtonHandler();
            handler.CorrectButtonPressed += Sort;
            Console.WriteLine("Enter:\n 1 to sort from A to Z\n 2 to sort from Z to A");
            while (!handler.ProceedEnteredString(Console.ReadLine(), list))
            {
                Console.WriteLine("Enter:\n 1 to sort from A to Z\n 2 to sort from Z to A");
            }
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        public static void Sort(object sender, SortArg e)
        {
            e.Collection.Sort();
            if (e.PressedButton == "1")
            {
                Console.WriteLine("Sorted from A to Z");
            }
            else
            {
                e.Collection.Reverse();
                Console.WriteLine("Sorted from Z to A");
            }
            Console.WriteLine(sender.ToString());
        }
    }
    class WrongSymbolException : Exception
    {
        public override string Message
        {
            get
            {
                return "Incorrect symbol was entered";
            }
        }
    }
    class PressedButtonHandler
    {
        public event HandlePressedButton CorrectButtonPressed;
        internal bool ProceedEnteredString(string s, List<string> collection)
        {
            try
            {
                if (s != "1" & s != "2")
                {
                    throw new WrongSymbolException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred");
                if (ex is WrongSymbolException)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            CorrectButtonPressed(this, new SortArg(s, collection));
            return true;
        }
    }
    class SortArg
    {
        public string PressedButton { get; }
        public List<string> Collection { get; }
        public SortArg(string pressedButton, List<string> collection)
        {
            PressedButton = pressedButton;
            Collection = collection;
        }
    }

}
