namespace MazeConsole;

public class FileLogger
{
    private string _logFilePath;
    public FileLogger()
    {
        _logFilePath = GetPathToLogFile();
    }

    public void AddLog(string message)
    {
        AddLog(new List<string> { message });
    }

    public void AddLog(List<string> messages)
    {
        using var fs = File.Open(_logFilePath, FileMode.Append);
        using var sw = new StreamWriter(fs);
        foreach (var message in messages)
        {
            sw.WriteLine($"{DateTime.Now.ToLocalTime()} {message}");
        }
    }

    private string GetPathToLogFile()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        var folder = GetParentXTimes(path, 2);
        var pathToLog = Path.Combine(folder, "log");
        CheckIsLogFolderExist(pathToLog);
        var logFile = Path.Combine(pathToLog, "today.log");
        return logFile;
    }

    private void CheckIsLogFolderExist(string pathToLog)
    {
        if (!Directory.Exists(pathToLog))
        {
            Directory.CreateDirectory(pathToLog);
        }
    }

    private string GetParentXTimes(string path, int times)
    {
        var answer = path;
        for (int i = 0; i < times; i++)
        {
            answer = Directory.GetParent(answer).Parent.FullName;
        }

        return answer;
    }
}
