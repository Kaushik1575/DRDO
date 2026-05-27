using System.Security.Claims;
using ApprenticeshipManagement.Data;
using ApprenticeshipManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApprenticeshipManagement.Controllers;

[Authorize(Roles = "Apprentice")]
public class ApprenticeController : Controller
{
    private readonly AppDbContext _db;

    public ApprenticeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var id))
            return RedirectToAction("ApprenticeLogin", "Account");

        var apprentice = await _db.Apprentices.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (apprentice == null)
            return RedirectToAction("ApprenticeLogin", "Account");

        var daysInProgram = Math.Max(0, (DateTime.UtcNow.Date - apprentice.CreatedAt.Date).Days);

        var model = new ApprenticeDashboardViewModel
        {
            FullName = apprentice.FullName,
            ApprenticeId = apprentice.ApprenticeId,
            TradeField = apprentice.Department,
            Email = apprentice.Email,
            MobileNumber = apprentice.MobileNumber,
            IsActive = apprentice.IsActive,
            RegisteredOn = apprentice.CreatedAt,
            DaysInProgram = daysInProgram
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var id))
            return RedirectToAction("ApprenticeLogin", "Account");

        var apprentice = await _db.Apprentices.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (apprentice == null)
            return RedirectToAction("ApprenticeLogin", "Account");

        var model = new EditApprenticeProfileViewModel
        {
            FullName = apprentice.FullName,
            TradeField = apprentice.Department,
            MobileNumber = apprentice.MobileNumber,
            Email = apprentice.Email,
            ApprenticeId = apprentice.ApprenticeId
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(EditApprenticeProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var id))
            return RedirectToAction("ApprenticeLogin", "Account");

        var apprentice = await _db.Apprentices.FirstOrDefaultAsync(a => a.Id == id);
        if (apprentice == null)
            return RedirectToAction("ApprenticeLogin", "Account");

        apprentice.FullName = model.FullName.Trim();
        apprentice.Department = model.TradeField.Trim();
        apprentice.MobileNumber = model.MobileNumber.Trim();

        await _db.SaveChangesAsync();
        TempData["Success"] = "Profile updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
