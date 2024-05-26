using System;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PF2e_Kingmaker_Bot.Commands;

public class Program
{
    private DiscordSocketClient _client;
    private InteractionService _commands;
    private IServiceProvider _services;
    private CommandHandler _commandHandler;




    public static async Task Main(string[] args)
    {             
        var MainProgram = new Program();
        await MainProgram.RunBotAsync();
    }

    public async Task RunBotAsync()
    {
        _services = ConfigureServices();
    

        _client = _services.GetRequiredService<DiscordSocketClient>(); //new DiscordSocketClient();
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;
        _client.SlashCommandExecuted += CommandReceived;

        string token;
        try
        {
            token = await File.ReadAllTextAsync("Secrets/DiscordBotKey.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading token file: {ex.Message}");
            return;
        }

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        _commandHandler = _services.GetRequiredService<CommandHandler>();
        await _commandHandler.InitializeAsync();

        await Task.Delay(-1);
    }

    private async Task ReadyAsync()
    {
        if (true) //Gérer le mode débug a l'air plus compliqué.
        {
            ulong serverID;

            try
            {
                serverID = ulong.Parse(await File.ReadAllTextAsync("Secrets/PushCommandToServer.txt"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading token file: {ex.Message}");
                return;
            }
            System.Console.WriteLine($"In debug mode, adding commands to {serverID}...");
            await _commands.RegisterCommandsToGuildAsync(serverID);
        }
        //else
        //{
        //    // this method will add commands globally, but can take around an hour
        //    await _commands.RegisterCommandsGloballyAsync(true);
        //}
        Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
    }


    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log); //À tester : Pourquoi certains font log.ToString(); ? J'aurais cru que le ToString implicite avait le même comportement.
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Content.ToLower() == "hello")
        {
            await message.Channel.SendMessageAsync("Hello, Discord!");
        }
    }

    private async Task CommandReceived(SocketCommandBase command)
    {
        await command.Channel.SendMessageAsync("Commande reçue");
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<InteractionService>()
            .AddSingleton<CommandHandler>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>())); //Proposé par ChatGPT ?
        //.AddSingleton<LogService>(); // Optional logging service

        return services.BuildServiceProvider();
    }
}

    //private static void Tests()
    //{
    //    try
    //    {
    //        DatabaseWriter myWriter = new DatabaseWriter();
    //    }
    //    catch (Exception ex) 
    //    {
    //        Console.WriteLine("OH NO !");
    //        Console.WriteLine(ex.ToString());
    //    }
    //    //Kingdom Valancie = new Kingdom("Valancie");
    //    //Valancie.AssignCharter(EnumCharter.Exploration);
    //    //Valancie.AssignHeartland(EnumHeartland.Plain);
    //    //Valancie.AssignGovernment(EnumGovernment.Feudalism);
    //    //Valancie.AddLeader("Sébaste Larivière", EnumLeaderRole.Ruler, true, true);
    //    //Valancie.AddLeader("Jacques Létourneau", EnumLeaderRole.None, false, false);

    //    //Valancie.TrainSkill(EnumSkills.Agriculture);
    //    ////Console.WriteLine(Valancie.SkillName.ToString());

    //    //Settlement ofTest = new Settlement(Valancie, "Test", new Hex(1, 1, EnumHeartland.Lake));
    //    //ofTest.Name = "Test2";
    //    //ofTest.IsCapital = true;
    //    //ofTest.SettlementType = EnumSettlementType.Village;
    //    //Console.WriteLine(ofTest.Name + " " + ofTest.IsCapital);
    //}