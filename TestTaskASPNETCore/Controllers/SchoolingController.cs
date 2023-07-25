using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TestTaskASPNETCore.Database;
using TestTaskASPNETCore.Models;

public class SchoolingController : Controller
{
    private MyDbContext _myDbContext;
    public SchoolingController(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(_myDbContext.SchoolingActivities.Include(s => s.UserInfo));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var model = await _myDbContext.SchoolingActivities
            .Include(s => s.UserInfo)
            .FirstOrDefaultAsync(s => s.Id == id);

       // await _myDbContext.UserInfos.FirstOrDefaultAsync(s => s.Id == model.UserInfoId); // КОСТЫЛЬ

        if (model == null)
            return RedirectToAction("Index");

        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(SchoolingActivityModel schoolingData)
    {
        schoolingData.AchievedTime = DateTime.UtcNow;

        // Устанавливаем обратную ссылку на объект UserInfo
        // schoolingData.UserInfo.SchoolingActivityModell = schoolingData;

        // Предполагая, что у вас есть DbSet в контексте базы данных для SchoolingActivityModel и UserInfo
        await _myDbContext.AddAsync(schoolingData.CopyTo());
        await _myDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost] //Для того что бы использовать HttpDelete надо было писать джава скрипт код, по дефолту не поддерживается
    public async Task<IActionResult> Delete(string id)
    {
        var activity = await _myDbContext.SchoolingActivities
            .Include(s => s.UserInfo)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (activity == null)
            return NotFound();

        _myDbContext.SchoolingActivities.Remove(activity);
        _myDbContext.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SchoolingActivityModel editedData)
    {
        if (editedData == null)
        {
            // Если запись не найдена, вернуть сообщение об ошибке или перенаправить на другую страницу
            return NotFound();
        }

        var findData = await _myDbContext.SchoolingActivities
            .Include(s => s.UserInfo)
            .FirstAsync(s => s.Id.Equals(editedData.Id));

        if (findData == null)
            return NotFound();

        if(findData == editedData)
            return RedirectToAction("Index");

        findData.Edit(editedData);

        _myDbContext.SchoolingActivities.Update(findData);
        await _myDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}