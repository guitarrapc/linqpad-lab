<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
</Query>

// https://gist.github.com/guitarrapc/3611f37df9159c43795ade355c2db1e9
// Inline optimization and string Interpolation. https://twitter.com/badamczewski01/status/1453993632365699072?s=20
BenchmarkRunner.Run<BenchmarkInline>();

[BenchmarkDotNet.Attributes.MemoryDiagnoser]
public class BenchmarkInline
{
    [Benchmark]
    public void StringInterpolationInline() => PrintAInline(0);

    [Benchmark]
    public void StringInterpolationNoInline() => PrintANoInline(0);

    [Benchmark]
    public void ToStringInline() => PrintBInline(0);

    [Benchmark]
    public void ToStringNoInline() => PrintBNoInline(0);

    // default no-inlining (!?)
    string PrintANoInline(int x) => $"{x}";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    string PrintAInline(int x) => $"{x}";

    // default inlining
    string PrintBInline(int x) => x.ToString();
    [MethodImpl(MethodImplOptions.NoInlining)]
    string PrintBNoInline(int x) => x.ToString();
}
