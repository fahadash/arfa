<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LogicLayer.Card>" %>

<div style="display:inline;width:75;height:107;" id="<%=Model.Image.Replace(".","_").Replace("-","_") %>">
<img src="<%=ResolveUrl("~/Content/Images/Classic/Deck/" + Model.Image) %>" />
</div>