using Microsoft.AspNetCore.Mvc;
using CoreCrud.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoreCrud.Models
{
    public class StudentController: Controller
    {
        private readonly StudentContext ctx;

        public  StudentController(StudentContext ctx)
        {
            this.ctx = ctx;
        }   

        public IActionResult ShowAllStudents()
        {
            var students = ctx.Students.ToList();
            return View(students);
        }

        public IActionResult AddStudent()
        {
            return View();
        }
       

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                ctx.Students.Add(model);
                await ctx.SaveChangesAsync();
                ModelState.AddModelError("msg", "Data has saved successfully");
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("msg", "Data could not be saved");
            }
            return View();
        }

        public IActionResult EditStudent(int id)
        {
            var student = ctx.Students.Find(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }
            try
            {
                ctx.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await ctx.SaveChangesAsync();
                return RedirectToAction("ShowAllStudents");
            }
            catch(Exception ex) {
                ModelState.AddModelError("msg", "Error on updation");
                return View();
            }
            
        }

        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var student = ctx.Students.Find(id);
                if (student != null)
                {
                    ctx.Students.Remove(student);   
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("ShowAllStudents");
        }

    }
}
