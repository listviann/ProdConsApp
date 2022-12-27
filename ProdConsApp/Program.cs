using ProdConsApp;

Mutex mutex = new();
Semaphore semaphoreFull = new(0, 10);
Semaphore semaphoreEmpty = new(10, 10);
Queue<int> buffer = new();
Producer producer = new(mutex, semaphoreFull, semaphoreEmpty);
Consumer consumer = new(mutex, semaphoreFull, semaphoreEmpty);
Thread producerThread = new(() => producer.Process(buffer));
Thread consumerThread = new(() => consumer.Process(buffer));

producerThread.Start();
consumerThread.Start();

while (true)
{
    var keyInfo = Console.ReadKey(true);

    switch (keyInfo.Key)
    {
        case ConsoleKey.Q:
            consumer.DoSlower();
            break;
        case ConsoleKey.W:
            consumer.DoFaster();
            break;
        case ConsoleKey.E:
            producer.DoSlower();
            break;
        case ConsoleKey.R:
            producer.DoFaster();
            break;
        case ConsoleKey.T:
            consumer.PauseThread();
            break;
        case ConsoleKey.Y:
            producer.PauseThread();
            break;
        case ConsoleKey.A:
            consumer.StopThread();
            break;
        case ConsoleKey.S:
            producer.StopThread();
            break;
    }
}
