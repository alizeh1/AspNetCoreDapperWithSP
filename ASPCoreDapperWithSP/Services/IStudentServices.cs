using ASPCoreDapperWithSP.Models;

namespace ASPCoreDapperWithSP.Services
{
    public interface IStudentServices
    {
        public List<Student> GetStudentList();
        public string InsertStudent(Student stdInsert);

        public string UpdateStudent(Student stdUpdate);
        public string DeleteStudent(Student stdDelete);
        public string GetById(int id);

    }
}
