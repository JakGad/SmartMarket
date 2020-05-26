namespace SmartMarketLibrary
{
    public static class Factory
    {
        public static DatabaseServices GetServices()
        {
            return new DatabaseServices(new Entities());
        }
    }
}
