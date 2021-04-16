using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

//Author: Dennis Granberg

//Class for Validity checks on Swedish personal numbers.
namespace Omegapoint_Uppgift
{
    public class Personnr : ValidityCheck
    {
        public List<int> Pnr {get; set;}

        /*Constructor takes a string as an input argument, 
          invokes the PreProcessInput method for some formatting checks.*/
        public Personnr(string InputPnr) : base(InputPnr)
        {
            //If the data format is correct, convert the data to a list of integers.
            Pnr = PreProcessInput(InputPnr).Select(x => Convert.ToInt32(x.ToString())).ToList();
        }

        /*Processes the data, removes any character that is not a digit,
          checks the length and slices the input before invoking a check for a valid date*/
        private string PreProcessInput(string InputPnr)
        {
                string CleanInput = new string(InputPnr.Where(char.IsDigit).ToArray());
                if (CleanInput.Length == 12)
                {
                    CleanInput = CleanInput.Substring(2);
                }
                string StrippedInput = CleanInput[..^4];

                if (!ValidDateFormat(StrippedInput))
                {
                    throw new ArgumentException("Wrong format on input.");
                }
                return CleanInput; 
        }

        /*Checks if the input is written in a correct date format*/
        private bool ValidDateFormat(string date)
        {
            DateTime dateValue;
            string format = "yyMMdd";
            return (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateValue));
        }

        //Check if the last number in the personnumber is correct. 
        public bool CheckValidity()
        {
            //Check if this personnumber have already been validated
            if (this.BeenValidated)
            {
                return this.Valid;
            }

            //Extract the last number in the personnumber
            int GivenControlNr = Pnr.Last();
            Pnr.RemoveAt(Pnr.Count() - 1);

            //Split the list into 2 separate lists depending on their indices.
            List<int> EvenPositions = Pnr.Where((x, i) => i % 2 == 0).ToList();
            List<int> OddPositions = Pnr.Where((x, i) => i % 2 != 0).ToList();

            //Multiply every number that was positioned at even positions by 2
            var ResEven = EvenPositions.Select(x => x * 2);
            //Sum all the numbers found at odd positions and save to the preliminary ControlNr.
            int ControlNr = OddPositions.Sum();

            /*Check the multiplied values if any number is greater than 10,
              split those numbers into separate numbers and add them.
              Add all values to the ControlNr.*/
            foreach (int i in ResEven)
            {
                var Tmp = i;
                if (Tmp >= 10)
                {
                    Tmp = (Tmp % 10) + 1;
                }
                ControlNr += Tmp;
            }

            //Do final calculations to retrieve the controlnr
            ControlNr = ControlNr % 10;
            ControlNr = 10 - ControlNr;
            ControlNr = ControlNr % 10;

            //Compare and send the result to parent for storage
            base.CompleteValidation(ControlNr == GivenControlNr);
            //Return result to caller
            return(this.Valid);
        }
    }
}