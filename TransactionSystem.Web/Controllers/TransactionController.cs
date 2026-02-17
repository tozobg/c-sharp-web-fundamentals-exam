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
    [HttpGet]
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
            ViewBag.CurrentBalance = selectedAccount.Balance;
        }

        return View(deposits);
    }

    [HttpGet]
    public async Task<IActionResult> CreateDeposit(int accountId)
    {
        AccountDto? account = await _accountService.GetByIdAsync(accountId);
        if (account == null)
        {
            return NotFound();
        }

        TransactionInputDto dto = new() { AccountId = account.Id, Money = 0 };

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
            await _transactionService.CreateDepositAsync(dto);

            return RedirectToAction(nameof(IndexDeposits), new { accountId });
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
            // On error, return with error message
            TempData["Error"] = "Could not delete deposit: " + ex.Message;
            return RedirectToAction(nameof(IndexDeposits), new { accountId });
        }
    }
    #endregion

    #region Wirthdraws

    [HttpGet]
    public async Task<IActionResult> IndexWithdrawals(int? accountId)
    {
        IEnumerable<AccountDto> accounts = await _accountService.GetAllAccountsAsync();
        ViewBag.Accounts = accounts;

        IEnumerable<TransactionDetailsDto> withdrawals = new List<TransactionDetailsDto>();

        if (accountId.HasValue && accountId.Value > 0)
        {
            withdrawals = await _transactionService.GetWithdrawalsByAccountIdAsync(accountId.Value);

            AccountDto selectedAccount = accounts.First(a => a.Id == accountId.Value);
            ViewBag.SelectedAccountId = accountId.Value;
            ViewBag.SelectedAccountName = selectedAccount.FullName;
            ViewBag.CurrentBalance = selectedAccount.Balance;
        }

        return View(withdrawals);
    }

    [HttpGet]
    public async Task<IActionResult> CreateWithdrawal(int accountId)
    {
        AccountDto? account = await _accountService.GetByIdAsync(accountId);
        if (account == null)
        {
            return NotFound();
        }

        var dto = new TransactionInputDto { AccountId = account.Id, Money = 0 };

        ViewBag.AccountName = account.FullName;
        ViewBag.AccountId = account.Id;

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWithdrawal(TransactionInputDto dto, int accountId)
    {
        try
        {
            await _transactionService.CreateWithdrawalAsync(dto);

            return RedirectToAction(nameof(IndexWithdrawals), new { accountId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message); // "Insufficient funds" will show here
            ViewBag.AccountId = accountId;

            return View(dto);
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteWithdrawal(int id, int accountId)
    {
        try
        {
            await _transactionService.DeleteWithdrawalAsync(id, accountId);

            // Redirect back to the index for THIS account
            return RedirectToAction(nameof(IndexWithdrawals), new { accountId });
        }
        catch (Exception ex)
        {
            // // On error, return with error message
            TempData["Error"] = "Could not delete deposit: " + ex.Message;
            return RedirectToAction(nameof(IndexWithdrawals), new { accountId });
        }
    }
    #endregion
}