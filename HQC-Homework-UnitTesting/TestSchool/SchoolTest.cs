using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolProject;

namespace SchoolUnitTests
{
    [TestClass]
    public class SchoolTest
    {
        //using reflection to refresh the private static unique id identifier for the tests
        [TestInitialize]
        public void RefreshCounter()
        {
            FieldInfo refresh = typeof(Student).GetField("uniqueIDIncrementer",
                BindingFlags.NonPublic | BindingFlags.Static);
            refresh.SetValue(null, (uint)10000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullName_ThrowException()
        {
            new School(null);
        }

        [TestMethod]
        public void ConstructorSchoolName_IsValid()
        {
            Assert.AreEqual("Hint", new School("Hint").SchoolName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhiteSpaceName_ThrowException()
        {
            new School("                ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingANullCourse_ThrowException()
        {
            var school = new School("Al");

            school.AddCourse(new Course(" "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingTheSameCourseMoreThanOnce_ThrowException()
        {
            var school = new School("Filipovo");
            var course = new Course("Alg");
            school.AddCourse(course);
            school.AddCourse(course);
        }

        [TestMethod]
        public void CheckIfACourseIsAddedCorrectly_IfTrue()
        {
            var school = new School("Banichka");
            var course = new Course("Alg");

            school.AddCourse(course);
            var splited = Regex.Split(school.ToString(), @"\W+").ToList();

            Assert.IsTrue(splited[0].Equals("Banichka") && splited[1].Equals("Alg"));
        }

        [TestMethod]
        public void CheckIfCourseIsRemoved_IfTrue()
        {
            var school = new School("A");
            var course = new Course("Breakfast");

            school.AddCourse(course);
            school.RemoveCourse(course);

            Assert.IsTrue((school.ToString().IndexOf(course.ToString()) <= 0));
        }
    }
}