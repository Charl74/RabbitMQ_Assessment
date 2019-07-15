using System;
using SharedLibrary;

namespace Producer
{
    class Sent
    {
        public static void Main()
        {
            Console.WriteLine("Please enter your name! or 'q' to quit");
            while (true)
            {
                var name = Console.ReadLine();
                if (name.ToLower() == "q") break;
                var newMessage = new Message()
                {
                    MessageText = "Hello my name is, ",
                    Name = name
                };

                SentFactory.Factory(newMessage);

                Console.WriteLine(" Sent :{0}", newMessage.MessageText + newMessage.Name);
            }
        }
    }
}
