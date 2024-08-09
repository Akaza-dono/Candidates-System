using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservation_System.Application.DTOs;
using Reservation_System.Domain;
using Reservation_System.Infrastructure.Commands.Candidates;
using Reservation_System.Infrastructure.Commands.Experience;
using Reservation_System.Infrastructure.Querys.Candidates;
using Reservation_System.Models;
using System.Diagnostics;

namespace Reservation_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
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
