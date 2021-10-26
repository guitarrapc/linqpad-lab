<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <IncludeLinqToSql>true</IncludeLinqToSql>
</Query>

// https://gist.github.com/guitarrapc/b90f3057597350c0e3e9571f503702cc
BenchmarkRunner.Run<BenchmarkNullable>();

[BenchmarkDotNet.Attributes.MemoryDiagnoser]
public class BenchmarkNullable
{
    [Benchmark]
    public void Slow()
    {
        NullaleSlow(0);
    }

    [Benchmark]
    public void Fast()
    {
        NullaleFast(0);
    }

    int? NullaleSlow(int? x)
    {
        for (int i = 0; i < 1000; i++)
        {
            x++;
        }
        return x;
    }

    int? NullaleFast(int? x)
    {
        if (x is int i32)
        {
            for (int i = 0; i < 1000; i++)
            {
                i32++;
            }
            x = i32;
        }
        return x;
    }
}