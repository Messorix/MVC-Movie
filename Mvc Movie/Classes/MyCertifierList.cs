using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Mvc_Movie.Classes
{
    public static class MyExtensions
    {
        public static string ToCertifierString(this List<Certifier> certifiers)
        {
            foreach (Certifier c in certifiers)
            {
                if (c.Certification.ISO_3166_1.ToLower() == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                {
                    return certifiers.FirstOrDefault().Certification.Certification;
                }
            }
            return null;
        }
    }
}