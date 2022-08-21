using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NameDayAPI_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class NameDayController : ControllerBase
    {
        //controller method for returning the GetNameDay api
        [HttpGet(Name = "GetNameDay")]
        public string GetNameDay(string? name, string? day)
        {
            try
            {
                //variable declarations
                string dateTimeUrl = "https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings";


                //name conditions
                if (name == "")
                {
                    throw new Exception("The name field is required and cannot be empty");
                }

                if (name == null)
                {
                    throw new Exception("The name field is required and cannot be null");
                }

                if (double.TryParse(name, out double n))
                {
                    throw new Exception("The name field is cannot be an integer or floating value, please enter a name using A-Z characters");
                }

                if(name.Length < 3)
                {
                    throw new Exception("The name field must be at least three characters");
                }

                if (name.Length > 30)
                {
                    throw new Exception("The name field must be less than thirty characters");
                }


                //day conditions
                if (day == "")
                {
                    throw new Exception("The day field is required and cannot be empty");
                }

                if (day == null )
                {
                    throw new Exception("The day field is required and cannot be null");
                }

                if (DateTime.TryParse(day, out DateTime temp) == false)
                {
                    throw new Exception("The day must be formatted in DateTime, for more information on compatible formats please visit: " + dateTimeUrl);
                }

                //conditions met
                DateTime today = Convert.ToDateTime(day);
                string msg = "Hi, " + name + ". Today is: " + day.ToString() + ". Have a great day!";

                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}
