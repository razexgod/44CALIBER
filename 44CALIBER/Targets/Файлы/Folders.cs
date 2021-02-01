namespace youknowcaliber
{
    public partial class GetFiles
    {
        public class Folders : IFolders
        {
            public string Source { get; private set; }
            public string Target { get; private set; }

            public Folders(string source, string target)
            {
                Source = source;
                Target = target;
            }
        }
    }
}
