using Discord;
using Discord.Interactions;
using DiscordNetTemplate.Attributes;
using DiscordNetTemplate.Helper;
using DiscordNetTemplate.Modals;
using Microsoft.Extensions.Logging;

namespace DiscordNetTemplate.Modules;

[UserPermission(GuildPermission.Administrator)]
public class CommandModule(ILogger<CommandModule> logger, EmbedHelper embedHelper) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("test", "Just a test command")]
    public async Task TestCommand()
        => await RespondAsync(embed: embedHelper.BasicEmbedBuilder("testCommand").Build());
    
    [MessageCommand("test")]
    public async Task TestMessageCommand()
        => await Context.Interaction.RespondWithModalAsync<WarningModal>("warning_msg");
    
    [ModalInteraction("warning_msg")]
    public async Task WarningModalInteraction(WarningModal modal)
    {
        await RespondAsync(embed: embedHelper.WarningEmbedBuilder($"Warning: {modal.Reason}").Build());
    }

}