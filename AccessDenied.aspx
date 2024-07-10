<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="RRD.GRESAdmin.Account.AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Access Denied</title>
    <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet">
    <style>
        body {
            font-family: 'Open Sans', sans-serif;
        }

        .error-page {
            padding: 50px;
            margin: 0 auto;
        }

        h1 {
            color: #C47C66;
            font-size: 30px!important;
            margin-top: 20px!important;
        }

        h2 {
            color: #C47C66;
            font-size: 22px!important;
        }

        .more-info {
            background-color: lightyellow;
            border: 1px solid #dadaac;
            padding: 15px;
            color: #8C8A85;
            margin-top: 15px;
            font-size: 11px!important;
        }

        .hidden {
            display: none;
        }
    </style>
</head>
<body>
    <div class="error-page">
        <img src="Content/images/icons/ico-padlock-red-128.png" />
        
        <h1>403 - Access Denied</h1>
        <h2>
           You do not have access to the requested page.
        </h2>        
    </div>
</body>
</html>
