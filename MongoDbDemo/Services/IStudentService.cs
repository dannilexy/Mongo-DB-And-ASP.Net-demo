using MongoDB.Driver;
using MongoDbDemo.Models;

namespace MongoDbDemo.Services
{
    public interface IStudentService
    {
        List<Student> Get();
        Student GetById(string id);
        Student Create(Student student);
        void Update(string id, Student student);
        void Delete(string id);

    }

    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IConfiguration _config;
        public StudentService(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient , IConfiguration _config)
        {
            this._config = _config;
           var database = mongoClient.GetDatabase(_config.GetValue<string>("StudentDatabaseSettings:DatabaseName"));
            _students =  database.GetCollection<Student>(_config.GetValue<string>("StudentDatabaseSettings:StudentCoursesCollectionName"));
        }
        public Student Create(Student student)
        {
           _students.InsertOne(student);
            return student;
        }

        public void Delete(string id)
        {
            _students.DeleteOne(str => str.Id == id);
        }

        public List<Student> Get()
        {
            return  _students.Find(st => true).ToList();
        }

        public Student GetById(string id)
        {
            return  _students.Find(st =>st.Id== id).FirstOrDefault();
        }

        public void Update(string id, Student student)
        {
            _students.ReplaceOne(str => str.Id == id, student);
        }
    }
}
