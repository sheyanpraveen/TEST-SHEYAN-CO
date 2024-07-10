<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="RRD.GRESAdmin.Home" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageHeaderContentPlaceHolder" runat="server">
    Selected Application
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContentPlaceHolder" runat="server">
    <div style="display:inline-block" class="d-inlineblock select-app">
        <telerik:RadComboBox ID="cboApplication" runat="server" OnSelectedIndexChanged="cboApplication_SelectedIndexChanged"
        Width="262px" AutoPostBack="true">
    </telerik:RadComboBox>
    </div>
    <div class="d-inlineblock pl5">
         <asp:Button runat="server" Text="Recycle application pool" ID="RecycleAppPool" OnClick="RecycleAppPool_Click" />
    </div>
   <div class="clearfix"></div>
    
    <asp:Label ID="LbErrorMessage" runat="server" CssClass="page-errors text-red pt5"></asp:Label>
    <asp:Label ID="LbsuccessMessage" runat="server" CssClass="text-green pt5"></asp:Label>

    <telerik:RadWindow RenderMode="Lightweight" runat="server" ID="RadWindowAppPoolRecycle" Width="450px" Height="250px" Modal="true"
        OffsetElementID="main" Style="z-index: 100001;" DestroyOnClose="true" Behaviors="Close, Move, Resize" Title="Recycle Application Pool">
        <ContentTemplate>
            <p style="text-align: left;">
                <asp:Literal ID="LtConfirmMessage" runat="server"></asp:Literal>
                <asp:BulletedList runat="server" ID="BlIisSiteNames"></asp:BulletedList>
                <asp:Literal ID="Literal1" runat="server">Are you sure you want to recycle this application pool?</asp:Literal>
            </p>
            <p style="text-align: center;">
                <asp:HiddenField ID="HfAppPoolName" runat="server" />
                <asp:Button ID="BtnYes" Text="Yes" runat="server" OnClick="BtnYes_Click" />
                <asp:Button ID="BtnNo" Text="No" runat="server" />
            </p>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>


