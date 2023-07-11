using CommunityToolkit.Mvvm.Messaging.Messages;

namespace UCG.siteTRAXLite.Messages
{
    public class LaunchingAppMessage : ValueChangedMessage<string>
    {
        public LaunchingAppMessage(string value) : base(value)
        {
        }
    }
}
