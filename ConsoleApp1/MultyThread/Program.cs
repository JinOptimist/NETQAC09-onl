// Поток данных
// Stream

// поток выполнения
// Thread

using MultyThread;
using System.Net.Http.Headers;

var reportGenerator = new ReportGenerator();
reportGenerator.GenerateAllRepots();


var tasks = new List<Task>();

for (int i = 0; i < 100000; i++)
{
    var task = new Task(DoMagic);
    tasks.Add(task);
}

tasks.ForEach(x => x.Start());

Task.WaitAll(tasks);

Console.ReadLine();

void DoMagic()
{
    var counter = 0;
    while (true)
    {
        counter++;
        Console.WriteLine("AAAAA "  + counter);
    }
}

void DoMagicB()
{
    var counter = 0;
    while (true)
    {
        counter++;
        Console.WriteLine("BB " + counter);
    }
}