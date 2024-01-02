using Microsoft.AspNetCore.Mvc;
using StudentTracking.VM.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTracking.UI.ViewComponents
{
    public class CardViewComponent : ViewComponent
    {
        public CardViewComponent()
        {

        }

        public IViewComponentResult Invoke(StudentSelectVM studentSelectVM)
        {

            return View(studentSelectVM);
        }
    }
}
