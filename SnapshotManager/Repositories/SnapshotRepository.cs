﻿//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SnapshotManager.Repositories
{
    using Base;
    using Services;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// See <see cref="ISnapshotRepository"/>.
    /// </summary>
    public class SnapshotRepository : ISnapshotRepository
    {
        private readonly IDatabaseServices _databaseServices;
        private readonly IDictionary<DatabaseInfo, IEnumerable<SnapshotInfo>> _snapshotsPerDatabaseDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        public SnapshotRepository()
            : this(new DatabaseServices())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRepository"/> class.
        /// </summary>
        public SnapshotRepository(IDatabaseServices databaseServices)
        {
            ArgumentChecks.AssertNotNull(databaseServices, nameof(databaseServices));

            this._databaseServices = databaseServices;
            this._snapshotsPerDatabaseDict = new Dictionary<DatabaseInfo, IEnumerable<SnapshotInfo>>();
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.TryLoadSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public LoadResult TryLoadSnapshots(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            this.ClearSnapshots(database);

            try
            {
                var snapshots = this._databaseServices.GetAllSnapshotsForDatabase(database);
                this._snapshotsPerDatabaseDict.Add(database, snapshots);

                return LoadResult.CreateSuccessful();
            }
            catch(SnapshotException ex)
            {
                return LoadResult.CreateFailed($"{ex} ({ex.InnerException.Message})");
            }
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.GetLoadedSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public IEnumerable<SnapshotInfo> GetLoadedSnapshots(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            if (!this._snapshotsPerDatabaseDict.ContainsKey(database))
            {
                return new SnapshotInfo[0];
            }

            return this._snapshotsPerDatabaseDict[database];
        }

        /// <summary>
        /// See <see cref="ISnapshotRepository.ClearSnapshots(DatabaseInfo)"/>.
        /// </summary>
        public void ClearSnapshots(DatabaseInfo database)
        {
            ArgumentChecks.AssertNotNull(database, nameof(database));

            if (this._snapshotsPerDatabaseDict.ContainsKey(database))
            {
                this._snapshotsPerDatabaseDict.Remove(database);
            }
        }
    }
}