#pragma checksum "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9fbfe178816f01f28f63d609ffaab2d71f6d3eaa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Templates_Template7), @"mvc.1.0.view", @"/Views/Dashboard/Templates/Template7.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\_ViewImports.cshtml"
using Manufacturing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\_ViewImports.cshtml"
using Manufacturing.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fbfe178816f01f28f63d609ffaab2d71f6d3eaa", @"/Views/Dashboard/Templates/Template7.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0caa5658a95cf9ca43215e109680ea880249d20", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_Templates_Template7 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
  
    ViewBag.E1Height = "220px";
    ViewBag.DHeight = "250px";
    Layout = "~/Views/Shared/Material/_Layouts.cshtml";
    ViewData["Title"] = ViewData["dashboard-name"];

#line default
#line hidden
#nullable disable
            DefineSection("Styles", async() => {
                WriteLiteral(@"
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/Dropify/0.2.2/css/dropify.min.css"">
    <style>
        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            padding-left: 15px;
            padding-right: 0px;
        }

        .white-box {
            padding: 15px;
            margin-bottom: 15px;
        }
    </style>
");
            }
            );
            WriteLiteral("\r\n<section id=\"content\">\r\n    <div class=\"container\">\r\n        <div class=\"card-header\">\r\n            <h1 class=\" card-title\">\r\n                ");
#nullable restore
#line 26 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
           Write(ViewData["dashboard-name"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h1>\r\n        </div>\r\n        <div class=\"card card-padding\">\r\n            <div class=\"row\">\r\n                <div class=\"col-lg-12 place p-r-5\" style=\"margin-top:20px\" id=\"3\">\r\n                    <div class=\"white-box\">\r\n");
#nullable restore
#line 33 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
                           string t3 = ViewData["Element3"].ToString(); Html.RenderPartial(t3); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-lg-4 place\" id=\"1\">\r\n                    <div class=\"white-box\" style=\"height:300px;\">\r\n");
#nullable restore
#line 40 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
                           string t1 = ViewData["Element1"].ToString(); Html.RenderPartial(t1); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n                <div class=\"col-lg-4 place\" id=\"2\">\r\n                    <div class=\"white-box\" style=\"height:300px;\">\r\n");
#nullable restore
#line 45 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
                           string t2 = ViewData["Element2"].ToString(); Html.RenderPartial(t2); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n                <div class=\"col-lg-4 place p-r-5\" id=\"4\">\r\n                    <div class=\"white-box\" style=\"height:300px;\">\r\n");
#nullable restore
#line 50 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
                           string t4 = ViewData["Element4"].ToString(); Html.RenderPartial(t4); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/Dropify/0.2.2/js/dropify.min.js""></script>
    <script>

        $('.place').click(function(event) {
            var place = $(this).attr('id');
            localStorage.setItem(""placement"", place);
            window.location = localStorage.getItem(""thisAddress"")+""/Dashboard/ElementList/""+'");
#nullable restore
#line 66 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\Dashboard\Templates\Template7.cshtml"
                                                                                        Write(ViewData["dashboardId"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
        });
        /*
        function EnableDisableEditing() {

            if($(""#editing-button"").text() == ""Enable Editing""){
                localStorage.setItem(""dashboard-editing"", 1);
                $(""#editing-button"").text(""Done Editing"");
                $(""#editing-message"").text(""* Dashboard Editing is Enabled, Click on Element to Update."");
            }
            else{
                localStorage.setItem(""dashboard-editing"", 0);
                $(""#editing-button"").text(""Enable Editing"");
                $(""#editing-message"").text(""* Enable Editing to Update Dashboard Elements"");
            }
        }

        function DeleteDashboard(){

            $.ajax(
            {
                type: ""POST"",
                url: 'Url.Action(""DeleteDashboard"", ""Dashboard"")',
                data: {
                    dashboardId: 'ViewData[""dashboardId""]'
                },
                error: function (result) {
                    alert(result);
             ");
                WriteLiteral(@"   },
                success: function (result) {

                    if (result.status == true) {

                        swal( ""Done!"", result.message, ""success"");
                        window.location = result.default_dashboard;
                    }
                    else {
                        swal(""Problem!"", result.message, ""warning"");
                    }
                }
            });
        }
        */
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
