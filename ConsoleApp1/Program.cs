using System;
using System.Collections.Generic;
using System.Linq;

//Binary Method
namespace ConsoleApp1
{
    class Program
    {
        public class BinarySol
        {
            public List<Line> table { get; set; } //table of lines
            public int tableSize { get; set; } = 0; //size of table
            public int numOfItems { get; set; } = 0; //number of items in table
            public int mod { get; set; } = 0; //mod of table
            public int maxNum { get; set; } = 999; //max number of items in table
            public int totalProbeCount { get; set; } = 0; //total probe count
            public double packingFactor { get; set; } = 0.9; //packing factor
            public List<Node> solutionList { get; set; } = new List<Node>(); //list of solutions

            public BinarySol(int tableSize, double packingFactor) //Could be created by giving a N size. It automatically fills the table with default packing factor being ~%90
            {
                table = Enumerable.Repeat(new Line(), tableSize).ToList(); //creates a table with N lines
                this.tableSize = tableSize; //sets table size
                mod = tableSize; //sets mod
                this.packingFactor = packingFactor; //sets packing factor
                createTable();
            }

            public BinarySol(int[] numbers, int tableSize)//Could be created with user given n value and hashing function find the smallest prime number greater than table size
            {
                table = Enumerable.Repeat(new Line(), tableSize).ToList(); //creates a table with N lines
                this.tableSize = tableSize; //sets table size
                mod = tableSize; //sets mod
                createTable(numbers); //fills table with random numbers which is created by in main
            }

            public void createTable()//Fills table with random values. Does not add the number if it already exist in the table.
            {
                Random rnd = new Random(); //creates a random number generator
                BinaryTree Solver = new BinaryTree(solutionList); //creates a binary tree for solving the problem
                while (numOfItems < tableSize * packingFactor)   
                {
                    bool added = placeVal(rnd.Next(maxNum), Solver);
                    numOfItems = added ? numOfItems + 1 : numOfItems;
                }
            }

            public void createTable(int[] numbers)//Fills the table with user given array values.
            {
                BinaryTree Solver = new BinaryTree(solutionList);
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numOfItems < tableSize) //if number of items is less than table size
                    {
                        bool added = placeVal(numbers[i], Solver); //places the number
                        numOfItems = added ? numOfItems + 1 : numOfItems; //if number is added, number of items increases
                    }
                }
            }

            public bool search(int key, int mode)//Searches for a value in the table. Return true if it is found.
            {
                int probeCount = 0;
                int homeAddress = key % mod; //finds home address
                int incr = Convert.ToInt32(key / mod) % mod; //finds increment second hashing func
                int currentAddress = (homeAddress + incr) % mod; //finds current address
                probeCount++;
                if (table[homeAddress].num == key) 
                {
                    totalProbeCount += probeCount;
                    table[homeAddress].probeCount = probeCount; //sets probe count
                    if (mode == 1)
                        Console.WriteLine("(Binary) Key " + key+ " Found at " +homeAddress +" Probe count is " +  table[currentAddress].probeCount);
                    return true;
                }
                while (currentAddress != homeAddress) 
                {
                    probeCount++;
                    if (table[currentAddress].num == key)
                    {
                        totalProbeCount += probeCount;
                        table[currentAddress].probeCount = probeCount; //sets probe count
                        if (mode == 1)
                            Console.WriteLine("(Binary) Key " + key+ " Found at " + currentAddress +" Probe count is " +  table[currentAddress].probeCount);
                        return true;
                    }
                    currentAddress = (currentAddress + incr) % mod; //finds next address
                }
                if (mode == 1)
                    Console.WriteLine("(Binary) Key " + key + " not found.");
                return false;
            }

