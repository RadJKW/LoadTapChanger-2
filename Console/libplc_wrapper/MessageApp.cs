using PlcTagLibrary;

namespace ConsoleTestsPLC
{
    public class MessageApp
    {
        //private readonly IMessages _messages;

        //public MessageApp(IMessages messages)
        //{
        //    _messages = messages;
        //}

        //public void Run()
        //{
        //    Console.WriteLine("Running");
        //    MessageTest();

        //}

        //public void MessageTest()
        //{
        //    Console.WriteLine($"Defualt Message: {_messages.Message}");
        //    // ask the user to input a new message and do not let them submit the message unless it is valid
        //    // valid message is both not null and not equal to the previous message
        //    string? newMessage;
        //    do
        //    {
        //        Console.WriteLine("Please enter a new message:");
        //        newMessage = Console.ReadLine();
        //    } while (string.IsNullOrEmpty(newMessage) || newMessage == _messages.Message);

        //    _messages.LogMessage();
        //    _messages.Message = newMessage;
        //}

    }
}
