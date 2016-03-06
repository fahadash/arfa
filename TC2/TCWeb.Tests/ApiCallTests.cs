using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCWeb.Utility;
using Newtonsoft.Json;

namespace TCWeb.Tests
{
    [TestClass]
    public class ApiCallTests
    {
        [TestMethod]
        public void Register()
        {
            TcApi api = new TcApi();

            TCServiceResult result = api.Register(new RegisterParams
                                            {
                                                username = "temp_" + Guid.NewGuid().ToString(),
                                                password = "tester",
                                                confirmpassword = "tester",
                                                firstname = "firstname",
                                                lastname = "lastname",
                                                age = 14,
                                            }
                                            );

            Assert.IsTrue(result.errorcode == "SUCCESS", "Wrong response code : " + result.errorcode + "\r\n" + result.message);
        }
        [TestMethod]
        public void Login()
        {
            TcApi api = new TcApi();
            string userName = "temp_" + Guid.NewGuid().ToString();

            TCServiceResult result = api.Register(new RegisterParams
            {
                username = userName,
                password = "tester",
                confirmpassword = "tester",
                firstname = "firstname",
                lastname = "lastname",
                age = 14,
            }
                                            );

            Assert.IsTrue(result.errorcode == "SUCCESS", "Register - Wrong response code : " + result.errorcode + "\r\n" + result.message);

            var obj = new { logintoken = string.Empty };

            TCServiceResponse response = api.Login(new LogInParams()
            {
                username = userName,
                password = "tester"
            });


            Assert.IsTrue(response.result.errorcode == "SUCCESS", "Login - Wrong response code : " + response.result.errorcode + "\r\n" + response.result.message);
        }

        [TestMethod]
        public void ChangePassword()
        {
            TcApi api = new TcApi();
            string userName = "temp_" + Guid.NewGuid().ToString();

            TCServiceResult result = api.Register(new RegisterParams
            {
                username = userName,
                password = "tester",
                confirmpassword = "tester",
                firstname = "firstname",
                lastname = "lastname",
                age = 14,
            }
                                            );

            Assert.IsTrue(result.errorcode == "SUCCESS", "Register - Wrong response code : " + result.errorcode + "\r\n" + result.message);


            TCServiceResponse response = api.ChangePassword(new ChangePasswordParams()
            {
                username = userName,
                currentpassword = "tester",
                newpassword = "tester1",
                confirmnewpassword = "tester1"
            });

            Assert.IsTrue(response.result.errorcode == "SUCCESS", "Change Password - Wrong response code : " + response.result.errorcode + "\r\n" + response.result.message);
        }


        [TestMethod]
        public void TableUpdate()
        {
            TcApi api = new TcApi();

            TableUpdateResponse result = api.GetTableUpdate(new GetTableUpdateParams
            {
                logintoken = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142",
                 tableid = 84,
            });

            Assert.IsTrue(result.result.errorcode == "SUCCESS", "Wrong response code : " + result.result.errorcode + "\r\n" + result.result.message);
        }
    }
}
