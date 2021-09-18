namespace Isu.Models
{
    public class Student
    {
        private int _id;
        private string _name;

        public Student(string name, int id)
        {
            StudentName = name;
            StudentId = id;
        }

        public string StudentName
        {
            get => _name;

            private set => _name = value;
        }

        public int StudentId
        {
            get => _id;

            private set => _id = value;
        }
    }
}