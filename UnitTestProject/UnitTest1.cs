using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomation;
using ServicesAndInterfaces.Services;
using ServicesAndInterfaces.Interfaces;
using Entities;

using Moq;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Persons.xml");
            Mock<CheckNode> chk = new Mock<CheckNode>(fileName);
            
            chk.Setup(x => x.IsNodeExist(2)).Returns(true);

            DataManager obje = new DataManager(chk.Object);
            Assert.AreEqual(obje.DeleteNode(2), true); 
        }
    }
}
