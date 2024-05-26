using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using PF2e_Kingmaker_Bot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF2e_Kingmaker_Bot.Commands;

// interation modules must be public and inherit from an IInterationModuleBase
public class ExampleCommands : InteractionModuleBase<SocketInteractionContext>
{
    // dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
    public InteractionService Commands { get; set; }
    private CommandHandler _handler;

    // constructor injection is also a valid way to access the dependecies
    public ExampleCommands(CommandHandler handler)
    {
        _handler = handler;
    }

    // our first /command!
    [SlashCommand("test", "This is a test command.")]
    public async Task Testing(string chaineDeTest)
    {            
        // reply with the answer
        await RespondAsync($"J'ai écrit: [**{chaineDeTest}**]");
    }
}
