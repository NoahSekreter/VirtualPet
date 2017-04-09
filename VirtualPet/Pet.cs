using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{

    class Pet
    {
        private string name;
        private int[] stats = new int[7];
        private Random r = new Random();

        //Default constructor if the user didn't put in a name
        public Pet()
        {
            this.name = "Cerea";

            //stats contains all of a pet's stats : The minimum for a stat is 0 while the maximum is 100
            stats[0] = 18; // 0 is love
            stats[1] = 8; // 1 is smarts
            stats[2] = 18; // 2 is hunger
            stats[3] = 22; // 3 is thirst
            stats[4] = 8; // 4 is boredom
            stats[5] = 14; // 5 is sleepyness
            stats[6] = 24; // 6 is sickness
        }

        public Pet(string name)
        {
            this.name = name;

            //stats contains all of a pet's stats : The minimum for a stat is 0 while the maximum is 100
            stats[0] = 18; // 0 is love
            stats[1] = 8; // 1 is smarts
            stats[2] = 18; // 2 is hunger
            stats[3] = 22; // 3 is thirst
            stats[4] = 8; // 4 is boredom
            stats[5] = 14; // 5 is sleepyness
            stats[6] = 24; // 6 is sickness
        }

        //Accessor methods, compressed to easily see the code and what it returns
        public string Name { get { return name; } }
        public int Love { get { return stats[0]; } }
        public int Smarts { get { return stats[1]; } }
        public int Hunger { get { return stats[2]; } }
        public int Thirst { get { return stats[3]; } }
        public int Boredom { get { return stats[4]; } }
        public int Sleepyness { get { return stats[5]; } }
        public int Sickness { get { return stats[6]; } }
        
        //All the actions are here
        public void Feed()
        {
            //Reduce hunger
            Console.WriteLine("You give " + name + " some algae, it seems really happy!");
            stats[2] -= r.Next(25, 50);
            Tick();
        }
        public void Hydrate()
        {
            //Reduce thirst
            Console.WriteLine("You get " + name + " a small pool of water.\nIt sucks the water up playfully!");
            stats[3] -= r.Next(25, 50);
            Tick();
        }
        public void Play()
        {
            //Reduce boredom
            Console.WriteLine("You play with your little slime pet, it's really appreciating your company!");
            stats[4] -= r.Next(30, 55);
            Tick();
        }
        public void Nap()
        {
            //Reduce sleepyness
            Console.WriteLine("You bring " + name + " to its bed, letting it have sweet dreams.");
            stats[5] -= r.Next(35, 50);
            Tick();
        }
        public void Heal()
        {
            //Reduce sickness
            Console.WriteLine("You go to the vet with " + name + ", and after a quick checkup your slime pet is feeling pretty great!");
            stats[6] -= r.Next(45, 60);
            Tick();
        }
        public void Train()
        {
            //Increase smarts
            Console.WriteLine("You spend your time teaching " + name + " new tricks and words.");
            stats[1] += r.Next(8, 14);
            Tick();
        }
        public void Cuddle()
        {
            //Increase love and has a chance of doing something extra
            Console.WriteLine("You relax with " + name + " cuddling beside you. It really enjoys your company!");
            stats[0] += 6;
            //Will do a random action, but there is a small chance nothing else will happen
            int extra = r.Next(0, 3);
            if(extra == 0)
            {
                Console.WriteLine(name + " fell asleep nuzzled up beside you. How cute!");
                stats[5] -= r.Next(35, 50);
            }
            if (extra == 1)
            {
                Console.WriteLine("You decide to give your pet slime a few flakes of algae to snack on. " + name + " is now extra happy!");
                stats[2] -= r.Next(20, 35);
            }
            if (extra == 2)
            {
                Console.WriteLine("You play a few video games. " + name + " seems quite interested!");
                stats[4] -= r.Next(30, 45);
            }
            Tick();
        }

        public void SmartPet()
        {
            //Depending on how smart it is, your pet will have a number of chances to perform smart actions (Rounded down)
            for(int i = 0; i < (int)Math.Floor((double)(stats[1] / 30)); i++)
            {
                //There is a one in three chance for each chance you get to perform a smart action
                if(r.Next(0, 2) == 0)
                {
                    //Is hunger the biggest concern?
                    if (stats[2] > stats[3] && stats[2] > stats[5])
                    {
                        Console.WriteLine(name + " decided to help itself to some extra algae flakes.");
                        stats[2] -= r.Next(8,16);
                    }
                    //Is thirst the biggest concern?
                    else if (stats[3] > stats[2] && stats[3] > stats[5])
                    {
                        Console.WriteLine(name + " went to hydrate itself a little bit.");
                        stats[3] -= r.Next(8, 16);
                    }
                    //Is sleep the biggest concern?
                    else
                    {
                        Console.WriteLine(name + " took a wee nap in its down time.");
                        stats[5] -= r.Next(8, 16);
                    }
                }
            }
        }

        //Makes changes every action the player takes. Can only be used by Pet class
        private void Tick()
        {
            //Goes through all stats except love and smarts
            for (int i = 2; i < stats.Length; i++)
            {
                if (stats[i] >= 85)
                {
                    //If any negative stat is too large, reduce love
                    stats[0] -= r.Next(6,9);
                }
                else if (stats[i] < 50)
                {
                    stats[0] += r.Next(1,2);
                }
                //stats[i] += 3;
            }
            
            //Gradually increase hungry, thirst, sleepyness, boredom, and sickness
            stats[2] += r.Next(5, 10);
            stats[3] += r.Next(5, 10);
            stats[4] += r.Next(12, 16); //Boredom is a bit faster
            stats[5] += r.Next(5, 10);
            stats[6] += r.Next(4, 8); //Sickness is a bit slower

            //Determines if your pet declares a smart action this turn
            SmartPet();

            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i] > 100)
                {
                    stats[i] = 100;
                }
                else if (stats[i] < 0)
                {
                    stats[i] = 0;
                }
            }
        }


        //Returns whatever condition your pet is in. i.e. hungry, thirsty, sleepy
        public string Condition()
        {
            string condition = "";
            int amount = 0;
            if(stats[2] >= 85)
            {
                amount++;
                condition += "hungry";
            }
            if (stats[3] >= 85)
            {
                amount++;
                condition += "thirsty";
            }
            if (stats[5] >= 85)
            {
                amount++;
                condition += "sleepy";
            }
            if (stats[6] >= 85)
            {
                amount++;
                condition += "sickly";
            }

            //Make condition grammatically correct
            if (amount >= 2)
            {
                condition = condition.Insert(condition.LastIndexOf('y', condition.Length-2) + 1, " and ");
            }
            if (amount == 3)
            {
                condition = condition.Insert(condition.IndexOf('y') + 1, ", ");
            }
            if (amount == 4)
            {
                condition = "hungry, thirsty, sleepy, and sickly";
            }
            return condition;
        }
        
        //Pet stats
        public void MyPetStatus()
        {
            //The different emotions of your pet
            string[] image = new string[8];
            if (stats[0] < 9 || stats[2] >= 85 || stats[3] >= 85 || stats[4] >= 85 || stats[5] >= 85 || stats[6] >= 85)
            {
                image[0] = "      _________      ";
                image[1] = "    _/         \\_    ";
                image[2] = "   /   __   __   \\   ";
                image[3] = "  /               \\  ";
                image[4] = " |     _______    |  ";
                image[5] = " |    /       \\   |  ";
                image[6] = "  \\              /   ";
                image[7] = "   \\____________/    ";
            }
            else if(stats[0] >= 60)
            {
                image[0] = "      _________  <3  ";
                image[1] = "    _/         \\_    ";
                image[2] = "   /   ^     ^   \\   ";
                image[3] = "  /               \\  ";
                image[4] = " |    \\_______/   |  ";
                image[5] = " |                |  ";
                image[6] = "  \\              /   ";
                image[7] = "   \\____________/    ";
            }
            else
            {
                image[0] = "      _________      ";
                image[1] = "    _/         \\_    ";
                image[2] = "   /             \\   ";
                image[3] = "  /   O       O   \\  ";
                image[4] = " |                |  ";
                image[5] = " |        o       |  ";
                image[6] = "  \\              /   ";
                image[7] = "   \\____________/    ";
            }

            //Display image and stats
            Console.WriteLine(image[0] + name);
            Console.WriteLine(image[1] + "Health: " + (100 - stats[6]));
            Console.WriteLine(image[2] + "Love: " + stats[0]);
            Console.WriteLine(image[3] + "Smarts: " + stats[1]);
            Console.WriteLine(image[4] + "Hunger: " + stats[2]);
            Console.WriteLine(image[5] + "Thirst: " + stats[3]);
            Console.WriteLine(image[6] + "Boredom: " + stats[4]);
            Console.WriteLine(image[7] + "Sleepyness: " + stats[5]);
        }
    }
}
