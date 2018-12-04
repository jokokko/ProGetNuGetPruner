using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NuGet;
using ProGetNuGetPruner.Model;
using RestSharp;

namespace ProGetNuGetPruner.Services
{
	public sealed class PackagePruner
	{
		public void Prune(string progetUri, string user, string password, string feed, int packagesToRetain, string packageRegex = null)
		{
			if (progetUri == null)
			{
				throw new ArgumentNullException(nameof(progetUri));
			}

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			if (password == null)
			{
				throw new ArgumentNullException(nameof(password));
			}


			if (feed == null)
			{
				throw new ArgumentNullException(nameof(feed));
			}

			if (packagesToRetain < 0)
			{
				throw new ArgumentException(nameof(packagesToRetain));
			}

			var client = new RestClient(progetUri);
			var feedDeserializer = new XmlSerializer(typeof(Feed));
			var packageIdRegex = new Regex(@"Packages\(Id='(.+?)'", RegexOptions.Compiled);
			var deleteCredentials = new NetworkCredential(user, password);
			
			var searchReq =
				new RestRequest(
					$"nuget/{feed}/Search()?$filter=IsAbsoluteLatestVersion&searchTerm=''&targetFramework=''&includePrerelease=true&skip=0&&semVerLevel=2.0.0")
				{
					RequestFormat = DataFormat.Xml
				};

			var result = client.Get<Feed>(searchReq);

			var packagesFeed = (Feed)feedDeserializer.Deserialize(new StringReader(result.Content));

			var shouldDelete = new Func<string, bool>(_ => true);

			if (!string.IsNullOrEmpty(packageRegex))
			{
				var r = new Regex(packageRegex);
				shouldDelete = s => r.IsMatch(s);
			}

			foreach (var e in packagesFeed.Entries)
			{
				var idMatch = packageIdRegex.Match(e.Id);

				if (!idMatch.Success)
				{
					continue;
				}

				var id = idMatch.Groups[1].Value;

				if (!shouldDelete(id))
				{
					Console.WriteLine($"Skipping {id}");
					continue;
				}

				var versions = new RestRequest($"nuget/{feed}/FindPackagesById()?id='{id}'&semVerLevel=2.0.0");

				var data = client.Get(versions);

				var packageVersions = (Feed)feedDeserializer.Deserialize(new StringReader(data.Content));
				
				var packagesToDelete = packageVersions.Entries
					.Select(x => new { x, SemVer = SemanticVersion.Parse(x.Properties.NormalizedVersion) })
					.OrderByDescending(x => x.SemVer)
					.Skip(packagesToRetain).ToList();

				foreach (var p in packagesToDelete.Select(x => x.x))
				{
					var deleteRequest = new RestRequest($"nuget/default/{id}/{p.Properties.Version}")
					{
						Credentials = deleteCredentials
					};
			
					var deleteResponse = client.Delete(deleteRequest);

					if (deleteResponse.StatusCode == HttpStatusCode.OK)
					{
						Console.WriteLine($"Deleted {id}-{p.Properties.Version}");
					}
					else
					{
						Console.WriteLine($"Deleted {id}-{p.Properties.Version} failed: {deleteResponse.StatusCode} {deleteResponse.ErrorMessage}");
					}
				}
			}
		}
	}
}