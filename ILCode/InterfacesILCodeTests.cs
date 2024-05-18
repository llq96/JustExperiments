namespace ExamplesForInterview;

public class InterfacesILCodeTests
{
    public void Run()
    {
    }

    public interface ITestInterface
    {
        //Const:
        public const float ConstField = 3.14f; // (Static) Const Field

        //Static:
        public static readonly string StaticField = "Text: "; // Static Field
        public static event Action StaticEvent; //Static Event

        public static int StaticProperty => 0; //Static Property

        static ITestInterface() => Console.WriteLine($"{nameof(ITestInterface)} Constructor"); //Static Constructor
        public static int StaticMethod() => 0; //Static Method
        public static float operator +(ITestInterface a, ITestInterface b) => 0; //Static Operator

        //Instance:
        public event Action InstanceEvent; // Instance Event
        public int InstanceValue => 0; // Instance Property
        public int InstanceMethod() => 0; // Instance Method
        public int this[int index] => 0; // Instance Indexer

        //Nested:
        public class NestedClass; //Nested Class
        public struct NestedStruct; //Nested Struct

        #region Virtual

        //Static:
        public static virtual event Action StaticVirtualEvent; //Static Virtual Event
        public static virtual int StaticVirtualProperty => 0; //Static Virtual Property
        public static virtual int StaticVirtualMethod() => 0; //Static Virtual Method
        public static virtual float operator -(ITestInterface a, ITestInterface b) => 0; //Static Virtual Operator

        //Instance:
        public virtual event Action InstanceVirtualEvent // Virtual Event
        {
            add { }
            remove { }
        }

        public virtual int InstanceVirtualValue => 0; // Instance Virtual Property
        public virtual int InstanceVirtualMethod() => 0; // Instance Virtual Method
        public virtual int this[float index] => 0; // Instance Virtual Indexer

        #endregion

        #region Abstract

        //Static:
        public static abstract event Action StaticAbstractEvent; //Static Abstract Event
        public static abstract int StaticAbstractProperty { get; } //Static Abstract Property
        public static abstract int StaticAbstractMethod(); //Static Abstract Method
        public static abstract float operator *(ITestInterface a, ITestInterface b); //Static Abstract Operator

        //Instance:
        public abstract event Action InstanceAbstractEvent;
        public abstract int InstanceAbstractValue { get; } // Instance Abstract Property
        public abstract int InstanceAbstractMethod(); // Instance Abstract Method
        public abstract int this[string index] { get; } // Instance Abstract Indexer

        #endregion

        //Not Compile:
        // public static implicit operator float(ITestInterface a) => 0;
        // public static explicit operator ITestInterface(int a) => null;
        // public implicit operator float(ITestInterface a) => 0;
        // public explicit operator ITestInterface(int a) => null;
        // public static abstract implicit operator float(ITestInterface a);
        // public static abstract explicit operator ITestInterface(int a);
        // public abstract implicit operator float(ITestInterface a);
        // public abstract explicit operator ITestInterface(int a);
    }

    public class ClassWithInterface : ITestInterface
    {
        private ITestInterface _testInterfaceImplementation;

        public event Action InstanceEvent
        {
            add => _testInterfaceImplementation.InstanceEvent += value;
            remove => _testInterfaceImplementation.InstanceEvent -= value;
        }

        public static event Action StaticAbstractEvent;
        public static int StaticAbstractProperty { get; }

        public static int StaticAbstractMethod()
        {
            throw new NotImplementedException();
        }

        static float ITestInterface.operator *(ITestInterface a, ITestInterface b)
        {
            throw new NotImplementedException();
        }

        public event Action InstanceAbstractEvent
        {
            add => _testInterfaceImplementation.InstanceAbstractEvent += value;
            remove => _testInterfaceImplementation.InstanceAbstractEvent -= value;
        }

        public int InstanceAbstractValue => _testInterfaceImplementation.InstanceAbstractValue;

        public int InstanceAbstractMethod()
        {
            return _testInterfaceImplementation.InstanceAbstractMethod();
        }

        public int this[string index] => _testInterfaceImplementation[index];
    }
}