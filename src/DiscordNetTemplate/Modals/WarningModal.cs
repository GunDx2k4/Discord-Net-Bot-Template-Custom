using Discord.Interactions;

namespace DiscordNetTemplate.Modals;

public class WarningModal : IModal
{
    public string Title => "Warning !!!";
        
    [InputLabel("Reason")]
    [ModalTextInput("reason_msg")]
    public string Reason { get; set; }
}