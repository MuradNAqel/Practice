using TestApp.Chapters.oop.Abstraction;

namespace TestApp.Chapters.OOP.Abstraction
{
    public static class Abstraction
    {
        // Abstract Behavior Example

        public static void RunAbstractionExample()
        {
            // Create a FlyingCar object
            FlyingCar flyingCar = new FlyingCar
            {
                Name = "SkyRunner",
                Speed = 150
            };
            flyingCar.Drive();

            // Use Vehicle's methods
            flyingCar.Start();  // From abstract class Vehicle
            flyingCar.Stop();   // From abstract class Vehicle

            Console.WriteLine();

            // Use IDrive's methods
            Console.WriteLine(" IDrive drivingCar = flyingCar");
            IDrive drivingCar = flyingCar; // Polymorphism through interface
            drivingCar.Drive();

            Console.WriteLine();

            // Use IFly's methods
            IFly flyingCarInterface = flyingCar; // Polymorphism through interface
            flyingCarInterface.Fly();
        }
    }
}
