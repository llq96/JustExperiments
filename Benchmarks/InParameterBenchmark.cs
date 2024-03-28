using BenchmarkDotNet.Attributes;

namespace ExamplesForInterview;

// Size: Just x,y,z
// | Method                    | Mean     | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
// |-------------------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
// | Test_UseProperties        | 25.58 ns | 0.284 ns | 0.266 ns |  1.00 |    0.00 |         - |          NA |
// | Test_UseProperties_WithIn | 84.94 ns | 0.621 ns | 0.485 ns |  3.32 |    0.05 |         - |          NA |
// | Test_UseFields            | 25.54 ns | 0.434 ns | 0.406 ns |  1.00 |    0.02 |         - |          NA |
// | Test_UseFields_WithIn     | 25.26 ns | 0.210 ns | 0.197 ns |  0.99 |    0.01 |         - |          NA |

// Size: x,y,z + 3 decimals
// | Method                    | Mean      | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
// |-------------------------- |----------:|---------:|---------:|------:|--------:|----------:|------------:|
// | Test_UseProperties        |  61.37 ns | 1.020 ns | 0.954 ns |  1.00 |    0.00 |         - |          NA |
// | Test_UseProperties_WithIn | 118.18 ns | 0.888 ns | 0.787 ns |  1.92 |    0.03 |         - |          NA |
// | Test_UseFields            |  57.85 ns | 0.855 ns | 0.800 ns |  0.94 |    0.02 |         - |          NA |
// | Test_UseFields_WithIn     |  58.39 ns | 1.153 ns | 1.328 ns |  0.96 |    0.03 |         - |          NA |

// Size: x,y,z + 30 decimals
// | Method                    | Mean     | Error   | StdDev  | Ratio | Allocated | Alloc Ratio |
// |-------------------------- |---------:|--------:|--------:|------:|----------:|------------:|
// | Test_UseProperties        | 559.8 ns | 1.41 ns | 1.25 ns |  1.00 |         - |          NA |
// | Test_UseProperties_WithIn | 591.7 ns | 0.92 ns | 0.81 ns |  1.06 |         - |          NA |
// | Test_UseFields            | 590.7 ns | 0.67 ns | 0.59 ns |  1.06 |         - |          NA |
// | Test_UseFields_WithIn     | 552.8 ns | 0.40 ns | 0.36 ns |  0.99 |         - |          NA |

[MemoryDiagnoser]
public class InParameterBenchmark
{
    [Benchmark(Baseline = true)]
    public void Test_UseProperties()
    {
        for (int i = 0; i < 100; i++)
        {
            MyVector3 vector3 = new MyVector3();
            var result = UseProperties(vector3);
        }
    }

    [Benchmark]
    public void Test_UseProperties_WithIn()
    {
        for (int i = 0; i < 100; i++)
        {
            MyVector3 vector3 = new MyVector3();
            var result = UseProperties_WithIn(in vector3);
        }
    }

    [Benchmark]
    public void Test_UseFields()
    {
        for (int i = 0; i < 100; i++)
        {
            MyVector3 vector3 = new MyVector3();
            var result = UseFields(vector3);
        }
    }

    [Benchmark]
    public void Test_UseFields_WithIn()
    {
        for (int i = 0; i < 100; i++)
        {
            MyVector3 vector3 = new MyVector3();
            var result = UseFields_WithIn(in vector3);
        }
    }

    private int UseProperties(MyVector3 vector3)
    {
        var x = vector3.X;
        var y = vector3.Y;
        var z = vector3.Z;
        x = vector3.X;
        y = vector3.Y;
        z = vector3.Z;
        x = vector3.X;
        y = vector3.Y;
        z = vector3.Z;
        var result = x + y + z;
        return result;
    }

    private int UseProperties_WithIn(in MyVector3 vector3)
    {
        var x = vector3.X;
        var y = vector3.Y;
        var z = vector3.Z;
        x = vector3.X;
        y = vector3.Y;
        z = vector3.Z;
        x = vector3.X;
        y = vector3.Y;
        z = vector3.Z;
        var result = x + y + z;
        return result;
    }

    private int UseFields(MyVector3 vector3)
    {
        var x = vector3._x;
        var y = vector3._y;
        var z = vector3._z;
        x = vector3._x;
        y = vector3._y;
        z = vector3._z;
        x = vector3._x;
        y = vector3._y;
        z = vector3._z;
        var result = x + y + z;
        return result;
    }

    private int UseFields_WithIn(in MyVector3 vector3)
    {
        var x = vector3._x;
        var y = vector3._y;
        var z = vector3._z;
        x = vector3._x;
        y = vector3._y;
        z = vector3._z;
        x = vector3._x;
        y = vector3._y;
        z = vector3._z;
        var result = x + y + z;
        return result;
    }

    private struct MyVector3
    {
        public int _x;
        public int _y;
        public int _z;
        // private decimal d0;
        // private decimal d1;
        // private decimal d2;
        // private decimal d3;
        // private decimal d4;
        // private decimal d5;
        // private decimal d6;
        // private decimal d7;
        // private decimal d8;
        // private decimal d9;
        // private decimal d10;
        // private decimal d11;
        // private decimal d12;
        // private decimal d13;
        // private decimal d14;
        // private decimal d15;
        // private decimal d16;
        // private decimal d17;
        // private decimal d18;
        // private decimal d19;
        // private decimal d20;
        // private decimal d21;
        // private decimal d22;
        // private decimal d23;
        // private decimal d24;
        // private decimal d25;
        // private decimal d26;
        // private decimal d27;
        // private decimal d28;
        // private decimal d29;

        public int X
        {
            get => _x;
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

        public MyVector3(int x, int y, int z)
        {
            // Console.WriteLine("Constructor ");
            _x = x;
            _y = y;
            _z = z;
        }

        public MyVector3()
        {
            // Console.WriteLine("Constructor parameterless");
        }

        public override string ToString()
        {
            return $"{_x} {_y} {_z}";
        }
    }
}