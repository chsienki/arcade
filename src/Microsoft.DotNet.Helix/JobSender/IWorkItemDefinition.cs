// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace Microsoft.DotNet.Helix.Client;

/// <summary>
/// Work Item definition with all required information already specified
/// that can either completed by calling <see cref="AttachToJob"/> or further extended.
/// </summary>
public interface IWorkItemDefinition
{
    string WorkItemName { get; }

    string Command { get; }

    /// <summary>
    /// Specifies timeout for the work item to finish inside of Helix.
    /// </summary>
    IWorkItemDefinition WithTimeout(TimeSpan timeout);

    /// <summary>
    /// Associates the work item with a test-result cache identity.
    /// </summary>
    /// <param name="testResultsCacheKey">
    /// A lowercase, 64-character SHA-256 value covering every payload and environment input that can affect the test result.
    /// </param>
    IWorkItemDefinition WithTestResultsCacheKey(string testResultsCacheKey);

    /// <summary>
    /// Complete work item and return to specification of the overarching job.
    /// </summary>
    /// <returns>Fluent job builder.</returns>
    IJobDefinition AttachToJob();
}
