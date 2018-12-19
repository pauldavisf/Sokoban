using System;
using System.Collections.Generic;

namespace Sokoban.Architecture
{
    public class Metrics
    {
        public Dictionary<Level,List<int>> HighSores { get; private set; }
        public Dictionary<Level, int> steps { get; set; }

        public Metrics()
        {

        }
    }
}
