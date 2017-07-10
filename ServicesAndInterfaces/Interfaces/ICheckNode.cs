using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Models;

namespace ServicesAndInterfaces.Interfaces
{
    public interface ICheckNode
    {


         string FileName { get; }
        
        bool IsNodeExist(int id);
    }
}