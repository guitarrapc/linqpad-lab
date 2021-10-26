<Query Kind="Statements" />

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