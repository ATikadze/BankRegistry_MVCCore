using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankRegistry_MVCCore.Controllers
{
    using Domain.ServiceInterfaces;
    using Domain;
    using Models;
    public class BankController : Controller
    {
        private IBankService _bankService;
        private IContactPersonService _contactPersonService;
        private IPositionService _positionService;

        public BankController(IBankService bankService, IContactPersonService contactPersonService, IPositionService positionService)
        {
            _bankService = bankService;
            _contactPersonService = contactPersonService;
            _positionService = positionService;
        }

        [HttpGet]
        public IActionResult New(int? bankId)
        {
            if (bankId != null)
            {
                BankSaveModel bankSaveModel = new BankSaveModel();

                Bank bank = _bankService.Set().Single(s => s.ID == bankId);
                bankSaveModel.BankModel = new BankModel()
                {
                    ID = bank.ID,
                    Name = bank.Name,
                    URL = bank.URL
                };

                ContactPerson contactPerson = _contactPersonService.Set().Single(s => s.BankID == bankId && s.PositionID == _positionService.Set().Single(ss => ss.Name.Equals("General Director")).ID);
                bankSaveModel.ContactPersonModel = new ContactPersonModel()
                {
                    FirstName = contactPerson.FirstName,
                    LastName = contactPerson.LastName,
                    DateOfBirth = contactPerson.DateOfBirth
                };

                return View(bankSaveModel);
            }
            return View(new BankSaveModel());
        }

        [HttpPost]
        public IActionResult New(BankSaveModel bankSaveModel, bool edit = false)
        {
            if (edit)
            {
                Bank newBank = _bankService.Set().Single(s => s.ID == bankSaveModel.BankModel.ID);
                newBank.Name = bankSaveModel.BankModel.Name;
                newBank.URL = bankSaveModel.BankModel.URL;
                _bankService.Commit();

                ContactPerson newContactPerson = _contactPersonService.Set().Single(s => s.BankID == bankSaveModel.BankModel.ID && s.PositionID == _positionService.Set().Single(ss => ss.Name.Equals("General Director")).ID);
                newContactPerson.FirstName = bankSaveModel.ContactPersonModel.FirstName;
                newContactPerson.LastName = bankSaveModel.ContactPersonModel.LastName;
                newContactPerson.DateOfBirth = bankSaveModel.ContactPersonModel.DateOfBirth;
                _contactPersonService.Commit();

                return RedirectToAction("Index", "Home");
            }

            Bank bank = new Bank()
            {
                ID = bankSaveModel.BankModel.ID,
                Name = bankSaveModel.BankModel.Name.Trim(),
                URL = bankSaveModel.BankModel.URL?.Trim()
            };
            _bankService.Save(bank);
            _bankService.Commit();

            ContactPerson contactPerson = new ContactPerson()
            {
                FirstName = bankSaveModel.ContactPersonModel.FirstName.Trim(),
                LastName = bankSaveModel.ContactPersonModel.LastName.Trim(),
                DateOfBirth = bankSaveModel.ContactPersonModel.DateOfBirth,
                BankID = _bankService.Set().Last().ID,
                PositionID = _positionService.Set().Single(s => s.Name.Equals("General Director")).ID
            };
            _contactPersonService.Save(contactPerson);
            _contactPersonService.Commit();

            return RedirectToAction("NewContactPerson", new { bankId = bank.ID });
        }

        [HttpGet]
        public IActionResult NewContactPerson(int bankId, int? contactPersonID)
        {
            if (contactPersonID != null)
            {
                if (_contactPersonService.Set().SingleOrDefault(s => s.BankID == bankId && s.ID == contactPersonID) == null)
                    return NotFound("Contact Person not found!");
                _contactPersonService.Remove(_contactPersonService.Set().Single(s => s.ID == contactPersonID));
                _contactPersonService.Commit();
                return RedirectToAction("NewContactPerson", new { bankId });
            }
            BankSaveModel bankSaveModel = new BankSaveModel();
            bankSaveModel.ContactPersonModels = _contactPersonService.Set().Where(w => w.BankID == bankId && w.PositionID != _positionService.Set().Single(s => s.Name.Equals("General Director")).ID).Select(s => (ContactPersonModel)s).ToList();
            bankSaveModel.BankModel = (BankModel)_bankService.Set().Single(s => s.ID == bankId);
            bankSaveModel.Positions = _positionService.Set().Select(s => (PositionModel)s).ToList();

            return View(bankSaveModel);
        }

        [HttpPost]
        public IActionResult NewContactPerson(BankSaveModel bankSaveModel)
        {
            _contactPersonService.Save(new ContactPerson()
            {
                BankID = bankSaveModel.BankModel.ID,
                PositionID = bankSaveModel.ContactPersonModel.PositionID,
                FirstName = bankSaveModel.ContactPersonModel.FirstName.Trim(),
                LastName = bankSaveModel.ContactPersonModel.LastName.Trim(),
                DateOfBirth = bankSaveModel.ContactPersonModel.DateOfBirth,
            });
            _contactPersonService.Commit();

            return RedirectToAction("NewContactPerson", new { bankId = bankSaveModel.BankModel.ID });
        }
    }
}