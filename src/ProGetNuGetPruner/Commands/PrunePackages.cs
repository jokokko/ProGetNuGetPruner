using System.Threading.Tasks;
using Oakton;
using ProGetNuGetPruner.Model;
using ProGetNuGetPruner.Services;

namespace ProGetNuGetPruner.Commands
{
	[Description("Prune NuGet packages from ProGet, retaining the provided number of the most recent packages", Name = "prune-packages")]
	// ReSharper disable once UnusedMember.Global
	public sealed class PrunePackages : OaktonAsyncCommand<PrunePackagesInput>
	{
		public override Task<bool> Execute(PrunePackagesInput input)
		{
			var pruner = new PackagePruner();

			var feed = string.IsNullOrEmpty(input.FeedNameFlag) ? "Default" : input.FeedNameFlag;
			var packagesToRetain = input.PackagesToRetainFlag ?? 10;

			pruner.Prune(input.ProGetAddress, input.ProGetUsername, input.ProGetPassword, feed, packagesToRetain, input.PackageRegexFlag);

			return Task.FromResult(true);
		}
	}
}