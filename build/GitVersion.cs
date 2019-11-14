using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class GitVersion
{
    public static (string shortVersion, string longVersion, string semanticVersion) Get(string repositoryPath, ulong buildNumber)
    {
        (ulong major, ulong minor, ulong revision, ulong commits, string shasum) = Get(repositoryPath);

        string label = string.Empty;

        if (commits == 0)
        {
            label = shasum;
        }
        else
        {
            label = $"beta{commits}-{shasum}";
        }

        string shortVersion = $"{major}.{minor}.{revision}";
        string version = $"{major}.{minor}.{buildNumber}.{revision}";
        string semanticVersion = $"{major}.{minor}.{revision}+{label}";

        return (shortVersion, version, semanticVersion);
    }

    public static (ulong major, ulong minor, ulong revision, ulong commits, string shasum) Get(string repositoryPath)
    {
        List<string> tags = new List<string>();

        using Repository repository = new Repository(repositoryPath);

        foreach (var tag in repository.Tags)
        {
            if (tag.IsAnnotated && (tag.FriendlyName.StartsWith('v') || tag.FriendlyName.StartsWith('V')))
            {
                tags.Add(tag.FriendlyName);
            }
        }

        tags.Sort();

        DescribeOptions options = new DescribeOptions
        {
            AlwaysRenderLongFormat = true,
            Strategy = DescribeStrategy.Default
        };

        string description = repository.Describe(repository.Head.Commits.First(), options);
        string latestTag = tags.Last();

        Regex tagQuery = new Regex(@"v(?<major>\d+).(?<minor>\d+).(?<revision>\d+)");
        Regex descriptionQuery = new Regex(@"(?<tag>.*)-(?<commits>\d+)-(?<shasum>.*)");

        var tagMatch = tagQuery.Match(latestTag);
        var descriptionMatch = descriptionQuery.Match(description);

        ulong major = ulong.Parse(tagMatch.Groups["major"].Value);
        ulong minor = ulong.Parse(tagMatch.Groups["minor"].Value);
        ulong revision = ulong.Parse(tagMatch.Groups["revision"].Value);

        ulong commits = ulong.Parse(descriptionMatch.Groups["commits"].Value);
        string shasum = descriptionMatch.Groups["shasum"].Value;

        return (major, minor, revision, commits, shasum);
    }
}
