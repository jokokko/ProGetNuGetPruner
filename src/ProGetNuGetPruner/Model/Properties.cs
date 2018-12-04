using System.Xml.Serialization;

namespace ProGetNuGetPruner.Model
{
	public sealed class Properties
	{
		[XmlElement("Version", Namespace = @"http://schemas.microsoft.com/ado/2007/08/dataservices")]
		public string Version { get; set; }
		[XmlElement("NormalizedVersion", Namespace = @"http://schemas.microsoft.com/ado/2007/08/dataservices")]
		public string NormalizedVersion { get; set; }
	}
}