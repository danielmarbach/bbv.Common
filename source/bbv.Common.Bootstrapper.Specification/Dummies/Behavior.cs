//-------------------------------------------------------------------------------
// <copyright file="Behavior.cs" company="bbv Software Services AG">
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

namespace bbv.Common.Bootstrapper.Specification.Dummies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    public class Behavior : IBehavior<ICustomExtension>
    {
        private readonly string access;

        public Behavior(string access)
        {
            this.access = access;
        }

        public void Behave(IEnumerable<ICustomExtension> extensions)
        {
            foreach (ICustomExtension extension in extensions)
            {
                var dumpMethod = extension.GetType().GetMethod("Dump", BindingFlags.Instance | BindingFlags.NonPublic);
                var action = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), extension, dumpMethod);
                action(string.Format(CultureInfo.InvariantCulture, "Behaving on {0} at {1}.", extension, this.access));
            }
        }
    }
}