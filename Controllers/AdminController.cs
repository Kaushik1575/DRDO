using ApprenticeshipManagement.Data;
using ApprenticeshipManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApprenticeshipManagement.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search)
    {
        var all = await _db.Apprentices.AsNoTracking().ToListAsync();

        var filtered = all.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            filtered = all.Where(a =>
                a.FullName.ToLowerInvariant().Contains(term) ||
                a.ApprenticeId.ToLowerInvariant().Contains(term) ||
                a.Department.ToLowerInvariant().Contains(term) ||
                a.Email.ToLowerInvariant().Contains(term) ||
                a.MobileNumber.Contains(term));
        }

        var list = filtered
            .OrderBy(a => a.ApprenticeId)
            .Select(a => new ApprenticeRowViewModel
            {
                Id = a.Id,
                ApprenticeId = a.ApprenticeId,
                FullName = a.FullName,
                TradeField = a.Department,
                Email = a.Email,
                Phone = a.MobileNumber,
                IsActive = a.IsActive
            })
            .ToList();

        var model = new AdminDashboardViewModel
        {
            AdminName = User.Identity?.Name ?? "Administrator",
            TotalApprentices = all.Count,
            ActiveApprentices = all.Count(a => a.IsActive),
            InactiveApprentices = all.Count(a => !a.IsActive),
            SearchQuery = search,
            Apprentices = list
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ViewApprentice(int id)
    {
        var apprentice = await _db.Apprentices.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (apprentice == null)
            return NotFound();

        ViewBag.Apprentice = apprentice;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditApprentice(int id)
    {
        var apprentice = await _db.Apprentices.FindAsync(id);
        if (apprentice == null)
            return NotFound();

        return View(new EditApprenticeViewModel
        {
            Id = apprentice.Id,
            ApprenticeId = apprentice.ApprenticeId,
            FullName = apprentice.FullName,
            TradeField = apprentice.Department,
            Email = apprentice.Email,
            MobileNumber = apprentice.MobileNumber,
            IsActive = apprentice.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditApprentice(EditApprenticeViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var apprentice = await _db.Apprentices.FindAsync(model.Id);
        if (apprentice == null)
            return NotFound();

        var email = model.Email.Trim().ToLowerInvariant();
        var apprenticeId = model.ApprenticeId.Trim();

        if (await _db.Apprentices.AnyAsync(a => a.Email == email && a.Id != model.Id))
        {
            ModelState.AddModelError(nameof(model.Email), "This email is already used.");
            return View(model);
        }

        if (await _db.Apprentices.AnyAsync(a => a.ApprenticeId == apprenticeId && a.Id != model.Id))
        {
            ModelState.AddModelError(nameof(model.ApprenticeId), "This apprentice ID is already used.");
            return View(model);
        }

        apprentice.ApprenticeId = apprenticeId;
        apprentice.FullName = model.FullName.Trim();
        apprentice.Department = model.TradeField.Trim();
        apprentice.Email = email;
        apprentice.MobileNumber = model.MobileNumber.Trim();
        apprentice.IsActive = model.IsActive;

        await _db.SaveChangesAsync();
        TempData["Success"] = "Apprentice updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var apprentice = await _db.Apprentices.FindAsync(id);
        if (apprentice == null)
            return NotFound();

        apprentice.IsActive = !apprentice.IsActive;
        await _db.SaveChangesAsync();

        TempData["Success"] = apprentice.IsActive
            ? $"{apprentice.FullName} activated."
            : $"{apprentice.FullName} deactivated.";

        return RedirectToAction(nameof(Index));
    }
}
