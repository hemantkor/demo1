using StudentWebApi.DBConnection;
using StudentWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentWebApi.Controllers
{
    public class StudentController : ApiController
    {
        StudentDao sdb = new StudentDao();
        // GET: api/Student
        public IEnumerable<Student> Get()
        {
            
            return sdb.GetAllStudents();
        }

        // GET: api/Student/5
        public Student Get(int id)
        {
            return sdb.GetStudentById(id);
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody]Student std)
        {
            if (sdb.Insert(std))
            {
                return Ok(std);
            }
            return NotFound();
        }

        // PUT: api/Student/5
        public IHttpActionResult Put([FromBody]Student std)
        {
            if (sdb.UpdateData(std))
            {
                return Ok(std);
            }
            return NotFound();

        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            if (sdb.DeleteStudent(id))
            {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
