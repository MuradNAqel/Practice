namespace TestApp.Chapters.oop.polymorphysim
{
    public class Animal
    {
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
        }
        public virtual void Sound()
        {
            Console.WriteLine(Name + "has Sound");
        }
    }
}
