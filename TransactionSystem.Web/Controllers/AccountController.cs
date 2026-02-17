using Microsoft.AspNetCore.Mvc;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.DTOs.Output;
using TransactionSystem.Core.Services;

namespace TransactionSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();

            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _accountService.CreateAccountAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.Message == "Full name must be between 3 and 255 characters.") ModelState.AddModelError("FullName", ex.Message);
                if (ex.Message == "Balance must be greater than 0.") ModelState.AddModelError("Balance", ex.Message);
                if (ex.Message == "Account number already exists.") ModelState.AddModelError("AccountNumber", ex.Message);

                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AccountDto? account = await _accountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            UpdateAccountDto updateDto = new()
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                FullName = account.FullName
            };

            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _accountService.UpdateAccountAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.Message == "The new Account Number is already in use by another account.")
                {
                    ModelState.AddModelError("AccountNumber", ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
                }

                return View(dto);
            }
        }
    }
}
