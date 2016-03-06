
$(document).ready(function () {


    $.ajaxSetup({
        error: function (x, e) {
            if (x.status == 0) {
                alert('You are offline!!\n Please Check Your Network.');
            } else if (x.status == 404) {
                alert('Requested URL not found.');
            } else if (x.status == 500) {
                alert('Internel Server Error.');
            } else if (e == 'parsererror') {
                alert('Error.\nParsing JSON Request failed.');
            } else if (e == 'timeout') {
                alert('Request Time out.');
            } else {
                alert('Unknow Error.\n' + x.responseText);
            }
        }
    });
});


jQuery.fn.card = function (origin) {


    this.each(function () {

        $(this).mouseover(function () {

            $(this).addClass("topmost");

        });

        $(this).mouseout(function () {

            $(this).removeClass("topmost");

        });

        $(this).click(function () {
            var cardObj = $(this);




            var loc = window.location.href;

            loc = loc.substring(0, loc.lastIndexOf("/Game") + 1);
            loc += "Services/GameService.asmx/Play";
            var card = "{card: { Image: \"" + cardObj.children().first().attr("id") + "\", Description: \"\" }}";
            var o = {
                type: "POST",
                url: loc,
                data: card,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    ProcessPlayResponse(data.d);



                },
                error: function (x, msg, err) {
                    //$("#canvas").html(x.responseText);
                    alert("Error occured");
                }

            };

            $.ajax(o);

        });
    });



};



function ProcessPlayResponse(X) {

    for (var i=0;i<X.length;i++)
    {
        clientAction = X[i];
        var sType = clientAction.ActionType;
        switch (sType) {
            case 0:
                MoveCard(clientAction.card.Image, TranslateLocation(clientAction.FromLocation), TranslateLocation(clientAction.ToLocation));
                break;
            default:
                //  alert("something else detected" + sType);

                break;

     }   

    }
}


function MoveCard(Card, From, To) {

    Log("Test");
    Log("Entered move card");
    fromPos = GetPosition(From);

    toPos = GetPosition(To);
    if (From.toLowerCase() == "bottom") {


        var imageName;
        
        imageName = "#" + jQuery.trim(Card);
        imageName = imageName.replace(/-/g, "_").replace(/\./g, "_");

       
        var cardObj = $(imageName);
        
        pos = cardObj.offset();

        var canvas = $("#canvas");

        var options = { left: toPos.left + Math.ceil(canvas.width() / 2) - 37, top: toPos.top + canvas.height() - 107

        };

        cardObj.appendTo("body");
        cardObj.css({ left: pos.left, top: pos.top , position: "absolute" });

        cardObj.animate(options);
        

    }
    else {
    }



}

function GetPosition(Pos) {
    var layer;
    switch (Pos.toLowerCase()) {
        case "top":
            layer = "#topPlayer";
            break;
        case "bottom":
            layer = "#bottomPlayer";
            break;
        case "left":
            layer = "#leftPlayer";
            break;
        case "right":
            layer = "#rightPlayer";
            break;
        default:
            layer = "#canvas";
            break;
    }
    return $(layer).position();


}

function TranslateLocation(x) {
//Canvas, Left, Right, Top, Bottom
    var loc;
    switch (x) {
        case 0:
            loc = "canvas";
            break;
        case 1:
            loc = "left";
            break;
        case 2:
            loc = "right";
            break;
        case 3:
            loc = "top";
            break;
        case 4:
            loc = "bottom";
            break;
    }

    return loc;
}

function Log(s) {
    var log = $("#logarea");

    log.val(log.val() + "\r\n" + s);

    setTimeout(function () {
        log.scrollTop(log.get(0).scrollHeight);
    }, 200);
}

function GetDOM(s)
{
    if (window.DOMParser) {
    xmlDoc=parser.parseFromString(s,"text/xml");
  }
else // Internet Explorer
  {
  xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
  xmlDoc.async="false";
  xmlDoc.loadXML(s);
}


}

function GetNodeData(dom, tagname) {

    return dom.getElementsByTagName(xpath)[0].nodeValue;
}