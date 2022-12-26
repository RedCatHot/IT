using EssentialDatabase.CLI.Constants;
using EssentialDatabase.CLI.Models;

namespace EssentialDatabase.CLI.States;

// The base State class declares methods that all Concrete State should
// implement and also provides a backreference to the Context object,
// associated with the State. This backreference can be used by States to
// transition the Context to another State.
public abstract class State
{
    public ICollection<CommandKey> Keys { get; protected set; } = new List<CommandKey>();

    protected CLIManager cliManager;

    public State() { }
    public State(ICollection<CommandKey> keys) => Keys = keys;

    public void SetContext(CLIManager context)
    {
        cliManager = context;
    }

    public abstract bool ProcessCommand(Command command);
}

class EssentialDbCommandState : State
{
    public override bool ProcessCommand(Command command) =>
       command.Value switch
       {
           CLICommands.HELP_COMMAND => cliManager.ChangeStateTo(new HelpCommandState()),
           CLICommands.DATABASE_COMMAND => cliManager.ChangeStateTo(new DatabaseCommandState()),
           CLICommands.TABLE_COMMAND => cliManager.ChangeStateTo(new DatabaseCommandState()),
           CLICommands.ROW_COMMAND => cliManager.ChangeStateTo(new DatabaseCommandState()),
           CLICommands.COLUMN_COMMAND => cliManager.ChangeStateTo(new DatabaseCommandState()),
           _ => false,
       };
}