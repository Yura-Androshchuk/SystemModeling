using System;
using System.Linq;
using Accord.Math;
namespace ModSysLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateNumbesrOneTenthousand();
            double[] numbers = new double[10000];
            double[] method1 = new double[10000];
            double[] gistRes = new double[20];
            double[] expected = new double[20];
            //10000 numbers from 0 to 1
            numbers = GenerateNumbesrOneTenthousand();
            //Generated numbers with method 1 with Ln
            method1 = GenerateNumbestMethod1(numbers, 2);
            //Gistogram 1 
            gistRes = CountResultsForGistogram(method1, 20);
            expected = GetIntervalCountValuesMethod1(method1, 20);
            Console.WriteLine("Experiment results 1");
            foreach (int item in gistRes)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Expected results 1");
            foreach (int item in expected)
            {
                Console.WriteLine(item);
            }
            double xForExp1 = XI(expected, gistRes);
            Console.WriteLine($"Avarage  = {AvarageValue(method1)}");
            Console.WriteLine($"Dispersion = {Dispersion(method1)}");
            Console.WriteLine($"XI result is: {xForExp1}");
            Console.WriteLine($"XI defoult where k = 18 and alpha = 0.05 = 28.9");
            //--------------------------------------------------------------------------------Part 2
            Console.WriteLine(); Console.WriteLine("Part 2");
            //Part 2
            double[] method2 = new double[10000];
            double[] gistRes2 = new double[20];
            double[] expected2 = new double[20];
            method2 = GenerateNumbersMethod2(numbers, 5, 12);
            gistRes2 = CountResultsForGistogram(method2, 20);
            double disp = Dispersion(method2);
            double avarage = AvarageValue(method2);
            expected2 = GetIntervalCountValuesMethod2(method2, 20, avarage, disp);
            Console.WriteLine("Experiment results 2");
            foreach (int item in gistRes2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Expected results 2");
            foreach (int item in expected2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Avarage  = {AvarageValue(method2)}");
            Console.WriteLine($"Dispersion = {Dispersion(method2)}");
            double xForExp2 = XI(expected2, gistRes2);
            Console.WriteLine($"XI result is: {xForExp2}");
            Console.WriteLine($"XI defoult where k = 17 and alpha = 0.05 = 27.6");
            //--------------------------------------------------------------------------------Part 3
            Console.WriteLine("Part 3");
            double[] method3 = new double[10000];
            double[] gistRes3 = new double[20];
            double[] expected3 = new double[20];
            double a = Math.Pow(5, 13);
            double c = Math.Pow(2, 31);
            method3 = GenerateNumbersMethod3(numbers, a, c);
            gistRes3 = CountResultsForGistogram(method3, 20);
            expected3 = GetIntervalCountValuesMethod3(method3, 20);
            Console.WriteLine("Experiment results 2");
            foreach (int item in gistRes3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Expected results 2");
            foreach (var item in expected3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Avarage  = {AvarageValue(method3)}");
            Console.WriteLine($"Dispersion = {Dispersion(method3)}");
            double xForExp3 = XI(expected3, gistRes3);
            Console.WriteLine($"XI result is: {xForExp2}");
            Console.WriteLine($"XI defoult where k = 17 and alpha = 0.05 = 27.6");
            Console.ReadLine();
        }
        public static double XI(double[] expected, double[] whatReallyIs) // Знаходимо хи квадрат
        {
            double sum = 0;
            for (int i = 0; i < 20; i++)
            {
                sum += Math.Pow(whatReallyIs[i] - expected[i], 2) / expected[i];
            }
            return sum;
        }
        public static double[] Laplace(double[] array, double avarage, double disp) // знаходимо функцію лапласа
        {
            double[] fixedArrayValues = new double[21];
            for (int i = 0; i < 21; i++)
            {
                fixedArrayValues[i] = (array[i] - avarage) / Math.Sqrt(disp);
            }

            double[] laplaceValues = new double[21];
            for (int i = 0; i < 21; i++)
            {
                laplaceValues[i] = (1/Math.Sqrt(2*Math.PI)) * (Math.Sqrt(Math.PI) * (Special.Erf(fixedArrayValues[i]/Math.Sqrt(2)) + 1)) / Math.Sqrt(2);
            }
            return laplaceValues;
        }
        public static double[] GenerateNumbesrOneTenthousand() // генеруєсо 10000 випадкових чисел
        {
            double[] numbers = new double[10000];
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                numbers[i] = rnd.NextDouble();
            }
            return numbers;
        }
        public static double[] GenerateNumbestMethod1(double[] array, int parametr)// генеруємо випадкові числа 1 формулою
        {
            double[] generatedNumbers = new double[10000];
            for (int i = 0; i < 10000; i++)
            {
                generatedNumbers[i] =  (-1*Math.Log(array[i]))/parametr;
            }
            return generatedNumbers;
        }
        public static double[] CountResultsForGistogram(double[] array, int k)// знаходимо кількість чисел, які потрапили у проміжок(значення стовпців гістограми частот)
        {
            double minValue = array.Min();
            double maxValue = array.Max();
            double h = (maxValue - minValue) / k;
            double[] keepIntervalValues = new double[21];
            keepIntervalValues[0] = minValue;
            for (int i = 1; i < 21; i++)
            {
                keepIntervalValues[i] += keepIntervalValues[i -1]+ h;
            }
            //foreach (var item in keepIntervalValues)
            //{
            //    Console.WriteLine(item);
            //}
            double[] countOfNumbers = new double [20];
            for (int i = 0; i < 20; i++)
            {
                countOfNumbers[i] = array.Count(r => r > keepIntervalValues[i] && r <= keepIntervalValues[i + 1]);
            }
            return countOfNumbers;
        }
        public static double[] GetIntervalCountValuesMethod1(double[] array, int k)// Знаходимо очікувану кількість потраплянь відповідно до експоненційному закону розподілу
        {
            double minValue = array.Min();
            double maxValue = array.Max();
            double h = (maxValue - minValue) / k;
            double[] keepIntervalValues = new double[21];
            keepIntervalValues[0] = minValue;
            for (int i = 1; i < 21; i++)
            {
                keepIntervalValues[i] += keepIntervalValues[i - 1] + h;
            }
            double[] chanceIntervalValues = new double[21];
            for (int i = 0; i < 21; i++)
            {
                chanceIntervalValues[i] = 1 - Math.Exp(-2 * keepIntervalValues[i]);// 2 is alpha here change if needed
            }
            double[] chanceIntervalCount = new double[20]; // value interval+1 minus value interval * 10000(number of ganerated) np^T
            for (int i = 0; i < 20; i++)
            {
                chanceIntervalCount[i] = (chanceIntervalValues[i + 1] - chanceIntervalValues[i]) * 10000;
            }
            return chanceIntervalCount;
        }
        public static double[] GetIntervalCountValuesMethod2(double[] array, double k, double avarage, double disp)//Знаходимо очікувану кількість потраплянь відповідно до нормального закону розподілу
        {
            double minValue = array.Min();
            double maxValue = array.Max();
            double h = (maxValue - minValue) / k;
            double[] keepIntervalValues = new double[21];
            keepIntervalValues[0] = minValue;
            for (int i = 1; i < 21; i++)
            {
                keepIntervalValues[i] += keepIntervalValues[i - 1] + h;
            }
            double[] laplaceValues = Program.Laplace(keepIntervalValues, avarage, disp);
            
            double[] chanceIntervalCount = new double[20]; // value interval+1 minus value interval * 10000(number of ganerated) np^t
            for (int i = 0; i < 20; i++)
            {
                chanceIntervalCount[i] = (laplaceValues[i + 1] - laplaceValues[i]) * 10000;
            }
            return chanceIntervalCount;

        }
        public static double[] GetIntervalCountValuesMethod3(double[] array, int k)//Знаходимо очікувану кількість потраплянь відповідно до рівномірного закону розпділу
        {
            double minValue = array.Min();
            double maxValue = array.Max();
            double h = (maxValue - minValue) / k;
            double[] keepIntervalValues = new double[21];
            keepIntervalValues[0] = minValue;
            for (int i = 1; i < 21; i++)
            {
                keepIntervalValues[i] += keepIntervalValues[i - 1] + h;
            }
            double[] chanceIntervalCount = new double[20]; // value interval+1 minus value interval * 10000(number of ganerated) np^t
            for (int i = 0; i < 20; i++)
            {
                chanceIntervalCount[i] = ((keepIntervalValues[i + 1] - keepIntervalValues[i]) / maxValue - minValue) * 10000;
            }
            return chanceIntervalCount;
        }


        //way 2
        public static double[] GenerateNumbersMethod2(double[] array, int a, int b)// генеруємо випадкові числа за 2 формулою
        {
            Random rnd = new Random();
            double[] hepler = new double[10000];
            double[] result = new double[10000];
            for (int i = 0; i < 10000; i++)
            {
                //sum of rand numbers
                double sum = 0;
                for (int j = 0; j < 12; j++)
                {
                    int value = rnd.Next(1, 9988);
                    sum += array[value + j];
                }
                hepler[i] = sum -6;
            }
            for (int i = 0; i < 10000; i++)
            {
                result[i] = a * hepler[i] + b;
            }
            //foreach (double item in result)
            //{
            //    Console.WriteLine(item);
            //}
            return result;
        }
        //way 3
        public static double[] GenerateNumbersMethod3(double[] array, double a, double c)// генеруємо випадкові числа за 3 формулою
        {
            double[] zValues = new double[10000];
            double[] result = new double[10000];
            zValues[0] = 10;
            for (int i = 1; i < 10000; i++)
            {
                zValues[i] = (zValues[i - 1] * a) % c;
            }
            for (int i = 0; i < 10000; i++)
            {
                result[i] = zValues[i]/c;
            }
            return result;
        }
        public static double AvarageValue(double[] array)// знаходимо середнє значення
        {
            double avarage = 0;
            for (int i = 0; i < 10000; i++)
            {
                avarage += array[i];
            }
            avarage = avarage / 10000;
            return avarage;
        }
        public static double Dispersion(double[] array)// рахуємо дисперсію
        {
            double sum = 0;
            double avarage = AvarageValue(array);
            for (int i = 0; i < 10000; i++)
            {
                sum+= Math.Pow(array[i] - avarage, 2);
                
            }
            double dispersion = sum* 1/9999;
            return dispersion;
        }
    }
    
    

}
