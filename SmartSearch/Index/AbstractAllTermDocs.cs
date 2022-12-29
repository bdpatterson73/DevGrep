using System;
using SmartSearch.Index;

namespace SmartSearch.Index
{
    /// <summary>
    /// Base class for enumerating all but deleted docs.
    /// 
    /// <p/>NOTE: this class is meant only to be used internally
    /// by Lucene; it's only public so it can be shared across
    /// packages.  This means the API is freely subject to
    /// change, and, the class could be removed entirely, in any
    /// Lucene release.  Use directly at your own risk! */
    /// </summary>
    public abstract class AbstractAllTermDocs : TermDocs
    {
        protected int maxDoc;
        protected int internalDoc = -1;

        protected AbstractAllTermDocs(int maxDoc)
        {
            this.maxDoc = maxDoc;
        }

        public void Seek(Term term)
        {
            if (term == null)
            {
                internalDoc = -1;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Seek(TermEnum termEnum)
        {
            throw new NotSupportedException();
        }

        public int Doc
        {
            get { return internalDoc; }
        }

        public int Freq
        {
            get { return 1; }
        }

        public bool Next()
        {
            return SkipTo(internalDoc + 1);
        }

        public int Read(int[] docs, int[] freqs)
        {
            int length = docs.Length;
            int i = 0;
            while (i < length && internalDoc < maxDoc)
            {
                if (!IsDeleted(internalDoc))
                {
                    docs[i] = internalDoc;
                    freqs[i] = 1;
                    ++i;
                }
                internalDoc++;
            }
            return i;
        }

        public bool SkipTo(int target)
        {
            internalDoc = target;
            while (internalDoc < maxDoc)
            {
                if (!IsDeleted(internalDoc))
                {
                    return true;
                }
                internalDoc++;
            }
            return false;
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected abstract void Dispose(bool disposing);

        public abstract bool IsDeleted(int doc);
    }
}
