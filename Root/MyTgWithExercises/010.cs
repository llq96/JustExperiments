// public class OperatorAddition_010
// {
//     public static void Main()
//     {
//         var final = new Data(10) + new Data(20);
//         Console.Write(final);
//     }
//
//     public class Data
//     {
//         private int _value;
//
//         public Data(int value)
//         {
//             _value = value;
//         }
//
//         public static Data operator +(Data first, Data second)
//         {
//             var sum = first._value + second._value;
//             return new Data(sum);
//         }
//
//         public override string ToString() => _value.ToString();
//     }
// }