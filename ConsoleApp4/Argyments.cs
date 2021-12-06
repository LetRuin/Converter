namespace ConsoleApp4
{
    public class Argyments
    {
        private string jsonPath;
        private string csvPath;
        private string separator;
        private string encoding;

        public Argyments(string[] args)
        {
            foreach (var arg in args)
            {
                parser(arg);
            }
        }

        void parser(string arg)
        {
            if (arg.Length != 0)
            {
                switch (arg)
                {
                    case "-h": 
                        
                        break;
                }
            }
        }
    }
}