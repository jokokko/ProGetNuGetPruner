using Oakton;

namespace ProGetNuGetPruner.Model
{
	public sealed class PrunePackagesInput
	{
		[Description("ProGet URI")]
		public string ProGetAddress { get; set; }
		[Description("ProGet username")]
		public string ProGetUsername { get; set; }
		[Description("ProGet user password")]
		public string ProGetPassword { get; set; }
		[Description("Feed name. Defaults to 'Default'")]
		public string FeedNameFlag { get; set; }
		[Description("Number of latest packages to retain. Defaults to 10")]
		public int? PackagesToRetainFlag { get; set; }
		[Description("Regex to match packages (name) to be deleted")]
		[FlagAlias("prf", true)]
		public string PackageRegexFlag { get; set; }
	}
}