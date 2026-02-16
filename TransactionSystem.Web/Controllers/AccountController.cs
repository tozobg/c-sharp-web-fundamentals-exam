using Microsoft.AspNetCore.Mvc;
using TransactionSystem.Core.DTOs.Input;
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

        // GET: /Account/Index
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();

            return View(accounts);
        }

        // GET: /Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _accountService.CreateAccountAsync(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
