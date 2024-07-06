namespace ExamplesForInterview;

public class Ideas
{
//--------------------------------------  True
// Console.WriteLine(int.MinValue + 2 == int.MaxValue + 3);

//--------------------------------------  Глобальные и локальные переменные с одинаковым именем
//--------------------------------------  Static Dynamic

//--------------------------------------  Изменение упакованной структуры прямо внутри объекта в куче
// object o = 123;
// Console.WriteLine(o); //123
// Unsafe.Unbox<int>(o) = 456;
// Console.WriteLine(o); //456

//--------------------------------------  Для задачи: await нельзя использовать в unsafe контексте, но async unsafe возможен
// public static async unsafe Task<int> Test()
// {
//     return 1;
// }

//--------------------------------------  Интересная проверка типов, можно задачу из этого придумать
// int? nullableInt = null;
// Console.WriteLine(nullableInt is int); //False
// Console.WriteLine(nullableInt is Nullable<int>); //False
//
// nullableInt = 0;
// Console.WriteLine(nullableInt is int); //True
// Console.WriteLine(nullableInt is Nullable<int>); //True

//--------------------------------------   В foreach классы под делегат создаются для каждого элемента

// List<int> list = new() { 1, 2, 3 };
// List<Action> actions = new();
// for (int i = 0; i<list.Count;i++)
// {
//     actions.Add(() => Console.Write(i));
// }
//
// foreach (var i in list)
// {
//     actions.Add(() => Console.Write(i));
// }
//
// actions.ForEach(x => x.Invoke());

//--------------------------------------   XmlSerializer не сериализует hashtable, но в остальных случаях сериализация работает

// var t = new Hashtable();
// t.Add("Hi!", "I'm here");
// t.Add("Hm", "Yup");
//
// var serializer = new XmlSerializer(typeof(Hashtable));
//
// using (var sw = new StringWriter())
// {
//     serializer.Serialize(sw, t);
//
//     Console.WriteLine(sw.ToString());
// }

// Hashtable hashtable = new();
// hashtable.Add(1, 10);
// hashtable.Add(2, 20);
// hashtable.Add(3, 30);
// var json = JsonConvert.SerializeObject(hashtable);
// Console.WriteLine(json);
// var hashtable2 = JsonConvert.DeserializeObject<Hashtable>(json);
// Console.WriteLine(hashtable2.Count);
// foreach (DictionaryEntry entry in hashtable2)
// {
//     Console.WriteLine($"{entry.Value} {entry.Key}");
// }

// Dictionary<int, int> dictionary = new();
// dictionary.Add(1, 10);
// dictionary.Add(2, 20);
// dictionary.Add(3, 30);
// var json = JsonConvert.SerializeObject(dictionary);
// Console.WriteLine(json);
// var dictionary2 = JsonConvert.DeserializeObject<Dictionary<int, int>>(json);
// Console.WriteLine(dictionary2[1]);
// Console.WriteLine(dictionary2[2]);
// Console.WriteLine(dictionary2[3]);

//-------------------------------------- Неочевидно, что такое есть

// CancellationTokenSource.CreateLinkedTokenSource //

//-------------------------------------- Поэкспериментировать с try

// ThreadPool.QueueUserWorkItem((obj) =>
// {
//     Console.WriteLine(obj.GetType());
//     throw new Exception("QWe");
// }, new bool());
// CancellationTokenSource source = new();
//
// Thread.Sleep(1000);
// Console.WriteLine("End");

//-------------------------------------- Разница в ответах при разных типах второго дженерика

// private static void Main()
// {
//     SomeClass1.Value = 1;
//     SomeClass2.Value = 2;
//     Console.WriteLine(SomeClass1.Value);
//     Console.WriteLine(SomeClass2.Value);
// }
//
// //
// public class SomeClass<T>
// {
//     public static int Value;
// }
// public class SomeClass1 : SomeClass<int>;
// public class SomeClass2 : SomeClass<int>;
}