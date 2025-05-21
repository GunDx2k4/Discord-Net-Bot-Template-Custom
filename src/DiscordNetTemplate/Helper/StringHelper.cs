namespace DiscordNetTemplate.Helper;

public class StringHelper
{
    public string ItalicsText(string text)
    {
        return $"*{text}*";
    }

    public string UnderlineText(string text)
    {
        return $"_{text}_";
    }

    public string BoldText(string text)
    {
        return $"**{text}**";
    }

    public string StrikethroughText(string text)
    {
        return $"~~{text}~~";
    }

    public string SpoilerhText(string text)
    {
        return $"|{text}|";
    }

    public string HeadersText(string text, int level = 1)
    {
        switch (level)
        {
            case 1:
                return $"# {text}";
            case 2:
                return $"## {text}";
            case 3:
                return $"### {text}";
            default:
                return $"{text}";
        }
    }

    public string MaskedLinks(string text, string link)
    {
        return $"[{text}]({link})";
    }

    public string HighlightText(string text)
    {
        return $"`{text}`";
    }

    public string CodeBlock(string text)
    {
        return $"```{text}```";
    }

    public string BlockQuotes(string text)
    {
        return $"> {text}";
    }

    public string BlockQuotesAll(string text)
    {
        return $">>> {text}";
    }
}