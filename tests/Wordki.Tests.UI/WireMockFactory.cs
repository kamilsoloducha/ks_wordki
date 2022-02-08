using System;
using System.Text;
using System.Text.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace Wordki.Tests.UI
{
    public static class WireMockFactory
    {
        public static WireMockServer Create(string url = "http://+:9999")
            => WireMockServer.Start(new WireMockServerSettings()
            {
                Urls = new[] { url },
                StartAdminInterface = true,
            });


        public static WireMockServer AddGetEndpoint(this WireMockServer server, string path, object response)
        {
            server.AddOptionEndpoint(path);

            server.Given(
                Request.Create()
                .WithPath(path)
                .UsingGet()
            )
            .RespondWith(
                Response.Create()
                .AddHeaders()
                .WithBody(SerializeObject(response))
                .WithStatusCode(200)
            );

            return server;
        }

        public static WireMockServer AddPutEndpoint(this WireMockServer server, string path, object response, Func<string, bool> bodyMatcher)
        {
            server.AddOptionEndpoint(path);

            server.Given(
                Request.Create()
                .WithPath(path)
                .WithBody(bodyMatcher)
                .UsingPut()
            )
            .RespondWith(
                Response.Create()
                .AddHeaders()
                .WithBody(SerializeObject(response))
                .WithStatusCode(200)
            );

            return server;
        }

        public static WireMockServer AddPostEndpoint(this WireMockServer server, string path, object response, Func<string, bool> bodyMatcher)
        {
            server.AddOptionEndpoint(path);

            server.Given(
                Request.Create()
                .WithPath(path)
                .WithBody(bodyMatcher)
                .UsingPost()
            )
            .RespondWith(
                Response.Create()
                .AddHeaders()
                .WithBody(SerializeObject(response))
                .WithStatusCode(200)
            );

            return server;
        }

        public static WireMockServer AddDeleteEndpoint(this WireMockServer server, string path)
        {
            server.AddOptionEndpoint(path);

            server.Given(
                Request.Create()
                .WithPath(path)
                .UsingDelete()
            )
            .RespondWith(
                Response.Create()
                .AddHeaders()
                .WithStatusCode(200)
            );

            return server;
        }

        public static WireMockServer AddOptionEndpoint(this WireMockServer server, string path)
        {
            server.Given(
                Request.Create()
                .WithPath(path)
                .UsingOptions()
            )
            .RespondWith(
                Response.Create()
                .AddHeaders()
            );
            return server;
        }
        private static IResponseBuilder AddHeaders(this IResponseBuilder builder)
         => builder.WithStatusCode(204)
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithHeader("Access-Control-Allow-Headers", "Authorization, Content-Type");

        private static string SerializeObject(object obj)
        => JsonSerializer.Serialize(obj,
                 new JsonSerializerOptions
                 {
                     PropertyNameCaseInsensitive = false,
                     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                 });

    }
}
