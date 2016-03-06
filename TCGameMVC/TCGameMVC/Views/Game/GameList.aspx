<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<TCGameMVC.Models.GameModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GameList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Game List</h2>
    
    <%foreach (TCGameMVC.Models.GameModel model in Model)
      { %>
     <%=Html.ActionLink(model.GameName, "Table", new { model.GameId}, null) %>
    
    <br />
    <% }%>
</asp:Content>
