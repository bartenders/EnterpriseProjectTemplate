namespace EPT.Infrastructure.Messages
{
    public class EmployeeAddedMessage
    {
        public EmployeeAddedMessage(string myMessage)
        {
            MyMessage = myMessage;
        }

        public string MyMessage { get; private set; }
    }
}