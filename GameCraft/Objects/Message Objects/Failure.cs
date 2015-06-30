using System.Collections.Generic;

namespace GameCraft
{
    public class Failure
    {
        private List<string> _failList = new List<string>();

        public Failure(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public List<string> FailList
        {
            get { return _failList; }
            set { _failList = value; }
        }
    }
}
