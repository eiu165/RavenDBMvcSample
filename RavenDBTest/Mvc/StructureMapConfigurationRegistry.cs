using System;
using System.Configuration;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using StructureMap.Configuration.DSL;

namespace RavenDBTest.Mvc
{
	public sealed class StructureMapConfigurationRegistry : Registry
	{
		public StructureMapConfigurationRegistry()
		{
			ForSingletonOf<IDocumentStore>().Use(() =>
				{
					var store = GetDocumentStore();
					store.Initialize();
					return store;
				});

			For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(context =>
			{
				return context.GetInstance<IDocumentStore>().OpenSession();
			});
		}

		private IDocumentStore GetDocumentStore()
		{
			var environmentName = ConfigurationManager.AppSettings.Get("Environment");

			switch (environmentName)
			{
				case "Debug":
					return GetEmbeddableDocumentStore();
				case "Release":
					return GetConnectionstringDocumentStore();
				default:
					throw new ArgumentException("environmentName");
			}
		}

		private IDocumentStore GetEmbeddableDocumentStore()
		{
			var store = new EmbeddableDocumentStore();
			store.DataDirectory = "App_Data";
			return store;
		}

		private IDocumentStore GetConnectionstringDocumentStore()
		{
			var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
			parser.Parse();

			var store = new DocumentStore
			{
				ApiKey = parser.ConnectionStringOptions.ApiKey,
				Url = parser.ConnectionStringOptions.Url,
			};

			return store;
		}
	}
}
