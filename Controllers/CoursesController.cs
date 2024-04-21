using Lab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {


        ITIContext db;

        public CoursesController(ITIContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public ActionResult<Course> get()
        {
           List<Course> s= db.Courses.ToList();
            if (s == null) return NotFound();
            else return Ok(s);

        }

        [HttpGet("{id}")]
        public ActionResult<Course> getbyid(int id)
        {
            Course s = db.Courses.Find(id);
            if (s == null) return NotFound();
            else return Ok(s);

        }

        [HttpGet("byname/{name}")]
        public ActionResult<Course> getbyname(string n)
        {
            Course s = db.Courses.FirstOrDefault(s => s.Crs_name == n);
            if (s == null) return NotFound();
            else return Ok(s);

        }

        [HttpPost]

        public ActionResult<Course> post(Course c)
        {
            if (c == null || !ModelState.IsValid) return BadRequest();
            else
            {
                db.Courses.Add(c);
                db.SaveChanges();

                return CreatedAtAction("getbyid", new { id = c.id }, c);
            }

        }

        [HttpDelete]
        public ActionResult<Course> deleteCourse(int id)
        {
            Course c = db.Courses.Find(id);
            if (c == null) return BadRequest();
            else
            {
                db.Courses.Remove(c);
                db.SaveChanges();
                return Ok(c);

            }
        }

        [HttpPut]
        public ActionResult put(int id, Course c)
        {
            if (c == null || c.id != id)
                return BadRequest();
            else
            {
                db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return NoContent();
            }
        }




    }
}
