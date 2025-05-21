using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordNetTemplate.Helper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordNetTemplate.Services;

public class InteractionHandler(DiscordSocketClient client, InteractionService interactionService, IServiceProvider services, ILogger<InteractionHandler> logger, EmbedHelper embedHelper) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), services);

        client.InteractionCreated += HandleInteraction;
        interactionService.InteractionExecuted += HandleInteractionExecuted;
    }

    private async Task HandleInteraction(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(client, interaction);

            var result = await interactionService.ExecuteCommandAsync(context, services);

            if (!result.IsSuccess)
                _ = Task.Run(() => HandleInteractionExecutionResult(interaction, result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    private Task HandleInteractionExecuted(ICommandInfo command, IInteractionContext context, IResult result)
    {
        if (!result.IsSuccess)
            _ = Task.Run(() => HandleInteractionExecutionResult(context.Interaction, result));
        return Task.CompletedTask;
    }

    private async Task HandleInteractionExecutionResult(IDiscordInteraction interaction, IResult result)
    {
        switch (result.Error)
        {
            case InteractionCommandError.UnmetPrecondition:
                logger.LogInformation($"Unmet precondition - {result.Error} [{result.ErrorReason}]");
                break;

            case InteractionCommandError.BadArgs:
                logger.LogInformation($"Unmet precondition - {result.Error} [{result.ErrorReason}]");
                break;

            case InteractionCommandError.ConvertFailed:
                logger.LogInformation($"Convert Failed - {result.Error} [{result.ErrorReason}]");
                break;

            case InteractionCommandError.Exception:
                logger.LogInformation($"Exception - {result.Error} [{result.ErrorReason}]");
                break;

            case InteractionCommandError.ParseFailed:
                logger.LogInformation($"Parse Failed - {result.Error} [{result.ErrorReason}]");
                break;

            case InteractionCommandError.UnknownCommand:
                logger.LogInformation($"Unknown Command - {result.Error} [{result.ErrorReason}]");
                break;

            case InteractionCommandError.Unsuccessful:
                logger.LogInformation($"Unsuccessful - {result.Error} [{result.ErrorReason}]");
                break;
        }

        if (!interaction.HasResponded)
        {
            await interaction.RespondAsync(embed: embedHelper.ErrorEmbedBuilder($"Error [{result.ErrorReason}]").Build());
        }
        else
        {
            await interaction.FollowupAsync(embed: embedHelper.ErrorEmbedBuilder($"Error [{result.ErrorReason}]").Build());
        }
    }
}