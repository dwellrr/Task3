namespace Task3.Helpers
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;

    public static class ButtonHelper
    {
        public static IHtmlContent TwoByTwoButton(this IHtmlHelper helper, string text1, string text2, string text3, string text4, string cssClass, int dataid)
        {

            var buttonHtml = $@"
            <div class='{cssClass}'; dataid='{dataid}'; style='display: grid; grid-template-rows: repeat(2, 1fr); grid-template-columns: repeat(2, 1fr); gap: 5px; border: 5px solid #ddd;'; >
                <div style='grid-row: 1; grid-column: 1; border: none'>
                    <input style='border: none' type='text' value='{text1}' readonly />
                </div>
                <div style='grid-row: 1; grid-column: 2; border: none'>
                    <input type='text' class='call-status' value='{text2}' readonly />
                </div>
                <div style='grid-row: 2; grid-column: 1;'>
                    <input style='border: none' type='text' value='{text3}' readonly />
                </div>
                <div style='grid-row: 2; grid-column: 2;'>
                    <input type='text' class='participation-status' value='{text4}' readonly />
                </div>
            </div>";

            return new HtmlString(buttonHtml);
        }
    }

}


