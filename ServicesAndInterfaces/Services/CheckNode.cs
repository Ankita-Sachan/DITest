using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Models;
using ServicesAndInterfaces.Interfaces;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace ServicesAndInterfaces.Services
{
    public class DataFileValidation : IDataFileValidation
    {

        private string fileName;
        public DataFileValidation(string _fileName)
        {
            fileName = _fileName;
        }

        public bool IsNodeExist(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode t = doc.SelectSingleNode("Persons/Person[@id='" + id + "']");

            if (t == null)
            {

                doc = null;
                return false;
            }

            else
            {
                doc = null;
                return true;
            }
        }



        string IDataFileValidation.FileName
        {
            get { return fileName; }
        }


        public bool IsFileExist()
        {
            if (File.Exists(fileName))
                return true;
            else
                return false;
        }
    }
}