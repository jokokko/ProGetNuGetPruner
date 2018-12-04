// From https://github.com/NuGet/NuGet2
// https://github.com/NuGet/NuGet2/blob/2.13/LICENSE.txt

using System;
using System.ComponentModel;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace NuGet
{
	public class SemanticVersionTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var stringValue = value as string;
			SemanticVersion semVer;
			if (stringValue != null && SemanticVersion.TryParse(stringValue, out semVer))
			{
				return semVer;
			}
			return null;
		}
	}
}
