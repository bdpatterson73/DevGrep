using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BLS.JSON;
using BLS.JSON.Linq;

namespace BLS.Search.Web.Hosts
{
    /// <summary>
    /// Bings API is a fucking joke... Don't even bother trying to use this.
    /// </summary>
    public class BingSearch
    {

         public static List<WebSearchResult> Search(string search_expression, Dictionary<string, object> stats_dict)
    {
        var url_template = "http://api.search.live.net/json.aspx?AppId=cuCKJLekT3rqVYLpyH8SMfT72q4Prk4gjV9A2pHWQa8=&Market=en-US&Sources=Web&Adult=Strict&Query={0}&Web.Count=50";
      var offset_template = "&Web.Offset={1}";
      var results_list = new List<WebSearchResult>();
      Uri search_url;
      int[] offsets = { 0, 50, 100, 150 };
      foreach (var offset in offsets)
      {
        if (offset == 0)
          search_url = new Uri(string.Format(url_template, search_expression));
        else
          search_url = new Uri(string.Format(url_template + offset_template, search_expression, offset));
 
        var page = new WebClient().DownloadString(search_url);
 
        JObject o = (JObject)JsonConvert.DeserializeObject(page);
 
        var results_query =
          from result in o["SearchResponse"]["Web"]["Results"].Children()
          select new WebSearchResult
              (
              url: result.Value<string>("Url").ToString(),
              title: result.Value<string>("Title").ToString(),
              content: result.Value<string>("Description").ToString(),
              engine: WebSearchResult.FindingEngine.bing
              );
 
        foreach (var result in results_query)
          results_list.Add(result);
      }
 
      return results_list;
    }

    }
}
