namespace BuildAgent
{
    public class BranchInfo
    {
        public BranchInfo(string name, string branch, string commit)
        {
            Name = name;
            Branch = branch;
            Commit = commit;
        }

        public string Name { get; }

        public string Branch { get; }

        public string Commit { get; }

        public override string ToString() => $"{Name} : {Commit} : {Branch}";
    }
}
