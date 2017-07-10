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
    public class DataManager : IDataManager
    {
        public List<Person> lstPerson = new List<Person>();
        public string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Persons.xml");

        public bool CreateXml() {
            if (!File.Exists(fileName))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("    ");
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = true;
                Person person = new Person();
                person.id = 1;
                person.Name = "Ankita Sachan";
                person.Age = 25;
                person.Gender = "Female";
                using (XmlWriter writer = XmlWriter.Create(fileName, settings))
                {
                    writer.WriteStartElement("Persons");
                    writer.WriteStartElement("Person");
                    writer.WriteAttributeString("id", Convert.ToString(person.id));
                    writer.WriteElementString("Name", person.Name);
                    writer.WriteElementString("Age", person.Age.ToString());
                    writer.WriteElementString("Gender", person.Gender);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.Flush();
                }
                Console.WriteLine("Xml File Path has been Created.\nPath is " + fileName + ".\n");
            }
            
            return true;
        }
        public List<Person> GetAll()
        {
            XDocument doc = null;
            doc = XDocument.Load(fileName);
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                doc = XDocument.Load(reader);
            }
            var query = from d in doc.Root.Descendants("Person")
                        select d;
           
            foreach (System.Xml.Linq.XElement q in query)
            {
                lstPerson.Add(new Person { id = Convert.ToInt16(q.FirstAttribute.Value), Name = q.Element("Name").Value, Age = Convert.ToInt16(q.Element("Age").Value), Gender = q.Element("Gender").Value });
            }
            doc = null;
            return lstPerson;
        }      
        public Person GetNode(int id)
        {
            Person person = new Person();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNodeList nodes = doc.GetElementsByTagName("Person");
            XmlNode xmlnode = doc.SelectSingleNode("//Person[@id=\"" + id + "\"]");
            if (xmlnode != null)
            {
                foreach (XmlNode node in nodes)
                {
                    person.id = Convert.ToInt16(node.Attributes[0].Value);
                    person.Name = node.ChildNodes[0].Value;
                    person.Age = Convert.ToInt16(node.ChildNodes[1].Value);
                    person.Gender = node.ChildNodes[2].Value;
                    //foreach (XmlAttribute attribute in node.Attributes)
                    //{
                    //    Console.WriteLine(attribute.Name + ": " + attribute.Value + "\n");
                    //}
                    //foreach (XmlNode childnode in node.ChildNodes)
                    //{

                    //    Console.WriteLine((childnode.ChildNodes[0].ParentNode).Name + ": " + childnode.ChildNodes[0].Value + "\n");
                    //}
                }
                return person;
            }
            else
                return null;
        }
        public bool DeleteNode(int id)
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
                t.ParentNode.RemoveChild(t);
                doc.Save(fileName);
                doc = null;
                return true;
            }
            
        }
        public Boolean InsertNode(Person person)
        {
            bool insertAfter = false;
            int idAfter=0;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNodeList nodes = doc.GetElementsByTagName("Person");
            XmlElement xmlElement;
            XmlElement child;

            List<int> lstIds = new List<int>();
            foreach (XmlNode node in nodes)
            {
                lstIds.Add(Convert.ToInt16(node.Attributes[0].Value));
            }
            if (lstIds.Count > 0)
            {
                lstIds.Sort();
               
                if (person.id > lstIds[lstIds.Count - 1])
                {
                    idAfter = lstIds[lstIds.Count - 1];
                    insertAfter = true;
                }
                else if (lstIds.Contains(person.id))
                {
                    doc = null;
                    return false;
                }
                else
                {
                    int i = 0;
                    while (person.id > lstIds[i])
                    {
                        i++;
                    }
                    i = i - 1;
                    if (i >= 0)
                    {
                        idAfter = lstIds[i];
                        insertAfter = true;
                    }
                    else {
                        idAfter = lstIds[i+1];
                        insertAfter = false;
                    }
                      
                }
                
               
            }
            else
            {
                var X = doc.GetElementsByTagName("Persons")[0];
                xmlElement = doc.CreateElement("Person");
                xmlElement.SetAttribute("id", person.id.ToString());
                //doc.DocumentElement.InsertAfter(xmlElement, xmlnode);


                 child = doc.CreateElement("Name");
                child.Attributes.RemoveNamedItem("xmlns");
                child.InnerXml = person.Name;
                xmlElement.AppendChild(child);

                child = doc.CreateElement("Age");
                child.InnerXml = person.Age.ToString();

                xmlElement.AppendChild(child);
                child = doc.CreateElement("Gender");
                child.InnerXml = person.Gender;
                xmlElement.AppendChild(child);
                var p = X.AppendChild(xmlElement);
                doc.Save(fileName);
                doc = null;
                return true;
            }
            XmlNode xmlnode = doc.SelectSingleNode("//Person[@id=\"" + idAfter + "\"]");
            XmlElement xmlElement1 = doc.CreateElement("Person");
            xmlElement1.SetAttribute("id", person.id.ToString());
            if (insertAfter) {
                doc.DocumentElement.InsertAfter(xmlElement1, xmlnode);
            }
            else
            {
                doc.DocumentElement.InsertBefore(xmlElement1, xmlnode);

            }
            xmlnode = doc.SelectSingleNode("//Person[@id=\"" + person.id + "\"]");
            child = doc.CreateElement("Name");
            child.Attributes.RemoveNamedItem("xmlns");
            child.InnerXml = person.Name;
            xmlnode.AppendChild(child);

            child = doc.CreateElement("Age");
            child.InnerXml = person.Age.ToString();

            xmlnode.AppendChild(child);
            child = doc.CreateElement("Gender");
            child.InnerXml = person.Gender;
            xmlnode.AppendChild(child);

            doc.Save(fileName);
            doc = null;
            return true;
        }
    }
}