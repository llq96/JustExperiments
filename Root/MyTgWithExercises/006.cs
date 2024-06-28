// public class SmartSwitch_006
// {
//     public static void Main()
//     {
//         int v1 = 1_000_000_000;
//         int v2 = 2_000_000_000;
//
//         unchecked
//         {
//             SmartSwitch(ref v1, ref v2);
//         }
//
//         Console.WriteLine($"{v1} {v2}");
//     }
//
//     private static void SmartSwitch(ref int a, ref int b)
//     {
//         checked
//         {
//             a = a + b;
//             b = a - b;
//             a = a - b;
//         }
//     }
// }

