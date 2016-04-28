using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static List<int> GetPrimeDigit(int[] initialArr)  // My algorithm for finding all prime numbers. Its too Slow. It runs about 7 minutes for 1 000 000 items, and about few seconds for 100 000 items.
        {
            List<int> temparr = new List<int>();

            for (int i = 2; i < initialArr.Length; i++)
            {
                if (temparr.Capacity == 0)
                {
                    temparr.Add(i);
                }
                if (i < 10)
                {
                    for (int l = 2; l < i; l++)
                    {
                        if (i % l == 0)
                        {
                            break;
                        }
                        if (l == i - 1)
                        {
                            temparr.Add(i);
                            break;
                        }
                    }
                }
                if ((i > 10) && (i % 2 == 0 || i % 3 == 0 || i % 5 == 0 || i % 7 == 0))
                {
                    continue;
                }
                for (int l = 2; l <= i / 2; l++)
                {
                    if (i % l == 0)
                    {
                        break;
                    }
                    if (l == i / 2)
                    {
                        temparr.Add(i);
                        break;
                    }
                }
            }
            return temparr;
        }

        static List<int> GetPrimeDigit(int _limit) //Sieve of Atkin  algorithm for finding all prime numbers   https://en.wikipedia.org/wiki/Sieve_of_Atkin . 
        {
            List<int> Primes = new List<int>();
            bool[] IsPrimes = new bool[_limit + 1];
            double sqrt = Math.Sqrt(_limit);
            var limit = (ulong)_limit;
            for (ulong x = 1; x <= sqrt; x++)
                for (ulong y = 1; y <= sqrt; y++)
                {
                    ulong x2 = x * x;
                    ulong y2 = y * y;
                    ulong n = 4 * x2 + y2;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                        IsPrimes[n] ^= true;

                    n -= x2;
                    if (n <= limit && n % 12 == 7)
                        IsPrimes[n] ^= true;

                    n -= 2 * y2;
                    if (x > y && n <= limit && n % 12 == 11)
                        IsPrimes[n] ^= true;
                }

            for (ulong n = 5; n <= sqrt; n += 2)
                if (IsPrimes[n])
                {
                    ulong s = n * n;
                    for (ulong k = s; k <= limit; k += s)
                        IsPrimes[k] = false;
                }
            IsPrimes[2] = true;
            IsPrimes[3] = true;

            Primes.Add(2); Primes.Add(3); Primes.Add(5);
            for (int i = 6; i <= _limit; i++)
            {  // добавлена проверка делимости на 3 и 5. В оригинальной версии алгоритма потребности в ней нет.
                if ((IsPrimes[i]) && (i % 3 != 0) && (i % 5 != 0))
                {
                    Primes.Add(i);
                    // Console.WriteLine(i);
                }
            }
            return Primes;
        }


        static List<int> GetСircularPrime(List<int> arr)
        {
            List<string> tempСircularPrime = new List<string>();
            List<int> СircularPrime = new List<int>();
            List<string> stringtemp = new List<string>();
            string temp = "";
            string circularStr = "";
            bool failCounter = false;

            foreach (var item in arr)
            {
                stringtemp.Add(item.ToString());
            }
            foreach (var item in stringtemp)
            {
                failCounter = false;
                string[] strarr = new string[item.Length];
                char tempChar = ' ';
                temp = item;
                if (Int32.Parse(item) < 9)
                {
                    tempСircularPrime.Add(temp);
                }
                for (int i = 0; i < item.Length - 1; i++)
                {
                    tempChar = temp[0];
                    for (int l = 0; l < item.Length - 1; l++)
                    {
                        if ((Int32.Parse(temp[l].ToString()) % 2 == 0) || temp[l] == '0' || temp[l] == '5')
                        {
                            failCounter = true;
                            break;
                        }
                        strarr[l] = temp[l + 1].ToString();
                    }
                    if (failCounter)
                    {
                        continue;
                    }
                    strarr[item.Length - 1] = tempChar.ToString();

                    for (int l = 0; l < strarr.Length; l++)
                    {
                        circularStr += strarr[l];
                    }
                    temp = circularStr;
                    tempСircularPrime.Add(temp);
                    circularStr = string.Empty;
                }
                if (CompareLists(tempСircularPrime, stringtemp))
                {
                    if (!СircularPrime.Contains(Int32.Parse(item)))
                    {
                        СircularPrime.Add(Int32.Parse(item));
                    }
                    foreach (var digit in tempСircularPrime)
                    {
                        if (!СircularPrime.Contains(Int32.Parse(digit)))
                        {
                            СircularPrime.Add(Int32.Parse(digit));
                        }
                    }
                }
                tempСircularPrime.Clear();
            }
            return СircularPrime;
        }
        static bool CompareLists(List<string> tempСircularPrime, List<string> stringtemp)
        {
            if (tempСircularPrime.Count == 0)
            {
                return false;
            }
            foreach (var item in tempСircularPrime)
            {
                int digit = Int32.Parse(item);
                if (digit > 10 && (digit % 2 == 0 || digit % 3 == 0 || digit % 5 == 0 || digit % 7 == 0))
                {
                    return false;
                }
                if (!stringtemp.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            int n = 1000000;
            int[] initialArr = new int[n];
            List<int> СircularPrime = new List<int>();

            for (int i = 0; i < initialArr.Length; i++)
            {
                initialArr[i] = i + 1;
            }
            Console.WriteLine("To use the Sieve of Atkin algorithm to get prime numbers, please enter the '1'");
            Console.WriteLine("Or if you want to use a different algorithm to get prime numbers(but its too slow variant), please enter '0' ");
            string choseKey = Console.ReadKey().KeyChar.ToString();
            switch (choseKey)
            {
                case "1":
                    СircularPrime = GetPrimeDigit(n); //Sieve of Atkin  algorithm
                    break;
                case "0":
                    Console.WriteLine("\nWaiting for primes .....");
                    СircularPrime = GetPrimeDigit(initialArr);//My algorithm
                    break;
                default:
                    break;
            }

            Console.WriteLine("\nThe number of prime numbers from {0} to {1}", СircularPrime.Count, n);
            Console.WriteLine("Waiting for counting cyclic primes .....");

            СircularPrime = GetСircularPrime(СircularPrime);
            Console.WriteLine("The amount of cyclic primes from {0} to {1}", СircularPrime.Count, n);

            Console.ReadKey();
        }
    }
}


