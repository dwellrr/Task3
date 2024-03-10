using System.ComponentModel.DataAnnotations;

namespace Task3.Models
{

    public class Client
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        // Add other client properties as needed
        public int age { get; set; }
        public string job { get; set; }
        public string married { get; set; }
        public string education { get; set; }
        public string def { get; set; }
        public int balance { get; set; }
        public string housing { get; set; }
        public string loan { get; set; }
        public string contact { get; set; } //contact commmunication type
        public int day { get; set; } = DateTime.Now.Day; //last contact day
        public string month { get; set; } = DateTime.Now.ToString("MMMM"); //last contact month
        public int duration { get; set; } //last conact duration in seconds
        public int campaign { get; set; } //number of contacts during this campaign
        public int pdays { get; set; } //number of days that passed by after the client was last contacted from a previous campaign
        public int previous { get; set; } //number of contacts performed before this campaign and for this client
        public string poutcome { get; set; } = "unknown"; //outcome of the previous marketing campaign
        public bool wasCalled { get; set; } //was called for this campaign?
        public bool isParticipating { get; set; }
        public int amount { get; set; }
    }

    public class ClientQueryInfo
    {
        [Required(ErrorMessage = "Account Number is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Account Number should only contain numbers.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Confirmation Code is required.")]
        public string ConfirmationCode { get; set; }
        // Add other client properties as needed
    }

    public class ClientCallInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public bool wasCalled { get; set; }
        public bool isParticipating { get; set; }
    }

    public class Call
    {
        public int Id { get; set; }
        public int durationh { get; set; }
        public int durationm { get; set; }
        public int durations { get; set; }

        public string outcome { get; set; }
        public int amount { get; set; }

        public DateTime date { get; set; } = DateTime.Now;

        public int daysSince { get; set; } = 0;
    }

    public class ClientCallViewModel
    {
        public Client client { get; set; }
        public Call call { get; set; }

    }
}

