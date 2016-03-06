<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TCGameMVC.Models.TableModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Table
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <div id="topPlayer" class="pane" style="width:100%;height:120px;text-align:center;">
           <%
           int i1=0;
           for(i1=0;i1<Model.TopPlayerCards;i1++)
           {
               int z1;
               z1 = i1 / 80 + 10;
               
              %>
              
      <div style="position:relative;left:-<%=i1*20%>px; top:0;display:inline;" >

              <% Html.RenderPartial("Card", new LogicLayer.Card() { Image = "back-blue-75-1.png" });
               %>
               </div>
               <%
           } %>
    </div>

    <div style="display:inline;">
        <div id="leftPlayer" class="pane" style="width:23%;vertical-align:middle;">
          <%
           int i2=0;
           for(i2=0;i2<Model.LeftPlayerCards;i2++)
           {
               int z2;
               
              %>
              
      <div style="position:relative; top:-<%=i2*50%>px;left:-<%=i2*70%>display:inline;" >

              <% Html.RenderPartial("Card", new LogicLayer.Card() { Image = "back-blue-75-1.png" });
               %>
               </div>
               <%
           } %>
    </div>
    <div style="display:inline;">
        <div id="canvas" class="pane" style="width:50%;height:430px;">
      <h1>Canvas</h1>
    </div>
    <br />
    <textarea id="logarea" rows="20" cols="50" ></textarea>
    </div>
        <div id="rightPlayer" class="pane" style="display:inline;width:23%;height:430px;vertical-align:middle;">
         <%
           int i3=0;
           for(i3=0;i3<Model.RightPlayerCards;i3++)
           {
              %>
              
      <div style="position:relative; top:-<%=i3*50%>px;left:-<%=i2*70%>display:inline;" >

              <% Html.RenderPartial("Card", new LogicLayer.Card() { Image = "back-blue-75-1.png" });
               %>
               </div>
               <%
           } %>
    </div>
    </div>
        <div id="bottomPlayer" class="pane" style="width:100%;height:120px;text-align:center;">
       <%
           int i=0;
           foreach (LogicLayer.Card card in Model.MyCards)
           {
               int z;
               i += 30;
               z = i / 80 + 10;
               
              %>
              
      <div  class="cardHolder" style="position:relative;left:-<%=i%>px; top:0;display:inline;" >

              <% Html.RenderPartial("Card", card);
               %>
               </div>
               <%
           } %>
    
</div>
<script language="javascript" type="text/javascript">
$(".cardHolder").card({x:500,y:500});
</script>
</asp:Content>