            public bool placeVal(int num, BinaryTree Solver)//Handles key placement for Binary Tree method.
            {
                int result = num % mod; //finds home address
                if (table[result].init == true && numOfItems < table.Capacity)
                {
                    if (!search(num, 0)) //if number is not found in the table
                    {
                        Solver.root = new Node(table[result].num, result, null, num);
                        Solver.root.setMode(mod);
                        Solver.solutionList = solutionList;
                        Solver.Solve(table);
                        return true;
                    }
                }
                else
                {
                    table[result] = new Line(num); //places the number
                    return true;
                }
                return false;
            }

            public void updateProbe()//Resets and calculates total probe count.
            {
                totalProbeCount = 0;
                foreach (Line line in table)
                {
                    if (line.init == true)
                    {
                        search(line.num, 0); //searches for the number
                    }
                }
            }

            public void printSolutionList()
            {
                Console.WriteLine("\n{0} collisions has occured.\n", solutionList.Count);
                foreach (Node root in solutionList) 
                {
                    Console.WriteLine("Collision between {0} and {1} at index {2} has occured.", root.carriedNum, root.num, root.address);
                    printTree(root, 0); //prints the tree
                    Console.WriteLine();
                }
            }
            public void printTab(int level) 
            {
                for (int i = 0; i < level; i++)
                {
                    Console.Write("\t");
                }
            }

            public void printTree(Node root, int level) 
            {
                if (root == null)
                    return;
                printTab(level);
                Console.WriteLine("{0}({1})", root.address, root.num);
                printTree(root.left, level + 1);
                printTree(root.right, level + 1);
            }

            public double getPackingFactor() 
            {
                return Convert.ToDouble(numOfItems) / Convert.ToDouble(tableSize) * 100;
            }

            public double getAvProbe() 
            {
                updateProbe();
                return Convert.ToDouble(totalProbeCount) / Convert.ToDouble(numOfItems);
            }

