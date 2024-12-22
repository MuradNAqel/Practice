namespace TestApp.Chapters.oop.Inheritance;

public class DiamondProblem
{

}

public class A
{
    public virtual void Print()
    {
        Console.WriteLine("A Implementation");
    }
}

public class B : A
{
    public override void Print()
    {
        Console.WriteLine("B Implementation");
    }
}

public class C : A
{
    public override void Print()
    {
        Console.WriteLine("C Implementation");
    }
}

public class D : B//, C If multiple inheritance was allowed 
{
    public override void Print()
    {
        base.Print();  //Which print will be implemented  B Implementation or C Implementation?
    }
}
//This ambiguity is what makes multible inheritance impossible 
//And we solve it by interfaces