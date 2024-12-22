namespace TestApp.Chapters.oop.Abstraction;

public class FlyingCar : Vehicle, IDrive, IFly
{
    public override void Start()
    {
        Console.WriteLine($"{Name} is starting.");
    }

    public void Drive()
    {
        Console.WriteLine($"{Name} is driving on the road at {Speed} km/h.");
    }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying in the sky at {Speed} km/h.");
    }
}
