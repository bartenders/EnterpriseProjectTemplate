namespace EPT.Infrastructure.Messages
{
    public class MyTestMessage
    {
        public MyTestMessage(string myMessage)
        {
            MyMessage = myMessage;
        }

        public string MyMessage { get; private set; }
    }
}