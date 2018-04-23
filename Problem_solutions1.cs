using System;
using System.Collections.Generic;
using System.Text;





namespace ProjEuler
{
    class Proj_euler_class1
    {
        /// <summary>
        /// Problem 1
        /// </summary>
       
        public long Solution_problem_1(int max)
        {
            long sum = 0;
            for (int i = 0; i < max; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }
            Console.WriteLine("The sum is: " + sum.ToString());
            return sum;
        }

        /// <summary>
        /// Problem 2
        /// </summary>

        public long Solution_problem_2(long max)
        {
            // Sum of even fibbonachi numbers
            // Every third fibonachi number is even (after two.)
            // O, O, E, O, O, E  (the sum of two odd numbers is always even, the sum of an odd number and an even number is always odd). 
            long sum = 0;
            long number = 2;
            long prev_value = 1;
            while( number < max)
            {
                sum += number;
                long new_prev_value = prev_value + number + number;
                number = (prev_value + number) + new_prev_value;
                prev_value = new_prev_value;
            }

            Console.WriteLine("The sum is: " + sum.ToString());
            return sum;
        }

        /// <summary>
        /// Problem 3
        /// </summary>

        //help function for problem 3.
        long Largest_divisor(long x)
        {
            long div = 2;
            while (div < x/2 +1)
            {
                if (x % div == 0)
                {
                    return Largest_divisor(x/div);
                }
                div++;
            }
            return x;
        }

        public long Solution_problem_3(long number)
        {
            long divisor = Largest_divisor(number);
            Console.WriteLine("the largest prime divisor is: " + divisor.ToString());
            return divisor;
        }


        /// <summary>
        /// Problem 4
        /// </summary>

        bool isPalindrome(int x)
        {
            string numstring = x.ToString();
            int kek = numstring.Length -1;
            for(int i  = 0; i < numstring.Length; i++)
            {
                if(numstring[i] != numstring[kek - i])
                {
                    return false;
                }
            }
            return true;
        }

        public int Solution_problem_4()
        {
            // largest palindrome given by two three digit factors. 
            // search space 100 * 100 -> 999*999
            int palindrome = 0;
            int prod1 = 0;
            int prod2 = 0;
            for(int x = 100; x < 1000; x++)
            {
                for(int y = 100; y < 1000; y++)
                {
                    if(x*y > palindrome)
                    {
                        if (isPalindrome(x * y))
                        {
                            palindrome = x * y;
                            prod1 = x;
                            prod2 = y;
                        }
                    }
                }
            }
            System.Console.WriteLine("The highest palindrome is: " + palindrome.ToString() + " facor1: " + prod1.ToString() + " " + prod2.ToString());
            return palindrome;  
          
        }

        /// <summary>
        /// Problem 5
        /// </summary>

        List<int> FindFactors(int x)
        {
            List<int> factors = new List<int>();
            int div = 2;
            while(div < x)
            {
                if(x%div == 0)
                {
                    x /= div;
                    factors.Add(div);
                }else
                {
                    div++;
                }
            }
            factors.Add(x);
            return factors;
        }

        public long Solution_problem_5(int max)
        {
            //Smallest number evenly dividable by numbers 1-20 is a number that has the minimal factorial base of all those numbers as factors.
            // ie: a number devisable without remain by: 1-20 will have (1*)2*2*2*2*3*3*5*7*11*13*17*19 as a subset of factors. ie: 232792560
            
            // Generate min factor representation of the set [1,max]
            Dictionary<int, int> set = new Dictionary<int, int>(); 
            for(int i = 2; i <= max; i++)
            {
                List<int> factors = FindFactors(i); // is sorted.
                int num = 0;
                int f_mem = 0;
                foreach ( int f in factors)
                {
                    if(f_mem != f)
                    {
                        num = 1; // Reset count of the number of the factor (ie: multiple 2's)
                    }
                    else
                    {
                        num++; 
                    }
                    f_mem = f;
                    int value;
                    if (set.TryGetValue(f, value: out value))
                    {
                        if(num > value)
                        {
                            set.Remove(f);
                            set.Add(f, num);
                        }
                    }
                    else
                    {
                        set.Add(f, num);
                    }
                }
            }
            long number = 1;
            // Generate number from min factor representation.
            for (int i = 2; i <= max; i++)
            {
                int value;
                if (set.TryGetValue(i, value: out value))
                {
                    for (int p = 0; p < value; p++)
                    {
                        number *= i;
                        Console.Write(i.ToString() + "*");
                    }
                }
            }
            Console.Write("\n");
            System.Console.WriteLine("The lowest number evenly divisible by numbers 1-max is: " + number.ToString());
            return number;
        }

