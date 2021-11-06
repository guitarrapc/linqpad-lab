<Query Kind="Statements" />

// https://gist.github.com/guitarrapc/da9b7f71d831c793f4c8c83cf06fa5d3
// Array Bounds Checks betwen .NET 5 and .NET 6. https://twitter.com/badamczewski01/status/1456711012099428359
// before .NET 5: Array Bound Check emmited. (BAD)
// after .NET 6: Array Bound Check removed. (GOOD)
void A(int[] a)
{
    if (a.Length > 3)
    {
        a[0] = 1;
        a[1] = 2;
        a[2] = 3;
    }
}