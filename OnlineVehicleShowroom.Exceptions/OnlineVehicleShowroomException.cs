using System;

namespace OnlineVehicleShowroom.Exceptions
{
    //Declaring Online Vehicle Showroom specific exception
    public class OnlineVehicleShowroomException : ApplicationException
    {
        //Declaring zero-parameterized constructor
        public OnlineVehicleShowroomException() : base()
        {

        }

        //Declaring signle-parameterized constructor with string parameter
        public OnlineVehicleShowroomException(string message) : base(message)
        {

        }

        //Declaring two-parameterized constructor with string and exception parameters
        public OnlineVehicleShowroomException(string message, Exception innerException) : base(message,innerException)
        {

        }
    }
}
