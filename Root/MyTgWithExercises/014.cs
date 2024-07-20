// public class MethodSealed_014
// {
//     private static void Main()
//     {
//         var c = new C();
//         c.Write();
//     }
//
//     public abstract class A
//     {
//         public abstract void Write();
//     }
//
//     public class B : A
//     {
//         public sealed override void Write()
//             => Console.WriteLine("B");
//     }
//
//     public class C : B
//     {
//         public override void Test()
//             => Console.WriteLine("C");
//     }
// }