using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karachun_alice.Data.Dto
{
    public class Command
    {

        private List<Command> CallingCommands = new List<Command>();

        private List<Command> SubCommands = new List<Command>();

        public string CommandText { get; set; }

        public delegate AliceResponseDto CommandActive();

        public CommandActive Active { get; set; } = () => new AliceResponseDto();

        public bool IsCalling(Command command) => CallingCommands.Any(x => x == command);

        public Command(string command)
        {
            CommandText = command;
        }

        public Command(string command, Command callingCommand, Command canselCommand)
        {
            CommandText = command;
            AddCallingCommand(callingCommand);

            AddSubCommand(canselCommand);
        }

        public Command(string command, List<Command> callingCommands)
        {
            CommandText = command;
            AddCallingCommands(callingCommands);
        }

        public IEnumerable<string> GetButtons() => SubCommands.Select(x => x.CommandText);

        public void AddCallingCommand(Command callingCommand)
        {
            CallingCommands.Add(callingCommand);
            callingCommand.AddSubCommand(this);
        }

        public void AddCallingCommands(IEnumerable<Command> callingCommands)
        {
            CallingCommands.AddRange(callingCommands);
            AddCallingCommands(callingCommands);
        }

        public void AddSubCommand(Command subCommand)
        {
            if(!SubCommands.Contains(subCommand))
                SubCommands.Add(subCommand);
        }

        private void AddSubInCommands(IEnumerable<Command> commands)
        {
            foreach (var c in commands)
                AddSubCommand(c);
        }

        public void AddInSubCommandYourself()
        {
            AddSubCommand(this);
        }
    }
}
