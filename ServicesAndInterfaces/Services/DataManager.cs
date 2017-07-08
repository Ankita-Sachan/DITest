using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Models;
using ServicesAndInterfaces.Interfaces;

namespace ServicesAndInterfaces.Services
{
    public class DataManager:IDataManager
    {
         List<Person> lstPerson =new List<Person>();
           
        public List<Person> GetAll()
        {
            lstPerson.Add(new Person { id = 1, Name = "Test1", Age = 25, Gender = "Female" });
            lstPerson.Add(new Person { id = 2, Name = "Test2", Age = 26, Gender = "Male" });
            lstPerson.Add(new Person { id = 3, Name = "Test3", Age = 27, Gender = "Female" });
            return lstPerson;
        }

        public Person Get(int id)
        {
            Person person = new Person();
            return lstPerson.Where(x=>x.id==id).FirstOrDefault();
        }
    }
}