using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdConsApp
{
    public interface IPCThreading
    {
        public void Process(Queue<int> buffer);
        public void DoFaster();
        public void DoSlower();
        public void StopThread();
        public void PauseThread();
    }
}
