<Query Kind="Statements" />

// https://gist.github.com/guitarrapc/da9b7f71d831c793f4c8c83cf06fa5d3
// before .NET 5: Array Bound Check emitted. (BAD)
// after .NET 6: Array Bound Check removed. (GOOD)
void A(int[] a)
{
    if (a.Length < 3) // if > 3. Array Bounds check are emitted.
        return;
    a[0] = 1;
    a[1] = 2;
    a[2] = 3;
}