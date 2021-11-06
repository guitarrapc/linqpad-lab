<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
</Query>

// https://gist.github.com/guitarrapc/d0b2b686567ff8e4a40bc324f31df2e3
// Unbox T with Unsafe.Unbox<T>(val) will remove allocation. https://twitter.com/badamczewski01/status/1452918677855145988
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