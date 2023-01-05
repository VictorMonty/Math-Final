// 223532IVCM
// Task: Implement the Carmichael number test. For given natural number it says if it is a Carmichael number.

using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Carmichael Number Test");
        Console.WriteLine("Enter a positive integer greater than 2:");

        // Accepts user input and converts it to an integer, saved as p
        Int64 p = Convert.ToInt64(Console.ReadLine());

        if (p <= 2)
        {
            Console.WriteLine("Error. Input must be an integer greater than 2.");
            Console.ReadLine();
            Environment.Exit(1);
        }

        // If the remainder of p divided by 2 is 0, p is even
        if (p % 2 == 0)
        {
            Console.WriteLine("Carmichael numbers cannot be even.");
            Console.ReadLine();
            Environment.Exit(1);
        }

        else
        {
            bool prime = millerRabin(p, 50);

            if (prime == true) //very likely a prime
            {
                Console.WriteLine(p + " is likely a prime number.");
                Console.ReadLine();
                Environment.Exit(1);
            }

            if (prime == false) //separating carmichael numbers from composite numbers...
            {

                int carmichael = (int)fermatCheck(p);

                if (carmichael == 1)
                {
                    Console.WriteLine("Carmichael");
                }
                else
                {
                    Console.WriteLine("Composite");
                }

            }
        }
    }

    // Miller Rabin Test
    public static bool millerRabin(Int64 p, int c)
    {

        Console.WriteLine("Miller-Rabin test started");

        Int64 k = p - 1;
        int n = 0;

        while (k % 2 == 0) // p - 1 = 2^n * k
        {
            k /= 2;
            n += 1;
        }

        long[] aArray;
        if (p < 4759123141) aArray = new long[] { 2, 7, 61 };
        else if (p < 341550071728321) aArray = new long[] { 2, 3, 5, 7, 11, 13, 17 };
        else aArray = new long[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 };

        for (int i = 0; i < aArray.Length; i++)
        {
            long a = Math.Min(p - 2, aArray[i]);
            BigInteger x = k;
            BigInteger y = 1;

            for (int j = 0; j < x; ++j)
            {
                y = (y * a) % p;
            }
            while (x != p - 1 && y != 1 && y != p - 1)
            {
                y = (y * y) % p;
                x *= 2;
            }
            if (y != p - 1 && x % 2 == 0)
            {
                Console.WriteLine("Miller-Rabin test returned composite");
                return false;
            }
        }
        Console.WriteLine("Miller-Rabin test returned prime");
        return true;
    }
    public static BigInteger fermatCheck(BigInteger p)
    {
        // not testing primality, just testing for carmichaels so smallest coprime a will do...
        int a = 2;
        BigInteger x = 1;

        // apply mod during exponentiation...
        for (int i = 0; i < p - 1; i++)
        {
            x = x * a;
            x = x % p;

            Console.WriteLine("x is " + x);

        }

        return x;
    }

}
