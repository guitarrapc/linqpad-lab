<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
</Query>

BenchmarkRunner.Run<BenchmarkUnbox>();

[BenchmarkDotNet.Attributes.MemoryDiagnoser]
public class BenchmarkUnbox
{
    [Benchmark]
    public void Slow()
    {
        A(0);
    }

    [Benchmark]
    public void Fast()
    {
        B(0);
    }

    void A(int x)
    {
        object o = x;
        for (var i = 0; i < 1000; i++)
            Inc((int)o);
    }
    object Inc(int x) => ++x;

    void B(int x)
    {
        object o = x;
        for (var i = 0; i < 1000; i++)
            Unsafe.Unbox<int>(o)++;
    }
}