namespace Library_Try.AllLayersInterfaces
{
    public interface ISender
    {
        void Send(string message, string addressFrom, string addressFor);
    }
}