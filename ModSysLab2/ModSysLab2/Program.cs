using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace ModSysLab2
{
    class Program
    {
        static void Main(string[] args)        {
            ServiceSystem s = new ServiceSystem(0, 500);
                s.Do(3);
            Console.ReadLine();
        }
    }

    public class Model
    {
        public int TimeForBeDone { get; set; }
        public int TimeFinishWork { get; set; }
        public int TimeBeginwork { get; set; }
        public int WaitingTime { get; set; }
        public int TimeiTCame { get; set; }
        public int PrevEndTime { get; set; }
        public string Message { get; set; }
        public int ThisMomentQueueLength { get; set; }
        public Model()
        {
            Random rnd = new Random();
            this.TimeForBeDone = rnd.Next(1, 10);
        }
    }
    public class ServiceSystem
    {
        public int StartTime { get; set; } // when the work starts
        public int EndWork { get; set; } // how long system has to work
        public int WaitingTime { get; set; } // all the time sstem waits gor another model
        public ServiceSystem(int st, int wt)
        {
            StartTime = st;
            EndWork = wt;
        }
        public Queue<Model> AllServicedModels = new Queue<Model>();
        Queue<Model> AllUncervicedModels = new Queue<Model>(); //

        Queue<Model> workingQueue = new Queue<Model>(); // черга очікування
        Queue<Model> waitingQueue = new Queue<Model>(); // черга очікування
        int timeDoingNothing = 0;
        public void Do(int limitWaitingQueue)        
        {
            for (int i = 1; i < EndWork; i++)
            {
                bool b = ModelIsCreated();
                
                if (b)
                {
                    Model justCreated = new Model();
                    if (workingQueue.Count == 0 && waitingQueue.Count == 0)
                    {
                        justCreated.TimeBeginwork = i;
                        justCreated.TimeFinishWork = i + justCreated.TimeForBeDone;
                        justCreated.WaitingTime = 0;
                        justCreated.TimeiTCame = i;
                        justCreated.Message = "WaitQ = 0 WQ =0";
                        justCreated.ThisMomentQueueLength = waitingQueue.Count();
                        workingQueue.Enqueue(justCreated);
                    }
                    else if (workingQueue.Count != 0 && waitingQueue.Count == 0)
                    {
                        Model look = workingQueue.Peek();
                        if (look.TimeFinishWork < i)
                        {
                            justCreated.TimeBeginwork = i;
                            justCreated.TimeFinishWork = i + justCreated.TimeForBeDone;
                            justCreated.WaitingTime = 0;
                            justCreated.TimeiTCame = i;
                            justCreated.Message = "WaitQ = 0 WQ !=0";
                            justCreated.ThisMomentQueueLength = waitingQueue.Count();
                            AllServicedModels.Enqueue(workingQueue.Dequeue());
                            workingQueue.Enqueue(justCreated);
                        }
                        else
                        {
                            justCreated.TimeBeginwork = look.TimeFinishWork;
                            justCreated.TimeFinishWork = look.TimeFinishWork + justCreated.TimeForBeDone;
                            justCreated.WaitingTime = look.TimeFinishWork - i;
                            justCreated.TimeiTCame = i;
                            justCreated.Message = "WaitQ = 0 WQ !=0 else";
                            justCreated.ThisMomentQueueLength = waitingQueue.Count();
                            waitingQueue.Enqueue(justCreated);
                        }
                    }
                    else if (workingQueue.Count != 0 && waitingQueue.Count != 0)
                    {
                        Model look = workingQueue.Peek();
                        if (look.TimeFinishWork < i)
                        {

                            justCreated.WaitingTime = waitingQueue.Sum(item => item.TimeForBeDone);
                            justCreated.TimeBeginwork = i + justCreated.WaitingTime;
                            justCreated.TimeFinishWork = justCreated.TimeBeginwork + justCreated.TimeForBeDone;
                            justCreated.TimeiTCame = i;
                            justCreated.Message = "WaitQ != 0 WQ !=0";
                            justCreated.ThisMomentQueueLength = waitingQueue.Count();
                            AllServicedModels.Enqueue(workingQueue.Dequeue());
                            workingQueue.Enqueue(waitingQueue.Dequeue());
                            waitingQueue.Enqueue(justCreated);
                        }
                        else
                        {
                            justCreated.WaitingTime = waitingQueue.Sum(item => item.TimeForBeDone) + (workingQueue.Peek().TimeFinishWork - i);
                            justCreated.TimeBeginwork = i + justCreated.WaitingTime;
                            justCreated.TimeFinishWork = justCreated.TimeBeginwork + justCreated.TimeForBeDone;
                            justCreated.TimeiTCame = i;
                            justCreated.Message = "WaitQ != 0 WQ !=0 else";
                            justCreated.ThisMomentQueueLength = waitingQueue.Count();
                            if (waitingQueue.Count < limitWaitingQueue)
                            {
                                waitingQueue.Enqueue(justCreated);
                            }
                            else
                            {
                                AllUncervicedModels.Enqueue(justCreated);
                            }
                        }
                    }
                    else { }
                }
                else 
                {
                    if (workingQueue.Count == 0 && waitingQueue.Count == 0)
                    {
                        timeDoingNothing += 1;
                    }
                    else if (workingQueue.Count != 0 && waitingQueue.Count == 0)
                    {
                        Model look = workingQueue.Peek();
                        if (look.TimeFinishWork < i)
                        {
                           AllServicedModels.Enqueue(workingQueue.Dequeue());
                        }
                        else
                        {

                        }
                    }
                    else if (workingQueue.Count != 0 && waitingQueue.Count != 0)
                    {
                        Model look = workingQueue.Peek();
                        if (look.TimeFinishWork < i)
                        {
                            AllServicedModels.Enqueue(workingQueue.Dequeue());
                            workingQueue.Enqueue(waitingQueue.Dequeue());
                        }
                        else
                        {

                        }
                    }
                    else { }
                }
            }


            Console.WriteLine("Serviced");
            foreach (Model item in AllServicedModels)
            {
                Console.WriteLine($"Begin:{item.TimeBeginwork} , End{item.TimeFinishWork} Wait: {item.WaitingTime} BeDone: {item.TimeForBeDone} TimeCame: {item.TimeiTCame}  {item.Message}");
            }
            Console.WriteLine("UNServiced");
            foreach (Model i in AllUncervicedModels)
            {
                Console.WriteLine($"Begin:{i.TimeBeginwork} , End{i.TimeFinishWork} Wait: {i.WaitingTime} BeDone: {i.TimeForBeDone} TimeCame: {i.TimeiTCame}  {i.Message}");
            }

            Console.WriteLine("System chilling");
            Console.WriteLine($"{timeDoingNothing}");
            int allTime = (AllServicedModels.Sum(item => item.WaitingTime) / AllServicedModels.Count());
            Console.WriteLine($"Сереній час очікування = {allTime}");

            double t = (double)AllServicedModels.Sum(item => item.TimeForBeDone) / (double)AllServicedModels.Count(); //середный час обробки
            double lambda = (double)AllServicedModels.Count() / (double)(EndWork - StartTime); //Інтенсивність надходження заявок
            double ro = (double)lambda * (double)t;                            //Коефійієнт завантаження
            double P = (double)AllUncervicedModels.Count() / ((double)AllUncervicedModels.Count() + (double)AllServicedModels.Count()); ;
            double Q = (double)AllServicedModels.Sum(item => item.TimeForBeDone * item.ThisMomentQueueLength) / (double)AllServicedModels.Count(); //сердній час перебування в черзі
            double L = Q * lambda; //середня довжина
            double N = (double)(ro - Math.Pow(ro, AllServicedModels.Count() + 1)) / (double)(1 - Math.Pow(ro, AllServicedModels.Count() + 1)); // середнє навантаження пристрою
            Console.WriteLine($"Rozmir cherhi: {limitWaitingQueue}");
            Console.WriteLine($"Kilist obsluschenih: {AllServicedModels.Count()}");
            Console.WriteLine($"Kilkist neobsluschenih: {AllUncervicedModels.Count()}");
            Console.WriteLine($"Shans vidmovi: {P}");
            Console.WriteLine($"Serednia dovschina: {L}");
            Console.WriteLine($"Serendia zavantaschenist: {N}");
            Console.WriteLine($"Intensivnist: {lambda}");
            Console.WriteLine($"Koefitzient navantaschenosti pristroy: {ro}");
            Console.WriteLine($"Serednie tzas obrobki: {t}");

        }



        public static bool ModelIsCreated()
        {
            Random rnd = new Random();
            double chance = rnd.NextDouble();
            if (chance <= 0.9) return true;
            else return false;
        }
    }
    
}
