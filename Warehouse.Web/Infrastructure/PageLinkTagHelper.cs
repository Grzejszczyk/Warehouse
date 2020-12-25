using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Pagination;

namespace Warehouse.Web.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            TagBuilder navTag = new TagBuilder("nav");
            navTag.Attributes["aria-label"] = "Page navigation";
            result.InnerHtml.AppendHtml(navTag);

            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            navTag.InnerHtml.AppendHtml(ulTag);

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder liTag = new TagBuilder("li");
                liTag.AddCssClass("page-item");
                ulTag.InnerHtml.AppendHtml(liTag);

                TagBuilder aTag = new TagBuilder("a");
                aTag.AddCssClass("page-link");
                aTag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNo = i, pageSize = PageModel.ItemsPerPage });
                aTag.InnerHtml.Append(i.ToString());
                liTag.InnerHtml.AppendHtml(aTag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
