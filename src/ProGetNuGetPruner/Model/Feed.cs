using System.Xml.Serialization;

namespace ProGetNuGetPruner.Model
{
	[XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
	public sealed class Feed
	{
		[XmlElement(ElementName = "entry")] public Entry[] Entries { get; set; }
	}
}