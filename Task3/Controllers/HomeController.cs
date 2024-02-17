
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task3.Models;

namespace Task3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FileRepository _repository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _repository = new FileRepository();
        }

        public IActionResult Index()
        {
            var stopwatchModel = new Models.Stopwatch();
            // Perform any additional setup if needed
            return View(stopwatchModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AuditView()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Assuming you have a method to retrieve client information from a file
        private Client GetClientInfo(string cardNumber)
        {
            // Call the repository to get the specific item
            var item = _repository.GetItemByCN(cardNumber);

            if (item != null)
            {
                // If the item is found, you can pass it to the view
                return item;
            }
            else
            {
                return new Client { Name = "NOTFOUND"};
                //return NotFound();
            }
            // Implement logic to read client information from a file
            // Return a ClientInfo object based on the card number
            // For simplicity, we'll create a dummy ClientInfo
            
        }



        [HttpPost]
        public IActionResult SubmitForm(ClientQueryInfo model)
        {
            if (ModelState.IsValid)
            {
                // Fetch additional client information based on the card number
                Client client = GetClientInfo(model.CardNumber);
                return View("ClientView", client);
            }
            else
            {
                // If the model state is not valid, return to the index view with errors
                return View("AuditView", model);
            }
        }

        [HttpPost]
        public IActionResult EvaluateCampaignChance([FromForm] int Id)
        {
            // Retrieve the client based on the provided ID
            var client = _repository.GetItemById(Id);

            if (client == null)
            {
                return NotFound();
            }

            MLModel.ModelInput mlinput = new MLModel.ModelInput
            {
                Age = client.age,
                Balance = client.balance,
                Campaign = client.campaign,
                Contact = client.contact,
                Day = client.day,
                Default = ConvertToBool(client.def),
                Education = client.education,
                Housing = ConvertToBool(client.housing),
                Job = client.job,
                Loan = ConvertToBool(client.loan),
                Marital = client.married,
                Month = client.month,
                Pdays = client.pdays,
                Poutcome = client.poutcome,
                Previous = client.previous
            };
            // TODO: Call your machine learning model to get prediction results
            var predictionResults = MLModel.Predict(mlinput);


            // Return results as JSON
            return Json(predictionResults);
        }

        public bool ConvertToBool(string text)
        {
            if (text == "no")
                return false;
            if (text == "yes")
                return true;
            else
                return false;
        }

        public IActionResult CallList()
        {
            // Read data from the JSON file
            var allItems = _repository.GetAllItems();
            List<ClientCallInfo> newList = new List<ClientCallInfo>();

            foreach (var originalModel in allItems)
            {
                // Create a new instance of NewModel with desired fields
                var newModel = new ClientCallInfo
                {
                    Name = originalModel.Name,
                    Surname = originalModel.Surname,
                    PhoneNumber = originalModel.PhoneNumber,
                    wasCalled = originalModel.wasCalled,
                    Id = originalModel.Id,
                    isParticipating = originalModel.isParticipating
                };

                // Append the new model to the new list
                newList.Add(newModel);
            }

            return View(newList);
        }

        public IActionResult Details(int id)
        {
            var user = _repository.GetItemById(id);
            ClientCallInfo info = new ClientCallInfo
            {
                Id = id,
                PhoneNumber = user.PhoneNumber,
                isParticipating=user.isParticipating,
                Name = user.Name,
                Surname=user.Surname,
                wasCalled=user.wasCalled
            };
            return View(info);
        }

    }



}

