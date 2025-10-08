namespace IceCreamWorld.Middleware
{
    public class SaveChangesException : Exception
    {
        public SaveChangesException(string? message)
            : base(message)
        {

        }

    }
}