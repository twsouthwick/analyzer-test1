﻿using System;
using System.Collections;
using System.Linq;

namespace BuildAgent
{
    public class AppVeyorInformation
    {
        public AppVeyorInformation()
        {
            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                if (entry.Key.ToString().StartsWith("APPVEYOR"))
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }
            }

            IsAppveyor = string.Equals(Environment.GetEnvironmentVariable("APPVEYOR"), "True", StringComparison.OrdinalIgnoreCase);

            PullRequest = GetPullRequestBranch();
            Branch = GetBranch();
            Version = GetVersion();
        }

        public bool IsAppveyor { get; }

        public BranchInfo PullRequest { get; }

        public BranchInfo Branch { get; }

        public string Version { get; }

        private static string GetVersion()
        {
            return Environment.GetEnvironmentVariable("GitVersion_NuGetVersionV2");
        }

        private static BranchInfo GetPullRequestBranch()
        {
            var name = Environment.GetEnvironmentVariable("APPVEYOR_PULL_REQUEST_HEAD_REPO_NAME");
            var branch = Environment.GetEnvironmentVariable("APPVEYOR_PULL_REQUEST_HEAD_REPO_BRANCH");
            var commit = Environment.GetEnvironmentVariable("APPVEYOR_PULL_REQUEST_HEAD_REPO_COMMIT");

            if (string.IsNullOrEmpty(commit))
            {
                return null;
            }

            return new BranchInfo(name, branch, commit);
        }

        private static BranchInfo GetBranch()
        {
            var commit = Environment.GetEnvironmentVariable("APPVEYOR_REPO_COMMIT");
            var branch = Environment.GetEnvironmentVariable("APPVEYOR_REPO_BRANCH");
            var name = Environment.GetEnvironmentVariable("APPVEYOR_REPO_NAME");

            return new BranchInfo(name, branch, commit);
        }
    }
}
