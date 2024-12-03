using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensionMethods
{
    public static T ElementAtOrDefault<T>(this IEnumerable<T> collection, int index, T defaultT)
    {
        T element = collection.ElementAtOrDefault(index);

        if (EqualityComparer<T>.Default.Equals(element))
            return defaultT;

        return element;
    }
}
