using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolProject
{
    public class School
    {
        private String schoolName;
        private readonly IList<Course> courses;

        public School(string schoolName)
        {
            this.SchoolName = schoolName;
            this.courses = new List<Course>();
        }

        public String SchoolName
        {
            get
            {
                return this.schoolName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("School name must not be empty or null.");
                }

                this.schoolName = value;
            }
        }

        public void AddCourse(Course currentCourse)
        {

            if (IsAlreadyInList(currentCourse))
            {
                throw new ArgumentException(String.Format(
                    "The course with name {0}, is already in the school course list.",
                    currentCourse.CourseName));
            }

            this.courses.Add(currentCourse);
        }

        private bool IsAlreadyInList(Course currentCourse)
        {
            foreach (var item in courses)
            {
                if (currentCourse == item)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveCourse(Course currentCourse)
        {
            this.courses.Remove(currentCourse);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(SchoolName + Environment.NewLine);

            foreach (var item in courses)
            {
                result.Append(item.CourseName + Environment.NewLine);
                result.Append(item);
            }

            return result.ToString();
        }
    }
}