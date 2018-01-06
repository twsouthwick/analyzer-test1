using System;

namespace BuildAgent
{
    public class AppVeyorInformation
    {
        public AppVeyorInformation()
        {
            IsAppveyor = string.Equals(Environment.GetEnvironmentVariable("APPVEYOR"), "True", StringComparison.OrdinalIgnoreCase);

            if (!IsAppveyor)
            {
                PullRequest = GetPullRequestBranch();
                Branch = GetBranch();
            }
        }

        public bool IsAppveyor { get; }

        public BranchInfo PullRequest { get; }

        public BranchInfo Branch { get; }

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
