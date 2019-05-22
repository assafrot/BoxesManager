using BoxesManger;
using BoxesManger.DataStructers;
using System;
using System.Text;
using System.Threading;
using System.Timers;

namespace Program
{
    public class UI : INotifier
    {
        static public BoxesManager manager;
        public StringBuilder log;
        System.Timers.Timer timer;

        public UI()
        {
            log = new StringBuilder();
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 6000000;
            timer.Enabled = true;

            manager = new BoxesManager(this, 40, 6, 5);
            manager.UpdateSupply(1, 3, 6);
            manager.UpdateSupply(1, 2.5, 13);
            Thread.Sleep(5001);//for demonstrating unused boxes removal
            manager.UpdateSupply(1.6, 2, 10);
            manager.UpdateSupply(1, 7, 12);
            manager.UpdateSupply(2, 8, 22);
            manager.UpdateSupply(3, 3, 1);
            manager.GetGiftBox(3, 3, out double x, out double y);
            manager.GetRidOfBoxes();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            manager.GetRidOfBoxes();
        }

        public void UpdateSuplly()
        {
            Console.WriteLine("Please enter box size and amount (x,y,amount)");
            if (double.TryParse(Console.ReadLine(), out double x) &&
            double.TryParse(Console.ReadLine(), out double y) &&
            int.TryParse(Console.ReadLine(), out int amnt))
                manager.UpdateSupply(x, y, amnt);
            else Console.WriteLine("Invalid input");
        }

        public void CheckSuplly()
        {
            Console.WriteLine("Please enter box size (x,y)");
            if (double.TryParse(Console.ReadLine(), out double x) &&
            double.TryParse(Console.ReadLine(), out double y))
                manager.GetBoxAmount(x, y, out int amnt);
            else Console.WriteLine("Invalid input");
        }

        public void GetGiftBox()
        {
            Console.WriteLine("Please enter gift size (x,y)");
            if (double.TryParse(Console.ReadLine(), out double x) &&
            double.TryParse(Console.ReadLine(), out double y))
            {
                if (manager.GetGiftBox(x, y, out double xx, out double yy))
                    Console.WriteLine($"The best box for yout gift is ({xx},{yy}).");
            }
            else Console.WriteLine("Invalid input");
        }

        public void RemoveUnused()
        {
            manager.GetRidOfBoxes();
        }

        public void ReadLog()
        {
            Console.WriteLine(log.ToString());
        }

        public void OnBoxRemoval(string msg)
        {
            log.Insert(0, Environment.NewLine);
            log.Insert(0, msg);
        }

        public void OnEmptyStock(string msg)
        {
            log.Insert(0, Environment.NewLine);
            log.Insert(0, $"{DateTime.Now} - {msg}");
        }

        public void OnError(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public void OnLowSuplly(string msg)
        {
            log.Insert(0, Environment.NewLine);
            log.Insert(0,$"{DateTime.Now} - {msg}");
        }

        public void OnSuccess(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
