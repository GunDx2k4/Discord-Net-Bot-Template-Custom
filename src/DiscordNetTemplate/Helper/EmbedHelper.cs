using Discord;
using Discord.WebSocket;

namespace DiscordNetTemplate.Helper;

public class EmbedHelper(DiscordSocketClient client, StringHelper stringHelper)
{
    public EmbedAuthorBuilder GetEmbedAuthorBot()
    {
        return new EmbedAuthorBuilder
        {
            Name = client.CurrentUser.Username,
            IconUrl = client.CurrentUser.GetAvatarUrl(),
            Url = $"https://discord.com/oauth2/authorize?client_id={client.CurrentUser.Id}&permissions=8&scope=bot%20applications.commands"
        };
    }

    public EmbedFooterBuilder GetEmbedFooter()
    {
        return new EmbedFooterBuilder
        {
            Text = $"{client.CurrentUser.Username} [{DateTime.Now.Year}]",
            IconUrl = client.CurrentUser.GetAvatarUrl(),
        };
    }
    
    public EmbedBuilder BasicEmbedBuilder(string content, EmbedAuthorBuilder author = null)
    {
        return new EmbedBuilder
        {
            Color = Color.Blue,
            Description = stringHelper.BoldText(content),
            Author = author ?? GetEmbedAuthorBot(),
            Timestamp = DateTimeOffset.Now
        };
    }

    public EmbedBuilder ErrorEmbedBuilder(string content)
    {
        return new EmbedBuilder
        {
            Color = Color.Red,
            Description = stringHelper.BoldText(content),
            Author = GetEmbedAuthorBot(),
            Timestamp = DateTimeOffset.Now
        };
    }

    public EmbedBuilder SuccessEmbedBuilder(string content)
    {
        return new EmbedBuilder
        {
            Color = Color.Green,
            Description = stringHelper.BoldText(content),
            Author = GetEmbedAuthorBot(),
            Timestamp = DateTimeOffset.Now
        };
    }

    public EmbedBuilder WarningEmbedBuilder(string content)
    {
        return new EmbedBuilder
        {
            Color = Color.Gold,
            Description = stringHelper.BoldText(content),
            Author = GetEmbedAuthorBot(),
            Timestamp = DateTimeOffset.Now
        };
    }
}