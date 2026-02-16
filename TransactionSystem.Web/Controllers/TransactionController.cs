using Microsoft.AspNetCore.Mvc;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.DTOs.Output;
using TransactionSystem.Core.Services;

public class TransactionController : Controller
{
    private readonly TransactionService _transactionService;
    private readonly AccountService _accountService;

    public TransactionController(TransactionService transactionService, AccountService accountService)
    {
        _transactionService = transactionService;
        _accountService = accountService;
    }

    #region Deposits
    // GET: Transaction/IndexDeposits
    public async Task<IActionResult> IndexDeposits(int? accountId)
    {
        // Always load all accounts for the dropdown
        IEnumerable<AccountDto> accounts = await _accountService.GetAllAccountsAsync();
        ViewBag.Accounts = accounts;

        // Empty list of deposits
        IEnumerable<TransactionDetailsDto> deposits = new List<TransactionDetailsDto>();

        // If the account selected
        if (accountId.HasValue && accountId.Value > 0)
        {
            deposits = await _transactionService.GetDepositsByAccountIdAsync(accountId.Value);

            AccountDto selectedAccount = accounts.First(a => a.Id == accountId.Value);
            ViewBag.SelectedAccountId = accountId.Value;
            ViewBag.SelectedAccountName = selectedAccount.FullName;
            ViewBag.CurrentBalance = selectedAccount?.Balance;
        }

        return View(deposits);
    }

    // GET: Transaction/CreateDeposit?accountId=1
    public async Task<IActionResult> CreateDeposit(int accountId)
    {
        var account = await _accountService.GetByIdAsync(accountId);
        if (account == null) return NotFound();

        // Pre-fill the DTO with the AccountId so the user doesn't have to
        var dto = new TransactionInputDto { AccountId = account.Id, Money = 0 };

        ViewBag.AccountName = account.FullName;
        ViewBag.AccountId = accountId;

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDeposit(TransactionInputDto dto, int accountId)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            // We pass the accountId separately to ensure we update the right record
            await _transactionService.CreateDepositAsync(dto);
            return RedirectToAction(nameof(IndexDeposits), new { accountId = accountId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error saving deposit: " + ex.Message);
            ViewBag.AccountId = accountId;
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteDeposit(int id, int accountId)
    {
        try
        {
            await _transactionService.DeleteDepositAsync(id, accountId);

            // Redirect back to the index for THIS account
            return RedirectToAction(nameof(IndexDeposits), new { accountId });
        }
        catch (Exception ex)
        {
            // If something goes wrong, we go back with an error message
            TempData["Error"] = "Could not delete deposit: " + ex.Message;
            return RedirectToAction(nameof(IndexDeposits), new { accountId });
        }
    }
    #endregion

    //public async Task<IActionResult> Index(int? accountId)
    //{
    //    // 1. Get all accounts for the dropdown
    //    var accounts = await _accountService.GetAllAccountsAsync();
    //    ViewBag.Accounts = accounts; // This feeds the dropdown

    //    // 2. If an account is selected, get their deposits
    //    IEnumerable<TransactionDetailsDto> deposits = new List<TransactionDetailsDto>();
    //    if (accountId.HasValue)
    //    {
    //        deposits = await _transactionService.GetDepositsByAccountIdAsync(accountId.Value);
    //        ViewBag.SelectedAccountId = accountId.Value;
    //    }

    //    return View(deposits);
    //}

    //#region Deposits

    //[HttpGet]
    //public IActionResult Deposit(int? accountNumber)
    //{
    //    // If the user clicked "Deposit" from the Index page, 
    //    // we pre-fill the DTO with the AccountNumber from the URL.
    //    var model = new TransactionInputDto { AccountNumber = accountNumber ?? 0 };
    //    return View(model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Deposit(TransactionInputDto dto)
    //{
    //    if (!ModelState.IsValid) return View(dto);

    //    try
    //    {
    //        await _transactionService.DepositAsync(dto);
    //        return RedirectToAction("Index", "Account");
    //    }
    //    catch (Exception ex)
    //    {
    //        ModelState.AddModelError("", ex.Message);
    //        return View(dto);
    //    }
    //}
    //#endregion

    //#region Wirthdraws

    //[HttpGet]
    //public IActionResult Withdraw(int? accountNumber)
    //{
    //    var model = new TransactionInputDto { AccountNumber = accountNumber ?? 0 };
    //    return View(model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Withdraw(TransactionInputDto dto)
    //{
    //    if (!ModelState.IsValid) return View(dto);

    //    try
    //    {
    //        await _transactionService.WithdrawAsync(dto);
    //        return RedirectToAction("Index", "Account");
    //    }
    //    catch (Exception ex)
    //    {
    //        ModelState.AddModelError("", ex.Message);
    //        return View(dto);
    //    }
    //}
    //#endregion
}


//using Microsoft.AspNetCore.Mvc;
//using TransactionSystem.Core.DTOs.Input;
//using TransactionSystem.Core.Services;

//namespace TransactionSystem.Web.Controllers
//{
//    public class TransactionController : Controller
//    {
//        private readonly TransactionService _transactionService;

//        public TransactionController(TransactionService transactionService)
//        {
//            _transactionService = transactionService;
//        }

//        // GET: /Transaction/Deposit
//        public IActionResult Deposit() => View();

//        [HttpPost]
//        public async Task<IActionResult> Deposit(TransactionInputDto dto)
//        {
//            await _transactionService.DepositAsync(dto);
//            return RedirectToAction("Index", "Account"); // Go back to account list to see new balance
//        }

//        // GET: /Transaction/Transfer
//        public IActionResult Transfer() => View();

//        [HttpPost]
//        public async Task<IActionResult> Transfer(TransferInputDto dto)
//        {
//            await _transactionService.TransferAsync(dto);
//            return RedirectToAction("Index", "Account");
//        }

//        // You can follow this same pattern for Withdraw!
//    }
//}
