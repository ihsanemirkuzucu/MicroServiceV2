using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

#region RabbitMQ

var rabbitMqUserName = builder.AddParameter("RABBITMQ-USERNAME");
var rabbitMqPassword = builder.AddParameter("RABBITMQ-PASSWORD");

var rabbitMq = builder.AddRabbitMQ("rabbitMQ", rabbitMqUserName, rabbitMqPassword, 5672).WithManagementPlugin(15672);

#endregion

#region Keycloak

var postgresUser = builder.AddParameter("POSTGRES-USER");
var postgresPassword = builder.AddParameter("POSTGRES-PASSWORD");
var keycloakDb = "keycloak-db";

var postgresDb = builder
    .AddPostgres("postgres-db-keycloak", postgresUser, postgresPassword, 5432)
    .WithImage("postgres", "16.2")
    .WithVolume("microservicev2_posrgres.db.keycloak.volume", "/var/lib/postgresql/data").AddDatabase(keycloakDb);

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "25.0")
    .WithEnvironment("KC_HOSTNAME_PORT", "8080")
    .WithEnvironment("KC_HOSTNAME_STRICT_BACKCHANNEL", "false")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_HOSTNAME_STRICT_HTTPS", "false")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false")
    .WithEnvironment("KC_HEALTH_ENABLED", "true")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "password")
    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KC_DB_URL", "jdbc:postgresql://postgres-db-keycloak/keycloak_db")
    .WithEnvironment("KC_DB_USERNAME", postgresUser)
    .WithEnvironment("KC_DB_PASSWORD", postgresPassword)
    .WithArgs("start").WaitFor(postgresDb)
    .WithHttpEndpoint(8080, 8080, "keycloak-http-endpoint");
var keycloakEndpoint = keycloak.GetEndpoint("keycloak-http-endpoint");

#endregion

#region Catalog-Api

var mongoUser = builder.AddParameter("MONGO-USERNAME");
var mongoPassword = builder.AddParameter("MONGO-PASSWORD");
var catalogMongoDb = builder.AddMongoDB("mongo-db-catalog", 27030, mongoUser, mongoPassword)
    .WithImage("mongo:8.0-rc")
    .WithDataVolume("mongo.db.catalog.volume")
    .AddDatabase("catalogdb");
var catalogApi = builder.AddProject<Projects.MicroServiceV2_Catalog_API>("microservicev2-catalog-api");

catalogApi
    .WithReference(catalogMongoDb).WaitFor(catalogMongoDb)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion

#region Basket-Api
var redisPassword = builder.AddParameter("REDIS-PASSWORD");
var basketRedisDb = builder.AddRedis("redis-db-basket")
    .WithImage("redis:7.0-alpine")
    .WithDataVolume("redis.db.basket.volume")
    .WithPassword(redisPassword);

var basketApi = builder.AddProject<Projects.MicroServiceV2_Basket_API>("microservicev2-basket-api");

basketApi.WithReference(basketRedisDb).WaitFor(basketRedisDb)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion

#region Discount-Api

var discountMongoDb = builder.AddMongoDB("mongo-db-discount", 27034, mongoUser, mongoPassword)
    .WithImage("mongo:8.0-rc")
    .WithDataVolume("mongo.db.discount.volume")
    .AddDatabase("discount-db");
var discountApi = builder.AddProject<Projects.MicroServiceV2_Discount_API>("microservicev2-discount-api");

catalogApi.WithReference(discountMongoDb).WaitFor(discountMongoDb)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion

#region File-Api
var fileApi = builder.AddProject<Projects.MicroServiceV2_File_API>("microservicev2-file-api");
fileApi.WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Payment-Api
var paymentApi = builder.AddProject<Projects.MicroServiceV2_Payment_API>("microservicev2-payment-api");
paymentApi.WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Order-Api

var sqlServerPassword = builder.AddParameter("SA-PASSWORD");
var sqlServerOrderDb = builder.AddSqlServer("sqlserver-db-order")
    .WithPassword(sqlServerPassword)
    .WithDataVolume("sqlserver.db.order.volume")
    .AddDatabase("order-db-aspire");
var orderApi = builder.AddProject<Projects.MicroServiceV2_Order_API>("microservicev2-order-api");
orderApi.WithReference(sqlServerOrderDb).WaitFor(sqlServerOrderDb)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);

#endregion

#region Gateway-Api
builder.AddProject<Projects.MicroServiceV2_Gateway>("microservicev2-gateway")
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

#region Web

var web = builder.AddProject<Projects.MicroServiceV2_WEB>("microservicev2-web");
web.WithReference(basketApi)
    .WithReference(catalogApi)
    .WithReference(discountApi)
    .WithReference(orderApi)
    .WithReference(fileApi)
    .WithReference(paymentApi)
    .WithReference(keycloakEndpoint).WaitFor(keycloak);
#endregion

builder.Build().Run();
