using System;

//Author: Dennis Granberg

/* Parent class for validity checks, contains logic which is universal for all validity checks */
namespace Omegapoint_Uppgift
{
    public class ValidityCheck
    {
        public bool Valid { get; private set;}
        public bool BeenValidated { get; private set;}

        /*Check if the data due for a validation check is null */
        public ValidityCheck(Object _RawData)
        {
            Valid = false;
            BeenValidated = false;
            if (_RawData == null)
            {
                throw new ArgumentNullException("Data is null, try again.");
            }
        }

        public void CompleteValidation(bool Result)
        {
            Valid = Result;
            BeenValidated = true;
        }
    }
}