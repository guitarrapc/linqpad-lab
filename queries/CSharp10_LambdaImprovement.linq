<Query Kind="Statements" />

// https://gist.github.com/guitarrapc/28d79b2ded824253c724a262204598e0
// C# 10 Lambda not has Natural Typeing, return type and Lambda Aatribute. https://ufcpp.net/study/csharp/cheatsheet/ap_ver10/#lambda-improvement
// only work after .NET 6
var x = 0;
var f =
    [A]
    [return: B]
    static int ([C] int x)
    => x;
f(1).Dump(); // 1. not 0.

class AAttribute : Attribute{ }
class BAttribute : Attribute{ }
class CAttribute : Attribute{ }