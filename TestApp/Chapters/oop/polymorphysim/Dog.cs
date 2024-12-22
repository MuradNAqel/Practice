namespace TestApp.Chapters.oop.polymorphysim;

public class Dog : Animal
{
    public Dog(string name) : base(name)
    {
    }

    public override void Sound()
    {
        Console.WriteLine("A " + this.Name + " Barks");
    }
}
