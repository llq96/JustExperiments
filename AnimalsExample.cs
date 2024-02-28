namespace ExamplesForInterview;

public class AnimalsExample
{
    public class Animal
    {
    }

    public class Cat : Animal
    {
    }

    public Animal GetNewAnimal() => new Animal();
    public Cat GetNewCat() => new Cat();

    public void CombAnimal(Animal animal)
    {
    }

    public void CombCat(Cat cat)
    {
    }

    public void Example1()
    {
        Action<Cat> combCatAction;
        combCatAction = CombAnimal;
    }

    public void Example2()
    {
        Action<Animal> combAnimalAction;
        // combAnimalAction = CombCat;
    }

    private void Example3()
    {
        Func<Cat> getCatFunc;
        // getCatFunc = GetNewAnimal();
    }

    private void Example4()
    {
        Func<Animal> getAnimalFunc;
        getAnimalFunc = GetNewCat;
    }
}