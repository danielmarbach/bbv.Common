//-------------------------------------------------------------------------------
// <copyright file="IDirectoryAccess.cs" company="bbv Software Services AG">
//   Copyright (c) 2008-2011 bbv Software Services AG
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace bbv.Common.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.AccessControl;

#pragma warning disable 1734

    /// <summary>
    /// Abstraction layer which simplifies access to directories.
    /// </summary>
    public interface IDirectoryAccess
    {
        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.Exists(System.String)"]/*' />
        bool Exists(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.CreateDirectory(System.String)"]/*' />
        IDirectoryInfoAccess CreateDirectory(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.CreateDirectory(System.String,System.Security.AccessControl.DirectorySecurity)"]/*' />
        IDirectoryInfoAccess CreateDirectory(string path, DirectorySecurity directorySecurity);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.Delete(System.String,System.Boolean)"]/*' />
        void Delete(string path, bool recursive);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.Delete(System.String)"]/*' />
        void Delete(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetFiles(System.String)"]/*' />
        IEnumerable<string> GetFiles(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetFiles(System.String,System.String)"]/*' />
        IEnumerable<string> GetFiles(string path, string searchPattern);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetFiles(System.String,System.String,System.IO.SearchOption)"]/*' />
        IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetDirectories(System.String)"]/*' />
        IEnumerable<string> GetDirectories(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetDirectories(System.String,System.String)"]/*' />
        IEnumerable<string> GetDirectories(string path, string searchPattern);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetDirectories(System.String,System.String,System.IO.SearchOption)"]/*' />
        IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetAccessControl(System.String)"]/*' />
        DirectorySecurity GetAccessControl(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetAccessControl(System.String,System.Security.AccessControl.AccessControlSections)"]/*' />
        DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetCreationTime(System.String)"]/*' />
        DateTime GetCreationTime(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetCreationTimeUtc(System.String)"]/*' />
        DateTime GetCreationTimeUtc(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetCurrentDirectory"]/*' />
        string GetCurrentDirectory();

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetDirectoryRoot(System.String)"]/*' />
        string GetDirectoryRoot(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetFileSystemEntries(System.String)"]/*' />
        IEnumerable<string> GetFileSystemEntries(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetFileSystemEntries(System.String,System.String)"]/*' />
        IEnumerable<string> GetFileSystemEntries(string path, string searchPattern);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetLastAccessTime(System.String)"]/*' />
        DateTime GetLastAccessTime(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetLastAccessTimeUtc(System.String)"]/*' />
        DateTime GetLastAccessTimeUtc(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetLastWriteTime(System.String)"]/*' />
        DateTime GetLastWriteTime(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetLastWriteTimeUtc(System.String)"]/*' />
        DateTime GetLastWriteTimeUtc(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetLogicalDrives"]/*' />
        IEnumerable<string> GetLogicalDrives();

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.GetParent(System.String)"]/*' />
        IDirectoryInfoAccess GetParent(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.Move(System.String,System.String)"]/*' />
        void Move(string sourceDirName, string destDirName);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetAccessControl(System.String,System.Security.AccessControl.DirectorySecurity)"]/*' />
        void SetAccessControl(string path, DirectorySecurity directorySecurity);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetCreationTime(System.String,System.DateTime)"]/*' />
        void SetCreationTime(string path, DateTime creationTime);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetCreationTimeUtc(System.String,System.DateTime)"]/*' />
        void SetCreationTimeUtc(string path, DateTime creationTimeUtc);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetCurrentDirectory(System.String)"]/*' />
        void SetCurrentDirectory(string path);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetLastAccessTime(System.String,System.DateTime)"]/*' />
        void SetLastAccessTime(string path, DateTime lastAccessTime);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetLastAccessTimeUtc(System.String,System.DateTime)"]/*' />
        void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetLastWriteTime(System.String,System.DateTime)"]/*' />
        void SetLastWriteTime(string path, DateTime lastWriteTime);

        /// <include file='mscorlib.xml' path='/doc/members/member[@name="M:System.IO.Directory.SetLastWriteTimeUtc(System.String,System.DateTime)"]/*' />
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
    }

#pragma warning restore 1734
}