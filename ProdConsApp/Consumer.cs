namespace ProdConsApp;

public class Consumer : IPCThreading
{
    private Mutex _mutex;
    private Semaphore _semaphoreFull;
    private Semaphore _semaphoreEmpty;
    private bool _execFlag = true;
    private bool _pauseFlag = false;
    int timeToSleep = 1000;

    public Consumer(Mutex mutex, Semaphore semaphoreFull, Semaphore semaphoreEmpty)
    {
        _mutex = mutex;
        _semaphoreFull = semaphoreFull;
        _semaphoreEmpty = semaphoreEmpty;
    }

    public void DoFaster()
    {
        if (timeToSleep > 100) timeToSleep -= 100;
    }

    public void DoSlower()
    {
        if (timeToSleep < 2000) timeToSleep += 100;
    }

    public void PauseThread() => _pauseFlag = true ? _pauseFlag == false : false;

    public void Process(Queue<int> buffer)
    {
        while (_execFlag)
        {
            if (!_pauseFlag)
            {
                _semaphoreFull.WaitOne();
                _mutex.WaitOne();

                try
                {
                    int data = buffer.Dequeue();
                    Console.WriteLine($"Consume data at: {DateTime.Now}");
                    foreach (var item in buffer)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Empty buffer");
                }

                _mutex.ReleaseMutex();
                _semaphoreEmpty.Release();
                Thread.Sleep(timeToSleep);
            }
        }
    }

    public void StopThread() => _execFlag = false;
}
