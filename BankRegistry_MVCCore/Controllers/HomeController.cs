using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankRegistry_MVCCore.Models;

namespace BankRegistry_MVCCore.Controllers
{
    using Domain.ServiceInterfaces;
    using Domain;

    public class HomeController : Controller
    {
        private IBankService _bankService;
        private IContactPersonService _contactPersonService;
        private IPositionService _positionService;

        public HomeController(IBankService bankService, IContactPersonService contactPersonService, IPositionService positionService)
        {
            _bankService = bankService;
            _contactPersonService = contactPersonService;
            _positionService = positionService;

            #region Adding Positions
            if (_positionService.Set().Count() == 0)
            {
                _positionService.Save(new Position() { Name = "Bank Teller" });
                _positionService.Save(new Position() { Name = "Bank Marketing Representative" });
                _positionService.Save(new Position() { Name = "Internal Auditor" });
                _positionService.Save(new Position() { Name = "Branch Manager" });
                _positionService.Save(new Position() { Name = "Loan Officer" });
                _positionService.Save(new Position() { Name = "Data Processing Officer" });
                _positionService.Save(new Position() { Name = "General Director" });
                _positionService.Commit();
            }
            #endregion
        }

        [HttpGet]
        public IActionResult Index(string text, int? id, bool remove = false)
        {
            if (remove && id != null)
            {
                if (_bankService.Set().SingleOrDefault(s => s.ID == id) == null)
                    return NotFound("Bank not found!");
                _bankService.Remove(_bankService.Set().Single(s => s.ID == id));
                _bankService.Commit();
            }
            else if (!string.IsNullOrWhiteSpace(text))
                return View(_bankService.Set().Where(w => w.Name.ToLower().Trim().Contains(text.ToLower().Trim())).Select(s => (BankModel)s).ToList());

            return View(_bankService.Set().Select(s => (BankModel)s).ToList());
        }
    }
}
