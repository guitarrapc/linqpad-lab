<Query Kind="Program" />

// ref: https://twitter.com/badamczewski01/status/1449319543336222725?s=20
void Main()
{
    string? x = null;
	IsNullOrEmpty(x).Dump();
	NullableLength(x).Dump();
	PropLength(x).Dump();
	TypeOrEmpty(x).Dump();
	TypeAndEmpty(x).Dump();

}

bool IsNullOrEmpty(string? x) => string.IsNullOrEmpty(x);
bool NullableLength(string? x) => x?.Length == 0 || x is null;
bool PropLength(string? x) => x is not {Length: > 0};
bool TypeOrEmpty(string? x) => x is null or "";
bool TypeAndEmpty(string? x) => x is string and "" or null;
