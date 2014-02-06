using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using ApertureCMS.Models;
using System.Web.Routing;

namespace ApertureCMS.Admin
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name);
        }

        public static Byte[] ToByteArray(this HttpPostedFileBase value)
        {
            if (value == null)
                return null;

            var array = new Byte[value.ContentLength];
            value.InputStream.Position = 0;
            value.InputStream.Read(array, 0, value.ContentLength);
            return array;

        }

        #region Helpers

        static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

                if (memberExp != null)
                    return true;
            }

            return false;
        }

        static bool IsConversion(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
        }

        /// <summary>
        /// Get SelectList for Html Dropdownlistfor helpler method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultText">Text for first entry. if empty no default</param>
        /// <returns></returns>
        public static List<SelectListItem> GetDropDownList<T>(string defaultText = "") where T : class, ApertureCMS.Models.ILookup
        {
            using (ApertureDataContext db = new ApertureDataContext())
            {
                var dbSet = db.Set<T>();
                var list = new List<SelectListItem>();
                if (!String.IsNullOrEmpty(defaultText))
                    list.Add(new SelectListItem() { Text = defaultText, Value = "" });

                list.AddRange(dbSet.ToList().Select((item, index) => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name

                }).ToList());
                return list;

            }
        }

       
            public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
            {
                return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
            }
            public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
            {
                ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
                string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
                if (String.IsNullOrEmpty(labelText))
                {
                    return MvcHtmlString.Empty;
                }

                TagBuilder tag = new TagBuilder("label");
                tag.MergeAttributes(htmlAttributes);
                tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

                TagBuilder span = new TagBuilder("span");
                span.SetInnerText(labelText);

                // assign <span> to <label> inner html
                tag.InnerHtml = span.ToString(TagRenderMode.Normal);

                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
        

        public static List<SelectListItem> GetPublishStatusDropDownList()
        {
            return new List<SelectListItem>(){
                new SelectListItem(){ Text = "Draft", Value="1"},
                new SelectListItem(){ Text = "Published", Value="2"}};
        }
        #endregion
    }
}