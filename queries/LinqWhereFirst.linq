<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
</Query>

// https://gist.github.com/guitarrapc/e400d5bc932b5368e28113829895c189
// Linq Where + First & First benchmark. https://twitter.com/badamczewski01/status/1454762216083836928
BenchmarkRunner.Run<BenchmarkLinqWhereFirst>();

[BenchmarkDotNet.Attributes.MemoryDiagnoser]
public class BenchmarkLinqWhereFirst
{
    private readonly int[] data;
    public BenchmarkLinqWhereFirst()
    {
        data = Enumerable.Range(1, 1000).ToArray();
    }
    [Benchmark]
    public int WhereFirst() => data.Where(x => x > 100).First();

    [Benchmark]
    public void First() => data.First(x => x > 100);
}
