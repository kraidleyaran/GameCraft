using System.Collections.Generic;

namespace GameCraft
{
    public class Failure
    {
        private string _name;
        private List<string> _failList = new List<string>();

        public Failure(string name)
        {
            _name = name;
        }

        public string Name {
            get { return _name; }
        }

        public List<string> FailList
        {
            get { return _failList; }
            set { _failList = value; }
        }
    }
}
