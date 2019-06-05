using MvcMovie.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Mvc_Movie.Classes
{
    public static class MyExtensions
    {
        public static string ToRestrictionString(this List<Restriction> restrictions)
        {
            foreach (Restriction r in restrictions)
            {
                if (r.ISO_3166_1.ToLower() == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                {
                    return restrictions.FirstOrDefault().Certification;
                }
            }
            return null;
        }
    }
}