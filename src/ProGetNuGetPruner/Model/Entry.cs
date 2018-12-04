using System.Xml.Serialization;

namespace ProGetNuGetPruner.Model
{
	public sealed class Entry
	{
		[XmlElement("id")] public string Id { get; set; }

		[XmlElement("properties", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
		public Properties Properties { get; set; }
	}
}