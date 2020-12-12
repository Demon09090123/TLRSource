using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TerrainGen
{
    public class WorkManager
    {
        private ConcurrentQueue<GenerateWork> _workQueue;

        private Task _workThread;
        public WorkManager()
        {
            _workQueue = new ConcurrentQueue<GenerateWork>();

            _workThread = new Task(updateWork, , );
        }

        public void QueueWork(GenerateWork work) => _workQueue.Enqueue(work);

        private void updateWork()
        {
            while(true) // FOR NOW
            {
                if (_workQueue.TryDequeue(out var work))
                    ThreadPool.QueueUserWorkItem(new WaitCallback(work.Work.Invoke), null);
                Thread.Sleep(50);
            }
        }

            



    }
    public class GenerateWork
    {
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int EndX { get; private set; }
        public int EndY { get; private set; }

        public MapGeneration Parent { get; private set; }

        private Action _callBack;

        public GenerateWork(int sX, int sY, int eX, int eY, MapGeneration p, Action<object> work, Action callBack)
        {
            StartX = sX;
            StartY = sY;
            EndX = eX;
            EndY = eY;
            Parent = p;
            Work = work;
            _callBack = callBack;
        }

        public Action<object> Work { get; set; }
        
        private void callBack(IAsyncResult result)
        {
            var action = result.AsyncState as Action;
            _callBack.Invoke();
            action.EndInvoke(result);
        }
    }
}
