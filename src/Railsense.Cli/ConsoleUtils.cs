using Spectre.Console;

namespace Railsense.Cli;

public static class ConsoleUtils
{
    public static void PrintHeader(string subtitle)
    {
        AnsiConsole.Render(new FigletText("Railsense")
            .Centered()
            .Color(Color.Red));
        AnsiConsole.Render(new Markup($"[yellow]{subtitle}[/]")
            .Centered());
        AnsiConsole.Render(new Text("Artificial Intelligence applied to railway traffic")
            .Centered());
        AnsiConsole.WriteLine(Environment.NewLine);
    }
}