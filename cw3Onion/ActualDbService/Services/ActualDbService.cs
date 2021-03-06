﻿using ActualDbService.Context;
using Onion.Domain.Entities;
using Onion.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion.ActualDbService.Services
{
    public class ActualDbService : IStudentDbService
    {
        private ActualDbContext _context = new ActualDbContext();
        public ActualDbService()
        {

        }
        public bool EnrollStudent(Student newStudent, int semestr)
        {
            _context.Student.Add(newStudent);
            return true;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Student.ToList();
        }
    }
}
