using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace ProjEuler
{
    class Proj_euler_class2
    {
        /// <summary>
        /// Problem 12
        /// </summary>
        int GetNumDivisors(long number)
        {
            // step 1: 1 and self are always divisors.
            int num_devisors = 2;
            for(int i = 2; i < Math.Sqrt(number); i++)
            {
                if(number%i == 0)
                {
                    num_devisors += 2; // i and number/i are divisors.
                }
            }
            if(number% Math.Sqrt(number) == 0)
            {
                num_devisors++; // add one if the sqare root is also a divisor.
            }


            return num_devisors;
        } 

        public long Solution_problem_12(int num_target)
        {
            // will return 1 if the target number of divisors is 0.
            long triangle_number = 1;
            int to_next = 2;
            int divisors = 1;
            while(divisors < num_target)
            {
                triangle_number += to_next;
                to_next++;
                divisors = GetNumDivisors(triangle_number);
            }
            System.Console.WriteLine("The first number with atleast " + num_target.ToString() + " divisors is: " + triangle_number.ToString());
            return triangle_number;
        }

        /// <summary>
        /// Problem 13
        /// </summary>
        
        public long Solution_problem_13(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);

                long overflow = 0;
                long[] sums = { 0, 0, 0, 0, 0 }; // each sum is supposed to represent 5 digits of the total sum. 
                while (sr.Peek() >= 0)
                {
                    string data = sr.ReadLine(); // know that the data is 50 long.
                    long of = 0; // temp overflow
                    for(int i = 0; i < 5; i++)
                    {
                        string TenDecStr = data.Substring(data.Length - 10);
                        long TenDecInt = Convert.ToInt64(TenDecStr);
                        sums[i] += TenDecInt + of;
                        // shave of overflow.
                        of = sums[i] / 10000000000;
                        sums[i] = sums[i] % 10000000000;
                        data = data.Remove(data.Length - 10); // shave off the last 10 digits.
                    }
                    overflow += of;
                }
                if(overflow == 0)
                {
                    Console.WriteLine("The first ten digits are: " + sums[4].ToString());
                    return sums[4];
                }
                string TenDecRes = overflow.ToString();
                TenDecRes += sums[4].ToString();
                TenDecRes = TenDecRes.Remove(10);
                Console.WriteLine("The first ten digits are: " + TenDecRes);

                return Convert.ToInt64(TenDecRes);
            }
            Console.WriteLine("Could not find problem input at the given path");
            return -1;
        }


        /// <summary>
        /// Problem 14
        /// </summary>
        
        // stolen solution with the cache... to solve what was really an int overflow problem.
        static int[] cache = new int[1000000];

        Int32 Collatz_sequence_length(Int64 number, Int32 cnt = 0)
        {
            Int64 starting_number = number;
            while (true)
            {
                if(number == 1 || number < starting_number) 
                {
                    cache[starting_number] = cnt + cache[number];
                    return cnt + cache[number];
                }
                else
                {
                    if(number%2 == 0)
                    {
                        number = number / 2;
                        cnt++;
                    }
                    else
                    {
                        number = number * 3 + 1;
                        cnt++;
                    }
                }
            }
        }
  
        public int Solution_problem_14(int max_starting_number)
        {
            // Longest collatz sequence
            cache[1] = 1;
            Int32 max_len = 1;
            Int32 max_number = 1;

            //Use dictionary with subssequence solution. 
            for(int i = 2; i < max_starting_number; i++)
            {
                Int32 val = Collatz_sequence_length(i);
                if(val > max_len)
                {
                    max_number = i;
                    max_len = val;
                }
            }
            Console.WriteLine("Number " + max_number.ToString() + " With a length of " + max_len.ToString());
            return max_number;
        }
        /// <summary>
        /// Problem 15
        /// </summary>
        
        // 20x20 grid. Only able to move down or right. -> exactly 40 "choices". 
        // However 20 of the choices has to be down. and 20 of the choices have to be up. 
        // How many way can you combine 20 A's and 20 B's
        // This can be seen as in how  many ways can you place 20 B's into a 40 grid?
        // Which is given as forty over twenty: 
        // 40
        // 20
        long NumOverNum(Int64 over, Int64 under)
        {
            long prod = 1;
            int under_stuff = 1;
            while(under_stuff <= under)
            {
                prod *= over;
                prod /= under_stuff;
                over--;
                under_stuff++;
            }
            return prod;
        }

        public long Solution_problem_15(int grid_size)
        {
            long solution = NumOverNum(2 * (grid_size), grid_size);
            Console.WriteLine("Solution is: " + solution.ToString());
            return solution;
        }

        /// <summary>
        /// Problem 16
        /// </summary>
        
        public int Solution_problem_16(int exp)
        {
            // Sum of the digits in 2^1000
            string number = "1"; // representing the number the "other way around"
            for(int i = 0; i < exp; i++)
            {
                //try ten at a time... start lowest. 
                int start_index = number.Length-1;
                int overflow = 0;

                string new_number = "";
                while (start_index >= 0) // perform one exponent.
                {
                    int num = Convert.ToInt16(number.Substring(start_index, 1));
                    num = num*2 + overflow;
                    overflow = num / 10;
                    num = num % 10;
                    new_number = num.ToString() + new_number;
                    start_index--;
                }
                if(overflow != 0)
                {
                    new_number = overflow.ToString() + new_number;
                }
                number = new_number;
                    
            }

            Int64 sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum += Convert.ToInt64(number.Substring(i, 1)); // sum decimals
            }

            Console.WriteLine(number);
            Console.WriteLine("Sum of all decimals is: " + sum.ToString());
            return (int)sum;
        }


        /// <summary>
        /// Problem 17
        /// </summary>
        
        // unelegant and ugly problem.
        int LetterCount(int number)
        {
            // one - two - three - four - five -six - seven - eight - nine
            int cnt = 0;

            int first = number % 10;
            int second = (number / 10) % 10;

            if ((second != 1))
            {
                // number does not contain teens.       
                if(first == 1 || first == 2 || first == 6)
                {
                    cnt += 3;
                }else if(first == 4 || first == 5 || first == 9)
                {
                    cnt += 4;
                }else if(first == 3 || first == 7 || first == 8)
                {
                    cnt += 5;
                }
                // Twenty - Thirty - Fourty - Fifty - Sixty - Seventy - Eighty - Ninety
                if( second == 5 || second == 6)
                {
                    cnt += 5;
                }else if(second == 2 || second == 3 || second == 4 || second == 8 || second == 9){
                    cnt += 6;
                }else if(second == 7)
                {
                    cnt += 7;
                }
            }
            else
            {
                // eleven, Twelve, Thirteen, Fourteen, fifteen, sixteen Seventeen, Eighteen, nineteen
                if (first == 1 || first == 2)
                {
                    cnt += 6;
                }
                else if (first == 4 || first == 3 || first == 8 || first == 9)
                {
                    cnt += 8;
                }
                else if (first == 5 || first == 6)
                {
                    cnt += 7;
                }else if (first == 7)
                {
                    cnt += 9;
                }
                else
                {
                    cnt += 3; // ten
                }
            }

            return cnt;
        }

        public int Solution_problem_17()
        {
            int under_hundred_sum = 0;
            int sum = 0;
            for (int i = 1; i < 100; i++)
            {
                under_hundred_sum += LetterCount(i);
                Console.WriteLine(i.ToString() + " -> " + LetterCount(i).ToString());
            }
            Console.WriteLine(under_hundred_sum.ToString());
            int constant = 0;
            for (int i = 1; i < 10; i++)
            {
                int con = 0;
                constant = 3 + 7; ; // and hundred
                if (i == 1 || i == 2 || i == 6)
                {
                    constant += 3; //one, two, six
                    con = 3;
                }
                else if (i == 4 || i == 5 || i == 9)
                {
                    constant += 4; // four, five, nine
                    con = 4;
                }
                else if (i == 3 || i == 7 || i == 8)
                {
                    constant += 5; // three, seven, eight
                    con = 5;
                }
                sum += 99 * (constant + under_hundred_sum) + 7+con; // one hundred and - sum(1-99), + one hundred
            }
            sum += under_hundred_sum;
            sum += 3 + 8; //one thousand.
            Console.WriteLine(sum.ToString());
            return 1;
        }


        /// <summary>
        /// Problem 18 (And problem 67)
        /// </summary>
        
        // Solved by solving subsums.
        // ie:
        //  4 5 7
        // 4 8 1 2
        // gets replaced by
        // 12 13 9
        // Removing the "lowest line" of the triangle each iteration, but conserving max possible value.
        public int Solution_problem_18(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                List<string> input = new List<string>();
                while (sr.Peek() >= 0)
                {
                    input.Add(sr.ReadLine());
                }
                //List<int> bottom_line = new List<int>();
                int[] bottom_line;
                List<int> top_line = new List<int>();
                for (int i = input.Count-1; i > 0; i--)
                {
                    if(i == input.Count - 1)
                    {
                        string[] line_1 = input[i].Split(" ");
                        List<int> in_list = new List<int>();
                        foreach (string j in line_1)
                        {
                            in_list.Add(Convert.ToInt32(j));
                        }
                        bottom_line = in_list.ToArray();
                    }
                    else
                    {
                        bottom_line = top_line.ToArray();
                    }
                    string[] line_2 = input[i-1].Split(" ");
                    top_line.Clear();
                    int index = 0;
                    foreach (string j in line_2)
                    {
                        int value = Convert.ToInt32(j);
                        top_line.Add(Math.Max(value + bottom_line[index], value + bottom_line[index+1])); // Add the maximum of the two possible nodes.
                        index++;
                    }
                }
                Console.WriteLine("the highest value is: " + top_line[0].ToString());
                return top_line[0];
            }
            return 1;
        }
        /// <summary>
        /// Problem 19 
        /// </summary>

        public int Solution_problem_19(int start_year, int end_year)
        {
            int n_sundays = 0;
            for(int year = start_year; year <= end_year; year++)
            {
                for (int m = 1; m <= 12; m++)
                {
                    DateTime date = new DateTime(year, m, 1);
                    if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        n_sundays++;
                    }
                }
            }
            Console.WriteLine("Between: " + start_year.ToString() + " and " + end_year.ToString() + " There is " + n_sundays.ToString() + " months that start with sundays");
            return n_sundays;
        }

        /// <summary>
        /// Problem 20 
        /// </summary>

        // Is there a way to determine properties of the sum of digits ?
        // Could just always brute force calculate the big number? 
        public int Solution_problem_20(int factorial)
        {
            string product = "1";
            int sum = 0;
            for(int i = 1; i <= factorial; i++)
            {
                int start_index = product.Length - 1;
                int overflow = 0;
                string new_product = "";
                while (start_index >= 0) // perform one exponent.
                {
                    int num = Convert.ToInt16(product.Substring(start_index, 1));
                    num = num * i + overflow;
                    overflow = num / 10;
                    num = num % 10;
                    new_product = num.ToString() + new_product;
                    start_index--;
                }
                if(overflow != 0)
                {
                    new_product = overflow.ToString() + new_product;
                }
                product = new_product;
            }
            for(int i = 0; i < product.Length; i++)
            {
                sum += Convert.ToInt32(product.Substring(i, 1));
            }

            Console.WriteLine("number factorial is: " + product);
            Console.WriteLine("the sum is: " + sum.ToString());
            return 1;
        }

        /// <summary>
        /// Problem 21 
        /// </summary>
        int sum_divisors(int number)
        {
            int sum = 1;
            double sqrt = Math.Sqrt(number);
            for (int i = 2; i < sqrt; i++)
            {
                if(number%i == 0)
                {
                    sum += i + (number / i);
                }
            }
            if(number % sqrt == 0)
            {
                sum += (int)sqrt;
            }
            return sum;
        }

        public int Solution_problem_21(int max_number)
        {
            int[] d = new int[max_number];
            for (int i = 0; i < max_number; i++)
            {
                d[i] = 0;
            }

            int sum_numbers = 0;
            for (int i = 2; i < max_number; i++)
            {
                if(d[i] == 0)
                {
                    // this number has not been encountered before. since a sum is atleast 1. 
                    d[i] = sum_divisors(i);
                    if(d[i] > max_number)
                    {
                        continue;
                    }
                    else if(d[d[i]] == i && i != d[i])
                    {
                        sum_numbers += i + d[i]; // i and d(i) are added.
                    }
                }
            }
            Console.WriteLine("Sum of all amicable numbers are: " + sum_numbers.ToString());
            return sum_numbers;
        }

        /// <summary>
        /// Problem 22 
        /// </summary>
       
        bool LessThan(string lhs, string rhs) // <
        {
            for(int i = 0; i < Math.Min(rhs.Length, lhs.Length); i++)
            {
                if(Convert.ToInt32(lhs[i]) < Convert.ToInt32(rhs[i]))
                {
                    return true;
                }else if (Convert.ToInt32(lhs[i]) == Convert.ToInt32(rhs[i]))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            if(lhs.Length < rhs.Length)
            {
                return true;
            }
            return false;
        }

        public long Solution_problem_22(string path)
        {
            // step 1: read file.
            long sum = 0;
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                if(sr.Peek() >= 0)
                {
                    string[] names = (sr.ReadLine().Replace("\"","")).Split(",");
                    for(int i = 1; i < names.Length; i++)
                    {
                        int index = i;
                        while(index > 0 && LessThan(names[index], names[index - 1]))
                        {
                            string temp = names[index - 1];
                            names[index - 1] = names[index];
                            names[index] = temp;
                            index--;
                        }

                    }
                    int name_pos = 1;
                    int offset = Convert.ToInt32('A') - 1;
                    foreach (string name in names)
                    {
                        int name_score = 0;
                        for(int i = 0; i < name.Length; i++)
                        {
                            name_score += Convert.ToInt32(name[i]) - offset;
                        }
                        sum += name_score * name_pos;
                        name_pos++;
                    }
                }
            }
            Console.WriteLine("Sum of all namescores: " + sum.ToString());
            return sum;
        }
        

        /// <summary>
        /// Problem 23 
        /// </summary>
        public long Solution_problem_23()
        {
            long sum = 1;
            List<int> abuntant_numbers = new List<int>();
            for(int i = 2; i < 28123; i++)
            {
                if(sum_divisors(i) > i)
                {
                    abuntant_numbers.Add(i);
                }
                bool is_sum = false;
                foreach (int a_number in abuntant_numbers)
                {
                    int test = i - a_number;
                    if(abuntant_numbers.BinarySearch(test) >= 0)
                    {
                        is_sum = true;
                    }
                    if(a_number > i / 2)
                    {
                        break;
                    }
                }
                if (!is_sum)
                {
                    sum += i;
                }
            }
            
            Console.WriteLine("Sum all numbers that cannot be written as the sum of two abundant numbers" + sum.ToString());
            return sum;
        }    
    
    }
}
