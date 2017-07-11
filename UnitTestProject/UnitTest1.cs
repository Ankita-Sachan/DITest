using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomation;
using ServicesAndInterfaces.Services;
using ServicesAndInterfaces.Interfaces;
using Entities.Models;

using Moq;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Persons.xml");
        private Mock<IDataFileValidation> chk = new Mock<IDataFileValidation>();

        public UnitTest1()
        {
            //Initializing file name 
            
        }
        //Sequentially Execute Test Case 
        [TestMethod]
        public void IsFileExist()
        {
            
            DataFileValidation obje = new DataFileValidation(fileName);
            Assert.IsTrue(obje.IsFileExist());
        }

        [TestMethod]
        public void CreateXml()
        {
            DataManager obje = new DataManager(chk.Object);
            Assert.AreEqual(obje.CreateXml(), true);
        }


        [TestMethod]
        public void IsNodeExist()
        {

            int nodeId = 1;
            DataFileValidation obje = new DataFileValidation(fileName);
            Assert.IsTrue(obje.IsNodeExist(nodeId));
        }
        [TestMethod]
        public void InsertNode()
        {
            chk.SetupGet(x => x.FileName).Returns(fileName);
            chk.Setup(x => x.IsFileExist()).Returns(true);
            Person person = new Person { id = 1, Name = "Ankita", Age = 2, Gender = "Female" };
           
            DataManager obje = new DataManager(chk.Object);
            Assert.AreEqual(obje.InsertNode(person), true);
        }
        [TestMethod]
        public void DeleteNode()
        {
            chk.SetupGet(x => x.FileName).Returns(fileName);
            int deleteNodeid = 1;
            DataManager obje = new DataManager(chk.Object);
            Assert.IsTrue(obje.DeleteNode(deleteNodeid));
        }


       
    }
}
