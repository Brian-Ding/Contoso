namespace Contoso
{
    public interface IAzureServiceBusEntity { }
    public interface IMessageSender<TEntity> where TEntity : IAzureServiceBusEntity { }
    public interface IMessageHandler<TEntity> where TEntity : IAzureServiceBusEntity { }

    public class AzureServiceBusSenderOption<TEntity> where TEntity : IAzureServiceBusEntity
    {
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
    }

    public class AzureServiceBusHandlerOption<TEntity> where TEntity : IAzureServiceBusEntity
    {
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
        public string Subscription { get; set; }
    }

    public interface IAzureServiceBusBuilder
    {
        IServiceCollection Services { get; }

        string ConnectionString { get; }
    }

    public class AzureServiceBusBuilder : IAzureServiceBusBuilder
    {
        public IServiceCollection Services { get; }
        public string ConnectionString { get; }

        public AzureServiceBusBuilder(IServiceCollection services, string connectionString)
        {
            Services = services;
            ConnectionString = connectionString;
        }
    }
}