        /// <summary>
        /// Problem 6
        /// </summary>

        public long Solution_problem_6(int max)
        {
            // i = 1-max: return sum(i)^2 - sum(i^2) 
            long sum = 0;
            long pow_sum = 0;
            for(int i = 1; i <= max; i++)
            {
                sum += i;
                pow_sum += i * i;
            }
            sum *= sum;
            System.Console.WriteLine("Diff iz: " + Math.Abs(sum - pow_sum).ToString());
            return Math.Abs(sum - pow_sum);
        }

        /// <summary>
        /// Problem 7
        /// </summary>

        // Check if a number is a prime number.
        bool IsPrime(long num)
        {
            // If no factor is found by the time you reach the sq. the number is a prime.
            for(int x = 3; x <= Math.Sqrt(num); x+=2)
            {
                if (num % x == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public long Solution_problem_7(int num)
        {
            // 2 is the first prime 
            if(num == 1)
            {
                return 2;
            }
            int cnt = 1;
            long iter = 1;

            while(cnt < num)
            {
                iter += 2; // Primes cannot be even. (besides two.) 
                if (IsPrime(iter))
                {
                    cnt++;
                }
            }
            System.Console.WriteLine("The num'th prime is: " + iter.ToString());
            return iter;
        }

        /// <summary>
        /// Problem 8
        /// </summary>
        static string prob_8 = "731671765313306249192251196744265747423553491949349698352031277450632623957831801698480186947885184385861560789112949495459501737958331952853208805511" +
            "125406987471585238630507156932909632952274430435576689664895044524452316173185640309871112172238311362229893423380308135336276614282806444486645238749" +
            "303589072962904915604407723907138105158593079608667017242712188399879790879227492190169972088809377665727333001053367881220235421809751254540594752243" +
            "525849077116705560136048395864467063244157221553975369781797784617406495514929086256932197846862248283972241375657056057490261407972968652414535100474" +
            "821663704844031998900088952434506585412275886668811642717147992444292823086346567481391912316282458617866458359124566529476545682848912883142607690042" +
            "242190226710556263211111093705442175069416589604080719840385096245544436298123098787992724428490918884580156166097919133875499200524063689912560717606" +
            "0588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

        public long Solution_problem_8(int len)
        {
            // Find the thirteen adjacent digits producting the highest product. in the prod_8 string.
            long product = 0;
            int index = 0;
            for(int i = 0; i < prob_8.Length - len; i++)
            {
                long mul = 1;
                for(int p = 0; p < len; p++)
                {
                    if(prob_8[i+p] == '0')
                    {
                        mul = 0;
                        break;
                    }
                    else
                    {
                        mul *= (int)Char.GetNumericValue(prob_8[i + p]);
                    }
                }
                if(mul > product)
                {
                    product = mul;
                    index = i;
                }
            }
            Console.WriteLine("Indexes: " + index.ToString() + " -> " + (index + len -1).ToString());
            for(int ind = index; ind < index + len; ind++)
            {
                if(ind == index + len - 1)
                {
                    Console.Write(prob_8[ind] + "\n");
                }
                else
                {
                    Console.Write(prob_8[ind] + "*");
                }
            }
            Console.WriteLine("Product: " + product.ToString());
            return product;
        }

        /// <summary>
        /// Problem 9
        /// </summary>
        List<int> SolvePythagoreanTriplet(int sum)
        {
            // a < b < c 
            // a + b + c = number
            // -> a < number/3, b < number/2
            int c = 0;
            for(int a = 0; a < sum/3; a++)
            {
                for(int b = a; b < sum/2; b++)
                {
                    int c_squared = a * a + b * b;
                    c = (int)Math.Sqrt(c_squared);
                    if(c*c != c_squared)
                    {
                        continue;
                    }
                    if(a+b+c == sum)
                    {
                        List<int> solution = new List<int>();
                        solution.Add(a); solution.Add(b); solution.Add(c);
                        return solution;
                    }
                }
            }
            return new List<int>();
        }

        public int Solution_problem_9(int sum)
        {
            List<int> solution = SolvePythagoreanTriplet(sum);
            if(solution.Count != 3)
            {
                Console.WriteLine("Something went wrong... returning -1" + solution.Count.ToString());
                return -1;
            }
            int mul = 1;
            for (int i = 0; i < 3; i++)
            {
                mul *= solution[i];
                Console.Write(solution[i].ToString() + " ");
            }
            Console.Write("\n");
            Console.WriteLine("Product is: " + mul.ToString());
            return mul;
        }

        /// <summary>
        /// Problem 10
        /// </summary>
        
        public long Solution_problem_10(int upper_limit)
        {
            long sum = 2;
            for(int i = 3; i < upper_limit; i += 2)
            {
                if (IsPrime(i))
                {
                    sum += i;
                }
            }
            Console.WriteLine("Sum is: " + sum.ToString());
            return sum;
        }

        /// <summary>
        /// Problem 11
        /// </summary>

        static int[,] array_prob_11 = { {08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08},
                                        {49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00},
                                        {81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65},
                                        {52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91},
                                        {22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80},
                                        {24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50},
                                        {32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70},
                                        {67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21},
                                        {24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72},
                                        {21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95},
                                        {78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92},
                                        {16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57},
                                        {86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58},
                                        {19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40},
                                        {04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66},
                                        {88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69},
                                        {04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36},
                                        {20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16},
                                        {20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54},
                                        {01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48}};
        public int Solution_problem_11()
        {
            
            
            int max = 0; 
            for(int x = 0; x < 20; x++)
            {
                for(int y = 0; y < 20; y++)
                {
                    // think filter: x -> x+3, y -> y+3
                    
                    if(x < 17)
                    {
                        // Calculate vertical products
                        int mul = array_prob_11[x, y] * array_prob_11[x+1, y] * array_prob_11[x+2, y] * array_prob_11[x+3, y];
                        if(mul > max)
                        {
                            max = mul;
                        }
                    }
                    if(y < 17)
                    {
                        int mul = array_prob_11[x, y] * array_prob_11[x, y+1] * array_prob_11[x, y+1] * array_prob_11[x, y+1];
                        if (mul > max)
                        {
                            max = mul;
                        }
                    }
                    if(x < 17 && y < 17)
                    {
                        int mul = array_prob_11[x, y] * array_prob_11[x+1, y + 1] * array_prob_11[x+1, y + 1] * array_prob_11[x+1, y + 1];
                        if (mul > max)
                        {
                            max = mul;
                        }
                    }
                    if (x > 3 && y < 17)
                    {
                        int mul = array_prob_11[x, y] * array_prob_11[x - 1, y + 1] * array_prob_11[x - 1, y + 1] * array_prob_11[x - 1, y + 1];
                        if (mul > max)
                        {
                            max = mul;
                        }
                    }
                    // Calculate horizontal products
                    // Calculate left diagonal products
                    // calculate right diagonal products
                }
            }
            Console.WriteLine("Sum is: " + max.ToString());
            return max;
        }

    }
}
