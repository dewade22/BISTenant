#pragma checksum "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aae51a19b9c47f3793973fd70294257e06f20cc9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DashboardDetail_stock), @"mvc.1.0.view", @"/Views/DashboardDetail/stock.cshtml")]
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
#nullable restore
#line 3 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aae51a19b9c47f3793973fd70294257e06f20cc9", @"/Views/DashboardDetail/stock.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0caa5658a95cf9ca43215e109680ea880249d20", @"/Views/_ViewImports.cshtml")]
    public class Views_DashboardDetail_stock : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Manufacturing.Models.Items.RawItem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/sources/vendors/bootgrid/jquery.bootgrid.updated.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
  
    ViewData["Title"] = "Stok "+ViewBag.productGroup;
    Layout = "~/Views/Shared/Material/_Layouts.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<section id=\"content\">\r\n    <div class=\"container\">\r\n        <div class=\"c-header\">\r\n            <h1>");
#nullable restore
#line 11 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
           Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n        </div>\r\n        <div class=\"card\">\r\n            <div class=\"card-header\">\r\n                <h2>Tabel Stok ");
#nullable restore
#line 15 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                          Write(ViewBag.productGroup);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
            </div>
            <div class=""card-body card-padding"">
                <table id=""tabelAlkohol"" class=""table table-vmiddle"" style=""table-layout:auto"">
                    <thead>
                        <tr>
                            <th data-column-id=""id"">ID</th>
                            <th data-column-id=""description"">Item Name</th>
                            <th data-column-id=""quantity"" data-formatter=""money"">Quantity</th>
                            <th data-column-id=""unit"">Unit</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 28 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                         foreach (var item in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td>");
#nullable restore
#line 31 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                               Write(item.ItemNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 32 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                               Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 33 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                               Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 34 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                               Write(item.BaseUnitofMeasure);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n");
#nullable restore
#line 36 "D:\Balimoon\Manufacturing_1_1_0_0\Manufacturing\Views\DashboardDetail\stock.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aae51a19b9c47f3793973fd70294257e06f20cc97745", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script>
        $('#tabelAlkohol').bootgrid({
            caseSensitive: false,
            formatters: {
                money: function (column, row) {
                    value = row[column.id].toString();
                    let val = parseFloat(value).toFixed(2);
                    value = val.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, ""$1,"");
                    console.log(val)
                    return value;
                }
            }
        })
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Manufacturing.Models.Items.RawItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591
