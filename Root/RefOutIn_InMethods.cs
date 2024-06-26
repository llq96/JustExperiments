﻿using System.Drawing;
using System.Numerics;

namespace ExamplesForInterview;

public class RefOutIn_InMethods
{
    public void Run()
    {
        // int someInt = 0;
        //
        // MethodWithRef(ref someInt);
        // Console.WriteLine($"Value After {nameof(MethodWithRef)} : {someInt}");
        //
        // MethodWithOut(out someInt);
        // Console.WriteLine($"Value After {nameof(MethodWithOut)} : {someInt}");
        //
        // MethodWithIn(someInt);
        // Console.WriteLine($"Value After {nameof(MethodWithIn)} : {someInt}");

        MyVector3 vector3 = new MyVector3(2, 2, 2);
        // Vector3_MethodWithIn(vector3);
        // Console.WriteLine($"MyVector3 After {nameof(Vector3_MethodWithIn)} : {vector3}");
        Vector3_MethodWithIn(vector3);
        Console.WriteLine($"MyVector3 After {nameof(Vector3_MethodWithIn)} : {vector3}");
    }

    private void MethodWithRef(ref int value)
    {
        value = 1;
    }

    private void MethodWithOut(out int value)
    {
        value = 2; //Need
    }

    private void MethodWithIn(in int value)
    {
        // value = 3; //Can not
    }

    private void Vector3_MethodWithIn(in MyVector3 value)
    {
        // value._x = 1; //"Cannot modify struct member when accessed struct is not classified as a variable"
        // value.X = 1;  //"Cannot modify struct member when accessed struct is not classified as a variable"
        value.SetX(1); //Можно, но есть предупреждение "Possibly impure struct method called on readonly variable: struct value always copied before invocation"
        //Можно даже если get модифицирует поля, нет ни ошибки ни предупреждения, выполняется, но вводит в заблуждение
        int a = value.Y;
        Console.WriteLine(value._x);
        Console.WriteLine();
        Console.WriteLine(value);
    }
}

public struct MyVector3
{
    public int _x;
    private int _y;
    private int _z;

    public int X
    {
        get => ++_x;
        set => _x = value;
    }

    public int Y
    {
        get => _y;
        set => _y = value;
    }

    public int Z
    {
        get => _z;
        set => _z = value;
    }

    public void SetX(int x)
    {
        X = x;
    }

    public MyVector3(int x, int y, int z)
    {
        Console.WriteLine("Constructor ");
        _x = x;
        _y = y;
        _z = z;
    }

    public MyVector3()
    {
        Console.WriteLine("Constructor parameterless");
    }

    public override string ToString()
    {
        return $"{_x} {_y} {_z}";
    }
}