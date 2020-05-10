using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe_Payment.Models;

namespace Stripe_Payment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions { 
                Amount=500,
                Description="Test Payment",
                Currency="usd",
                Customer = customer.Id,
                ReceiptEmail=stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"OrderId","111"},
                    { "PostCode","E15NB"},
                }
            });

            if(charge.Status == "succeeded" || charge.Status == "pending" || charge.Status == "failed")
            {
                string BalanceTranjectionId = charge.BalanceTransactionId;
                return View();
            }
            else
            {

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
