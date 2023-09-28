namespace ProdConsApp;

public class Producer : IPCThreading
{
    private Mutex _mutex;
    private Semaphore _semaphoreFull;
    private Semaphore _semaphoreEmpty;
    private bool _execFlag = true;
    private bool _pauseFlag = false;
    private int timeToSleep = 1000;

    public Producer(Mutex mutex, Semaphore semaphoreFull, Semaphore semaphoreEmpty)
    {
        _mutex = mutex;
        _semaphoreFull = semaphoreFull;
        _semaphoreEmpty = semaphoreEmpty;
    }

    public void Process(Queue<int> buffer)
    {
        while (_execFlag)
        {
            if (!_pauseFlag)
            {
                _semaphoreEmpty.WaitOne();
                _mutex.WaitOne();

                int data = ProduceData();

                if (buffer.Count < 10)
                {
                    buffer.Enqueue(data);
                }
                else
                {
                    throw new OverflowException("Buffer overflow");
                }

                Console.WriteLine($"Produce data at: {DateTime.Now}");

                foreach (var item in buffer)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();

                _mutex.ReleaseMutex();
                _semaphoreFull.Release();
                Thread.Sleep(timeToSleep);
            }
        }
    }

    public void DoFaster()
    {
        if (timeToSleep > 100)
        {
            timeToSleep -= 100;
        }
    }

    public void DoSlower()
    {
        if (timeToSleep < 2000)
        {
            timeToSleep += 100;
        }
    }

    public void PauseThread() => _pauseFlag = true ? _pauseFlag == false : false;
    private int ProduceData() => new Random().Next(1, 100);
    public void StopThread() => _execFlag = false;
}

