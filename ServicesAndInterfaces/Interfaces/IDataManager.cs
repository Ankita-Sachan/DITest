using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Models;

namespace ServicesAndInterfaces.Interfaces
{
    public interface IDataManager
    {
        bool CreateXml();
        List<Person> GetAll();
        Person GetNode(int id);
        bool DeleteNode(int id);
        Boolean InsertNode(Person person);
    }
}