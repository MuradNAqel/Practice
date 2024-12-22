namespace TestApp.Chapters.oop.polymorphysim;

public class Cat : Animal
{
    public Cat(string name) : base(name)
    {
    }

    public override void Sound()
    {
        Console.WriteLine("A " + this.Name + " Meyaws");
    }
}
