<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
</Query>

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
