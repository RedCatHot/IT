using EssentialDatabase.CLI.Constants;
using EssentialDatabase.CLI.Models;
using EssentialDatabase.CLI.States;
using EssentialDatabase.Management;
using System.Text.RegularExpressions;

namespace EssentialDatabase.CLI;

public class CLIManager
{
    private State _state;
    private DatabaseManager _databaseManager;

    public CLIManager(DatabaseManager dbManager)
    {
        ChangeStateTo(new StartReadingState());
        _databaseManager = dbManager;
    }

    public bool ChangeStateTo(State state)
    {
        if (state == null)
            return false;

        _state = state;
        _state.SetContext(this);

        return true;
    }

    public void StartConsoleReading()
    {
        do
        {
            string? commandLine = Console.ReadLine();
            if (!string.IsNullOrEmpty(commandLine))
                ProcessCommandsQueue(GetCommandsQueue(commandLine));
        } while (true);
    }

    private void ProcessCommandsQueue(Queue<Command> commands)
    {
        bool isSuccess = true;

        while (isSuccess && commands.Any())
        {
            isSuccess = _state.ProcessCommand(commands.Dequeue());
        }

        if (isSuccess)
        {
            //TODO _state.Validate
            var result = _state switch
            {
                CreateDatabaseCommandState command => ProcessCreateDatabaseCommand(command),
                _ => throw new NotImplementedException(),
            };

            Console.WriteLine(result);
        }
        else
            PrintHelp();
    }

    private string ProcessCreateDatabaseCommand(CreateDatabaseCommandState createDatabase)
    {
        return ">>> Database created with Id 241";
    }

    private Queue<Command> GetCommandsQueue(string commandLine)
    {
        Regex rx = new(@"\S+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        var words = rx.Matches(commandLine).Select(x => x.Value).ToList();

        if (words is null)
            return new Queue<Command>();

        Command? currentCommand = null;
        int length = words.Count;
        var result = new Queue<Command>();

        for (int i = 0; i < length; i++)
        {
            var word = words[i];

            if (word.StartsWith('-') && currentCommand is not null)
            {
                var isLastCommand = i == length - 1;

                //check if it is the last word
                if (i == length - 1)
                    currentCommand.Keys.Add(new(Key: word, Value: null));
                else
                {
                    var nextWord = words[i + 1];
                    if (!nextWord.StartsWith('-'))
                    {
                        currentCommand.Keys.Add(new(Key: word, Value: nextWord));
                        i++; //skip next word because we have already taken it
                    }
                }
            }
            else
            {
                currentCommand = new(word);
                result.Enqueue(currentCommand);
            }
        }

        return result;
    }

    public static void PrintHelp()
    {
        Console.WriteLine("Help:");
        Console.WriteLine($"-> {CLICommands.ESSENTIAl_DB_COMMAND}");
        Console.WriteLine($"-> {CLICommands.DATABASE_COMMAND}");
        Console.WriteLine($"-> {CLICommands.TABLE_COMMAND}");
        Console.WriteLine($"-> {CLICommands.ROW_COMMAND}");
        Console.WriteLine($"-> {CLICommands.COLUMN_COMMAND}");
    }

    public static string AskKey(string key)
    {
        Console.Write($"{key}: ");

        string? value;
        do
            value = Console.ReadLine();
        while (value is null);

        return value;
    }
}
