
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }

        public IActionResult ClientView(Client client)
        {
            Client confirmedClient = _repository.GetItemById(client.Id);
            var viewModel = new ClientCallViewModel
            {
                client = confirmedClient,
                call = new Call()
            };

            return View(viewModel);
        }

        public IActionResult NewClientView()
        {
            // Instantiate a new instance of your model
            var model = new Client();

            model.DateOfBirth = DateTime.Now;
            // Optionally, set default values for properties if needed
            //model.def = "no";

            // Pass the model to the view
            return View(model);
        }

        public IActionResult EditClient(int id) 
        {
            Client client = _repository.GetItemById(id);
            return View(client);
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

        private Client GetClientInfo(string AccountNumber)
        {
            // Call the repository to get the specific item
            var item = _repository.GetItemByCN(AccountNumber);

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

            
        }



        [HttpPost]
        public IActionResult SubmitForm(ClientQueryInfo model)
        {
            if (ModelState.IsValid)

            {
                Client client = GetClientInfo(model.AccountNumber);
                if (client.Name == "NOTFOUND")
                {
                    TempData["ErrorMessage"] = "Client not found.";
                    return View("AuditView");
                }
                var viewModel = new ClientCallViewModel
                {
                    client = client,
                    call = new Call()
                };

                // Fetch additional client information based on the Account number
                
                return View("ClientView", viewModel);
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

        //public IActionResult CreateOrEdit(int? id)
        //{
        //
        //}

        [HttpPost]
        public ActionResult Create(Client model)
        {
            if (ModelState.IsValid)
            {
                // Extract data from the view model and save it to the database
                // Example:
                var client = new Client
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber,
                    age = model.age,
                    job = model.job,
                    married = model.married,
                    education = model.education,
                    def = model.def,
                    balance = model.balance,
                    housing = model.housing,
                    loan = model.loan,
                    contact = model.contact,
                    day = DateTime.Now.Day,
                    month = DateTime.Now.ToString("MMMM"),
                    campaign = model.campaign,
                    pdays = model.pdays,
                    previous = model.previous,
                    poutcome = model.poutcome,
                    wasCalled = model.wasCalled,
                    isParticipating = model.isParticipating
                };

                // Save the client data to the database
                // Example:
                _repository.AddItem(client);

                // Redirect to a success page or perform any other action
                return RedirectToAction("ClientView", "Home", client);
            }

            // If the model state is not valid, return the view with validation errors
            return View("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateItem(client);
                return RedirectToAction("ClientView", "Home", client);
            }

            return View("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddCall(ClientCallViewModel newcall)
        {

            _repository.AddCall(newcall.client.Id, newcall.call);

            Client client = _repository.GetItemById(newcall.client.Id);
            return RedirectToAction("ClientView", "Home", client);
        }

        [HttpPost]
        public ActionResult DeleteClient(ClientCallViewModel dclient) {

            _repository.DeleteItem(dclient.client.Id);
            return RedirectToAction("Index", "Home");
        }

    }




}

