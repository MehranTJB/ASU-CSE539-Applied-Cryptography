using System;
using System.Numerics; 
using System.Collections.Generic;

namespace P4
{
    class Program
    {   
        // Find Greaest Common Divisor by Euclidean Algorithm
	// 1: Let a, b be the two numbers
	// 2. a mod b = R
	// 3. Let a = b and b = R
	// 4. Repeat Steps 2 , 3 until a mod b is greater than 0
	// 5. Greatest Common Divisor = b
	public static BigInteger Greatest_Common_Divisor(BigInteger Divisor , BigInteger Dividend)
        {
	    // ed = 1 mod (p - 1)(q - 1)
	    // e -> a
	    // phi_n (p -1)(q - 1) -> b
            BigInteger Temp1 = 0, Temp2 = 0, Temp3 = 1, Temp4 = 1, Temp5 = 1;
            BigInteger Quotient = 0, Remainder = 0, Grst_Comm_Div = 0;
	    while(Divisor != 0)
            {
                Quotient = Dividend / Divisor;
                Remainder = Dividend % Divisor;
                Temp1 = Grst_Comm_Div - Temp4 * Quotient;
                Temp2 = Temp3 - Temp5 * Quotient;
                Dividend = Divisor;
                Divisor = Remainder;
                Grst_Comm_Div = Temp4;
                Temp3 = Temp5;
                Temp4 = Temp1;
                Temp5 = Temp2;
            }
            return Grst_Comm_Div;
        } 
        public static string P4(string[] args)
        {

            BigInteger e = 65537;
            int p_e = 0, p_c = 0, q_e = 0, q_c = 0;
            BigInteger CipherText = 0;
            BigInteger PlaintText = 0;
            int Index = 1;
            foreach(var Item in args)
            {   
                switch (Index)
                {
                    case 1:
                        p_e = int.Parse(Item);
                        break;
                    case 2:
                        p_c = int.Parse(Item);
                        break;
                    case 3:
                        q_e = int.Parse(Item);
                        break;
                    case 4:
                        q_c = int.Parse(Item);
                        break;
                    case 5:
                        CipherText = BigInteger.Parse(Item);
                        break;
                    case 6:
                        PlaintText = BigInteger.Parse(Item);
                        break;
                    default:
                        break;
                }
                Index++;
            }
            // ed = 1 mod (p - 1)(q - 1)
            // e -> a
            // phi_n (p -1)(q - 1) -> b

            BigInteger p_Prime = 0, q_Prime = 0;
            p_Prime = BigInteger.Subtract(BigInteger.Pow(2, p_e), p_c);
            q_Prime = BigInteger.Subtract(BigInteger.Pow(2, q_e), q_c);
            BigInteger phi_n = BigInteger.Multiply(p_Prime - 1, q_Prime - 1);
            BigInteger Exp = Greatest_Common_Divisor(e, phi_n);
            
	    BigInteger Result1 = BigInteger.ModPow(CipherText, Exp, p_Prime * q_Prime);
            
	    BigInteger Result2 = BigInteger.ModPow(PlaintText, e, p_Prime * q_Prime);
            
	    string Result = Result1.ToString() + "," + Result2.ToString();
            Console.WriteLine(Result);
            return Result;
        }

        static void Main(string[] args)
        {
            P4(args); 
        }
    }
}

