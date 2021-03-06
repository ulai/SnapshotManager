﻿//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager
{
    using System;

    /// <summary>
    /// Represents an exception which may occur during snapshot handling.
    /// </summary>
    [Serializable]
    public class SnapshotException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotException"/> class.
        /// </summary>
        public SnapshotException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotException"/> class.
        /// </summary>
        public SnapshotException(string message)
            : base(message)
        {
        }
    }
}
