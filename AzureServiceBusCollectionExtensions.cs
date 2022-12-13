using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Contoso
{
    public static class AzureServiceBusCollectionExtensions
    {
        public static IAzureServiceBusBuilder AddAzureServiceBus(this IServiceCollection services, string connectionString)
        {
            var builder = new AzureServiceBusBuilder(services, connectionString);

            return builder;
        }
    }

    public static class AzureServiceBusBuilderExtensions
    {
        public static IAzureServiceBusBuilder AddSender<TEntity, TMessageSender>(
            this IAzureServiceBusBuilder builder, string topic)
            where TEntity : IAzureServiceBusEntity
            where TMessageSender : class, IMessageSender<TEntity>
        {
            AddSender<TEntity, TMessageSender>(builder.Services, builder.ConnectionString, topic);

            return builder;
        }

        public static IAzureServiceBusBuilder AddHandler<TEntity, TMessageHandler>(
            this IAzureServiceBusBuilder builder,
            string topic,
            string subscription
        )
            where TEntity : IAzureServiceBusEntity
            where TMessageHandler : class, IMessageSender<TEntity>
        {
            AddHandler<TEntity, TMessageHandler>(builder.Services, builder.ConnectionString, topic, subscription);

            return builder;
        }

        internal static void AddSender<TEntity, TMessageSender>(
            IServiceCollection services,
            string connectionString,
            string topic)
            where TEntity : IAzureServiceBusEntity
            where TMessageSender : class, IMessageSender<TEntity>
        {
            services.Configure<AzureServiceBusSenderOption<TEntity>>((o) =>
            {
                o.ConnectionString = connectionString;
                o.Topic = topic;
            });
            services.TryAddSingleton<IMessageSender<TEntity>, TMessageSender>();
        }

        internal static void AddHandler<TEntity, TMessageHandler>(
            IServiceCollection services,
            string connectionString,
            string topic,
            string subscription
        )
            where TEntity : IAzureServiceBusEntity
            where TMessageHandler : class, IMessageSender<TEntity>
        {
            services.Configure<AzureServiceBusHandlerOption<TEntity>>((o) =>
            {
                o.ConnectionString = connectionString;
                o.Topic = topic;
                o.Subscription = subscription;
            });
            services.TryAddSingleton<IMessageSender<TEntity>, TMessageHandler>();
        }
    }
}