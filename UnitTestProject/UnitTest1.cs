using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAutomation;
using ServicesAndInterfaces.Services;
using ServicesAndInterfaces.Interfaces;
using Entities;

using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<DataManager> chk = new Mock<DataManager>();
            chk.Setup(x => x.IsNodeExist(2)).Returns(true);

            DataManager obje = new DataManager();
            Assert.AreEqual(obje.insertEmployee(chk.Object), true); 
        }
    }
}
