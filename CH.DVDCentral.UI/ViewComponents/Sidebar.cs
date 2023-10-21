using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            //We are loading the programs and filtering the declarations
            return View(GenreManager.Load().OrderBy(p => p.Description));
        }

        
    }
}
