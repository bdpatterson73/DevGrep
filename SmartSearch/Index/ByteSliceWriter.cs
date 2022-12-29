// ***********************************************************************
// Assembly         : SmartSearch
// Author           : Brian Patterson
// Created          : 12-01-2012
//
// Last Modified By : Brian Patterson
// Last Modified On : 12-01-2012
// ***********************************************************************
// <copyright file="ByteSliceWriter.cs" company="Borderline Software, Inc.">
//     Copyright (c) Borderline Software, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using SmartSearch.Support;

namespace SmartSearch.Index
{
    /// <summary>
    /// Class to write byte streams into slices of shared
    /// byte[].  This is used by DocumentsWriter to hold the
    /// posting list for many terms in RAM.
    /// </summary>
	public sealed class ByteSliceWriter
	{
        /// <summary>
        /// The slice
        /// </summary>
		private byte[] slice;
        /// <summary>
        /// The upto
        /// </summary>
		private int upto;
        /// <summary>
        /// The pool
        /// </summary>
		private readonly ByteBlockPool pool;

        /// <summary>
        /// The offset0
        /// </summary>
		internal int offset0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSliceWriter" /> class.
        /// </summary>
        /// <param name="pool">The pool.</param>
		public ByteSliceWriter(ByteBlockPool pool)
		{
			this.pool = pool;
		}

        /// <summary>
        /// Set up the writer to write at address.
        /// </summary>
        /// <param name="address">The address.</param>
		public void  Init(int address)
		{
			slice = pool.buffers[address >> DocumentsWriter.BYTE_BLOCK_SHIFT];
			System.Diagnostics.Debug.Assert(slice != null);
			upto = address & DocumentsWriter.BYTE_BLOCK_MASK;
			offset0 = address;
			System.Diagnostics.Debug.Assert(upto < slice.Length);
		}

        /// <summary>
        /// Write byte into byte slice stream
        /// </summary>
        /// <param name="b">The b.</param>
		public void  WriteByte(byte b)
		{
			System.Diagnostics.Debug.Assert(slice != null);
			if (slice[upto] != 0)
			{
				upto = pool.AllocSlice(slice, upto);
				slice = pool.buffer;
				offset0 = pool.byteOffset;
				System.Diagnostics.Debug.Assert(slice != null);
			}
			slice[upto++] = b;
			System.Diagnostics.Debug.Assert(upto != slice.Length);
		}

        /// <summary>
        /// Writes the bytes.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="len">The len.</param>
		public void  WriteBytes(byte[] b, int offset, int len)
		{
			int offsetEnd = offset + len;
			while (offset < offsetEnd)
			{
				if (slice[upto] != 0)
				{
					// End marker
					upto = pool.AllocSlice(slice, upto);
					slice = pool.buffer;
					offset0 = pool.byteOffset;
				}
				
				slice[upto++] = b[offset++];
				System.Diagnostics.Debug.Assert(upto != slice.Length);
			}
		}

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>The address.</value>
	    public int Address
	    {
	        get { return upto + (offset0 & DocumentsWriter.BYTE_BLOCK_NOT_MASK); }
	    }

        /// <summary>
        /// Writes the V int.
        /// </summary>
        /// <param name="i">The i.</param>
	    public void  WriteVInt(int i)
		{
			while ((i & ~ 0x7F) != 0)
			{
				WriteByte((byte) ((i & 0x7f) | 0x80));
				i = Number.URShift(i, 7);
			}
			WriteByte((byte) i);
		}
	}
}