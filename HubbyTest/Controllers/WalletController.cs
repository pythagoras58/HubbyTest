using HubbyTest.Model;
using Microsoft.AspNetCore.Mvc;

namespace HubbyTest.Controllers;


[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private List<Wallet> _wallets = new List<Wallet>();

    public WalletController()
    {
        _wallets.Add(new Wallet { ID = 1, Name = "My Wallet", Type = "card", AccountNumber = "1234567890123456", AccountScheme = "visa", CreatedAt = DateTime.Now, Owner = "+233555123456" });
        _wallets.Add(new Wallet { ID = 2, Name = "My Mobile Money", Type = "momo", AccountNumber = "0551234567", AccountScheme = "mtn", CreatedAt = DateTime.Now, Owner = "+233555123456" });
    }
    
    // GET /api/wallets
    [HttpGet]
    public async Task<IEnumerable<Wallet>> GetWallets()
    {
        return await Task.FromResult(_wallets);
    }
    
    
    // GET wallet by ID
    [HttpGet("{id:int}")]
    public async Task<Wallet> GetWalletById(int id)
    {
        return await Task.FromResult(_wallets.FirstOrDefault(x => x.ID == id));
    }
    
    // create wallet
    [HttpPost]
    public async Task<Wallet> CreateWallet(Wallet wallet)
    {
        if (_wallets.Any(w => w.AccountNumber.Substring(0, 6) == wallet.AccountNumber.Substring(0, 6)))
        {
            throw new Exception("Wallet Duplication Not Allowed");
        }

        if (_wallets.Count(w => w.Owner == wallet.Owner) >= 5)
        {
            throw new Exception("A user can not have more than 5 wallets");
        }

        wallet.ID = _wallets.Count() + 1;
        wallet.CreatedAt = DateTime.Now;
        _wallets.Add(wallet);

        return await Task.FromResult(wallet);
    }
    
    
    // delete wallet
    [HttpDelete("{id:int}")]
    public async Task DelteWallet(int id)
    {
        var wallet = _wallets.FirstOrDefault(x => x.ID == id);

        if (wallet != null)
        {
            _wallets.Remove(wallet);
        }
        else
        {
            throw new Exception($"Wallet with ID {id} is not found.");
        }

        await Task.CompletedTask;
    }
}