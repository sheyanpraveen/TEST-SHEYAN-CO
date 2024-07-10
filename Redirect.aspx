<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Redirect.aspx.cs" Inherits="RRD.GRESAdmin.Redirect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function RefreshParent() {
            window.parent.location.href = window.parent.location.href;
        }
    </script>
</asp:Content>
