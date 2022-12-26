using EssentialDatabase.CLI.Constants;
using EssentialDatabase.CLI.Models;

namespace EssentialDatabase.CLI.States;

class StartReadingState : State
{
    public override bool ProcessCommand(Command command) =>
       command.Value switch
       {
           CLICommands.ESSENTIAl_DB_COMMAND => cliManager.ChangeStateTo(new EssentialDbCommandState()),
           _ => false,
       };
}
