using Discord;
using Discord.Interactions;
using DiscordNetTemplate.Attributes;
using DiscordNetTemplate.Helper;
using Microsoft.Extensions.Logging;

namespace DiscordNetTemplate.Modules;

[UserPermission(GuildPermission.Administrator)]
public class CommandModule(ILogger<CommandModule> logger, EmbedHelper embedHelper) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("test", "Just a test command")]
    public async Task TestCommand()
        => await RespondAsync(embed: embedHelper.BasicEmbedBuilder("testCommand").Build());

}