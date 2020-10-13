#pragma checksum "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5ad1819f6c7f1ae5dbe5293d191b2e2b45310c81"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\Isabe\CAProject\CAProject\Views\_ViewImports.cshtml"
using CAProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Isabe\CAProject\CAProject\Views\_ViewImports.cshtml"
using CAProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ad1819f6c7f1ae5dbe5293d191b2e2b45310c81", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"45a5e57a77a3e05fffddf6820fac83f035f5d91d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Home.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Home_AddToCart.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    PaginatedList<Product> paginatedList = (PaginatedList<Product>)ViewData["paginatedList"];
    var prevDisabled = !paginatedList.HasPreviousPage ? "disabled" : "";
    var nextDisabled = !paginatedList.HasNextPage ? "disabled" : "";

    List<Cart> cart = (List<Cart>)ViewData["Cart"];

    Dictionary<int, int> paginatedStockCount = (Dictionary<int, int>)ViewData["paginatedStockCount"];

    string changedCart = (string)ViewData["changedCart"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "5ad1819f6c7f1ae5dbe5293d191b2e2b45310c815509", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n\r\n<div style=\"font-family:\'Lucida Sans\', \'Lucida Sans Regular\', \'Lucida Grande\', \'Lucida Sans Unicode\', Geneva, Verdana, sans-serif\">\r\n    <h2>PRODUCTS</h2>\r\n    <hr />\r\n</div>\r\n\r\n<div>\r\n");
#nullable restore
#line 97 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
     if (changedCart == "true")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <script>\r\n            if (sessionStorage.getItem(\"alerted\") == null) {\r\n                alert(\"Removed some items from your cart beacuse its sold out\");\r\n                sessionStorage.setItem(\"alerted\", \"yes\");\r\n            }\r\n        </script>\r\n");
#nullable restore
#line 105 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<div>\r\n");
#nullable restore
#line 109 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
     if (changedCart == "true")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <script>\r\n            if (sessionStorage.getItem(\"alerted\") == null) {\r\n                alert(\"Removed some items from your cart beacuse its sold out\");\r\n                sessionStorage.setItem(\"alerted\", \"yes\");\r\n            }\r\n        </script>\r\n");
#nullable restore
#line 117 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<div class=\"product-container\">\r\n");
#nullable restore
#line 121 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
     foreach (Product product in paginatedList)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"product-item\">\r\n            <div>\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 3624, "\"", 3696, 1);
#nullable restore
#line 125 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
WriteAttributeValue("", 3631, Url.Action("Index", "ProductPg", new { productid = product.Id }), 3631, 65, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <img");
            BeginWriteAttribute("src", " src=", 3724, "", 3743, 1);
#nullable restore
#line 126 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
WriteAttributeValue("", 3729, product.Image, 3729, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=", 3743, "", 3761, 1);
#nullable restore
#line 126 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
WriteAttributeValue("", 3748, product.Name, 3748, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                </a>\r\n            </div>\r\n            <div>\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 3845, "\"", 3917, 1);
#nullable restore
#line 130 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
WriteAttributeValue("", 3852, Url.Action("Index", "ProductPg", new { productid = product.Id }), 3852, 65, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <h5>");
#nullable restore
#line 131 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
                   Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                </a>\r\n            </div>\r\n            <div>\r\n                <p>");
#nullable restore
#line 135 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
              Write(product.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            </div>\r\n            <div>\r\n");
#nullable restore
#line 138 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
                 if (paginatedStockCount[product.Id] > 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button class=\"btn btn-primary add-to-cart\"");
            BeginWriteAttribute("product_id", " product_id=", 4251, "", 4274, 1);
#nullable restore
#line 140 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
WriteAttributeValue("", 4263, product.Id, 4263, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span class=\"mr-1\">$");
#nullable restore
#line 140 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
                                                                                                      Write(product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>Add To Cart</button>\r\n");
#nullable restore
#line 141 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button");
            BeginWriteAttribute("value", " value=", 4425, "", 4443, 1);
#nullable restore
#line 144 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
WriteAttributeValue("", 4432, product.Id, 4432, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\" disabled>\r\n                        Out Of Stock\r\n                    </button>\r\n");
#nullable restore
#line 147 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n");
#nullable restore
#line 150 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ad1819f6c7f1ae5dbe5293d191b2e2b45310c8112977", async() => {
                WriteLiteral("\r\n    Previous\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 154 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
              WriteLiteral(paginatedList.PageIndex - 1);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4710, "btn", 4710, 3, true);
            AddHtmlAttributeValue(" ", 4713, "btn-default", 4714, 12, true);
#nullable restore
#line 155 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
AddHtmlAttributeValue(" ", 4725, prevDisabled, 4726, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ad1819f6c7f1ae5dbe5293d191b2e2b45310c8115741", async() => {
                WriteLiteral("\r\n    Next\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 160 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
              WriteLiteral(paginatedList.PageIndex + 1);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4856, "btn", 4856, 3, true);
            AddHtmlAttributeValue(" ", 4859, "btn-default", 4860, 12, true);
#nullable restore
#line 161 "C:\Users\Isabe\CAProject\CAProject\Views\Home\Index.cshtml"
AddHtmlAttributeValue(" ", 4871, nextDisabled, 4872, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ad1819f6c7f1ae5dbe5293d191b2e2b45310c8118599", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
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
