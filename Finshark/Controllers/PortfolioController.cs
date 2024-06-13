using Finshark.Extensions;
using Finshark.Interfaces;
using Finshark.Migrations;
using Finshark.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Finshark.Controllers;
[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{


    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockrepo;
    private readonly IPortfolioRepository _portfoliorepo;
    public PortfolioController(UserManager<AppUser> UserManager,
    IStockRepository StockRepo, IPortfolioRepository PortfolioRepo)
    {
        _userManager = UserManager;
        _stockrepo = StockRepo;
        _portfoliorepo = PortfolioRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        var AppUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfoliorepo.GetUserPortfolio(AppUser);
        return Ok(userPortfolio);

    }
        
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateUserPortfolio(string Symbol)
    {
        var user = User.GetUsername();
        var appuser = await _userManager.FindByNameAsync(user);
        var stock = await _stockrepo.GetBySymbol(Symbol);
        if(stock == null) return BadRequest("Stock Does not Exist");

        var userPortfolio = await _portfoliorepo.GetUserPortfolio(appuser);
        if (userPortfolio.Any(e => e.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("Stock is already in Portfolio");

        var portfolio = await _portfoliorepo.CreateUserPortfolio(appuser, stock);
        if(portfolio == null) return StatusCode(500, "Could not Create");
        else return Created();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string Symbol)
    {
        var username = User.GetUsername();
        var _appuser = await _userManager.FindByNameAsync(username);
        
        var userPortfolio = await _portfoliorepo.GetUserPortfolio(_appuser);

        var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == Symbol.ToLower());

        if (filteredStock.Any())
        {
            await _portfoliorepo.DeletePortfolio(_appuser, Symbol);
        }
        else
        {
            return BadRequest("Stock not in your Portfolio");
        }
        return Ok();
    }
}
