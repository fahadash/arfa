<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="TC.Game" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <style type="text/css">
    <!--
    .pane
    {
       border: 1px solid #000000 ;
       display:inline-table;
       
    }
    -->
    </style>
    <form id="form1" runat="server">
    <div id="topPlayer" class="pane" style="width:100%;height:120px;">
     <h1>Top Player</h1>
    </div>
        <div id="leftPlayer" class="pane" style="width:23%;height:430px;">
      <h1>Left Player</h1>
    </div>
        <div id="canvas" class="pane" style="width:50%;height:430px;">
      <h1>Canvas</h1>
    </div>
        <div id="rightPlayer" class="pane" style="width:23%;height:430px;">
      <h1>Right Player</h1>
    </div>
        <div id="bottomPlayer" class="pane" style="width:100%;height:120px;">
      <h1>Bottom Player</h1>
    </div>
    </form>
</body>
</html>
