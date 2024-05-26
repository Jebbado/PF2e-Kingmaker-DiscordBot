using Discord.Commands;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF2e_Kingmaker_Bot.Commands;

public class SlashCommandsModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Replies with pong.")]
    public async Task PingAsync()
    {
        await RespondAsync("Pong!");
    }

    [SlashCommand("youtell", "Replies with your message.")]
    public async Task PingAsync(string message)
    {
        await RespondAsync("Did you just say : " + message + "?");
    }

    [SlashCommand("create_kingdom", "Creates a kingdom.")]
    public async Task CreateKingdom(string kingdomName)
    {
        try
        {
            Kingdom createdKingdom = new Kingdom();

            createdKingdom.ServerChannelName = "";
            createdKingdom.KingdomName = kingdomName;

            await createdKingdom.SaveKingdom();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error reading token file: {ex.StackTrace}");
            await RespondAsync("Response");
        }
    }
}
