using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ITPPro.Extensions
{
    public static class HtmlExtensionscs
    {
        public static MvcHtmlString SuccessMessage(this HtmlHelper helper)
        {
            string message = (string)helper.ViewContext.Controller.TempData[Constants.SuccessMessageKey];
            return ModalWindow(helper, message, "success");
        }

        public static MvcHtmlString ErrorMessage(this HtmlHelper helper)
        {
            string message = (string)helper.ViewContext.Controller.TempData[Constants.ErrorMessageKey];
            return ModalWindow(helper, message, "error");
        }

        private static MvcHtmlString ModalWindow(HtmlHelper helper, string message, string htmlContentClass)
        {
            if (!string.IsNullOrEmpty(message))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<div class=\"modal\" id=\"error-modal\">");
                builder.AppendFormat("<div class=\"modal-content {0}\">", htmlContentClass);
                builder.Append("<span class=\"close\">&times;</span>");
                builder.AppendFormat("<h3>{0}</h3>", message);
                builder.Append("</div>");
                builder.Append("</div>");

                string script = System.Web.Optimization.Scripts.Render("~/bundles/custom").ToHtmlString();
                builder.Append(script);

                return MvcHtmlString.Create(builder.ToString());
            }
            return MvcHtmlString.Create("");
        }

        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, bool disabled = false)
        {
            if (expression.Body.Type != typeof(DateTime))
                return MvcHtmlString.Create("");

            var exprBody = (MemberExpression)expression.Body;
            string propertyName = exprBody.Member.Name;
            string propertyId = propertyName + "-id";

            string disabledString = disabled ? "readonly = \"readonly\"" : string.Empty;

            var valueFunc = expression.Compile();
            var parsedValue = valueFunc(html.ViewData.Model);
            DateTime? parsedDateTime = parsedValue as DateTime?;

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<input type =\"text\" id=\"{0}\" class=\"form-control\" name=\"{1}\" {2}>",
                propertyId, propertyName, disabledString);
            builder.AppendFormat("<script>$('#{0}').datepicker();", propertyId);
            builder.AppendFormat("$('#{0}').datepicker('option', 'dateFormat', 'yy-mm-dd');", propertyId);
            if (parsedDateTime != null && parsedDateTime.Value.Ticks != 0)
            {
                builder.AppendFormat("$('#{0}').val('{1}');", propertyId, parsedDateTime.Value.ToShortDateString());
            }
            builder.Append("</script>");

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}