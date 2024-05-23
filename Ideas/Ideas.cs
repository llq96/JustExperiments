namespace ExamplesForInterview;

public class Ideas
{
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