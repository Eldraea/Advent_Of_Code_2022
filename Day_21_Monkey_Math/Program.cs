string input = File.ReadAllText("../../../input.txt");

Console.WriteLine(GetNumberYelledByRootMonkey(input, 1));
Console.WriteLine(GetNumberYelledByRootMonkey(input, 2));

Expr GetNumberYelledByRootMonkey(string input, int part)
{
    if(part ==1)
        return Parse(input, "root", false).Simplify();
    var expr = Parse(input, "root", true) as Equal;
    while (!(expr.left is Var))
    {
        expr = Solve(expr);
    }
    return expr.right;
}

Equal Solve(Equal eq) =>
    eq.left switch
    {
        Operation(Const l, "+", Expr r) => new Equal(r, new Operation(eq.right, "-", l).Simplify()),
        Operation(Const l, "*", Expr r) => new Equal(r, new Operation(eq.right, "/", l).Simplify()),
        Operation(Expr l, "+", Expr r) => new Equal(l, new Operation(eq.right, "-", r).Simplify()),
        Operation(Expr l, "-", Expr r) => new Equal(l, new Operation(eq.right, "+", r).Simplify()),
        Operation(Expr l, "*", Expr r) => new Equal(l, new Operation(eq.right, "/", r).Simplify()),
        Operation(Expr l, "/", Expr r) => new Equal(l, new Operation(eq.right, "*", r).Simplify()),
        Const => new Equal(eq.right, eq.left),
        _ => eq
    };

Expr Parse(string input, string name, bool part2)
{

    var context = new Dictionary<string, string[]>();
    foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.None))
    {
        var parts = line.Split(" ");
        context[parts[0].TrimEnd(':')] = parts.Skip(1).ToArray();
    }

    Expr buildExpr(string name)
    {
        var parts = context[name];
        if (part2)
        {
            if (name == "humn")
                return new Var("humn");
            else if (name == "root")
                return new Equal(buildExpr(parts[0]), buildExpr(parts[2]));
        }
        if (parts.Length == 1)
            return new Const(long.Parse(parts[0]));
        else
            return new Operation(buildExpr(parts[0]), parts[1], buildExpr(parts[2]));
    }

    return buildExpr(name);
}
interface Expr
{
    Expr Simplify();
}

record Const(long Value) : Expr
{
    public override string ToString() => Value.ToString();
    public Expr Simplify() => this;
}

record Var(string name) : Expr
{
    public override string ToString() => name;
    public Expr Simplify() => this;
}

record Equal(Expr left, Expr right) : Expr
{
    public override string ToString() => $"{left} == {right}";
    public Expr Simplify() => new Equal(left.Simplify(), right.Simplify());
}

record Operation(Expr left, string op, Expr right) : Expr
{
    public override string ToString() => $"({left}) {op} ({right})";
    public Expr Simplify()
    {
        return (left.Simplify(), op, right.Simplify()) switch
        {
            (Const l, "+", Const r) => new Const(l.Value + r.Value),
            (Const l, "-", Const r) => new Const(l.Value - r.Value),
            (Const l, "*", Const r) => new Const(l.Value * r.Value),
            (Const l, "/", Const r) => new Const(l.Value / r.Value),
            (Expr l, _, Expr r) => new Operation(l, op, r),
        };
    }
}