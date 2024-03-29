namespace ExamplesForInterview;

public class AnimalsExample
{
    private delegate void MyAction<in T>(T obj);
    private delegate TResult MyFunc<out TResult>();

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
        MyAction<Cat> combCatAction;
        combCatAction = CombAnimal;
        combCatAction.Invoke(null);
    }

    public void Example2()
    {
        MyAction<Animal> combAnimalAction;
        // combAnimalAction = CombCat;
    }

    private void Example3()
    {
        MyFunc<Cat> getCatFunc;
        // getCatFunc = GetNewAnimal();
    }

    private void Example4()
    {
        MyFunc<Animal> getAnimalFunc;
        getAnimalFunc = GetNewCat;
    }

    public void Example5()
    {
        Cat[] cats = { GetNewCat() };
        // Warning: Co-variant array conversion from Cat[] to Animal[] can cause run-time exception on write operation
        Animal[] animals = cats;
        animals[0] = new Animal(); //ArrayTypeMismatchException
    }

    private void Example6()
    {
        MyAction<Cat> combCatAction = CombCat;
        MyAction<Animal> combAnimalAction = CombAnimal;
        combCatAction = combAnimalAction; //Need "in"
        // combAnimalAction = combCatAction; //Cannot convert
    }

    private void Example7()
    {
        MyFunc<Cat> getCatFunc = GetNewCat;
        MyFunc<Animal> getAnimalFunc = GetNewAnimal;
        // getCatFunc = getAnimalFunc; //Cannot convert
        getAnimalFunc = getCatFunc; //Need "out"
    }
}