using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MyBlog.Core
{
    public class RssViewResult : ActionResult
    {
        private SyndicationFeed RssFeed { get; set; }
        public RssViewResult(SyndicationFeed feed)
        {
            RssFeed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/rss+xml";
            Rss20FeedFormatter rss = new Rss20FeedFormatter(RssFeed);
            using (XmlWriter xml = XmlWriter.Create(response.Output))
            {

                rss.WriteTo(xml);
            }

        }




    }
}