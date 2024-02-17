using Microsoft.AspNetCore.Mvc;
using Task3.Models;

namespace Task3.Controllers
{

    public class StopwatchController : Controller
    {
        private readonly Stopwatch _stopwatchModel;

        public StopwatchController(Stopwatch stopwatchModel)
        {
            _stopwatchModel = stopwatchModel;
        }

        public IActionResult Index()
        {
            return View(_stopwatchModel);
        }

        public IActionResult Start()
        {
            _stopwatchModel.IsRunning = true;
            return RedirectToAction("Index", "Home");
            //return View(_stopwatchModel);
        }

        public IActionResult Stop()
        {
            _stopwatchModel.IsRunning = false;
            return RedirectToAction("Index", "Home");
            //return View(_stopwatchModel);
        }
    }

}
