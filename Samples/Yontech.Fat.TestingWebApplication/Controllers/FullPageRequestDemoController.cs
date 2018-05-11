using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Yontech.Fat.TestingWebApplication.Models;

namespace Yontech.Fat.TestingWebApplication.Controllers
{
    public class FullPageRequestDemoController:Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var viewModel = new FullPageRequestViewModel();
            return View("Index", viewModel);
        }

        [HttpPost]
        public ViewResult ConcatenateName(string firstName, string lastName)
        {
            string concatenatedResult = null;
            if (string.IsNullOrWhiteSpace(firstName))
            {
                concatenatedResult = "Missing first name";
            }
            else if (string.IsNullOrWhiteSpace(lastName))
            {
                concatenatedResult = "Missing last name";
            }
            else
            {
                concatenatedResult = $"{firstName} {lastName}";
            }

            var viewModel = new FullPageRequestViewModel()
            {
                FirstName = firstName,
                LastName = lastName,
                ConcatenatedNameResult = concatenatedResult
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public ViewResult LongRequest([FromQuery] int? timeToSleep)
        {
            var vm = new FullPageRequestViewModel();

            if(timeToSleep.HasValue && timeToSleep > 0)
            {
                Thread.Sleep(timeToSleep.Value);
                vm.Message = $"Request lasted {timeToSleep} miliseconds";
            }

            return View("Index", vm);
        }
        
    }
}
