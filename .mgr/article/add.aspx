<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="mgr_article_add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>article-add</title>
    <link href="../static/style/c.css" rel="stylesheet" />

    <script src="../static/script/global.js"></script>

    <!--treeview-->
    <link rel="stylesheet" href="../static/script/jquery.treeview/jquery.treeview.css" />
    <script src="../static/script/jquery.treeview/lib/jquery.js" type="text/javascript"></script>
    <script src="../static/script/jquery.treeview/lib/jquery.cookie.js" type="text/javascript"></script>
    <script src="../static/script/jquery.treeview/jquery.treeview.js" type="text/javascript"></script>
    <style type="text/css">
        .file {
            cursor: pointer;
        }
    </style>

    <script>
        function SelectThis(text, id) {
            document.getElementById("categoryID").value = id;
            document.getElementById("categoryText").innerHTML = text;
        }


        $(document).ready(function () {
            $("#browser").treeview();
        });

    </script>
    <!--/treeview-->


    <script src="../script/ckeditor/ckeditor.js" type="text/javascript"></script>

</head>
<body>
    <form id="mainForm" runat="server">
        <div id="main">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td valign="top" width="200">
                        <!--#include file="../menu.html"-->
                    </td>

                    <td valign="top">
                        <div class="box">




                            <form action="?action=save" method="post" name="mainform">
                                <table border="0" cellpadding="0" cellspacing="0" class="table" width="100%">
                                    <tr>
                                        <th colspan="2">添加文章
                                        </th>

                                    </tr>

                                    <tr>
                                        <td width="96">标题</td>
                                        <td>
                                            <input type="text" class="textInput" name="Title" size="96" maxlength="200" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>副标题</td>
                                        <td>
                                            <input type="text" class="textInput" name="Subtitle" size="96" maxlength="200" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">描述</td>
                                        <td>
                                            <textarea rows="5" cols="96" name="Description"></textarea>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>系列</td>
                                        <td>
                                            <input type="text" class="textInput" name="SeriesName" size="96" maxlength="50" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>关键字</td>
                                        <td>
                                            <input type="text" name="Keyword" size="10" class="textInput" maxlength="50" />
                                            <input type="text" name="Keyword" size="10" class="textInput" maxlength="50" />
                                            <input type="text" name="Keyword" size="10" class="textInput" maxlength="50" />
                                            <input type="text" name="Keyword" size="10" class="textInput" maxlength="50" />
                                            <input type="text" name="Keyword" size="10" class="textInput" maxlength="50" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>封面图</td>
                                        <td>
                                            <input type="text" class="textInput" name="Cover" size="96" />
                                            <iframe src="../upload/SingleUpload.aspx?action=original&callback=window.parent.uploadCover($data)" width="360" height="36" scrolling="no" frameborder="0"></iframe>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>类别
                                        </td>
                                        <td>

                                            <input type="hidden" id="categoryID" name="CatalogID" value="0" />

                                            <div class="select" onmouseover="this.getElementsByTagName('ul')[0].style.display='block'" onmouseout="this.getElementsByTagName('ul')[0].style.display='none'">
                                                <span id="categoryText"></span>
                                                <ul id="browser" class="filetree" style="padding-right: 20px">
                                                    <%=Mgr.Services.ArticlesCatalogsService.GetCategoryList() %>
                                                </ul>
                                            </div>
                                        </td>

                                    </tr>
                                </table>
                            </form>


                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
