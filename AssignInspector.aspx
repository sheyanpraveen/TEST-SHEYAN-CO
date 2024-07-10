<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignInspector.aspx.cs"
    Inherits="RRD.GRESAdmin.AssignInspector" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assign Inspector</title>
    <style type="text/css">
        .rcbHeader ul, .rcbFooter ul, .rcbItem ul, .rcbHovered ul, .rcbDisabled ul
        {
            width: 100%;
            display: inline-block;
            margin: 0;
            padding: 0;
            list-style-type: none;
        }
        
        .rcbHeader ul:after, .rcbFooter ul:after, .rcbItem ul:after, .rcbHovered ul:after, .rcbDisabled ul:after
        {
            content: ".";
            display: block;
            visibility: hidden;
            font-size: 0;
            line-height: 0;
            height: 0;
            clear: both;
        }
        
        .col1
        {
            float: left;
            width: 60px;
            margin: 0;
            padding: 0 5px 0 0;
            line-height: 14px;
        }
         .col1, .col2, 
        {
            float: left;
            width: 140px;
            margin: 0;
            padding: 0 5px 0 0;
            line-height: 14px;
        }
        .col3, .col4
        {
            float: left;
            width: 40px;
            margin: 0;
            padding: 0 5px 0 0;
            line-height: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript"> 
        <!--
                //Custom js code section used to edit records, store changes and switch the visibility of column editors

                //global variables for edited cell and edited rows ids
                var editedCell;
                var arrayIndex = 0;
                var editedItemsIds = [];

                function RowCreated(sender, eventArgs) {
                    var dataItem = eventArgs.get_gridDataItem();

                    //traverse the cells in the created client row object and attach click handler for each of them
//                    for (var i = 1; i < dataItem.get_element().cells.length; i++) {
//                        var cell = dataItem.get_element().cells[i];
//                        if (cell) {
//                            $addHandler(cell, "click", Function.createDelegate(cell, ShowColumnEditor));
//                        }
//                    }
                }
                //detach the ondblclick handlers from the cells on row disposing
                function RowDestroying(sender, eventArgs) {
                    if (eventArgs.get_id() === "") return;
                    var row = eventArgs.get_gridDataItem().get_element();
                    var cells = row.cells;
                    for (var j = 0, len = cells.length; j < len; j++) {
                        var cell = cells[j];
                        if (cell) {
                            $clearHandlers(cell);
                        }
                    }
                }

                function RowClick(sender, eventArgs) {
                    if (editedCell) {
                        //if the click target is table cell or span and there is an edited cell, update the value in it
                        //skip update if clicking a span that happens to be a form decorator element (having a class that starts with "rfd")
                        if ((eventArgs.get_domEvent().target.tagName == "TD") ||
                        (eventArgs.get_domEvent().target.tagName == "SPAN" && !eventArgs.get_domEvent().target.className.startsWith("rfd"))) {
                            UpdateValues(sender);
                            editedCell = false;
                        }
                    }
                }
                function ShowColumnEditor() {
                    editedCell = this;

    
                    var colEditor = this.getElementsByTagName("div")[0] ||
                                    this.getElementsByTagName("input")[0] ||
                                    this.getElementsByTagName("select")[0];

                    //if the column editor is a form decorated select dropdown, show it instead of the original 
                    if (colEditor.className == "rfdRealInput" &&
                        colEditor.tagName.toLowerCase() == "select")
                        colEditor = Telerik.Web.UI.RadFormDecorator.getDecoratedElement(colEditor);
                    colEditor.style.display = "";
                    if (colEditor.tagName == "DIV") {
                        var combo = $find(colEditor.id);
                        combo.get_inputDomElement().focus();
                    }
                    else {
                        colEditor.focus();
                    }

                }
                function StoreEditedItemId(editCell) {
                    //get edited row key value and add it to the array which holds them
                    var gridRow = $find(editCell.parentNode.id);
                    var rowKeyValue = gridRow.getDataKeyValue("InspectionOrderID");
                    Array.add(editedItemsIds, rowKeyValue);
                }
                function HideEditor(editCell, editorType) {
                    //get reference to the label in the edited cell
                    var lbl = editCell.getElementsByTagName("span")[0];

                    switch (editorType) {
                        case "textbox":
                            var txtBox = editCell.getElementsByTagName("input")[0];
                            if (lbl.innerHTML != txtBox.value) {
                                lbl.innerHTML = txtBox.value;
                                editCell.style.border = "1px dashed";

                                StoreEditedItemId(editCell);
                            }
                            txtBox.style.display = "none";
                            break;
                        case "checkbox":
                            var chkBox = editCell.getElementsByTagName("input")[0];
                            if (lbl.innerHTML.toLowerCase() != chkBox.checked.toString()) {
                                lbl.innerHTML = chkBox.checked;
                                editedCell.style.border = "1px dashed";

                                StoreEditedItemId(editCell);
                            }
                            chkBox.style.display = "none";
                            editCell.getElementsByTagName("span")[1].style.display = "none";
                            break;
                        case "dropdown":
                            var ddl = $find(editCell.getElementsByTagName("div")[0].id);
                            var selectedValue = ddl.get_value();
                            var selectedText = ddl.get_text();
                            if (selectedValue == "") selectedText = "";
                            if (lbl.innerHTML != selectedValue || lbl.innerHTML != selectedText) {
                                lbl.innerHTML = selectedText;
                                editCell.style.border = "1px dashed";

                                StoreEditedItemId(editCell);
                            }
                            //                            ddl.get_element().style.display = "none";
                            break;
                        case "datetimepicker":
                            var dtPickerDIV = editCell.getElementsByTagName("div")[0];
                            var dtPickerID = editCell.getElementsByTagName("input")[0].id;
                            var dtPickerClientID = dtPickerID.replace("_dateInput_text", "");
                            var dtPicker = $find(dtPickerClientID);
                            if (lbl.innerHTML != dtPicker.get_textBox().value) {
                                lbl.innerHTML = dtPicker.get_textBox().value;
                                editCell.style.border = "1px dashed red";
                                StoreEditedItemId(editCell);
                            }
                            dtPickerDIV.style.display = "none";
                            break;

                        default:
                            break;
                    }
                    lbl.style.display = "inline";
                }
                function UpdateValues(grid) {
                    //determine the name of the column to which the edited cell belongs
                    var tHeadElement = grid.get_element().getElementsByTagName("thead")[0];
                    var headerRow = tHeadElement.getElementsByTagName("tr")[0];
                    var colName = grid.get_masterTableView().getColumnUniqueNameByCellIndex(headerRow, editedCell.cellIndex);

                    //based on the column name, extract the value from the editor, update the text of the label and switch its visibility with that of the column
                    //column. The update happens only when the column editor value is different than the non-editable value. We also set dashed border to indicate
                    //that the value in the cell is changed. The logic is isolated in the HideEditor js method
                    switch (colName) {

                        case "AssignedCompanyInspectorID":
                            HideEditor(editedCell, "dropdown");
                            break;

                        case "Discontinued":
                            HideEditor(editedCell, "checkbox");
                            break;
                        case "CompanyID":
                            HideEditor(editedCell, "dropdown");
                            break;
                        default:
                            break;
                    }
                }

                function CancelChanges() {
                    if (editedItemsIds.length > 0) {
                        $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest("");
                    }
                    else {
                        alert("No pending changes to be discarded");
                    }
                    editedItemsIds = [];
                }
                function ProcessChanges() {
                    //extract edited rows ids and pass them as argument in the ajaxRequest method of the manager
                    if (editedItemsIds.length > 0) {
                        var Ids = "";
                        for (var i = 0; i < editedItemsIds.length; i++) {
                            Ids = Ids + editedItemsIds[i] + ":";
                        }
                        $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest(Ids);
                    }
                    else {
                        alert("No pending changes to be processed");
                    }
                    editedItemsIds = [];
                }
                function RadGrid1_Command(sender, eventArgs) {
                    //Note that this code has to be executed if you postback from external control instead from the grid (intercepting its onclientclick handler for this purpose),
                    //otherwise the edited values will be lost or not propagated in the source
                    if (editedItemsIds.length > 0) {
                        if (eventArgs.get_commandName() == "Sort" || eventArgs.get_commandName() == "Page" || eventArgs.get_commandName() == "Filter") {
                            if (confirm("Any unsaved edited values will be lost. Choose 'OK' to discard the changes before proceeding or 'Cancel' to abort the action and process them.")) {
                                editedItemsIds = [];
                            }
                            else {
                                //cancel the chosen action
                                eventArgs.set_cancel(true);

                                //process the changes
                                ProcessChanges();
                                editedItemsIds = [];
                            }
                        }
                    }
                }
                window.onunload = function () {
                    //this code should also be executed on postback from external control (which rebinds the grid) to process any unsaved data
                    if (editedItemsIds.length > 0) {
                        if (confirm("Any unsaved edited values will be lost. Choose 'OK' to discard the changes before proceeding or 'Cancel' to abort the action and process them.")) {
                            editedItemsIds = [];
                        }
                        else {
                            //process the changes
                            ProcessChanges();
                            editedItemsIds = [];
                        }
                    }
                };
                function UpdateItemCountField(sender, args) {
                    //set the footer text
                    sender.get_dropDownElement().lastChild.innerHTML = "A total of " + sender.get_items().get_count() + " items";
                }
                function OnClientSelectedIndexChangedEventHandler(sender, args) {
                    //sender represents the combobox that has fired the event
                    //the code below obtains the item that has been changed
                    var item = args.get_item();

                    var rowIndex = sender.get_element().parentNode.parentNode.rowIndex - 1;
                    alert(rowIndex);

                    //get edited row key value and add it to the array which holds them
                    var gridRow = $find(sender.get_element().parentNode.parentNode.id);
                    var rowKeyValue = gridRow.getDataKeyValue("InspectionOrderID");
                    Array.add(editedItemsIds, rowKeyValue)

                }


     -->    
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All"
            EnableRoundedCorners="false" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                        <telerik:AjaxUpdatedControl ControlID="Label1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                        <telerik:AjaxUpdatedControl ControlID="Label1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
        <telerik:RadInputManager ID="RadInputManager1" EnableEmbeddedBaseStylesheet="false"
            Skin="" runat="server">
            <telerik:TextBoxSetting BehaviorID="StringBehavior" InitializeOnClient="true" EmptyMessage="type here" />
        </telerik:RadInputManager>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="SqlDataSource1" OnItemDataBound="OnItemDataBoundHandler"
            Width="97%" ShowStatusBar="True" AllowSorting="True" PageSize="15" GridLines="None"
            AllowPaging="True" runat="server" AutoGenerateColumns="False" CellSpacing="0">
            <MasterTableView TableLayout="Fixed" DataKeyNames="InspectionOrderID" EditMode="InPlace"
                ClientDataKeyNames="InspectionOrderID" CommandItemDisplay="Bottom">
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <CommandItemTemplate>
                    <div style="height: 30px; text-align: right;">
                        <asp:Image ID="imgCancelChanges" runat="server" ImageUrl="~/Images/cancel.gif" AlternateText="Cancel changes"
                            ToolTip="Cancel changes" Height="24px" Style="cursor: pointer; margin: 2px 5px 0px 0px;"
                            onclick="CancelChanges();" />
                        <asp:Image ID="imgProcessChanges" runat="server" ImageUrl="~/Images/ok.gif" AlternateText="Process changes"
                            ToolTip="Process changes" Height="24px" Style="cursor: pointer; margin: 2px 5px 0px 0px;"
                            onclick="ProcessChanges();" />
                    </div>
                </CommandItemTemplate>
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn UniqueName="InspectionOrderID" DataField="InspectionOrderID"
                        HeaderText="InspectionOrderID" ReadOnly="True" DataType="System.Guid" FilterControlAltText="Filter InspectionOrderID column"
                        SortExpression="InspectionOrderID" />
                    <telerik:GridBoundColumn UniqueName="OrderNo" DataField="OrderNo" HeaderText="OrderNo"
                        ReadOnly="True" HeaderStyle-Width="5%" DataType="System.Int32" FilterControlAltText="Filter OrderNo column"
                        SortExpression="OrderNo" />
                    <telerik:GridBoundColumn UniqueName="OrderDt" DataField="OrderDt" HeaderText="OrderDt"
                        SortExpression="OrderDt" DataType="System.DateTime" FilterControlAltText="Filter OrderDt column" />
                    <telerik:GridBoundColumn UniqueName="LoanNo" DataField="LoanNo" HeaderText="LoanNo"
                        SortExpression="LoanNo" HeaderStyle-Width="5%" FilterControlAltText="Filter LoanNo column" />
                    <telerik:GridBoundColumn UniqueName="RushStatus" DataField="RushStatus" HeaderText="RushStatus"
                        SortExpression="RushStatus" HeaderStyle-Width="10%" DataType="System.Int32" FilterControlAltText="Filter RushStatus column" />
                    <telerik:GridTemplateColumn UniqueName="AssignedCompanyInspectorID" HeaderText="Assigned Inspector"
                        SortExpression="AssignedCompanyInspectorID" HeaderStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedCompanyInspectorID" runat="server" Text='<%# Eval("AssignedCompanyInspectorID") %>'
                                Style="display: none" />
                            <telerik:RadComboBox runat="server" ID="ddlCompanyID" EnableLoadOnDemand="True" MarkFirstMatch="true"
                                DataTextField="InspectorNo" EmptyMessage="Choose an Inspector" Text='<%# Eval("InspectorNo") %>'
                                OnClientItemsRequested="UpdateItemCountField" OnItemsRequested="RadComboBox1_ItemsRequested"
                                DataValueField="AssignedCompanyInspectorID" AutoPostBack="false" HighlightTemplatedItems="true"
                                Height="140px" Width="280px" DropDownWidth="420px" OnSelectedIndexChanged="OnSelectedIndexChangedHandler"
                                OnClientSelectedIndexChanged="OnClientSelectedIndexChangedEventHandler">
                                <HeaderTemplate>
                                    <ul>
                                        <li class="col1">InspectorNo</li>
                                        <li class="col2">CompanyName</li>
                                        <li class="col3">State</li>
                                        <li class="col4">Zip</li>
                                    </ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li class="col1">
                                            <%# DataBinder.Eval(Container, "Text")%>
                                        </li>
                                        <li class="col2">
                                            <%# DataBinder.Eval(Container, "Attributes['CompanyName']")%></li>
                                        <li class="col3">
                                            <%# DataBinder.Eval(Container, "Attributes['CompanyState']")%></li>
                                        <li class="col4">
                                            <%# DataBinder.Eval(Container, "Attributes['CompanyZip']")%></li>
                                    </ul>
                                </ItemTemplate>
                                <FooterTemplate>
                                    A total of
                                    <asp:Label runat="server" ID="RadComboItemsCount" />
                                    items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="ETA" DataType="System.Decimal" FilterControlAltText="Filter ETA column"
                        HeaderText="ETA" SortExpression="ETA" UniqueName="ETA">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ClientDueDt" DataType="System.DateTime" FilterControlAltText="Filter ClientDueDt column"
                        HeaderText="ClientDueDt" SortExpression="ClientDueDt" UniqueName="ClientDueDt">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnCommand="RadGrid1_Command" OnRowCreated="RowCreated" OnRowDestroying="RowDestroying" />
            </ClientSettings>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
        </telerik:RadGrid>
        <br />
        <asp:Label ID="Label1" runat="server" EnableViewState="false" />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:SiteInspectionsConnectionString %>"
            SelectCommand="SELECT InspectionOrder.InspectionOrderID, InspectionOrder.OrderNo, InspectionOrder.OrderDt, InspectionOrder.LoanNo, InspectionOrder.RushStatus, InspectionOrder.ETA, InspectionOrder.ClientDueDt, InspectionOrder.AssignedCompanyInspectorID, Company.InspectorNo FROM InspectionOrder LEFT OUTER JOIN Company ON InspectionOrder.AssignedCompanyInspectorID = Company.CompanyID ORDER BY OrderDt"
            runat="server" UpdateCommand="UPDATE [InspectionOrder] SET [AssignedCompanyInspectorID] = @AssignedCompanyInspectorID WHERE [InspectionOrderID] = @InspectionOrderID">
            <UpdateParameters>
                <asp:Parameter Name="AssignedCompanyInspectorID" Type="String" />
                <asp:Parameter Name="InspectionOrderID" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SiteInspectionsConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select CompanyID as [AssignedCompanyInspectorID], InspectorNo, CompanyName, CompanyStreet1, CompanyCity, CompanyState, CompanyZip  from [Company] where CompanyRelationshipID = 4 order by InspectorNo" />
        <!-- content end -->
    </div>
    </form>
</body>
</html>
