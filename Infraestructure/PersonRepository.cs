using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using QueryApi.Domain;
using System.Threading.Tasks;

namespace QueryApi.Repositories
{
    public class PersonRepository
    {
        List<Person> _persons;

        public PersonRepository()
        {
            var fileName = "dummy.data.queries.json";
            if(File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                _persons = JsonSerializer.Deserialize<IEnumerable<Person>>(json).ToList();
            }
        }

        // retornar todos los valores
        public IEnumerable<Person> GetAll()
        {
            var query = _persons.Select(person => person);
            return query;
        }

        // retornar campos especificos
        public IEnumerable<Object> GetFields()
        {
            var query = _persons.Select(person => new
            {
                NombreCompleto = $"{person.FirstName} {person.LastName}",
                AnioNacimiento = DateTime.Now.AddYears(person.Age * -1).Year,
                CorreoElectronico = person.Email
            });
            return query;
        }
        // retornar elementos que sean iguales
        public IEnumerable<Person> GetByGender(char gender)
        {
            var query = _persons.Where(person => person.Gender == gender);
            return query;
        }
        // Retornar elementos que sean diferentes
        public IEnumerable<Person> GetDifference(char gender, int age)
        {
            var query = _persons.Where(person => person.Age <= age && person.Gender != gender);
            return query;
        }
        //Retornar personas cuya edad se encuentre entre los 20 y 30 años
        public IEnumerable<Person> GetByAgeRange(int age1, int age2)
        {
            var query = _persons.Where(person => person.Age > age1 && person.Age < age2);
            return query;
        }

        //Retornar diferentes trabajos
        public IEnumerable<string> GetJobs()
        {
            var query = _persons.Select(person => person.Job).Distinct();
            return query;
        }
        // retornar valores que contengan
        public IEnumerable<Person> GetStartsWith(string word)
        {
            var query = _persons.Where(person => person.Job.StartsWith(word));
            return query;
        }

        public IEnumerable<Person> GetContains(string word)
        {
            var query = _persons.Where(person => person.FirstName.Contains(word));
            return query;
        }

        public IEnumerable<Person> GetByList(List<int> ages)
        {
            var query = _persons.Where(person => ages.Contains(person.Age));
            return query;
        }

        //Retornar personas mayores a 40 años ordenadas por edad
        public IEnumerable<Person> GetByAge(int age)
        {
            var query = _persons.Where(person => person.Age > age).OrderBy(person => person.Age);
            return query;
        } 

        // retornar elementos ordenados
        public IEnumerable<Person> GetOrdered(int age)
        {
            var query = _persons.Where(person => person.Age < age).OrderBy(person => person.Age);
            return query;
        }
        public IEnumerable<Person> GetOrderedDesc(int age1, int age2)
        {
            var query = _persons.Where(person => person.Age > age1 && person.Age < age2).OrderByDescending(person => person.Age);
            return query;
        }
        // retorno cantidad de elementos
        public int CountPerson(char gender)
        {
            var query = _persons.Count(person => person.Gender == char.ToUpper(gender));
            return query;
        }
        // Evalua si un elemento existe
        public bool ExistPerson(string lastName)
        {
            var query = _persons.Exists(person => person.LastName == lastName);
            return query;
        }
        // retornar solo un elemento
        public Person GetFirst(string job, int age)
        {
            var query = _persons.Single(person => person.Job == job && person.Age == age);
            return query;
        }
        // retornar solamente unos elementos
        public IEnumerable<Person> TakePerson(string job, int takeNumber)
        {
            var query = _persons.Where(person => person.Job == job).Take(takeNumber);
            return query;
        }
        // retornar elementos saltando posición
        public IEnumerable<Person> SkipPerson(string job, int skipNumber)
        {
            var query = _persons.Where(person => person.Job == job).Skip(skipNumber);
            return query;
        }
        public IEnumerable<Person> SkipTakePerson(string job, int skipNumber, int takeNumber)
        {
            var query = _persons.Where(person => person.Job == job).Skip(skipNumber).Take(takeNumber);
            return query;
        }
    }
}