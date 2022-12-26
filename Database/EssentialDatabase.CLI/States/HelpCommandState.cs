using EssentialDatabase.CLI.Models;

namespace EssentialDatabase.CLI.States;

class HelpCommandState : State
{
    public override bool ProcessCommand(Command command) =>
       command.Value switch
       {
           _ => false,
       };
}
