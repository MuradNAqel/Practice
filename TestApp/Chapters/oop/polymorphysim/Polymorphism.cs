namespace TestApp.Chapters.oop.polymorphysim
{
    public static class Polymorphism
    {
        public static void RunPolymorphism()
        {
            Animal cat = new Cat("Cat");
            Animal dog = new Dog("Dog");

            cat.Sound();
            dog.Sound();

            Animal animal = new Dog("Dog");
            animal.Sound();

            animal = new Cat("Cat");
            animal.Sound();
        }
    }
}
