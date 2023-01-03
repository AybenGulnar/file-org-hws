using System;
/*Ayben Gülnar 191180041*/

namespace ConsoleApp1
{
    /*If you try small at first when entering a value, you can see the result more clearly.
     It works correctly as you can see from "printsolution" for very large values, but it takes a little longer, so please wait :)
    While applying hashing, I took the value from the user and found the smallest prime number larger than the value and 
    assigned it as the table size. As I researched I found this to have several advantages such as avoiding infinite loops. 
    I asked you this in class, and you said it would be better that way. 
    While writing, I took care not to include repetitive expressions and to make the naming properly.
    And Note: I used Rider IDE (if you had any problem with Vs)*/
    
    public class TestClass
    {
        public static int[] keys = CreateRandomDifferentValues();

        public static int tableSize = FindNextPrimeNumber();

        public static int FindNextPrimeNumber()
        {
            int num = keys.Length;
            for (var i = num + 1;; i++) {
                var isPrime = true;
                for (var d = 2; d * d <= i; d++) {
                    if (i % d == 0) {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) {
                    Console.WriteLine("Table size:" +i);
                    return i;
                }
            }
            
        }

        public static int[] CreateRandomDifferentValues()
        {
            Console.WriteLine("Please enter a value to count: ");
            int value = Convert.ToInt32(Console.ReadLine());
            
            Random random = new Random();
            int[] values = new int[value];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = random.Next(0, 10000);
                for (int j = 0; j < i; j++)
                {
                    if (values[i] == values[j])
                    {
                        i--;
                        break;
                    }
                }
            }

            Console.WriteLine("Random values: " + string.Join(", ", values));
            return values;

        }
        static void Main(string[] args)
            {
                Reisch.ReischHashing reisch = new Reisch.ReischHashing();
                reisch.placeIntoEischArray(keys,tableSize);
                reisch.showLastStatus();

                Program.BinarySol binarySol = new Program.BinarySol(keys, tableSize);
                //Program.Table treeTable2 = new Program.Table(47 ,0.8); //Constructor with table size and packing factor. Values are created randomly.

                binarySol.showLastStat();
                binarySol.printSolutionList(); //Prints solutions trees for each collision.
                
                 Console.WriteLine("******");
                 if (binarySol.getAvProbe() < reisch.averageProbe(tableSize)) 
                     Console.WriteLine("According to Average Probe Count Binary tree is better than Reisch");
        
                 else if (binarySol.getAvProbe() > reisch.averageProbe(tableSize)) 
                     Console.WriteLine("According to Average Probe Count Reisch is better than Binary tree");
        
                 else 
                     Console.WriteLine("According to Average Probe Count Binary tree and Reisch are equal");
                 
                 Console.WriteLine("******");
                    
                 Console.WriteLine("Do you want to search a key? (y/n)");
                 String answer = Console.ReadLine();
                 if (answer == "y")
                 {
                     Console.WriteLine("Please enter a key: ");
                     int key = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                     binarySol.search(key, 1);
                     reisch.searchKeyIndex(key,tableSize);

                 }
        
                 else
                 {
                     Console.WriteLine("Have a nice dayy :)");
                 }
                 
            }

        }
}
