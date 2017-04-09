using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
    class Program
    {
        static void Main(string[] args)
        {
            Pet myPet;
            string response; //This will always be used for Console.ReadLine()
            int selectedOption = 0;

            Console.WriteLine("\n   < > < > < > Welcome welcome welcome to the magical pet shop! < > < > < >\n");
            Console.WriteLine("I know you're dying to get your new pet, so let's get started!\nWe've saved a lovely little blue Slime just for you!");
            Console.Write("What would you like to name your new pet? If you type in nothing, I'll give it a name for you! ");
            response = Console.ReadLine();
            if (response.ToLower() == "kek")
            {
                Kek(); //Easter egg. Infinite loop
            }
            if (response == "")
            {
                Console.WriteLine("Don't know a good name? I got one! \"Cerea\"!");
                myPet = new Pet();
            }
            else
            {
                Console.WriteLine(response + "? I like it!");
                myPet = new Pet(response);
            }

            Console.WriteLine("\nYou take your new slime pet home, it seems quite excited.");

            do
            {
                //The options you have with your pet
                Console.WriteLine();
                Console.WriteLine("What would you like to do with " + myPet.Name + "?");
                Console.WriteLine("1. Give food");
                Console.WriteLine("2. Give water");
                Console.WriteLine("3. Nap");
                Console.WriteLine("4. Take to vet");
                Console.Write("5. Play");
                //If pet isn't feeling well, display the reason why
                if (myPet.Hunger < 85 && myPet.Thirst < 85 && myPet.Sleepyness < 85 && myPet.Sickness < 85)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(" : " + myPet.Name + " is too " + myPet.Condition() + " to play.");
                }
                Console.Write("6. Train");
                //If pet isn't feeling well, display the reason why
                if (myPet.Hunger < 85 && myPet.Thirst < 85 && myPet.Sleepyness < 85 && myPet.Sickness < 85)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(" : " + myPet.Name + " is too " + myPet.Condition() + " to train.");
                }
                Console.Write("7. Cuddle");
                //If love isn't sufficient, the player will see the message
                if (myPet.Love >= 60)
                {
                    Console.WriteLine();
                }
                else if (myPet.Love < 60)
                {
                    Console.WriteLine(" : You require at least 60 Love to cuddle.");
                }
                Console.WriteLine("8. Check stats");
                Console.WriteLine("9. Quit the game");

                //Player enters in number
                Console.Write("\n> ");
                response = Console.ReadLine();
                //Check to see if what they typed in was an actual number
                while (!IsNumber(response))
                {
                    Console.WriteLine("\nSorry, could you type in a valid number?");
                    Console.Write("\n> ");
                    response = Console.ReadLine();
                }
                selectedOption = int.Parse(response);

                Console.WriteLine();

                //Do an action depending on what they entered
                switch(selectedOption)
                {
                    case 1:
                        Transition();
                        myPet.Feed();
                        break;
                    case 2:
                        Transition();
                        myPet.Hydrate();
                        break;
                    case 3:
                        Transition();
                        myPet.Nap();
                        break;
                    case 4:
                        Transition();
                        myPet.Heal();
                        break;
                    case 5:
                        //Won't work unless pet is healthy
                        if (myPet.Hunger >= 85 || myPet.Thirst >= 85 || myPet.Sleepyness >= 85 || myPet.Sickness >= 85)
                        {
                            Console.WriteLine(myPet.Name + " isn't ready to play. Take care of your slime first!");
                        }
                        else
                        {
                            Transition();
                            myPet.Play();
                        }
                        break;
                    case 6:
                        //Won't work unless pet is healthy
                        if (myPet.Hunger >= 85 || myPet.Thirst >= 85 || myPet.Sleepyness >= 85 || myPet.Sickness >= 85)
                        {
                            Console.WriteLine(myPet.Name + " isn't ready to train. Take care of your slime first!");
                        }
                        else
                        {
                            Transition();
                            myPet.Train();
                        }
                        break;
                    case 7:
                        if (myPet.Love < 60)
                        {
                            Console.WriteLine(myPet.Name + " isn't in the mood to cuddle. You gotta love your pet some more first!");
                        }
                        else
                        {
                            Transition();
                            myPet.Cuddle();
                        }
                        break;
                    case 8:
                        myPet.MyPetStatus();
                        break;
                    case 9:
                        break;
                    default:
                        Console.WriteLine("Sorry, that's not one of the available actions.");
                        break;
                }
                //If the pet is not taken care of, end the game
                if(myPet.Love == 0 || myPet.Sickness == 100 || myPet.Hunger == 100 || myPet.Thirst == 100 || myPet.Sleepyness == 100)
                {
                    Console.Write("\n...It seems you've neglected your pet a bit too much. ");
                    selectedOption = 9;
                }
            } while (selectedOption != 9);

            //If the player did not take care of their pet, display one of these messages
            if (myPet.Love == 0)
            {
                Console.WriteLine(myPet.Name + " ran away.\n\n\t\t     < > < > < > GAME OVER < > < > < >\n");
            }
            else if (myPet.Sickness == 100)
            {
                Console.WriteLine(myPet.Name + " died from sickness.\n\n\t\t     < > < > < > GAME OVER < > < > < >\n");
            }
            else if (myPet.Hunger == 100)
            {
                Console.WriteLine(myPet.Name + " died from hunger.\n\n\t\t     < > < > < > GAME OVER < > < > < >\n");
            }
            else if (myPet.Thirst == 100)
            {
                Console.WriteLine(myPet.Name + " died from thirst.\n\n\t\t     < > < > < > GAME OVER < > < > < >\n");
            }
            else if (myPet.Sleepyness == 100)
            {
                Console.WriteLine(myPet.Name + " died from sleep deprivation.\n\n\t\t     < > < > < > GAME OVER < > < > < >\n");
            }
            //If they took care of their pet, display this instead
            else
            {
                //Congradulate player for maxing out love
                if(myPet.Love == 100)
                {
                    Console.WriteLine("Congradulations! " + myPet.Name + " loves you to bits!\nAs a reward, go restart the game, but put in \"kek\" as the name...\n");
                }
                Console.WriteLine("Thanks for playing!");
            }
        }

        //Slow transition into things
        static void Transition()
        {
            foreach (char c in ". . . ")
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(350);
            }
            Console.WriteLine("\n");
        }

        //Checks to see if the string is a number
        static bool IsNumber(string str)
        {
            //Prevents fatal error if string is nothing or too large for the int to contain the number
            if(str.Length == 0 || str.Length > 6)
            {
                return false;
            }

            //Checks every character to see if it is a number
            foreach(char c in str)
            {
                if(c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        //This is a silly and completely pointless feature, however it is vastly entertaining
        static void Kek()
        {
            bool shrink = true;
            string space1 = "             ";
            string space2 = "";
            string magic;
            while (1 < 2) //deadmau5 reference. Also intentional infinite loop.
            {
                if (space1.Length == 13) //Should whitespace #1 shrink now?
                {
                    shrink = true;
                }
                if (space1.Length == 0) //Should whitespace #1 grow now?
                {
                    shrink = false;
                }
                if (shrink) //Shrink whitespace #1, grow whitespace #2
                {
                    space1 = space1.Substring(0, space1.Length - 1);
                    space2 += " ";
                }
                else //Shrink whitespace #2, grow whitespace #1
                {
                    space2 = space2.Substring(0, space2.Length - 1);
                    space1 += " ";
                }

                //Creating the magic!
                magic = "KEK" + space1 + "KEK" + space2 + "KEK" + space2 + "KEK" + space1 + "KEK" + space1 + "KEK" + space2 + "KEK" + space2 + "KEK" + space1 + "KEK";

                Console.WriteLine(magic); //Unleashing the magic!
                System.Threading.Thread.Sleep(20); //Speed control. Bigger number = Slow, sensual ride. Smaller number = Maximum overkek!
            }
        }
    }
}
