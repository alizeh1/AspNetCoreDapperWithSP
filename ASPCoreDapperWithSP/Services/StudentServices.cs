using ASPCoreDapperWithSP.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using Newtonsoft.Json;

namespace ASPCoreDapperWithSP.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IConfiguration _configuration;

       

        public StudentServices(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("studentdb");
            providerName = "System.Data.SqlClient";
        }
        public string connectionString { get; }
        public string providerName { get;}


        public IDbConnection connection
        {
            get { return new SqlConnection(connectionString); }

        }
        public string DeleteStudent(Student stdDelete)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var sdnt = dbConnection.Query<Student>("[DeleteStudent]", new { StudentId = stdDelete.StudentId }, commandType: CommandType.StoredProcedure);
                    if (sdnt != null && sdnt.FirstOrDefault().response == "Deleted Successfully")
                    {
                        result = "Deleted Successfully";
                    }
                    dbConnection.Close();
                    return result;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return message;
            }
        }

        public string GetById(int id)
        {
            Student obj = new Student();
            obj.StudentId = id;
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var list = dbConnection.Query<Student>("GetById", new { obj.StudentId }, commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();

                    // Convert the list to a JSON string
                    string jsonString = JsonConvert.SerializeObject(list);

                    return jsonString;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error
                Console.WriteLine($"Error: {ex.Message}");
                return ""; // or return null; depending on your error handling strategy
            }
        }

        public List<Student> GetStudentList()
        {
            List<Student> list = new List<Student>();
            try
            {
                using(IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    list=dbConnection.Query<Student>("GetStudentList", commandType:CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return list;

                }

            }catch(Exception ex)
            {
                string message=ex.Message;
                return list;
            }
        }

        public string InsertStudent(Student stdInsert)
        {
            string result="";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var sdnt = dbConnection.Query<Student>("InsertStudent",new { StudentName=stdInsert.StudentName, EmailAddress=stdInsert.EmailAddress, City=stdInsert.City, CreatedBy=stdInsert.CreatedBy }, commandType:CommandType.StoredProcedure);
                   
                    dbConnection.Close();
                    return result ;

                }
            }
            catch(Exception ex)
            {
                string message=ex.Message;
                return message;
            }
        }

        public string UpdateStudent(Student stdUpdate)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var sdnt = dbConnection.Query<Student>("UpdateStudent", new { StudentName = stdUpdate.StudentName, EmailAddress = stdUpdate.EmailAddress, City = stdUpdate.City, CreatedBy = stdUpdate.CreatedBy, StudentId=stdUpdate.StudentId }, commandType: CommandType.StoredProcedure);
                    dbConnection.Close();
                    return result;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return message;
            }
        }
    }
}
