using System;
//reisch method
namespace ConsoleApp1
{
    public class Reisch
    {

        public class ReischHashing
        {
            
            private int[] keys = TestClass.keys; //array of keys
            static int[] reischArray = new int[TestClass.tableSize];  //array of reisch table

            static String[] links = new String[TestClass.tableSize]; //array of links
            
            Random rnd = new Random();
            public void placeIntoEischArray(int[] keysArray, int size)  
            {
                int homeAddress;
                int randomAddress;

                for (int i = 0; i < keys.Length; i++)  
                {
                    homeAddress = keysArray[i] % size;

                    if (reischArray[homeAddress] == 0)
                    {
                        reischArray[homeAddress] = keys[i]; //if home address is empty, place key there
                    }
                    else if (reischArray[homeAddress] != 0 && links[homeAddress] == null) 
                    {

                        do
                        {
                            randomAddress = rnd.Next(size);
                        } while (reischArray[randomAddress] != 0);

                        reischArray[randomAddress] = keys[i];
                        links[homeAddress] = randomAddress + "";
                    }
                    else if (reischArray[homeAddress] != 0 && links[homeAddress] != null)
                    {

                        do
                        {
                            randomAddress = rnd.Next(size); //if home address is not empty, find random address
                        } while (reischArray[randomAddress] != 0); //if random address is not empty, find another random address

                        reischArray[randomAddress] = keys[i];
                        int tempAddress = Int32.Parse(links[homeAddress]); //if random address is empty, place key there
                        links[homeAddress] = randomAddress + ""; //if home address has link, place link there
                        links[randomAddress] = tempAddress + ""; //if random address has link, place link there
                    }
                }
            }

            public void showLastStatus() 
            {
                Console.WriteLine("Reisch Hashing");
                Console.WriteLine("Index\tKeys\tLinks");
                for (int i = 0; i < reischArray.Length; i++)
                    if (links[i] != null)
                        Console.WriteLine(i + "\t" + reischArray[i] + "\t" + links[i]);
                    else
                        Console.WriteLine(i + "\t" + reischArray[i] + "\t" + "null");
                averageProbe(TestClass.tableSize);
                averageProbeCount(TestClass.tableSize);
                packingFactor(TestClass.tableSize);
                totalProbeCount(TestClass.tableSize);

            }

            public void searchKeyIndex(int key, int size) 
            {
                int probe = 1;
                if (reischArray[key % size] == key) 
                {
                    Console.WriteLine("(Reisch) Key " + key + " found in index " + key % size + " with " + probe + " probe");
                }
                else if (links[key % size] != null)
                {
                    probe++;
                    int temp = Int32.Parse(links[key % size]);
                    if (reischArray[temp] == key)
                    {
                        Console.WriteLine("(Reisch) Key " + key + " found in index " + temp + " with " + probe + " probe");
                    }
                    else
                    {
                        Console.WriteLine("(Reisch) Key " + key + " not found");
                    }
                }
                else
                {
                    Console.WriteLine("(Reisch) Key " + key + " not found");
                }
            }

            public void totalProbeCount(int size)
            {
                int probe = 0;
                foreach (int key in keys)
                {
                    if (reischArray[key % size] == key)
                    {
                        probe++;
                    }
                    else if (links[key % size] != null)
                    {
                        probe++;
                        int temp = Int32.Parse(links[key % size]);
                        if (reischArray[temp] == key)
                        {
                            probe++;
                        }
                    }
                }

                Console.WriteLine("Total probe count:" + probe);
            }

            public void packingFactor(int size)
            {
                int count = 0;
                foreach (int j in reischArray)
                {
                    if (j != 0)
                    {
                        count++;
                    }
                }

                double pf = Convert.ToDouble(count) / Convert.ToDouble(size) * 100;
                Console.WriteLine("\nPacking factor:" + pf +"%");
            }

            public float averageProbeCount(int size)
            {
                int probe = 0;
                foreach (int key in keys)
                {
                    if (reischArray[key % size] == key)
                    {
                        probe++;
                    }
                    else if (links[key % size] != null)
                    {
                        probe++;
                        int temp = Int32.Parse(links[key % size]);
                        if (reischArray[temp] == key)
                        {
                            probe++;
                        }
                    }
                }

                double apc = Convert.ToDouble(probe) / Convert.ToDouble(keys.Length);
                Console.Write("Reisch Algorithm Average Probe Count:" + apc);
                return (float)apc;
            }

            public float averageProbe(int size)
            {
                int probe = 0;
                foreach (int key in keys)
                {
                    if (reischArray[key % size] == key)
                    {
                        probe++;
                    }
                    else if (links[key % size] != null)
                    {
                        probe++;
                        int temp = Int32.Parse(links[key % size]);
                        if (reischArray[temp] == key)
                        {
                            probe++;
                        }
                    }
                }

                double apc = Convert.ToDouble(probe) / Convert.ToDouble(keys.Length);
                return (float)apc; 
            }
            
        }

    }

}