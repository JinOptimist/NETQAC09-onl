var builder = new MazeBuilder();
var maze = builder.BuildTestMaze(8, 5);

var drawer = new MazeDrawer();
drawer.Draw(maze);