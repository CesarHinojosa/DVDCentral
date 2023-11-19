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
                UserName = "bfoote",
                Password = "maple"
            }));
            Assert.IsTrue(UserManager.Login(new User
            {
                UserName = "kfrog",
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
                    UserName = "",
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
                    UserName = "bfoote",
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
                    UserName = "BFOOTE",
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
                    UserName = "bfoote",
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
            

            int id = 0;
            User user = new User
            {
                FirstName = "Test",
                LastName = "Test",
                UserName = "Test",
                Password = "Test"


            };

            int results = UserManager.Insert(user, true);
            Assert.AreEqual(1, results);

        }
    }
}
