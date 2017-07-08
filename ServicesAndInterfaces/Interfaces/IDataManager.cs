using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Models;

namespace ServicesAndInterfaces.Interfaces
{
    public interface IDataManager
    {
        List<Person> GetAll();
        Person Get(int id);
    }
}