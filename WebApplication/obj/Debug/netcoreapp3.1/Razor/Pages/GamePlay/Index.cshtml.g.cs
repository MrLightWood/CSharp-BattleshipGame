#pragma checksum "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cacb021510d85f7866f40b3f7f46b1a7b5185af5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebApplication.Pages.GamePlay.Pages_GamePlay_Index), @"mvc.1.0.razor-page", @"/Pages/GamePlay/Index.cshtml")]
namespace WebApplication.Pages.GamePlay
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
#line 1 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\_ViewImports.cshtml"
using WebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\_ViewImports.cshtml"
using WebApplication.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
using Domain.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cacb021510d85f7866f40b3f7f46b1a7b5185af5", @"/Pages/GamePlay/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9f47ad64adc7b4d7ede40e6bf7914a2af9903622", @"/Pages/_ViewImports.cshtml")]
    public class Pages_GamePlay_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 35 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cacb021510d85f7866f40b3f7f46b1a7b5185af54076", async() => {
                WriteLiteral("\r\n    <title>BATTLESHIP</title>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cacb021510d85f7866f40b3f7f46b1a7b5185af55075", async() => {
                WriteLiteral("\r\n\r\n<div class=\"d-flex justify-content-between px-2\">\r\n    <span>Player ");
#nullable restore
#line 48 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
            Write(Model.BattleShipGame.PlayerA);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n    <span>Player ");
#nullable restore
#line 49 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
            Write(Model.BattleShipGame.PlayerB);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n</div>\r\n<div class=\"d-flex justify-content-between px-2\">\r\n    <table class=\"table table-striped table-dark\">\r\n");
#nullable restore
#line 53 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
         for (int y = 0; y < Model.BattleShipGame.GetLengthBoard(1); y++)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n");
#nullable restore
#line 56 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                 for (var x = 0; x < Model.BattleShipGame.GetLengthBoard(0); x++)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                     if(!Model.BattleShipGame.NextMoveByP1 && Model.BattleShipGame.PlayerBType == (int) EPlayerType.Human)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                         if (!Model.GameFinished)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <td>");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cacb021510d85f7866f40b3f7f46b1a7b5185af57282", async() => {
#nullable restore
#line 62 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                                                                                 Write(GetCellContent(x, y, 1));

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 62 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
AddHtmlAttributeValue("", 1751, GetCellColor(x, y, 1), 1751, 22, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 62 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                    WriteLiteral(Model.Game.GameId);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 62 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                                                     WriteLiteral(x);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["x"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-x", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["x"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 62 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                                                                      WriteLiteral(y);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["y"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-y", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["y"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("</td>\r\n");
#nullable restore
#line 63 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <td><span");
                BeginWriteAttribute("class", " class=\"", 1999, "\"", 2029, 1);
#nullable restore
#line 66 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
WriteAttributeValue("", 2007, GetCellColor(x, y, 1), 2007, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 66 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                Write(GetCellContent(x, y, 1));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></td>\r\n");
#nullable restore
#line 67 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                         
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <td><span");
                BeginWriteAttribute("class", " class=\"", 2201, "\"", 2231, 1);
#nullable restore
#line 71 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
WriteAttributeValue("", 2209, GetCellColor(x, y, 1), 2209, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 71 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                            Write(GetCellContent(x, y, 1));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></td>\r\n");
#nullable restore
#line 72 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </tr>\r\n");
#nullable restore
#line 75 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    </table>\r\n    \r\n    <table class=\"table table-striped table-light d-b\">\r\n");
#nullable restore
#line 80 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
         for (int y = 0; y < Model.BattleShipGame.GetLengthBoard(1); y++)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n");
#nullable restore
#line 83 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                 for (var x = 0; x < Model.BattleShipGame.GetLengthBoard(0); x++)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 85 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                     if(Model.BattleShipGame.NextMoveByP1 && Model.BattleShipGame.PlayerAType == (int) EPlayerType.Human)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 87 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                         if(!Model.GameFinished)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <td>");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cacb021510d85f7866f40b3f7f46b1a7b5185af515835", async() => {
#nullable restore
#line 89 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                                                                                 Write(GetCellContent(x, y, 2));

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 89 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
AddHtmlAttributeValue("", 2893, GetCellColor(x, y, 2), 2893, 22, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                    WriteLiteral(Model.Game.GameId);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                                                     WriteLiteral(x);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["x"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-x", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["x"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                                                                      WriteLiteral(y);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["y"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-y", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["y"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("</td>\r\n");
#nullable restore
#line 90 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <td><span");
                BeginWriteAttribute("class", " class=\"", 3141, "\"", 3171, 1);
#nullable restore
#line 93 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
WriteAttributeValue("", 3149, GetCellColor(x, y, 2), 3149, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 93 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                                Write(GetCellContent(x, y, 2));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></td>\r\n");
#nullable restore
#line 94 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                         
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <td><span");
                BeginWriteAttribute("class", " class=\"", 3343, "\"", 3373, 1);
#nullable restore
#line 98 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
WriteAttributeValue("", 3351, GetCellColor(x, y, 2), 3351, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 98 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                                            Write(GetCellContent(x, y, 2));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></td>\r\n");
#nullable restore
#line 99 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 99 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </tr>\r\n");
#nullable restore
#line 102 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    </table>\r\n</div>\r\n<div class=\"container\">\r\n");
#nullable restore
#line 112 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
     if(Model.GameFinished)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <div class=\"card text-white bg-success mb-3 text-center\">\r\n          <div class=\"card-header\">Game Has Been Finished</div>\r\n            <div class=\"card-body\">\r\n");
#nullable restore
#line 117 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                 if(!Model.BattleShipGame.NextMoveByP1)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <h5 class=\"card-title\">Player ");
#nullable restore
#line 119 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                             Write(Model.BattleShipGame.PlayerA);

#line default
#line hidden
#nullable disable
                WriteLiteral(" Has won this game</h5>\r\n");
#nullable restore
#line 120 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <h5 class=\"card-title\">Player ");
#nullable restore
#line 123 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                                             Write(Model.BattleShipGame.PlayerB);

#line default
#line hidden
#nullable disable
                WriteLiteral(" Has won this game</h5>\r\n");
#nullable restore
#line 124 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </div>\r\n        </div>\r\n");
#nullable restore
#line 127 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 6 "C:\Users\saile\RiderProjects\Demo01\WebApplication\Pages\GamePlay\Index.cshtml"
 
    string GetCellContent(int x, int y, int boardNum)
    {
        var res = @Model.BattleShipGame.GetCellValue(x, y, boardNum) switch
        {
            ECellState.Empty => "#",
            ECellState.Bomb => "X",
            ECellState.Ship => "#",
            ECellState.Shiphit => "@",
            _ => ""
            };
        return res;
    }
    
    string GetCellColor(int x, int y, int boardNum)
    {
        var res = @Model.BattleShipGame.GetCellValue(x, y, boardNum) switch
        {
            ECellState.Empty => "text-warning",
            ECellState.Bomb => "text-danger",
            ECellState.Ship => "text-warning",
            ECellState.Shiphit => "text-success",
            _ => ""
            };
        return res;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication.Pages.GamePlay.Index> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication.Pages.GamePlay.Index> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication.Pages.GamePlay.Index>)PageContext?.ViewData;
        public WebApplication.Pages.GamePlay.Index Model => ViewData.Model;
    }
}
#pragma warning restore 1591