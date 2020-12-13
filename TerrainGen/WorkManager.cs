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

            _workThread = new Task(updateWork, creationOptions: TaskCreationOptions.LongRunning);
        }

        public void Start() => _workThread.Start();

        public void QueueWork(GenerateWork work) => _workQueue.Enqueue(work);

        private void updateWork()
        {
            while(true) // FOR NOW
            {
                if (_workQueue.TryDequeue(out var work))
                    ThreadPool.QueueUserWorkItem(new WaitCallback(onWork), work);
                Thread.Sleep(50);
            }
        }

        private void onWork(object genWork)
        {
            var generate = (GenerateWork)genWork;
            var work = generate.Work;
            work.BeginInvoke(generate.callBack, work);
        }
    }
    public struct GenerateWork
    {
        private Action _callBack;

        public GenerateWork(Action work, Action callBack)
        {
            Work = work;
            _callBack = callBack;
        }

        public Action Work { get; set; }
        
        public void callBack(IAsyncResult result)
        {
            var action = result.AsyncState as Action;
            _callBack.Invoke();
            action.EndInvoke(result);
        }
    }
}
