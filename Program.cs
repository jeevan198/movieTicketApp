using System;
using System.Collections;
using System.Collections.Generic;
namespace MoviePlex
{
    class Program
    {
        static void Main(string[] args)
        {
            Main project = new Main();
            project.Welcome();
            //--------------------------------------------------------------------------
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nPress enter to Exit : ");
            Console.ReadLine();
        }
        
    }
}