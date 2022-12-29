// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON
{
    /// <summary>
    ///     Instructs the <see cref="JsonSerializer" /> how to serialize the object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public abstract class JsonContainerAttribute : Attribute
    {
        // yuck. can't set nullable properties on an attribute in C#
        // have to use this approach to get an unset default state
        internal bool? _isReference;
        internal bool? _itemIsReference;
        internal ReferenceLoopHandling? _itemReferenceLoopHandling;
        internal TypeNameHandling? _itemTypeNameHandling;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonContainerAttribute" /> class.
        /// </summary>
        protected JsonContainerAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonContainerAttribute" /> class with the specified container Id.
        /// </summary>
        /// <param name="id">The container Id.</param>
        protected JsonContainerAttribute(string id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets the collection's items converter.
        /// </summary>
        /// <value>The collection's items converter.</value>
        public Type ItemConverterType { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether to preserve object references.
        /// </summary>
        /// <value>
        ///     <c>true</c> to keep object reference; otherwise, <c>false</c>. The default is <c>false</c>.
        /// </value>
        public bool IsReference
        {
            get { return _isReference ?? default(bool); }
            set { _isReference = value; }
        }

        /// <summary>
        ///     Gets or sets a value that indicates whether to preserve collection's items references.
        /// </summary>
        /// <value>
        ///     <c>true</c> to keep collection's items object references; otherwise, <c>false</c>. The default is <c>false</c>.
        /// </value>
        public bool ItemIsReference
        {
            get { return _itemIsReference ?? default(bool); }
            set { _itemIsReference = value; }
        }

        /// <summary>
        ///     Gets or sets the reference loop handling used when serializing the collection's items.
        /// </summary>
        /// <value>The reference loop handling.</value>
        public ReferenceLoopHandling ItemReferenceLoopHandling
        {
            get { return _itemReferenceLoopHandling ?? default(ReferenceLoopHandling); }
            set { _itemReferenceLoopHandling = value; }
        }

        /// <summary>
        ///     Gets or sets the type name handling used when serializing the collection's items.
        /// </summary>
        /// <value>The type name handling.</value>
        public TypeNameHandling ItemTypeNameHandling
        {
            get { return _itemTypeNameHandling ?? default(TypeNameHandling); }
            set { _itemTypeNameHandling = value; }
        }
    }
}