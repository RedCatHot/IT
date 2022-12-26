using EssentialDatabase.CLI.Constants;
using EssentialDatabase.CLI.Models;

namespace EssentialDatabase.CLI.States;
class DatabaseCommandState : State
{
    public override bool ProcessCommand(Command command) =>
       command.Value switch
       {
           CLICommands.CREATE_COMMAND => cliManager.ChangeStateTo(new CreateDatabaseCommandState(command.Keys)),
           _ => false,
       };
}
