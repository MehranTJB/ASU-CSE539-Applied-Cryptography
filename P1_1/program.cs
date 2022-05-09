using System;
using System.IO;
using System.Collections;

namespace P1_1
{
    class Program
    {
        public static byte[] StegFunc(byte[] InputBytes, byte[] bmpImgBytes)
        {
            BitArray InputBits = new BitArray(InputBytes);

            byte[] TempByteArray = new byte[bmpImgBytes.Length]; // just a placeholder so that the code works from scatch without errors
            
            for(int i = 0; i < 26; i++)
            {
                TempByteArray[i] = bmpImgBytes[i];
            }

            for(int i = 0; i < 96; i += 2)
            {
                Byte Counter;
                int ImgZoneMax = (i / 8 + 1) * 8 - 1;
                int ImgZoneMin = (i / 8) * 8;
                int ImageZone = ImgZoneMax - (i - ImgZoneMin);
                if(InputBits[ImageZone] == true && InputBits[ImageZone - 1] == true)
                    Counter = 3;
                else if(InputBits[ImageZone] == true && InputBits[ImageZone - 1] == false)
                    Counter = 2;
                else if(InputBits[ImageZone] == false && InputBits[ImageZone - 1] == true)
                    Counter = 1;
                else Counter = 0;
                TempByteArray[i / 2 + 26] = BitConverter.GetBytes(bmpImgBytes[i / 2 + 26] ^ Counter)[0];
            }

            return TempByteArray;
        }

        public static string getInputFromCommandLine(string[] args)
        {
            string input = "";
            if (args.Length == 1)
            {
                input = args[0]; // Gets the first string after the 'dotnet run' command
            }
            else
            {
                Console.WriteLine("Incorrect input! use 'dotnet run' followed by a string of array HEX code");
            }
            return input;
        }


        public static string P1_1(string[] args)
        {
            byte[] bmpImgBytes = new byte[]
            {
                0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00,
                0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00,0x18,0x00,
                0x00,0x00,0xFF,0xFF,0xFF,0xFF,0x00,0x00,0xFF,
                0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0x00,0x00,0x00,
                0xFF,0xFF,0xFF,0x00,0x00,0x00,0xFF,0x00,0x00,
                0xFF,0xFF,0xFF,0xFF,0x00,0x00,0xFF,0xFF,0xFF,
                0xFF,0xFF,0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,
                0x00,0x00,0x00};

            string Input = getInputFromCommandLine(args);

        
            string[] InputArray=Input.Split(' ');
            byte[] InputBytes = new byte[12]; 

            for(int i = 0; i < InputArray.Length; i++)
            {
                InputBytes[i] = Convert.ToByte(InputArray[i], 16);
            }
            
            byte[] Result = StegFunc(InputBytes, bmpImgBytes); 

            string OutputResult = BitConverter.ToString(Result).Replace("-", " "); 
            Console.WriteLine(OutputResult); 

            return OutputResult; 
        }

        static void Main(string[] args)
        {   
            P1_1(args);            
        }

    }
}

