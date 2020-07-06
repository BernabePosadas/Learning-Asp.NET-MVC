using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserTask.Objects.Static;
using UserTask.Objects;
using UserTask.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        UserParameters payload = new UserParameters
        {
            FirstName = "Bernabe",
            LastName = "Posadas",
            Email = "bernabe_posadas@yahoo.com",
            Password = SingletonObjects.Hasher.GenerateHash(Encoding.UTF8.GetBytes("password"))
        };
        string Guid = "a8343416-8516-4f63-8e3a-c8edd4690ad1";

        [TestMethod]
        public void TestADBConnection()
        {
            MySQLClass DBInstance = SingletonObjects.DB;
            Assert.IsTrue(DBInstance.ConnectionTest());
        }
        [TestMethod]
        public void TestBHasher()
        {
            SHA256Hasher hasher = SingletonObjects.Hasher;
            string hash1 = hasher.GenerateHash(Encoding.UTF8.GetBytes("password"));
            string hash2 = hasher.GenerateHash(Encoding.UTF8.GetBytes("password"));
            Assert.IsTrue(hasher.CompareHash(hash1, hash2));
        }
        [TestMethod]
        public void TestCUserRetrieveGUID()
        {

            User user = new User();
            Assert.IsTrue(user.RetrieveUserByGUID(Guid));
            Assert.AreEqual(user.FirstName, payload.FirstName);
            Assert.AreEqual(user.LastName, payload.LastName);
            Assert.AreEqual(user.Email, payload.Email);
            Assert.AreEqual(user.Password, payload.Password);

        }
        [TestMethod]
        public void TestDUserRetrieveEmail()
        {

            User user = new User();
            Assert.IsTrue(user.RetrieveUserByEmail(payload.Email));
            Assert.AreEqual(user.FirstName, payload.FirstName);
            Assert.AreEqual(user.LastName, payload.LastName);
            Assert.AreEqual(Guid, user.UserGUID.ToString());
            Assert.AreEqual(user.Password, payload.Password);
        }
        [TestMethod]
        public void TestECreateTask()
        {
            User user = new User();
            user.RetrieveUserByGUID(Guid);
            TaskItem item = new TaskItem
            {
                TaskName = "Unit Testing",
                TaskDescription = "Unit test software first to increase build confidence",
                IsDone = "no"
            };
            Assert.IsTrue(user.UserTask.Create(item));

        }
        [TestMethod]
        public void TestFUpdateTask()
        {
            User user = new User();
            user.RetrieveUserByGUID(Guid);
            TaskItem item = user.UserTask.Tasks[0];
            item.IsDone = "yes";
            Assert.IsTrue(user.UserTask.Update(item));
        }
        [TestMethod]
        public void TestGRemoveTask()
        {
            User user = new User();
            user.RetrieveUserByGUID(Guid);
            TaskItem item = user.UserTask.Tasks[0];
            Assert.IsTrue(user.UserTask.Delete(item.ItemGUID.ToString()));
        }
    }
}
