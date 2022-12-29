using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSearch.Analysis;
using SmartSearch.Analysis.Standard;
using SmartSearch.Documents;
using SmartSearch.Index;
using SmartSearch.QueryParsers;
using SmartSearch.Search;
using SmartSearch.Store;

namespace DevGrep.Classes.IndexedFiles
{
    internal class IndexSearchHelper
    {
        private string _searchPath;
        private string _searchText;
        private string _extensionList;
        private bool _includeSubdirectories;
        private string _indexLocation;
        private IndexSearchResultList _resultList;
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexSearchHelper" /> class.
        /// </summary>
        /// <param name="searchPath">The search path.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="extensionList">The extension list.</param>
        /// <param name="includeSubdirectory">if set to <c>true</c> [include subdirectory].</param>
        internal IndexSearchHelper(string indexLocation, string searchPath, string searchText, string extensionList, bool includeSubdirectory)
        {
            _indexLocation = indexLocation;
            _searchPath = searchPath;
            _searchText = searchText;
            _extensionList = extensionList;
            _includeSubdirectories = includeSubdirectory;
        }

        internal IndexSearchResultList ResultList
        {
            get { return _resultList; }
        }

        /// <summary>
        /// Starts the search.
        /// </summary>
        internal void StartSearch()
        {
            _resultList = new IndexSearchResultList();
            String field = "contents";
            String queries = null;
            int repeat = 0;
            bool raw = true;
            String normsField = null;
            bool paging = true;
            int hitsPerPage = 10;

            IndexReader indexReader = null;
            try
            {
                // only searching, so read-only=true
                indexReader = IndexReader.Open(FSDirectory.Open(new System.IO.DirectoryInfo(_indexLocation)), true); // only searching, so read-only=true

                if (normsField != null)
                    indexReader = new OneNormsReader(indexReader, normsField);

                Searcher searcher = new IndexSearcher(indexReader);
                Analyzer analyzer = new StandardAnalyzer(SmartSearch.Util.Version.SmartSearch_30);

                StreamReader queryReader;
                if (queries != null)
                {
                    queryReader = new StreamReader(new StreamReader(queries, Encoding.Default).BaseStream, new StreamReader(queries, Encoding.Default).CurrentEncoding);
                }
                else
                {
                    queryReader = new StreamReader(new StreamReader(Console.OpenStandardInput(), Encoding.UTF8).BaseStream, new StreamReader(Console.OpenStandardInput(), Encoding.UTF8).CurrentEncoding);
                }

                var parser = new QueryParser(SmartSearch.Util.Version.SmartSearch_30, field, analyzer);


                Query query = parser.Parse(_searchText);
                    Console.Out.WriteLine("Searching for: " + query.ToString(field));

                    if (repeat > 0)
                    {
                        // repeat & time as benchmark
                        DateTime start = DateTime.Now;
                        for (int i = 0; i < repeat; i++)
                        {
                            searcher.Search(query, null, 50000);//TODO This number is a TOP number.
                        }
                        DateTime end = DateTime.Now;
                        Console.Out.WriteLine("Time: " + (end.Millisecond - start.Millisecond) + "ms");
                    }

                    if (paging)
                    {
                        DoPagingSearch(queryReader, searcher, query, raw, false);
                    }
                    //else
                    //{
                    //    DoStreamingSearch(searcher, query);
                    //}
                
                queryReader.Close();
            }
            finally
            {
                if (indexReader != null)
                {
                    indexReader.Dispose();
                }
            }
        }

        private void DoPagingSearch(StreamReader input, Searcher searcher, Query query, bool raw, bool interactive)
        {
            int hitsPerPage = 50000; //TODO Look in to this.

            // Collect enough docs to show 5 pages
            var collector = TopScoreDocCollector.Create(5 * hitsPerPage, false);
            searcher.Search(query, collector);
            var hits = collector.TopDocs().ScoreDocs;

            int numTotalHits = collector.TotalHits;
            Console.Out.WriteLine(numTotalHits + " total matching documents");

            int start = 0;
            int end = Math.Min(numTotalHits, hitsPerPage);

            while (true)
            {
                if (end > hits.Length)
                {
                    collector = TopScoreDocCollector.Create(numTotalHits, false);
                    searcher.Search(query, collector);
                    hits = collector.TopDocs().ScoreDocs;
                }

                end = Math.Min(hits.Length, start + hitsPerPage);

                for (int i = start; i < end; i++)
                {
                    if (raw)
                    {
                        IndexSearchResult isr = new IndexSearchResult(hits[i].Doc, hits[i].Score,
                                                                      searcher.Doc(hits[i].Doc).Get("path"));
                        _resultList.Add(isr);
                        //Console.Out.WriteLine("doc=" + hits[i].Doc + " score=" + hits[i].Score + " Path=" + searcher.Doc(hits[i].Doc).Get("path"));
                        continue;
                    }

                    Document doc = searcher.Doc(hits[i].Doc);
                    String path = doc.Get("path");
                    if (path != null)
                    {
                        Console.Out.WriteLine((i + 1) + ". " + path);
                        String title = doc.Get("title");
                        if (title != null)
                        {
                            Console.Out.WriteLine("   Title: " + doc.Get("title"));
                        }
                    }
                    else
                    {
                        Console.Out.WriteLine((i + 1) + ". " + "No path for this document");
                    }
                }

                if (!interactive)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Use the norms from one field for all fields.  Norms are read into memory,
        /// using a byte of memory per document per searched field.  This can cause
        /// search of large collections with a large number of fields to run out of
        /// memory.  If all of the fields contain only a single token, then the norms
        /// are all identical, then single norm vector may be shared. 
        /// </summary>
        private class OneNormsReader : FilterIndexReader
        {
            private readonly String field;

            public OneNormsReader(IndexReader in_Renamed, String field)
                : base(in_Renamed)
            {
                this.field = field;
            }

            public override byte[] Norms(String field)
            {
                return in_Renamed.Norms(this.field);
            }
        }
    }
}
