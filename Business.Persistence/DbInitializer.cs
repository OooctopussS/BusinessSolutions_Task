namespace Business.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(BusinessDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
