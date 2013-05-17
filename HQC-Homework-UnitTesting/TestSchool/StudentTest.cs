using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolProject;

namespace SchoolUnitTests
{
    [TestClass]
    public class StudentTest
    {
        //using reflection to refresh the private static unique id identifier for the tests
        [TestInitialize]
        public void RefreshCounter()
        {
            FieldInfo refresh = typeof(Student).GetField("uniqueIDIncrementer", BindingFlags.NonPublic | BindingFlags.Static);
            refresh.SetValue(null, (uint)10000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StudenWithNullNameException()
        {
            new Student(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StudentWith_OnlyWhiteSpaceInName_Exception()
        {
            new Student("  ");
        }

        [TestMethod]
        public void ConstructorTestingWithName()
        {
            var student = new Student("Margarin Filipov");
            Assert.AreEqual("Margarin Filipov", student.FullName, "Full name is not equal.");
        }

        [TestMethod]
        public void ConstructorCheck_IDnumber_IsInValidRange()
        {
            var student = new Student("Hartienov");
            Assert.IsTrue((Student.LowerBoundUniqueID <= student.UniqueID) &&
                          (student.UniqueID <= Student.UpperBoundUniqueID),
                "Student Id is not in the correct range");
        }

        [TestMethod]
        public void CompareToString()
        {
            var student = new Student("Marmalad Palachinkov");

            Assert.AreEqual("Marmalad Palachinkov - " + student.UniqueID, student.ToString(),
                "ToString is not returning in correct format.");
        }

        [TestMethod]
        public void CheckIf_NextStudent_GetCorrectIDIncrement()
        {
            var student = new Student("Marmalad Palachinkov");
            var secondStudent = new Student("Siren Banicharov");

            Assert.IsTrue(student.UniqueID + 1 == secondStudent.UniqueID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CheckIfUpperBoundary_IfExceeded_ThrowException()
        {
            for (int i = 0; i < 100000; i++)
            {
                new Student("Test student");
            }
        }

        //bonus one :)
        [TestMethod]
        public void CheckBoundaries_IfAreCorrect()
        {

            Assert.IsTrue(Student.LowerBoundUniqueID == 10000, "LowerBoundarie should be 10000");
            Assert.IsTrue(Student.UpperBoundUniqueID == 99999, "UpperBoundarie should be 99999");
        }

    }
}