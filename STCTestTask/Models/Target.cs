namespace STCTestTask.Models
{
    public class Target
    {
        public string Name { get; set; }
        public List<string> Actions { get; set; }
        public List<string> Dependencies { get; set; }

        public Target(string name)
        {
            Name = name;
            Actions = new List<string>();
            Dependencies = new List<string>();
        }
    }
}
