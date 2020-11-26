using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    //L'interfaccia a differenza della classe non contiene logica
    //Rappresenta l'elenco dei metodi e parametri
    //E' come se fosse un contratto, vincola le classi che la utilizzano
    //Devono essere public
    //Scrivo prima l'intefaccia e poi la sviluppo
        public interface ICourseService
    {
         Task<List<CourseViewModel>> GetCoursesAsync();
         Task<CourseDetailViewModel> GetCourseAsync(int id);

         
    }
}