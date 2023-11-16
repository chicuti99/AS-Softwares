using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketBE.Entities
{
    public class ThreadControl
    {
        public bool IsAlive { get; set; }
        public StringBuilder Json { get; set; }
        public bool Complete { get; set; }

        public ThreadControl()
        {
            IsAlive = false;
            Json = new StringBuilder();
            Complete = false;
        }

        public void Reset()
        {
            IsAlive = false;
            Json = new StringBuilder();
            Complete = false;
        }
    }
}
