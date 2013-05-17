using System;
using System.Linq;

namespace SchoolProject
{
    public class Student
    {
        public const uint LowerBoundUniqueID = 10000;
        public const uint UpperBoundUniqueID = 99999;

        private String fullName;
        private uint uniqueID;
        private static uint uniqueIDIncrementer = 10000;

        public Student(string fullName)
        {
            this.FullName = fullName;
            this.UniqueID = uniqueIDIncrementer++;
        }

        public String FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be null or empty.");
                }

                this.fullName = value;
            }
        }

        public uint UniqueID
        {
            get
            {
                return this.uniqueID;
            }
            private set
            {
                if (value < LowerBoundUniqueID || UpperBoundUniqueID < value)
                {
                    throw new ArgumentOutOfRangeException(String.Format("ID number should be in range of [{0}..{1}].",
                        LowerBoundUniqueID, UpperBoundUniqueID));
                }

                this.uniqueID = value;
            }
        }

        public override string ToString()
        {
            return FullName + " - " + UniqueID;
        }
    }
}