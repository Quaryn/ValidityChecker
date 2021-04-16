using System;

//Author: Dennis Granberg

/*Driver for ValidityChecks.*/
namespace Omegapoint_Uppgift
{
    class ValidityChecker
    {
        static void Main(string[] args)
        {
            bool RunProgram = true;
            Console.WriteLine("Welcome to ValidityChecker 1.0!");

            while(RunProgram)
            {
                int Option;
                Console.WriteLine("Select your option:");
                Console.WriteLine("[1]: Personnumber Validation");
                Console.WriteLine("[2]: Personnumber Tests (Not implemented yet)");
                Console.WriteLine("[3]: Exit program");
                try 
                {
                    Option = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                switch (Option)
                {
                    case 1:
                        Console.WriteLine("Enter personnumber to validate:");
                        string GivenInput = Console.ReadLine();
                        try 
                        {
                            Personnr GivenPnr = new Personnr(GivenInput);
                            if(GivenPnr.CheckValidity())
                            {
                                Console.WriteLine("The given personnumber is valid.");
                            }
                            else
                            {
                                Console.WriteLine("The given personnumber is invalid.");
                            }                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }                        
                        break;

                    case 2:
                        Console.WriteLine("Tests not implemented yet.");
                        break;

                    case 3:
                        Console.WriteLine("Exiting program.");
                        RunProgram = false;
                        break;

                    default:
                        Console.WriteLine("Not a valid input, try again");
                        break;                  
                }
            }            
        }
    }
}
