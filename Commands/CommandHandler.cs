using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PF2e_Kingmaker_Bot.Commands;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(IServiceProvider services)
    {
        _client = services.GetRequiredService<DiscordSocketClient>();
        _commands = services.GetRequiredService<InteractionService>();
        _services = services;
    }


    //public async Task InstallCommandsAsync()
    //{
    //    // Hook the MessageReceived event into our command handler
    //    _client.MessageReceived += HandleCommandAsync;

    //    // Here we discover all of the command modules in the entry assembly and load them.
    //    await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
    //}

    public async Task InitializeAsync()
    {
        // Add the public modules that inherit InteractionModuleBase<T> to the InteractionService
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        // Process the interaction created events
        _client.InteractionCreated += HandleInteractionAsync;
        _client.Ready += OnReadyAsync;        
    }

    private async Task HandleInteractionAsync(SocketInteraction arg)
    {
        var ctx = new SocketInteractionContext(_client, arg);
        await _commands.ExecuteCommandAsync(ctx, _services);
    }

    public async Task RegisterCommandsAsync()
    {
        // Remplacez par l'ID de votre serveur (guild) pour enregistrer les commandes pour un serveur spécifique
        string serverID = "1116906695831076865"; //await File.ReadAllTextAsync("Secrets/PushCommandToServer.txt");
        ulong guildId = ulong.Parse(serverID);

        var guild = _client.GetGuild(guildId);
        if (guild != null)
        {
            // Enregistre les commandes pour un serveur spécifique
            await _commands.RegisterCommandsToGuildAsync(guildId);
        }
        else
        {
            // Enregistre les commandes globalement
            await _commands.RegisterCommandsGloballyAsync();
        }
    }

    private async Task OnReadyAsync()
    {
        await RegisterCommandsAsync();
    }
}