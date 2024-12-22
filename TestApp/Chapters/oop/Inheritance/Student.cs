using TestApp.Chapters.Chapter_3;

namespace TestApp.Chapters.oop.Inheritance
{
    public class Student : BaseEntity //Studdent can not inherit User class (Multible inheritance is not allowed) solved by Interface or Composition {ABSTRACTION   }
    {
        public decimal GPA { get; set; }
        public Course[] CoursesDone { get; set; }

    }
}
