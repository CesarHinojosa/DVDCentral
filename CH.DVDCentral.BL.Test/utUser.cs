using CH.DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.DVDCentral.BL.Test
{
    public class utUser
    {
        public void Seed()
        {
            UserManager.Seed();

        }


        [TestMethod]
        public void Load()
        {
            Assert.IsTrue(UserManager.Load().Count > 0);
        }

        [TestMethod]
        public void LoginSuccessfulTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new User
            {
                UserId = "bfoote",
                Password = "maple"
            }));
            Assert.IsTrue(UserManager.Login(new User
            {
                UserId = "kfrog",
                Password = "misspiggy"
            }));
        }

        [TestMethod]
        public void LoginFailureNoUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User
                {
                    UserId = "",
                    Password = "maple"
                }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
        [TestMethod]
        public void LoginFailureBadPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User
                {
                    UserId = "bfoote",
                    Password = "birch"
                }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
        [TestMethod]
        public void LoginFailureBadUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User
                {
                    UserId = "BFOOTE",
                    Password = "maple"
                }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }
        [TestMethod]
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(UserManager.Login(new User
                {
                    UserId = "bfoote",
                    Password = ""
                }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }

        [TestMethod]
        public void Insert()
        {
            //int id = 0;
            //Customer customer = new Customer
            //{
            //    FirstName = "Test",
            //    LastName = "Test",
            //    UserID = 9,
            //    Address = "Test",
            //    City = "Test",
            //    State = "WI", // Has to be only two characters 
            //    Zip = "Test",
            //    Phone = "Test",
            //    ImagePath = "Test"


            //};
            //int results = CustomerManager.Insert(customer, true);
            //Assert.AreEqual(1, results);

            int id = 0;
            User user = new User
            {
                FirstName = "Test",
                LastName = "Test",
                UserId = "Test",
                Password = "Test"


            };

            int results = UserManager.Insert(user, true);
            Assert.AreEqual(1, results);

        }
    }
}
