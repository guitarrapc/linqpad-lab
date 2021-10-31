<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
</Query>

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
