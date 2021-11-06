<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

// https://gist.github.com/guitarrapc/c045ba10ab2477688eb4e971a6ae942c
BenchmarkRunner.Run<BenchmarkInterpolatedHandler>();

[BenchmarkDotNet.Attributes.MemoryDiagnoser]
public class BenchmarkInterpolatedHandler
{
    [Benchmark]
    public void StringInterpolationInline() => PrintAInline(0);

    [Benchmark]
    public void StringInterpolationNoInline() => PrintANoInline(0);

    [Benchmark]
    public void ToStringInline() => PrintBInline(0);

    [Benchmark]
    public void ToStringNoInline() => PrintBNoInline(0);

    [Benchmark]
    public void InterpolatedStringHandlerInline() => InterpolatedStringHandlerInline(0);

    [Benchmark]
    public void InterpolatedStringHandlerNoInline() => InterpolatedStringHandlerNoInline(0);

    // default no-inlining (!?)
    string PrintANoInline(int x) => $"{x}";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    string PrintAInline(int x) => $"{x}";

    // default inlining
    string PrintBInline(int x) => x.ToString();
    [MethodImpl(MethodImplOptions.NoInlining)]
    string PrintBNoInline(int x) => x.ToString();

    // default no-inlining
    void InterpolatedStringHandlerNoInline(int x) => WriteLine($"{x}");
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void InterpolatedStringHandlerInline(int x) => WriteLine($"{x}");
    string WriteLine(DummyHandler handler) => handler.ToString();

    [InterpolatedStringHandler]
    public ref struct DummyHandler
    {
        public int LiteralLength { get; }
        public int FormattedCount { get; }

        private const int _formattedAverageLength = 5;
        private StringBuilder _builder;

        public DummyHandler(int literalLength, int formattedCount)
        {
            LiteralLength = literalLength;
            FormattedCount = formattedCount;
            _builder = new StringBuilder(literalLength + (formattedCount * _formattedAverageLength));
        }

        public void AppendLiteral(string s) { _builder.Append(s); }
        public void AppendLiteral(int s) { _builder.Append(s.ToString()); }
        public void AppendFormatted<T>(T x) { AppendLiteral(x.ToString()); }

        public override string ToString() => _builder.ToString();
    }
}
