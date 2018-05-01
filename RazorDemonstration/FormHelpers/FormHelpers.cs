using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorDemonstration.FormHelpers
{
    public static class FormHelpers
    {
        public static HtmlString LabelWithTextBox(string id, string labelText, string inputValue, bool? isReadOnly, string formCss)
        {
            var html = new StringBuilder();
            html.Append($"<div class='form-group {formCss}'>");
            html.Append($"<label for='{id}'>{labelText}</label>");
            html.Append($"<input class='form-control' type='text' name='{id}' id='{id}' value='{inputValue}' ");
            
            if (isReadOnly.HasValue && isReadOnly.Value)
            {
                html.Append("readonly");
            }

            html.Append("/></div>");


            return new HtmlString(html.ToString());
        }
    }
}