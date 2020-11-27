using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
         //Accoppiamento debole
         //Adesso il controllorer dipende dall'intefaccia
         //il quale non ha alcun dato implementativo....
         //L'importante è che abbia i metodi per esporre i dati
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;

        }
        public async Task<IActionResult> Index(string search, int page, string orderby, bool ascending)
        {
            //return Content("Sono Index");  
            //         var courseService = new CourseService(); //istanzio classe servizio applicativo
            List<CourseViewModel> courses = await courseService.GetCoursesAsync(search, page); //Richiamo il metodo da servizio applicativo di elenco corsi
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            //         return Content($"Sono Detail, ho ricevuto l'id {id}");  
            //       var courseService = new CourseService(); //istanzio classe servizio applicativo
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            return View(viewModel);

        }

    }
}