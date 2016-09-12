<%@ Page Title="" Language="C#" MasterPageFile="~/JobPages/Master/Pages.Master" AutoEventWireup="true" CodeBehind="RefreshResume.aspx.cs" Inherits="Job.UI.JobPages.job.RefreshResume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
        <table>
            <tr><td><input type="text" subtag="sub" id="userName" /></td></tr>
            <tr><td><input type="text" subtag="sub" id="userPass" /></td></tr>
            <tr><td><input type="button"  id="submit" /></td></tr>
            <tr><td ></td></tr>
        </table>
    </div>
    <script type="text/javascript">
        $("#submit").click(function () {
            var mydata = new MyDataPack("zhaopinPrcoess.ashx", "MyLogin");
            mydata.getValueSetData("sub");
            var obj = new Object();
            obj.type = "";
            mydata.addValue("obj", obj)

            $.ajax({
                type: "post",
                dataType: "json",
                url: "",
                data: "",
                beforeSend: function (XMLHttpRequest) {
                    $.blockUI({ type: 2 });
                },
                complete: function (jqXHR, status, responseText) {
                    $.unblockUI();
                },
                success: function (data) {
                },
                error: function (msg) {
                }
            });
        });
    </script>
</asp:Content>
