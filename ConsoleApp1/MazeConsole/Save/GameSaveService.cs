using System.Text.Json;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Intefaces;

namespace MazeConsole.Save;

// сохранение и загрузка
public class GameSaveService
{
    private const string SaveFileName = "savegame.json";

    private readonly string _saveDirectory; // папка Save/savefile рядом с проектом
    private readonly string _saveFilePath;  // полный путь к savegame.json
    private readonly GameSaveMapper _mapper = new(); // маппер лабиринта

    public GameSaveService()
    {
        _saveDirectory = ResolveSaveDirectory();
        _saveFilePath = Path.Combine(_saveDirectory, SaveFileName);
    }

    // Ищем проект и кладём JSON в Save/savefile 
    private static string ResolveSaveDirectory()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "MazeConsole.csproj")))
            {
                return Path.Combine(directory.FullName, "save", "savefile");
            }

            directory = directory.Parent;
        }

        return null;
    }

    // сохранение
    public void SaveGame(IMaze maze)
    {
        Directory.CreateDirectory(_saveDirectory); // создать  папку savefile для хранения сейва, если ещё нет
        var dto = _mapper.ToDto(maze); // лабиринт >>> снапшот
        File.WriteAllText(_saveFilePath, JsonSerializer.Serialize(dto)); // снапшот >>> JSON
    }

    public bool isSaveFileExists() => File.Exists(_saveFilePath);

    // загрузка
    public Maze LoadGame()
    {
        var json = File.ReadAllText(_saveFilePath);
        var dto = JsonSerializer.Deserialize<GameSaveDto>(json)!; // JSON >>> снапшот
        return _mapper.FromDto(dto); // снапшот >>> лабиринт
    }
}
