using Server.Models;
using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories
{
	class StudentRepository
	{
		private static SchoolContext _context = new SchoolContext();

		public static List<Student> GetAll => _context.Students.ToList();
		public static Student FindByName(string name) =>
			_context.Students.SingleOrDefault(x => x.Name == name);
	}
}
