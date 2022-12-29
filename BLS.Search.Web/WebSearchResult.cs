using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLS.Search.Web
{
    public class WebSearchResult
    {
        public string url;
        public string title;
        public string content;
        public FindingEngine engine;

        public enum FindingEngine { google, bing, google_and_bing };

        public WebSearchResult(string url, string title, string content, FindingEngine engine)
        {
            this.url = url;
            this.title = title;
            this.content = content;
            this.engine = engine;
        }
    }
}
