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
			var environmentName = ConfigurationManager.AppSettings.Get("Environment");

			ForSingletonOf<IDocumentStore>().Use(() =>
				{
					var store = GetDocumentStore(environmentName);
					store.Initialize();
					return store;
				});

			For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(context =>
			{
				return context.GetInstance<IDocumentStore>().OpenSession();
			});
		}

		private IDocumentStore GetDocumentStore(string environmentName)
		{
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
			store.DataDirectory = "App_Date";
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
