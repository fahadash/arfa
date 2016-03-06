using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace TCWeb.Utility
{
    public class TCServiceResult
    {
        public string errorcode {get; set;}
        public string message { get; set; }
    }

    public class TCServiceResponse
    {
        public TCServiceResult result { get; set; }
        public object response { get; set; }
    }

    public class RegisterParams
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string reserved1 { get; set; }
        public string reserved2 { get; set; }
    }

    public class LogInParams
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    public class ChangePasswordParams
    {
        public string username { get; set; }
        public string currentpassword { get; set; }
        public string newpassword { get; set; }
        public string confirmnewpassword { get; set; }
    }
    
    /*
         * ,"response":{"tableid":84,"tablename":"FullGame_First3HandWinner008",
         * "Owner":{"UserId":8,"UserName":"test3","Password":null,"Firstname":"test","Lastname":"test","Age":14},
         * "users":[{"userinfo":{"userid":6,"username":"Test1","firstname":"test","lastname":"test"},"gamescore":0,"currentcard":{"cardid":51,"cardalias":"2C","cardname":"2 of Clubs"},"dominant":false,"lastheartbeat":"2013-02-11T22:42:55.043","turn":false,"isyou":false,"yourcards":null},
         * {"userinfo":{"userid":7,"username":"test2","firstname":"test","lastname":"test"},"gamescore":0,"currentcard":{"cardid":45,"cardalias":"4D","cardname":"4 of Diamonds"},"dominant":false,"lastheartbeat":"2013-02-11T22:42:55.043","turn":false,"isyou":false,"yourcards":null},
         * {"userinfo":{"userid":8,"username":"test3","firstname":"test","lastname":"test"},"gamescore":0,"currentcard":null,"dominant":false,"lastheartbeat":"2013-02-11T22:42:55.043","turn":false,"isyou":true,"yourcards":[{"cardid":2,"cardalias":"AS","cardname":"Ace of Spades"},{"cardid":3,"cardalias":"AC","cardname":"Ace of Clubs"},{"cardid":8,"cardalias":"KH","cardname":"King of Hearts"},{"cardid":11,"cardalias":"QC","cardname":"Queen of Clubs"},{"cardid":15,"cardalias":"JC","cardname":"Jack of Clubs"},{"cardid":19,"cardalias":"10C","cardname":"10 of Clubs"},{"cardid":20,"cardalias":"10H","cardname":"10 of Hearts"},{"cardid":21,"cardalias":"10D","cardname":"10 of Diamonds"},{"cardid":23,"cardalias":"9C","cardname":"9 of Clubs"},{"cardid":26,"cardalias":"8S","cardname":"8 of Spades"},{"cardid":31,"cardalias":"7C","cardname":"7 of Clubs"},{"cardid":41,"cardalias":"5D","cardname":"5 of Diamonds"},{"cardid":46,"cardalias":"3S","cardname":"3 of Spades"}]},
         * {"userinfo":{"userid":9,"username":"test4","firstname":"test","lastname":"test"},"gamescore":0,"currentcard":{"cardid":39,"cardalias":"5C","cardname":"5 of Clubs"},"dominant":false,"lastheartbeat":"2013-02-11T22:42:55.043","turn":false,"isyou":false,"yourcards":null}],"handsaccumulated":0}
         */

    public class Card
    {
        public int cardid { get; set; }
        public string cardalias { get; set; }
        public string cardname { get; set; }
    }

    public class TCUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
    }
    public class TCUser1
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public class TCPlayer
    {
        public TCUser1 userinfo { get; set; }
        public int gamescore { get; set; }
        public Card currentcard { get; set; }
        public bool dominant { get; set; }
        public DateTime lastheartbeat {get; set;}
        public bool turn  {get; set; }
        public bool isyou { get; set; }
        public Card[] yourcards { get; set; }
    }
    public class TCUpdateTableResponse
    {
        public int tableid { get; set; }
        public string tablename { get; set; }
        public TCUser owner { get; set; }
        public TCPlayer[] users { get; set; }
    }

    public class TableUpdateResponse
    {
        public TCServiceResult result { get; set; }

        public TCUpdateTableResponse response { get; set; }
    }

    public class GetTableUpdateParams
    {
        public string logintoken { get; set; }
        public int tableid { get; set; }
    }

    public class TcApi
    {
        HttpClient _client;
        string baseAddress = "http://localhost/";
        string _apiRootRegister = "TCService/api/UserAccount";

        string _apiRootTable = "TCService/api/Table";

        public string TableServiceUrl
        {
            get
            {
                return baseAddress + _apiRootTable;
            }
        }

        public TcApi()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


        }

        public TCServiceResult Register(RegisterParams registerParams)
        {
            string uriAdd = _apiRootRegister + "/Register";

            var response =  _client.PostAsJsonAsync(uriAdd, registerParams);

            response.Wait();

            TCServiceResponse svcResponse = response.Result.Content.ReadAsAsync<TCServiceResponse>().Result;
            response.Wait();

            return svcResponse.result;
        }


        public TCServiceResponse Login(LogInParams loginParams)
        {
            string uriAdd = _apiRootRegister + "/LogIn";

            var response = _client.PostAsJsonAsync(uriAdd, loginParams);

            response.Wait();

            TCServiceResponse svcResponse = response.Result.Content.ReadAsAsync<TCServiceResponse>().Result;
            response.Wait();

            return svcResponse;
        }

        public TCServiceResponse ChangePassword(ChangePasswordParams changeParams)
        {
            string uriAdd = _apiRootRegister + "/ChangePassword";

            var response = _client.PostAsJsonAsync(uriAdd, changeParams);

            response.Wait();

            TCServiceResponse svcResponse = response.Result.Content.ReadAsAsync<TCServiceResponse>().Result;
            response.Wait();

            return svcResponse;
        }

        public TableUpdateResponse GetTableUpdate(GetTableUpdateParams updateParams)
        {
            string uriAdd = _apiRootTable + "/GetTableUpdate";

            var response = _client.PostAsJsonAsync(uriAdd, updateParams);

            response.Wait();

            TableUpdateResponse svcResponse = response.Result.Content.ReadAsAsync<TableUpdateResponse>().Result;
            response.Wait();

            return svcResponse;
        }

    }
}