            public void showLastStat()
            {
                updateProbe();
                Console.WriteLine("{0}", "Binary Tree Method"); 
                Console.WriteLine("Index\tKeys\tProbe Count"); 
                for (int i = 0; i < table.Count; i++)
                {
                    Console.WriteLine(i + "\t" + table[i].num + "\t" + table[i].probeCount);
                    //Console.WriteLine("{0,3} {1,-25}", i, table[i].getString());
                }
                Console.WriteLine("Table Mod:"+ mod);
                Console.WriteLine("Value Count:" + numOfItems);
                Console.WriteLine("Packing Factor:" + getPackingFactor() + "%");
                Console.WriteLine("Avarage Probe Count:" + getAvProbe());
            }
            
        }

        internal class Line//Represents each line in the table.
        {
            public int num { get; set; }
            public int link { get; set; }
            public int probeCount { get; set; }
            public bool init { get; set; } = false;
            public Line() 
            {
                num = -1; //sets number to -1 to represent empty
                link = -1;
                probeCount = -1;
                init = false;
            }
            public Line(int num) 
            {
                this.num = num;
                link = -1;
                init = true;
            }
            public Line(int num, int link)
            {
                this.num = num;
                this.link = link;
                init = true;
            }
            public string getString()
            {
                string number = init == false ? "---" : num.ToString();
                string probeStr = probeCount == -1 ? string.Empty :  probeCount.ToString();
                return string.Format("{0,4}  {1}", number, probeStr);
            }
        }

        internal class Node //each node in tree
        {
            public int num { get; set; } = 0;
            public int address { get; set; } = 0;
            public int carriedNum { get; set; } = -1;
            public Node left { get; set; } = null;
            public Node right { get; set; } = null;
            public Node parent { get; set; } = null;
            public static int mod { get; set; } = -1;
            public Node(int num, int address, Node parent, int carriedNum)
            {
                this.num = num;
                left = null;
                right = null;
                this.parent = parent;
                this.address = address;
                this.carriedNum = carriedNum;
            }
            public bool continueLeft(List<Line> table, Node parent)//Adds a left node to current node.
            {
                int incr = Convert.ToInt32(parent.carriedNum / mod) % mod;
                int newAddress = (parent.address + incr) % mod;
                if (table[newAddress].init == false)
                {
                    parent.left = new Node(table[newAddress].num, newAddress, parent, parent.carriedNum);
                    return true;
                }
                else
                {
                    parent.left = new Node(table[newAddress].num, newAddress, parent, parent.carriedNum);
                    return false;
                }
            }
            public bool addRight(List<Line> table, Node parent)//Adds a right node to current node.
            {
                int incr = Convert.ToInt32(parent.num / mod) % mod;
                int newAddress = (parent.address + incr) % mod;
                if (table[newAddress].init == false)
                {
                    parent.right = new Node(table[newAddress].num, newAddress, parent, parent.num);
                    return true;
                }
                else
                {
                    parent.right = new Node(table[newAddress].num, newAddress, parent, parent.num);
                    return false;
                }
            }
            public void traceBack(Node current, List<Line> table)//Returns a list of nodes from found empyt node to root node.
            {
                List<Node> shiftList = new List<Node>();
                while (current != null)
                {
                    shiftList.Add(current);
                    current = current.parent;
                }
                shiftNodes(shiftList, table);
            }
            public void shiftNodes(List<Node> shiftList, List<Line> table)//Analyzes shift list and does the neccassary shift operations.
            {
                int currentCarry = shiftList[0].carriedNum;
                List<Node> orders = new List<Node>();
                orders.Add(shiftList[0]);//Order list contains which values will moved to which index.
                foreach (Node node in shiftList) //find the order of the shift operations
                {
                    if (node.carriedNum != currentCarry)
                    {
                        currentCarry = node.carriedNum;
                        orders.Add(node);
                    }
                }
                foreach (Node node in orders)//Executes the order list.
                {
                    table[node.address] = new Line(node.carriedNum); //sets the table value to the carried value.
                }
            }

            public void setMode(int num)  //Sets the mod value for the tree.
            {
                mod = num;
            }
        }

        internal class BinaryTree
        {
            public Node root { get; set; }
            public List<Node> solutionList { get; set; } = new List<Node>();

            public BinaryTree(int collisionAddress, int homeNum, int collidedNum, int mod, List<Node> solutionList) 
            {
                root = new Node(homeNum, collisionAddress, null, collidedNum); //Creates the root node.
                root.setMode(mod);
                this.solutionList = solutionList;
            }

            public BinaryTree(List<Node> solutions)
            {
                solutionList = solutions; 
            }

            public void Solve(List<Line> table)//Handles collisions for binary tree method.
            {
                List<Node> leaves = new List<Node>();
                getLeaves(root, leaves);
                bool left = false, right = false;
                while (left == false && right == false)
                {
                    foreach (Node leaf in leaves)
                    {
                        left = leaf.continueLeft(table, leaf);
                        if (left == true)
                        {
                            leaf.traceBack(leaf.left, table);
                            solutionList.Add(root);
                            break;
                        }
                        right = leaf.addRight(table, leaf);
                        if (right)
                        {
                            leaf.traceBack(leaf.right, table);
                            solutionList.Add(root);
                            break;
                        }
                    }
                    leaves = new List<Node>();
                    getLeaves(root, leaves);
                }
            }

            public void getLeaves(Node root, List<Node> leaves) 
            {
                if (root != null)   //Gets all the leaves of the tree.
                {
                    if (root.left == null && root.right == null)
                    {
                        leaves.Add(root);
                    }
                    getLeaves(root.left, leaves);
                    getLeaves(root.right, leaves);
                }
               /* {
                    if (root.left == null && root.right == null)
                    {
                        leaves.Add(root);
                    }
                    if (root.left != null)
                    {
                        getLeaves(root.left, leaves);
                    }
                    if (root.right != null)
                    {
                        getLeaves(root.right, leaves); 
                    }
                }*/
            }
        }
       
    }
}

    
    
  