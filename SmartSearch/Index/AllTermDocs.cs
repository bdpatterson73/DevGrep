using SmartSearch.Index;
using BitVector = SmartSearch.Util.BitVector;

namespace SmartSearch.Index
{

    class AllTermDocs : AbstractAllTermDocs
	{
		protected internal BitVector deletedDocs;
				
		protected internal AllTermDocs(SegmentReader parent) : base(parent.MaxDoc)
		{
			lock (parent)
			{
				this.deletedDocs = parent.deletedDocs;
			}
		}

        protected override void Dispose(bool disposing)
        {
            // Do nothing.
        }

        public override bool IsDeleted(int doc)
        {
            return deletedDocs != null && deletedDocs.Get(doc);
        }
	}
}