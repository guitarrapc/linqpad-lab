<Query Kind="Statements">
  <Namespace>System.Runtime.CompilerServices</Namespace>
</Query>

// ref: https://gist.github.com/akarpov89/03c53ea537451c7790341d7f697a89ec
using System.Runtime.CompilerServices;
using static ParsingExtensions;
MatchNameAge("Name: Jone Doe; Age: 10"); // Jone Doe: 10
MatchNameAge("Name: 10Jone Doe; Age: 10"); // 10Jone Doe: 10
MatchNameAge("Name: Jone Doe 100000; Age: 10"); // Jone Doe 100000: 10
MatchNameAge("Name: Jone Doe ;; Age: 10"); // Jone Doe ;: 10
MatchNameAge("Namesss: Jone Doe; Age: 10"); // Does not match
MatchNameAge("Name: Jone Doe ;Age: 101s"); // Does not match.
MatchNameAge("Name: Jone Doe; Age: 10; Foo: bar"); // Does not match.

void MatchNameAge(string input)
{
    string? name = null;
    int age = 0;

    if (input.TryParse($"Name: {PlaceHolder(ref name)}; Age: {PlaceHolder(ref age)}"))
    {
        Console.WriteLine($"{name}: {age}");
    }
    else
    {
        Console.WriteLine($"Does not match.");
    }
}

public static class ParsingExtensions
{
    public static PlaceholderCell<T?> PlaceHolder<T>(ref T? arg) => new(ref arg);
    public static bool TryParse(this string input, [InterpolatedStringHandlerArgument("input")] ref TryParseHandler handler)
    {
        return handler.Ok;
    }
}

public readonly unsafe ref struct PlaceholderCell<T>
{
    private readonly void* _ptr;
    public PlaceholderCell(ref T? arg) => _ptr = Unsafe.AsPointer(ref arg);
    
    public bool IsNull => _ptr is null;
    public ref T Get() => ref Unsafe.AsRef<T>(_ptr);
    public void Set(T arg) => Unsafe.Write(_ptr, arg);
}

[InterpolatedStringHandler]
public ref struct TryParseHandler
{
    private ReadOnlySpan<char> _input;
    private PlaceholderCell<string?> _substringPlaceholder;

    public TryParseHandler(int literalLength, int formattedCount, ReadOnlySpan<char> input)
    {
        _input = input;
        _substringPlaceholder = default;
        Ok = true;
    }

    public bool Ok { get; private set; }

    private bool Failed()
    {
        Ok = false;
        return false;
    }

    public bool AppendLiteral(string literal)
    {
        if (!_substringPlaceholder.IsNull)
        {
            var index = _input.IndexOf(literal, StringComparison.Ordinal);
            if (index < 1)
                return Failed();

            _substringPlaceholder.Set(_input.Slice(0, index).ToString());
            _substringPlaceholder = default;

            _input = _input.Slice(index + literal.Length);

            return true;
        }

        if (_input.StartsWith(literal, StringComparison.Ordinal))
        {
            _input = _input.Slice(literal.Length);
            return true;
        }

        return Failed();
    }

    public bool AppendFormatted(PlaceholderCell<string?> placeholder)
    {
        if (_input.Length == 0)
            return Failed();

        if (!_substringPlaceholder.IsNull)
            return Failed();

        _substringPlaceholder = placeholder;

        return true;
    }

    public bool AppendFormatted(PlaceholderCell<int> placeholder)
    {
        if (_input.Length == 0)
            return Failed();

        var startPos = 0;
        while (startPos < _input.Length && !char.IsDigit(_input[startPos]))
            startPos++;

        if (startPos >= _input.Length)
            return Failed();

        var endPos = startPos;
        while (endPos < _input.Length - 1 && char.IsDigit(_input[endPos + 1]))
            endPos++;

        var numberSlice = _input.Slice(startPos, endPos - startPos + 1);
        if (!int.TryParse(numberSlice, out var value))
            return Failed();

        placeholder.Set(value);

        if (!_substringPlaceholder.IsNull)
        {
            _substringPlaceholder.Set(_input.Slice(0, startPos).ToString());
            _substringPlaceholder = default;
        }

        _input = _input.Slice(endPos + 1);

        return true;
    }
}