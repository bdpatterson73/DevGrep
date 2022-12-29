// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON.Schema
{
    /// <summary>
    ///     Represents the callback method that will handle JSON schema validation events and the
    ///     <see
    ///         cref="ValidationEventArgs" />
    ///     .
    /// </summary>
    public delegate void ValidationEventHandler(object sender, ValidationEventArgs e);
}