using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BLS.JSON;
using BLS.JSON.Linq;

namespace BLS.Search.Web.Hosts
{
    public class GoogleSearch
    {
        public static List<WebSearchResult> Search(string search_expression, 
      Dictionary<string, object> stats_dict)
    {
      var url_template = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&rsz=large&safe=active&q={0}&start={1}";
      Uri search_url;
      var results_list = new List<WebSearchResult>();
      int[] offsets = { 0, 8, 16, 24, 32, 40, 48 };
      foreach (var offset in offsets)
      {
        search_url = new Uri(string.Format(url_template, search_expression, offset));
 
        var page = new WebClient().DownloadString(search_url);
 
        JObject o = (JObject)JsonConvert.DeserializeObject(page);
 
        var results_query =
          from result in o["responseData"]["results"].Children()
          select new WebSearchResult(
              url: result.Value<string>("url").ToString(),
              title: result.Value<string>("title").ToString(),
              content: result.Value<string>("content").ToString(),
              engine: WebSearchResult.FindingEngine.google
              );
 
        foreach (var result in results_query)
          results_list.Add(result);
      }
 
      return results_list;
    }
    }
}
