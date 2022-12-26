using EssentialDatabase.CLI.Constants;
using EssentialDatabase.CLI.Models;

namespace EssentialDatabase.CLI.States;

class CreateDatabaseCommandState : State
{
    public CreateDatabaseCommandState(ICollection<CommandKey> keys) : base(keys) { }

    public override bool ProcessCommand(Command command) =>
       command.Value switch
       {
           _ => false,
       };

    public bool ValidateRequiredKeys() => Keys.Any(x => x.Key == CLIKeys.NAME_KEY && x.Value is not null);
}