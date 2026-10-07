// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.DotNet.Helix.Client;
using Moq;
using Xunit;

namespace Microsoft.DotNet.Helix.JobSender.Test;

public class WorkItemDefinitionTests
{
    [Fact]
    public async Task SendAsyncIncludesTestResultsCacheKey()
    {
        const string cacheKey = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";
        var definition = new WorkItemDefinition(null, "workitem");
        definition.WithCommand("run-tests").WithEmptyPayload();
        definition.WithTestResultsCacheKey(cacheKey);

        JobListEntry entry = await definition.SendAsync(
            new Mock<IBlobContainer>(MockBehavior.Strict).Object,
            "container",
            log: null,
            CancellationToken.None);

        Assert.Equal(cacheKey, entry.TestResultsCacheKey);
    }

    [Fact]
    public async Task SendAsyncOmitsTestResultsCacheKeyByDefault()
    {
        var definition = new WorkItemDefinition(null, "workitem");
        definition.WithCommand("run-tests").WithEmptyPayload();

        JobListEntry entry = await definition.SendAsync(
            new Mock<IBlobContainer>(MockBehavior.Strict).Object,
            "container",
            log: null,
            CancellationToken.None);

        Assert.Null(entry.TestResultsCacheKey);
    }
}
