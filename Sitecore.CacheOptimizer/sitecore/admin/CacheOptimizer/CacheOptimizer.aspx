<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheOptimizer.aspx.cs" Inherits="Sitecore.CacheOptimizer.sitecore.admin.CacheOptimizer.CacheOptimizer" %>

<%@ Register Src="~/sitecore/admin/CacheOptimizer/Controls/SubLayoutCache.ascx" TagPrefix="uc1" TagName="SubLayoutCache" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cache Optimizer</title>
    
    <link href="/sitecore/admin/CacheOptimizer/Design/Css/cacheoptimizer.css" rel="stylesheet"/>
    <link href="/sitecore/admin/CacheOptimizer/Design/Libs/jquery-ui-1.11.1.custom/jquery-ui.min.css" rel="stylesheet"/>
    <link href="/sitecore/admin/CacheOptimizer/Design/Libs/jquery-ui-1.11.1.custom/jquery-ui.theme.min.css" rel="stylesheet"/>
    <script src="/sitecore/admin/CacheOptimizer/Design/Libs/jquery-2.1.1.min.js"></script>
    <script src="/sitecore/admin/CacheOptimizer/Design/Libs/jquery-ui-1.11.1.custom/jquery-ui.min.js"></script>
    <script src="/sitecore/admin/CacheOptimizer/Design/Js/cacheOptimizer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <div id="tabs">
         <!-- <ul>
            <li><a href="#tabs-1">Layouts</a></li>
            <li><a href="#tabs-2">Sitecore Caching</a></li>
          </ul>-->
          <div id="tabs-1">
            <uc1:SubLayoutCache runat="server" ID="SubLayoutCache" />
          </div>
          <!--<div id="tabs-2">
            <p>Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p>
          </div>-->
        </div>

    </div>
    </form>
</body>
</html>
