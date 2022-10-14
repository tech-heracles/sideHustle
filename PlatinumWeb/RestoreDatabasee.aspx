<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestoreDatabase.aspx.cs" Inherits="PlatinumWeb.RestoreDatabase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restore Database</title>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/css/materialize.min.css" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Zenh87qX5JnK2Jl0vWa8Ck2rdkQ2Bzep5IDxbcnCeuOxjzrPF/et3URy9Bv1WTRi" crossorigin="anonymous" />

</head>
<body>
    <form id="form1" runat="server">
        <nav>
            <div class="nav-wrapper">
              <ul class="left hide-on-med-and-down">
                <li><a onclick="NdryshoDatbazen()">Kthe databazen ne gjendjen e zgjedhur</a></li>
              </ul>
            </div>
        </nav>
        <div>
            <asp:DropDownList runat="server" ID="select" AppendDataBoundItems="true" CssClass="form-select">
                <asp:ListItem Text="Zgjidhni versionin" Value="" />
            </asp:DropDownList>
        </div>
    </form>
</body>
<script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/js/materialize.min.js">
</script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.1/jquery.min.js" integrity="sha512-aVKKRRi/Q/YV+4mjoKBsE4x3H+BkegoM/em46NNlCqNTmUYADjBbeNefNxYV7giUp0VxICtqdrbqU7iVaeZNXA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script>
    function NdryshoDatbazen() {
        $.ajax({
            type: "POST",
            url: Utils.getServerApiUrl("Rregjistrime", "restoreDatabase"),
            data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (response) {
            if (response != null) {
                console.log("success");
                console.log(response);

            } else {
                console.log("Something went wrong");
            }

        }).fail(function (response) {
            console.log(response.responseText);
        });
    }

</script>
</html>
