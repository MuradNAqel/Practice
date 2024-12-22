namespace TestApp.Chapters.oop.Abstraction;

public abstract class Vehicle
{
    public string Name { get; set; }
    public int Speed { get; set; }

    public abstract void Start(); // Abstract method to be implemented by subclasses

    public void Stop()
    {
        Console.WriteLine($"{Name} is stopping.");
    }
}
