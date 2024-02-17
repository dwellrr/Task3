using System.ComponentModel.DataAnnotations;

namespace Task3.Models
{

    public class Client
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
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
        public string contact { get; set; }
        public int day { get; set; }
        public string month { get; set; }
        public int campaign { get; set; }
        public int pdays { get; set; }
        public int previous { get; set; }
        public string poutcome {  get; set; }
        public bool wasCalled { get; set; }
        public bool isParticipating { get; set; }
    }

    public class ClientQueryInfo
    {
        [Required(ErrorMessage = "Card Number is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Card Number should only contain numbers.")]
        public string CardNumber { get; set; }

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
}
