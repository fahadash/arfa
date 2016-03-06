using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TC2.Models
{
    public class CreateGameModel
    {
        [DataType(DataType.Text)]
        [DisplayName("Game title (optional)")]
        string GameTitle {get; set;}
    }


    public class JoinGameModel
    {
        [DataType(DataType.Url)]
        int gameNumbers;
    }


    public interface IJoinGameService
    {

    }

}