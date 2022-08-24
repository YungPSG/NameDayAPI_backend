using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace NameDayAPI_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class NameDayController : ControllerBase
    {

        /* **************************************** DEMO EXPLANATION HERE: **********************************************************
         * In the demo request it was asked that name and day be required fields. It appears here as nullable but this is intentional. 
         * Making these fields nullable from the input side allows us to have more detailed error handling, so these fields are still 
         * required, just more robustly handled. Also, before this update, the exception handler would only return the first exception
         * that it hit. This has since been modified so that the user of the api can get a list of all the exceptions the api has hit. 
         * Also, the validation logic has been moved into two private void methods for increased readability.      
         ***************************************************************************************************************************/

        #region public methods
        //controller method for returning the GetNameDay api
        [HttpGet(Name = "GetNameDay")]
        public string GetNameDay(string? name, string? day)
        {
            try
            {
                //variable declarations
                List<Exception> exceptions = new List<Exception>();

                validateName(name, exceptions);
                validateDay(day, exceptions);

                if(exceptions.Count > 0)
                {
                    throw new AggregateException(
                        "Error(s) encountered...",
                        exceptions);
                }
                else
                {
                    //conditions met
                    DateTime today = Convert.ToDateTime(day);
                    string msg = "Hi, " + name + ". Today is: " + day.ToString() + ". Have a great day!";

                    return msg;
                }             
            }
            catch (AggregateException ex)
            {
                return ex.ToString();
            }

        }
        #endregion

        //private method for validating the name input field for the GetNameDay api
        #region private methods
        private void validateName(string? name, List<Exception> exceptions)
        {
            //name conditions
            if (name == "")
            {
                exceptions.Add(new Exception("The name field is required and cannot be empty"));
            }

            if (name == null)
            {
                exceptions.Add(new Exception("The name field is required and cannot be null"));
            }

            if (!Regex.Match(name, "^[a-zA-Z]*$").Success)
            {
                exceptions.Add(new Exception("The name field can only contain a-z characters."));
            }

            if (double.TryParse(name, out double n))
            {
                exceptions.Add(new Exception("The name field is cannot be an integer or floating value, please enter a name using A-Z characters"));
            }

            if (name.Length < 3)
            {
                exceptions.Add(new Exception("The name field must be at least three characters"));
            }

            if (name.Length > 30)
            {
                exceptions.Add(new Exception("The name field must be less than thirty characters"));
            }        
        }

        //private method for validating the day input field for the GetNameDay api
        private void validateDay(string? day, List<Exception> exceptions)
        {
            const string dateTimeUrl = "https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings";

            //day conditions
            if (day == "")
            {
                exceptions.Add(new Exception("The day field is required and cannot be empty"));
            }

            if (day == null)
            {
                exceptions.Add(new Exception("The day field is required and cannot be null"));
            }

            if (DateTime.TryParse(day, out DateTime temp) == false)
            {
                exceptions.Add(new Exception("The day must be formatted in DateTime, for more information on compatible formats please visit: " + dateTimeUrl));
            }
        }
        #endregion
    }
}
