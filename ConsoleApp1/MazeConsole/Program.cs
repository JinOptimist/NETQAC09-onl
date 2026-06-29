var builder = new MazeBuilder();
var maze = builder.BuildTestMaze();

var drawer = new MazeDrawer();
drawer.Draw(maze);