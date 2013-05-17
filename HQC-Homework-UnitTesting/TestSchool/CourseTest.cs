using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolProject;

namespace SchoolUnitTests
{
    [TestClass]
    public class CourseTest
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
            new Course(null);
        }

        [TestMethod]
        public void ConstructorCourseName_IsValid()
        {
            Assert.AreEqual("Hint", new Course("Hint").CourseName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhiteSpaceName_ThrowException()
        {
            new Course("                ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingInCourse_NullStudent_ThrowException()
        {
            var spirt = new Course("Spirt");
            spirt.AddStudent(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingTheSameStudentMoreThanOnce_ThrowException()
        {
            var spirt = new Course("Spirt");
            var student = new Student("Peter");
            spirt.AddStudent(student);
            spirt.AddStudent(student);
        }

        [TestMethod]
        public void AddingStudent_CheckingIfHeExistInCourse()
        {
            var course = new Course("Course");
            var student = new Student("Petkan");
            course.AddStudent(student);

            var index = course.ToString().IndexOf(student.ToString());
            Assert.IsTrue(index >= 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveStudenByIncorrectID_ThrowException()
        {
            var spirt = new Course("Spirt");
            spirt.AddStudent(new Student("sad"));
            spirt.RemoveStudentByID(99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveStudenFromEmptyList_ThrowException()
        {
            var spirt = new Course("Spirt");
            spirt.RemoveStudentByID(99);
        }

        [TestMethod]
        public void RemovingCorrectlyStudent_TrueIfRemovedCorrectly()
        {
            var spirt = new Course("Spirt");
            var student = new Student("sad");

            spirt.AddStudent(student);
            spirt.RemoveStudentByID(student.UniqueID);

            var index = spirt.ToString().IndexOf(student.ToString());
            Assert.IsTrue(index < 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemovingStudentThatDoesNotExistInTheList()
        {
            var bakery = new Course("Bakery");
            var student = new Student("Fhilip");

            bakery.AddStudent(student);
            bakery.RemoveStudentByID(20000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExceedTheUpperBoundOfCourse_ThrowException()
        {
            var bakery = new Course("Bakery");

            for (int i = 0; i < 31; i++)
            {
                bakery.AddStudent(new Student("Asparuh"));
            }
        }
    }
}