using System;
using System.Collections.Generic;
using System.Text;

namespace ProjEuler
{
    class Proj_euler_class3
    {
        /// <summary>
        /// Problem 24 
        /// </summary>
       
        public long Solution_problem_24(long index)
        {
            // there are 10 possibilities, for the first element.
            // there are 9 possibilities for the second element. 
            // etc.
            // thus total number is 10!
            // (I1 +1 ) * 9! < index => first number is higher than I1.     
            // (I1 +1 ) * 8! < index => second number is higher than I2.
            // I3 * 7!  etc.

            // Make list over possible combinations remaining (10!, 9!, 8!, 7!...)
            index = index - 1;
            long[] combinations = new long[11];
            combinations[0] = 1;  
          
            for(int n = 1; n <= 10; n++)
            {
                combinations[n] = n*combinations[n-1];
            }

            if(index > combinations[10] || index < 0)
            {
                Console.WriteLine("index outside of bounds...");
                return -1;
            }

            // Construct the number : 0-9, 0-8, 0-7, .. 0-1, 0 representing the number with the given index.
            // that allows us to reconstruct the number from a set.
            List<long> number = new List<long>();
            long remainder = index;
            for(int i = 9; i > 0; i--)
            {
                number.Add(remainder / combinations[i]);
                remainder = remainder % combinations[i];
            }
            number.Add(0);

            // reconstruct the number.
            List<int> set = new List<int>();
            for(int i = 0; i < 10; i++)
            {
                set.Add(i);
            }
            string solution = "";
            foreach(int n in number)
            {
                solution = solution + set[n];
                set.RemoveAt(n);
            }
            Console.WriteLine(solution);
            return Convert.ToInt64(solution);
        }

        /// <summary>
        /// Problem 25 
        /// </summary>
        public System.Numerics.BigInteger Solution_problem_25(int num_digits)
        {
            System.Numerics.BigInteger fib1 = 2;
            System.Numerics.BigInteger fib2 = 1;
            int index = 3;
            while((fib1.ToString()).Length < num_digits)
            {
                System.Numerics.BigInteger temp = fib1;
                fib1 = fib1 + fib2;
                fib2 = temp;
                index++;
            }
            Console.WriteLine("the first fibonnatchi number to have that many digits is: " + index.ToString());
            return fib1;
        }

        /// <summary>
        /// Problem 26 
        /// </summary>
   
        // Get back to this.
        public int Solution_problem_26(int maxd)
        {
            
            // step 1: find primes under maxid, and base repeating digits.
            // step 2: find prime factorization of all non prime numbers under maxid.  
            // step 2 alt: Iterate over primes to find the "best" combo-number under a 1000. 
            
            //  --- Considerations ---
            // for a lot of primes, but not all: 1/n, has n-1: repeating digits. //insignificant.
            // num repeating digits for a number given as: prime*prime,
            // is for n > 5 = n*(n-1)
            // and n^3 - n^2 for 1/n^3.

            // 1/(n*m) : has (n-1)*(m-1)/2: repeating digits.
            // when estimating 

            // find factors. Use formulae if it contains known values.
            // if not
            return 1;
        }

