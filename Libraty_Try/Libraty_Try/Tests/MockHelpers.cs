using System;
using Library_Try.AllLayersInterfaces;
using Library_Try.Helpers;

namespace Library_Try.Tests
{
    public class MockLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class  MockConfig : IConfig
    {
        public MockConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; }
    }

    public class SendToConsole: ISender
    {
        public virtual void Send(string message, string addressFrom, string addressFor)
        {
            Console.WriteLine(message, addressFor, addressFrom);
        }
    }
}