        /// <summary>
        /// Problem 27 
        /// </summary>
        bool IsPrime(long num)
        {
            // If no factor is found by the time you reach the sq. the number is a prime.
            if(num == 2)
            {
                return true;
            }else if(num%2 == 0)
            {
                return false;
            }else if(num <= 1)
            {
                return false;
            }

            for (int x = 3; x <= Math.Sqrt(num); x += 2)
            {
                if (num % x == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int Solution_problem_27(int lim_a, int lim_b)
        {
            // a, b specify: n^n + an + b, longest list of conseq primes n(x) , x = [0,1...)
            int max_a = 0;
            int max_b = 0;
            int max_sequence = 0;
            for(int a = -lim_a +1; a < lim_a; a++)
            {
                for(int b = 2; b <= lim_b; b++)
                {
                    int num = b;
                    int n = 0;
                    while (IsPrime(num))
                    {
                        n++;
                        num = n * n + a * n + b;
                    }
                    if(n > max_sequence)
                    {
                        max_sequence = n;
                        max_a = a;
                        max_b = b;
                    }
                }
            }
            Console.WriteLine(" a = " + max_a.ToString() + " and b = " + max_b.ToString());
            Console.WriteLine("the sequence is: " + max_sequence.ToString() + "  The product is" + (max_a*max_b).ToString());
            return 1;
        }

        /// <summary>
        /// Problem 28 
        /// </summary>

        public void Solution_problem_28(int spiral_size)
        {
            // The numbers along the dialog follows a spesific pattern. 
            // base number 1.
            // then 3,5,7,9 ie: four numbers incremented by 2
            // then 13, 17, 21, 25 ie: four numbers incremented by 4. etc.

            // n steps: (spiral_size -1 /2)
            long sum = 1;
            long num = 1;
            for(int step = 2; step <= spiral_size-1; step += 2)
            {
                sum += 10 * step + 4 * num; // sum = sum + (num + step) + (num + 2*step) + (num + 3*step) + (num + 4*step) = 4*num + 10*step;
                num += 4 * step;
            }
            Console.WriteLine(sum.ToString());
        }

        /// <summary>
        /// Problem 29 
        /// </summary>

            /*
        List<int> PrimeFactors(int number)
        {
            List<int> factors = new List<int>();
            int divisor = 2;
            while (divisor <= Math.Sqrt(number)){
                if(number%divisor == 0)
                {
                    factors.Add(divisor);
                    number /= divisor;
                }
                else if(divisor >= 3)
                {
                    divisor += 2;
                }
                else
                {
                    divisor++;
                }
            }
            factors.Add(number);
            return factors;
        }

        double HarmonicSum(int n)
        {
            double sum = 0.0;
            for(double i = 1; i < n; i++)
            {
                sum += 1.0 / (n*i);
            }
            return sum;
        }

        // distinct values from: a^b  _ where  2 <= a <= lim_a, 2<=b<=lim_b 
        public void Solution_problem_29(int lim_a, int lim_b)
        {
            // if there would be no duplicates: there would be (lim_a-1)*(lim_b-1) entries.
            // by prime factoring every number under lim_a, possible to determine the number of duplicates ?
            // 14^n ( 2^n * 7^n) 
            // 49 ^n (7^2n) -> lim_a/2 - 1 duplicates. 
            // as long as number of prime factors share.

            // 7^3 * 2^3 -> 2^2 * 7^2 is already part of the solution.
            int sum = (lim_a -1)*(lim_b -1);
            for (int a = 2; a <= lim_a; a++)
            {
                List<int> prime_factors = PrimeFactors(a);
                // prime number.

                if (prime_factors.Count != 1)
                {
                    // find highest common divisor, amongs the number of primes present.

                    // factor 1 -> prime: whole +
                    // factor 2 -> halved. 
                    // factor 3 -> -one third. - half of one third. -> halved. //first element is not part. So calculate as if its a hundred.
                    // factor 4 -> - (one fourth - half of one forth) - one third of one fourth  
                    // Different factors: 
                    int cnt = 1;
                    List<int> occurence = new List<int>();
                    int min_occ = 99;
                    for(int p =1; p < prime_factors.Count; p++)
                    {
                        if(prime_factors[p] != prime_factors[p - 1])
                        {
                            occurence.Add(cnt);
                            if(cnt < min_occ)
                            {
                                min_occ = cnt;
                            }
                            cnt = 1;
                        }
                        else
                        {
                            cnt++;
                        }
                    }
                    occurence.Add(cnt);
                    if(cnt < min_occ)
                    {
                        min_occ = cnt;
                    }

                    int rel_factor= 1;
                    for(int i = 1; i <= min_occ; i++)
                    {
                        int c = 0;
                        foreach (int o in occurence)
                        {
                            if(o%i == 0)
                            {
                                c++;
                            }
                        }
                        if (c == occurence.Count)
                        {
                            rel_factor = i;
                        }
                    }
                    Console.WriteLine("rel factor: " + rel_factor.ToString());
                    
                    if(rel_factor != 1)
                    {
                        int var = (int)Math.Floor(lim_a * (HarmonicSum(rel_factor)));
                        sum = sum -  var;
                        Console.WriteLine("sum -= " + var.ToString());
                    }

                }
            }
            System.Console.WriteLine("the number of distinct powers are: " + sum.ToString());
        }
        */

        public void Solution_problem_29(int lima, int limb)
        {
            SortedSet<double> set = new SortedSet<double>();
            int sum = 0;
            for(int a = 2; a <= lima; a++)
            {
                for(int b = 2; b <= limb; b++)
                {
                    double number = Math.Pow(a,b);
                    set.Add(number);
                }
            }
            sum = set.Count;
            Console.WriteLine(sum.ToString());
        }

        /// <summary>
        /// Problem 30 
        /// </summary>

        public void Solution_problem_30()
        {
            
            // if it contains 9^5 or 8 is has to be atleast five digits.  
            
            long[] Powers = new long[10];
            for(int i = 0; i< 10; i++)
            {
                Powers[i] = (long)Math.Pow(i, 5);
            }
            long upper_bound = Powers[9] * 6; // 9^5 has 5 digits, meaninging that to get seven digits 11*9^5 is needed. Thus six digits is max. 

            long sum_numbers = 0; 

            for(int num = 2; num < upper_bound; num++)
            {
                int tmp = num;
                long sum = 0;
                while(tmp > 0 && sum <= num)
                {
                    int digit = tmp % 10;
                    sum += Powers[digit];
                    tmp /= 10;
                }
                if(sum == num)
                {
                    sum_numbers += num;
                    Console.WriteLine(" the number: " + num.ToString());  
                }
            }

            Console.WriteLine(sum_numbers.ToString());
        }

        /// <summary>
        /// Problem 31 
        /// </summary>

        public void Solution_problem_31(int max)
        {
            // 
            int[] coins = { 1, 2, 5, 10, 20, 50, 100, 200 };
            // 
            int[] num_ways = new int[max + 1];
            num_ways[0] = 1;

            foreach(int coin in coins)
            {
                for(int i = coin; i <= max; i++)
                {
                    num_ways[i] += num_ways[i - coin];
                }
            }
            Console.WriteLine(num_ways[200].ToString());
        }

        /// <summary>
        /// Problem 32 
        /// </summary>

        public void Solution_problem_32()
        {
            // Pandigital products: ie: 39,186,7254 - Where 7254 is a product where multiplicand 
            // 100*100 = 10000 
            // 99*99 = 9801 
            // implies that the multiplicands must have: 2,3 digits or 1,4 digits.
            SortedSet<int> set = new SortedSet<int>();
            SortedSet<int> products = new SortedSet<int>();
            long sum = 0;
            for(int mul1 = 2; mul1 < 99; mul1++)
            {
                for(int mul2 = 100; mul2 < 5000; mul2++)
                {
                    set.Clear();
                    bool pandigit = true;
                    int digits = mul1;
                   
                    while(digits > 0 && pandigit)
                    {
                        int digit = digits % 10;
                        if (!set.Add(digit) || digit == 0)
                        {
                            pandigit = false;
                        }
                        digits /= 10;
                    }
                    digits = mul2;
                    while (digits > 0 && pandigit)
                    {
                        int digit = digits % 10;
                        if (!set.Add(digit) || digit == 0)
                        {
                            pandigit = false;
                        }
                        digits /= 10;
                    }
                    digits = mul1 * mul2;
                    while (digits > 0 && pandigit)
                    {
                        int digit = digits % 10;
                        if (!set.Add(digit) || digit == 0)
                        {
                            pandigit = false;
                        }
                        digits /= 10;
                    }

                    if (pandigit && set.Count == 9)
                    {
                        products.Add(mul1 * mul2);
                    }

                }
            }

            foreach(int product in products)
            {
                sum += product;
            }
            Console.WriteLine(sum.ToString());
        }



        /// <summary>
        /// Problem 33 
        /// </summary>

        void LowestFraction(ref int numerator, ref int denominator)
        {
            int divisor = 2;
            while(divisor < denominator)
            {
                if(denominator%divisor == 0 && numerator%divisor == 0)
                {
                    denominator /= divisor;
                    numerator /= divisor;
                }
                else
                {
                    divisor++;
                }
            }

        }


        public void Solution_problem_33()
        {
            // less than one. and with two digits in both numerator and denominator.
            List<int> num_digits = new List<int>();
            List<int> den_digits = new List<int>();

            int final_num = 1;
            int final_den = 1;

            for (int numerator = 10; numerator <= 99; numerator++)
            {

                // first check if they share a digit. 

                // find the fraction it would have to be equal. 

                // find the lowest representation of both fractions.
                // if they are equal, then shit is cool.

                num_digits.Clear();
                num_digits.Add(numerator % 10);
                num_digits.Add((numerator / 10)%10);
                for(int denominator = numerator+1; denominator <= 99; denominator++)
                {
                    int n,d;
                    if (num_digits.Contains(denominator % 10) && denominator % 10 != 0)
                    {
                        d = (denominator/10) % 10;
                        num_digits.Remove(denominator % 10);
                        n = num_digits[0];
                        num_digits.Add(denominator % 10);
                    }
                    else if (num_digits.Contains((denominator / 10) % 10) && (denominator/10) % 10 != 0)
                    {
                        d = (denominator) % 10;
                        num_digits.Remove((denominator/10)%10);
                        n = num_digits[0];
                        num_digits.Add((denominator / 10) % 10);
                    }
                    else
                    {
                        continue;
                    }

                    int tmp_num = numerator;
                    int tmp_den = denominator;
                    LowestFraction(ref tmp_num, ref tmp_den);
                    LowestFraction(ref n, ref d);
                    if(n == tmp_num && d == tmp_den)
                    {
                        final_den *= d;
                        final_num *= n;
                    }
                  

                }
            }
            LowestFraction(ref final_num, ref final_den);
            Console.WriteLine(final_den.ToString());

        }

        /// <summary>
        /// Problem 34 
        /// </summary>

        public void Solution_problem_34()
        {
            // find the sum of all numbers who are equal to the sum of the factorial of their digits.
            // any number containing 9, must be larger than 362880
            // 9! is a six digit number -> 7*9! is at most 7 digits. -> 2540160 ... 7* 9!  

            // possible to limit search space further? 

            long max_value = 2540160;
            long[] factorial = new long[10];
            factorial[0] = 1;
            for(int i = 1; i < 10; i++)
            {
                factorial[i] = factorial[i - 1] * i;
            }

            long sum = 0;
            for(long num = 3; num < max_value; num++)
            {
                long remainder = num;
                long fact_sum = 0;
                while(remainder > 0 && fact_sum < num)
                {
                    fact_sum += factorial[remainder % 10];
                    remainder /= 10;
                }
                if(fact_sum == num)
                {
                    sum += num;
                }
            }

            Console.WriteLine(sum.ToString());
        }


        /// <summary>
        /// Problem 35 
        /// </summary>

        long RotateNumber(long number)
        {
            long digit = number % 10;
            number /= 10;
            number += digit * (long) Math.Pow(10, number.ToString().Length);
            return number;
        }

        public void Solution_problem_35(long max_value)
        {
            // Circular primes:
            // we only need to check all permutations of numbers containing 1,3,7,9. Once we pass 10.
            // if 137 is not a prime, it does not matter if 371, or 713 is.
            SortedSet<long> circ_prime = new SortedSet<long>();

            // all 1 digit primes are circular.
            for (int n = 2; n < Math.Min(8,max_value); n++)
            {
                if (IsPrime(n))
                {
                    circ_prime.Add(n);
                }
            }
            
            for (int n = 11; n < max_value; n+= 2)
            {
                string number = n.ToString();
                // not sure if this is slower or faster, than just checking if the number is a prime.
                if (number.Contains("2") || number.Contains("4") || number.Contains("6") || number.Contains("8") || number.Contains("5") || number.Contains("0")) 
                {
                    continue;
                }
                else if (IsPrime(n))
                {
                    // it has already been added.
                    if (circ_prime.Contains(n))
                    {
                        continue;
                    }

                    //  
                    List<long> check = new List<long>();
                    check.Add(n);
                    for(int i = 0; i < number.Length-1; i++)
                    {
                        long r_prime = RotateNumber(check[i]);
                        if (IsPrime(r_prime))
                        {
                            check.Add(r_prime);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if(check.Count == number.Length)
                    {
                        foreach(long prime in check)
                        {
                            if(prime < max_value)
                            {
                                circ_prime.Add(prime);
                            }
                        }
                    }


                }
            }
            Console.WriteLine(circ_prime.Count.ToString());
        }
        
        bool IsPalindrome(string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i] != s[s.Length-1-i])
                {
                    return false;
                }
            }
            return true;
        }

        void MakePalindromes(int pal, ref long odd, ref long even)
        {
            string palindrome = pal.ToString();
            odd = 0;
            even = 0;
            for (int i = 0; i < palindrome.Length; i++)
            {
                 if(i == palindrome.Length - 1)
                {
                    odd += Convert.ToInt64(palindrome.Substring(i, 1)) * (long)Math.Pow(10, i); 
                }
                else
                {
                    odd += Convert.ToInt64(palindrome.Substring(i, 1)) * (long)Math.Pow(10, i);
                    odd += Convert.ToInt64(palindrome.Substring(i, 1)) * (long)Math.Pow(10, palindrome.Length*2 - i -2);
                }

                even += Convert.ToInt64(palindrome.Substring(i, 1)) * (long)Math.Pow(10, i);
                even += Convert.ToInt64(palindrome.Substring(i, 1)) * (long)Math.Pow(10, palindrome.Length * 2 - i - 1);

            }

        }

        public void Solution_problem_36(long max_number)
        {
            // already have a simple function to check for palindromes. 
            // in base number 10.

            // since any binary number cannot end in 0.
            // all valid numbers have to be odd.

            // Only check palindromes? 
            // 1 - 11, 2 - 22, 3 -33, 4 - 44,... 151 - 1551 etc.
            // Only check numbers below the limit.
            // stop when the one with odd number of digits exceeds max number. 

            long even = 0, odd = 0;
            long sum = 0;
            int pal = 1;
            while(odd < max_number)
            {
                MakePalindromes(pal, ref odd, ref even); 
                string base2 = Convert.ToString(odd, 2);
                if(IsPalindrome(base2))
                {
                    sum += odd;
                }
                base2 = Convert.ToString(even, 2);
                if (IsPalindrome(base2) && even <= max_number)
                {
                    sum += even;
                }
                pal++;
            }
            Console.WriteLine(sum.ToString());  
        }

        public void Solution_problem_201(int subset_size)
        {
            // The total number is (100)
            //                      50
            // ie: binominal coefficient. 

            // total number of subsets. 
            // if for a number n.
            // U(B,n) contains k subsets with the same value. then there are k* (100 - 2n)
            //                                                                   (50-n)
            // number of smaller subsets possible to make without the n*2 entries that will end up with the same sum as another entry. 
            // Problem: 
            
            //A sqare number can be given as.
            // n^2 = 1 + 3 + ... + 2n-3 + 2n-1


            
        }


        public void Solution_problem_187(int max_number)
        {
            //Max number = 10^8.
            // n -> composite integers.
            // step 1: find the list of all primes.
            SortedSet<int> numbers = new SortedSet<int>();
            for (int i = 2; i < max_number/2 +1; i++)
            {
                numbers.Add(i);
            }
            
            List<int> primes = new List<int>();
            
            while (numbers.Count > 0)
            {
                primes.Add(numbers.Min);
                int rem = numbers.Min * 2;
                while(rem < numbers.Max)
                {
                    numbers.Remove(rem);
                    rem += numbers.Min;
                }
                numbers.Remove(numbers.Min);
            }

            long count = 0;
            for(int i = 0; i < primes.Count; i++)
            {
                for(int j = i; j < primes.Count; j++)
                {
                    int product = primes[i] * primes[j];
                    if(product > max_number)
                    {
                        break;
                    }
                    count++;
                }
            }

            Console.WriteLine(count.ToString());

        }

    }
}
