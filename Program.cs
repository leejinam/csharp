﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Collections.Immutable;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;

namespace csharp
{
    delegate int myDele (int x);

    class Program
    {
        static int MySum(int number)
        {
            return number + number;
        }
        static int MyDouble (int x) => x * x;
        static void Main(string[] args)
        {
            myDele f = MySum;
            int numberResult = f(2);
            Console.WriteLine(numberResult);
            myDele f2 = MyDouble;
            numberResult = f2(10);
            numberResult = f2.Invoke(9);
            Console.WriteLine(numberResult);
            


            Console.WriteLine("Hello World!");

            Product product = new Product("bestGood");
            Console.WriteLine(product.Name);
            Console.WriteLine(product.ToString());

            Product oldProduct = new Product(name: "Good Product");
            Product newProduct = new Product(name: "Good Product");
            Console.WriteLine(oldProduct.GetHashCode());
            Console.WriteLine(newProduct.GetHashCode());
            Console.WriteLine(oldProduct.Equals(newProduct));

            Man tom = new Man("tom");
            tom.Shout();
            
            Random random = new Random();
            List<int> numberList = new List<int>();

            for (int i = 1; i <= 10; i++)
            {
                numberList.Add(random.Next(i * 10  - 9, i * 10));
            }

            foreach (int number in numberList)
            {
                Console.WriteLine(number);
            }

            foreach (int num in getNumber())
            {
                Console.WriteLine(num);
            }

            var list = new MyList();
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }

            IEnumerator it = list.GetEnumerator();
            while (it.MoveNext())
            {
                Console.WriteLine(it.Current);
            }

            IEnumerable<int> rangeNumList = Enumerable.Range(1, 100);

            // Pick 9 unique, random, numbers between 1..10 inclusive
            List<int> values = rangeNumList.ToList();
            Random r = new Random();
            List<int> UniqueRandoms = new List<int>();
            
            for (int i = 0; i < 9; ++i)
            {
                int idx = r.Next(0, values.Count);
                UniqueRandoms.Add(values[idx]);
                values.RemoveAt(idx);
            }

            foreach (int uNum in UniqueRandoms)
            {
                Console.WriteLine(uNum);
            }

            IEnumerable<Lecture> lectureList = Enumerable.Repeat<Lecture>(new Lecture
            {
                Id = Guid.NewGuid(),
                Name = "hello",
                Score = random.Next(1, 4),
            }, 10);

            List<Lecture> lecList = new List<Lecture>();
            Guid lecId = Guid.NewGuid();
            var lecName = Guid.NewGuid().ToString();
            for (int i = 0; i <= 10; i++)
            {
                lecList.Add(new Lecture
                {
                    Id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString(),
                    Score = random.Next(1,4),
                });
            }

            foreach (Lecture lec in lectureList)
            {
                //Console.WriteLine($"lec id {lec.Id}  \n lec name {lec.Name} \n lec score {lec.Score}");
                //Console.WriteLine($"lec's hascode : {lec.GetHashCode()}");
            }

            foreach (Lecture lec in lecList)
            {
                //Console.WriteLine($"lec id {lec.Id}  \n lec name {lec.Name} \n lec score {lec.Score}");
                //Console.WriteLine($"lec's hascode : {lec.GetHashCode()}");
            }

            lecList.Clear();
            for (int i = 0; i <= 10; i++)
            {
                lecList.Add(new Lecture
                {
                    Id = lecId,
                    Name = lecName,
                    Score = 3,
                });
            }

            foreach (Lecture lec in lecList)
            {
                //Console.WriteLine($"lec id {lec.Id}  \n lec name {lec.Name} \n lec score {lec.Score}");
                //Console.WriteLine($"lec's hascode : {lec.GetHashCode()}");
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("tom", "korea");
            Console.WriteLine(dict.GetHashCode());
            dict.Add("ken", "seoul");
            Console.WriteLine(dict.GetHashCode());

            Console.WriteLine(dict["tom"]);

            List<int> nList = new List<int> { 1, 10, 2, 5, 6, 8, 4, 3, 9, 7};
            //Comparer comparer = new Comparer(CultureInfo.CurrentCulture);
            nList.Sort((int a, int b) => a.CompareTo(b));

            foreach (int d in nList)
            {
                //Console.WriteLine(d);
            }

            nList.Sort((int a, int b) => b.CompareTo(a));

            foreach (int d in nList)
            {
                //Console.WriteLine(d);
            }

            lecList.Clear();
            for (int i = 0; i <= 10; i++)
            {
                lecList.Add(new Lecture
                {
                    Id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString(),
                    Score = random.Next(1,4),
                });
            }

            Console.WriteLine("CompareTo=====");
            lecList.Sort((Lecture a, Lecture b) => {
                if (a.Score == b.Score) return a.Name.CompareTo(b.Name);
                return a.Score.CompareTo(b.Score);
            });
            foreach (Lecture lec in lecList)
            {
                //Console.WriteLine($"lec id {lec.Id}  \n lec name {lec.Name} \n lec score {lec.Score}");
            }
            Console.WriteLine("OrderBy And ThenBy");
            lecList.OrderBy(x => x.Score).ThenBy(x => x.Name);
            foreach (Lecture lec in lecList)
            {
                //Console.WriteLine($"lec id {lec.Id}  \n lec name {lec.Name} \n lec score {lec.Score}");
            }


            IEnumerable<int> rangeTimeWindowNumber = Enumerable.Range(1, 10);
            List<int> rangeTimeWindowNumberList = rangeTimeWindowNumber.ToList();
            IEnumerable<int> rangeChargeDayNumber = Enumerable.Range(1, 10);
            List<int> rangeChargeDayNumberList = rangeChargeDayNumber.ToList();

            CancelllationPolicy cancelllationPolicy = new CancelllationPolicy();
            cancelllationPolicy.IsRefundable = true;

            for (int i = 0; i < 10; i++)
            {
                int idxTimeWindow = random.Next(0, rangeTimeWindowNumberList.Count - 1);
                int idxChargeDay = random.Next(0, rangeChargeDayNumberList.Count - 1);

                CancellationFeeRule cancellationFeeRule = new CancellationFeeRule
                {
                    TimeWindow = rangeTimeWindowNumberList[idxTimeWindow],
                    ChargeDays = rangeChargeDayNumberList[idxChargeDay],
                    ChargePercentage = random.Next(1, 10) * 10,
                };

                cancelllationPolicy.CancellationFeeRules.Add(cancellationFeeRule);

                rangeTimeWindowNumberList.RemoveAt(idxTimeWindow);
                rangeChargeDayNumberList.RemoveAt(idxChargeDay);
            }
            bool isSorted = false;
            isSorted = IsSorted(cancelllationPolicy.CancellationFeeRules);
            Console.WriteLine(isSorted);

            cancelllationPolicy.CancellationFeeRules.Clear();
            for (int i = 0; i < 10; i++)
            {
                CancellationFeeRule cancellationFeeRule = new CancellationFeeRule
                {
                    TimeWindow = i,
                    ChargeDays = 10 - i,
                    ChargePercentage = 100 - (i * 10),
                };
                cancelllationPolicy.CancellationFeeRules.Add(cancellationFeeRule);
            }

            isSorted = IsSorted(cancelllationPolicy.CancellationFeeRules);
            Console.WriteLine(isSorted);

            MyButton btn = new MyButton();
            btn.Click += new EventHandler(btn_Click);
            btn.Text = "Run";

            Dictionary<int, string> levelOfStudents = new Dictionary<int, string>();
            for (int i = 0; i < 500000; i++)
            {
                levelOfStudents.Add(i, Guid.NewGuid().ToString());
            }
            HashSet<int> studentList = new HashSet<int>();
            for (int i = 0; i < 500000; i++)
            {
                studentList.Add(i);
            }
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string levelLiteral = string.Empty;
            levelOfStudents.TryGetValue(45000, out levelLiteral);
            if (levelLiteral != "")
            {
                watch.Stop();
                Console.WriteLine("ElapsedMilliseconds : " + watch.ElapsedMilliseconds);
            }
            watch.Reset();
            watch.Start();
            int level = 0;
            studentList.TryGetValue(45000, out level);
            if (level != 0)
            {
                watch.Stop();
                Console.WriteLine("ElapsedMilliseconds : " + watch.ElapsedMilliseconds);
            }

            int.TryParse("100", out int resultParse);
            Console.WriteLine(resultParse);

            int.TryParse("209", out var resultNumber);
            Console.WriteLine(resultNumber);

            var expression = new {
                Name = "Tome",
                Age = 10,
                Id = Guid.NewGuid(),
            };
            Console.WriteLine(expression.Name.GetType().Name);
            
            var myList = new {
                Items = new[] { new { Id = Guid.NewGuid(), Name = "Tom" }, new { Id = Guid.NewGuid(), Name = "John" }, new { Id = Guid.NewGuid(), Name = "Bob" }, new { Id = Guid.NewGuid(), Name = "John" }, new { Id = Guid.NewGuid(), Name = "Patrick" } },
            };
            var resultLinq = from item in myList.Items
            where item.Name == "John"
            select item.Id;

            Console.WriteLine(resultLinq.Count());

            var children = new Child();
            children.Say();
            children.Move();

            var parent = children as Parent;
            parent.Say();
            parent.Move();
            
            var child = parent as Child;
            child.Say();
            child.Move();


            int numA = int.MaxValue;
            Console.WriteLine(numA + 1);
            Console.WriteLine(unchecked(numA + 1));
            
            // Action Delegate
            Action<int> myAct1 = delegate (int num)
            {
                Console.WriteLine(num * 200);
            };
            myAct1(10);

            // Action Lamda
            Action<int> myAct2 = (num) => Console.WriteLine(num * 300);
            myAct2(7);


            Func<int, int> myFunc = (num) =>
            {
                return num * 2;
            };

            Func<string, string, string> myFunc2 = (s1, s2) =>
            {
                return $"{s1} : {s2}";
            };


            Predicate<string> myPred = (s) =>
            {
                return int.TryParse(s, out int number);
            };


            // Switch Patteren Matching
            object[] data = { 1, null, 10, new Circle(5), new Man("Lee"), "", new Rect(98), };

            foreach (var item in data)
            {
                switch(item)
                {
                    case null:
                        Console.WriteLine("null");
                        break;
                    case Circle c:
                        Console.WriteLine($"{c} is Circle");
                        break;
                    case Man m:
                        Console.WriteLine($"{m} is Man");
                        break;
                    case Rect re:
                        Console.WriteLine($"{re} is Rect");
                        break;
                }
            }

            // Func , Predicate
            Console.WriteLine(myFunc(100));
            Console.WriteLine(myFunc2("hello", "jinam"));
            Console.WriteLine(myPred("1001"));

            // Tuple
            Console.WriteLine("TupleData : " + MyTuple(Enumerable.Range(1, 201).ToList()));
            (int sum, int count) =  MyTuple(Enumerable.Range(1, 201).ToList());
            Console.WriteLine($"Tuple Deconstruction \n sum: {sum} \n count: {count}");
            Console.WriteLine("Local Function : " + MyLocalFunction(81, 100));
            

            // Tuple 7 LeftNamed
            (int count, string label) leftNamedTupleCsharp7 = (98, "myTuple");
            Console.WriteLine($"count: {leftNamedTupleCsharp7.count} label: {leftNamedTupleCsharp7.label}");

            // Tuple 7 RightNamed
            var rightNamedTupleCsharp7 = (myCount: 100, myLabel: "myTuple");
            Console.WriteLine($"count: {rightNamedTupleCsharp7.myCount} label: {rightNamedTupleCsharp7.myLabel}");

            // defines the type returned
            Func<IEnumerable<int>, (int Min, int Max)> minMaxFunc = (numbers) => {
                int min = int.MaxValue;
                int max = int.MinValue;
                foreach(var n in numbers)
                {
                    min = (n < min) ? n : min;
                    max = (n > max) ? n : max;
                }
                return (min, max);
            };

            // return tuple
            var range = minMaxFunc(Enumerable.Range(1, 100));
            Console.WriteLine($"range's min value is {range.Min} and max valud is {range.Max}");

            // unpackage the members of a tuple
            (int minValue, int maxValue) = minMaxFunc(Enumerable.Range(1, 500));
            Console.WriteLine($"range's minValue is {minValue} and maxValue is {maxValue}");

            // discards value
            var (_, maxValueAfterDiscards) = minMaxFunc(Enumerable.Range(0, 1000));
            Console.WriteLine($"maxValue is {maxValueAfterDiscards}");
            
            // Tuple 7.1 Inferred tuple element names
            int tupleCount = 10;
            string tupleLabel = "myTuple";
            var pair = (tupleCount, tupleLabel);
            Console.WriteLine($"count: {pair.tupleCount} label: {pair.tupleLabel}");
            Console.WriteLine($"{pair.tupleCount.GetType().Name}, {pair.tupleLabel.GetType().Name}");
            pair.tupleCount = 100;

            // Change Elements value in Tuple
            (int firstNumber ,int secondNumber) = (10, 50);
            Console.WriteLine($"First is {firstNumber} and Second is {secondNumber}");
            (secondNumber, firstNumber) = (firstNumber, secondNumber);
            Console.WriteLine($"First is {firstNumber} and Second is {secondNumber}");
            int firstNum = 10;
            int secondNum = 200;
            (secondNum, firstNum) = (firstNum, secondNum);
            Console.WriteLine($"First is {firstNum} and Second is {secondNum}");

            var tupleData = (1, 5, 6, 1, 9, 1, 2, 4, 5, 6, 7, 11, 100, 56, 12, 18, 921, 1081, 23, 1, 23, 0);
            Console.WriteLine(tupleData.Item22);


            var pairByAnonymousType = new 
            {
                count = 10,
                label = "label",
            };
            Console.WriteLine($"count: {pairByAnonymousType.count} label: {pairByAnonymousType.label}");
        



            
            //Deconstruct
            var studentNew = new Student("Brian", "Computer Science", "waldo@gmail.com");
            var (name, dept, email, id) = studentNew;
            Console.WriteLine($"name : {name} \n dept : {dept} \n email : {email} \n id : {id}");

            // ref local
            int numberA = 100;
            ref int numberB = ref numberA;
            numberB = 200;
            Console.WriteLine($"numberA is {numberA}");


            // ref return
            int[] scores = new int[] { 90, 80, 100, 40, 56, 87, 100, 72, 60 };
            ref int scoreData = ref studentNew.GetScore(scores, 2);
            Console.WriteLine($"score data : {scoreData}");
            scoreData = 90;
            Console.WriteLine($"score data : {scoreData}");


            Algorithm month = new Algorithm();

            DateTime ddd = new DateTime(2016, 7, 22);
            Console.WriteLine(month.GetDay(7, 22));


            DateTime firstDate = new DateTime(2016, 1, 1);
            for (int i = 1; i <= 366; i++)
            {
                if (month.GetDay(firstDate.Month, firstDate.Day) != firstDate.DayOfWeek.ToString("").Substring(0, 3).ToUpper())
                {
                    Console.WriteLine($"{firstDate} {firstDate.DayOfWeek} / {month.GetDay(firstDate.Month, firstDate.Day)} error");
                }
                firstDate = firstDate.AddDays(1);
            }

            Algorithm stringSort = new Algorithm();
            Console.WriteLine(stringSort.GetSortedString("Zbcdefg"));


            //Console.ReadLine();

            // dynamic data
            var _commands = new List<dynamic>
            {
                new
                {
                    room_type_id = Guid.NewGuid(),
                    date_conditions = new List<dynamic>
                    {
                        new
                        {
                            start_date = new DateTime(2018, 1, 1),
                            end_date = new DateTime(2018, 1, 7),
                            days_of_week = new string[] { "sunday", "monday", "friday" },
                        },
                        new
                        {
                            start_date = new DateTime(2018, 2, 8),
                            end_date = new DateTime(2018, 2, 14),
                            days_of_week = new string[] { "thursday", "saturday" },
                        },
                    },
                    availibility = 100,
                },
            };

            Console.WriteLine(_commands[0].room_type_id);
            foreach (dynamic command in _commands)
            {
                foreach (dynamic dateCondition in command.date_conditions)
                {
                    Console.WriteLine(dateCondition.start_date);
                    Console.WriteLine(dateCondition.end_date);
                }
            }

            var builder = ImmutableArray.CreateBuilder<int>();
            builder.Add(10);
            builder.Add(20);
            builder.Add(30);
            var listImmutable = ImmutableArray.CreateRange(new List<int> { 100, 200, 300 });
            ImmutableArray<int> array = builder.ToImmutable();

            var numberListC = new List<int>{ 100, 200, 300 };
            numberListC.ForEach(item => Console.WriteLine(item += 200));
            foreach(var n in numberListC)
            {
                Console.WriteLine(n);
            }

            Func<int, int> fNumber = (number) =>
            {
                return number * 2;
            };

            fNumber(100);

            var dNumber = myTestDelegate(func: fNumber, 100);
            Console.WriteLine(dNumber);

            Algorithm aTest = new Algorithm("HelloWorld");
            Console.Write(aTest.Name);
            // aTest.Name = "test"; // error


            // Tuple Compare 7.3
            if (("Seoul", 1000) == ("Seoul", 1000))
            {
                Console.WriteLine("Same");
            }
            else
            {
                Console.WriteLine("Different");
            }
            
            // ref and out both are passed as reference
            int thirdNumber, forthNumber = 5;
            getDouble(out thirdNumber, ref forthNumber);
            Console.WriteLine(thirdNumber);
            Console.WriteLine(forthNumber);


            // ref return
            var ns = new NumberStore();
            ref var searchedNumber = ref ns.FindNumber(3);
            Console.WriteLine(searchedNumber);
            searchedNumber *= 2;
            Console.WriteLine(searchedNumber);


            // Json Desializing
            var file = Path.Combine("test.json");
            var text = File.ReadAllText(file);
            var result = Regex.Replace(text, @"\/\*[\s\S]*?\*\/|([^:|^""]|^)\/\/.*", "");
            var json = JsonConvert.SerializeObject(result.Replace(@"\", ""));
            //result.Replace(@"\", "")

            var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        MetadataPropertyHandling = MetadataPropertyHandling.Ignore
                    };
            var dResult = JsonConvert.DeserializeObject<AppSettings>(text.Replace("\n", "").Replace(@"\", ""), settings);
            Console.WriteLine("UseTranspiling: " + dResult.UseTranspiling);
            
            StreamReader sr = new StreamReader(file);
            string jsonString = sr.ReadToEnd();
            //var ser = new JavaScriptSerializer();
            var ro = JsonConvert.DeserializeObject<AppSettings>(jsonString);

            Comparison<string> comp = CompareLength;
            Comparison<string> comparer = (left, right) => left.Length.CompareTo(right.Length);
            var nameList = new List<string>
            {
                "tiger", "lion"
            };
            nameList.Sort(comparer);
            foreach(var n in nameList)
            {
                Console.WriteLine(n);
            }

            Expression<Func<int, int>> addFive = (num) => num + 5;
            if(addFive.NodeType == ExpressionType.Lambda)
            {
                var lambdaExp = (LambdaExpression)addFive;
                var param = lambdaExp.Parameters.First();

                Console.WriteLine(param.Name);
                Console.WriteLine(param.Type);
            }

            var one = Expression.Constant(1, typeof(int));
            var two = Expression.Constant(2, typeof(int));

            var addtionResult = Expression.Add(one, two);
            var subsResult = Expression.Subtract(one, two);
            var multiplyResult = Expression.Multiply(one, two);

            var myWishes = new List<WishList>()
            {
                new WishList { Name = "Swimming", Priority = 1 },
                new WishList { Name = "Traveling Europe", Priority = 3 },
                new WishList { Name = "Buying Car", Priority = 2 },
                new WishList { Name = "Moving Apartment", Priority = 4 },
            };

            myWishes.Sort(new CompareWish());
            foreach (var wish in myWishes)
            {
                Console.WriteLine(wish.Name);
            }


            Console.WriteLine(addtionResult);
            Console.WriteLine(subsResult);
            Console.WriteLine(multiplyResult);



            /* Include this "using" directive...
            using Microsoft.Azure.Documents.Client;
            */

            /* var cosmosUri = new Uri("https://8a649c4e-0ee0-4-231-b9ee.documents.azure.com:443/");
            DocumentClient client = new DocumentClient(cosmosUri, "BjMViNLJLvmI6JMKRspTubsOmzw0falt9m1D70YjlQTWqejMs7qos8lzaFEO40aiojn93wogVvVt7A30diyu7Q==");
            Uri documentUri = UriFactory.CreateDocumentUri("ToDoList", "Items", "student_001");
            var todoItem = client.ReadDocumentAsync<ToDoItem>(documentUri);
            var todoResult = todoItem.GetAwaiter().GetResult().Document;

            // Create Database
            var cosmosClient = new CosmosClient(
                "https://8a649c4e-0ee0-4-231-b9ee.documents.azure.com:443/", 
                "BjMViNLJLvmI6JMKRspTubsOmzw0falt9m1D70YjlQTWqejMs7qos8lzaFEO40aiojn93wogVvVt7A30diyu7Q==", 
                new CosmosClientOptions()
                {
                    ApplicationRegion = Regions.EastUS2,
                });
            
            var database = client.CreateDatabaseAsync(
                new Microsoft.Azure.Documents.Database
                { 
                    Id = "JohnDb" 
                }).Result;

            Console.WriteLine(todoResult.Name);
            Console.ReadLine();
            System.DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss").Substring(0, 2); */


            // C# 7 - Patteren Matching
            IEnumerable<Int32> numberList2 = Enumerable.Range(0, 10).AsEnumerable<Int32>();
            int sumOfNumber = 1;
            foreach (var n in numberList2)
            {
                switch(n)
                {
                    case Int32 greaterThan5 when greaterThan5 > 5:
                        sumOfNumber *= n;
                        break;
                    case Int32 smallerThan3 when smallerThan3 < 3:
                        sumOfNumber -= n;
                        break;
                }
            }
            Console.WriteLine(sumOfNumber);

            // Define a provider and two observers.
            Console.WriteLine("observable test...");
            LocationTracker provider = new LocationTracker();
            LocationReporter reporter1 = new LocationReporter("FixedGPS");
            reporter1.Subscribe(provider);
            LocationReporter reporter2 = new LocationReporter("MobileGPS");
            reporter2.Subscribe(provider);

            provider.TrackLocation(new Location(47.6456, -122.1312));
            reporter1.Unsubscribe();
            provider.TrackLocation(new Location(47.6677, -122.1199));
            provider.TrackLocation(null);
            provider.EndTransmission();
        }

        static int CompareLength(string left, string right) =>
            left.Length.CompareTo(right.Length);

        static void getDouble(out int number, ref int secondNumer)
        {
            secondNumer = secondNumer * secondNumer;
            // out variable is write only
            number = secondNumer * 2;
        }

        static int myTestDelegate(Func<int, int> func, int n)
        {
            return func(n) + 1000;
        }

        static int MyLocalFunction(int number, int number2)
        {
            return number + MyFormula(number);
            int MyFormula(int n)
            {
                return n.GetHashCode() % 2 + number2;
            }
        }

        static (int, int)MyTuple(List<int> numberList)
        {
            return (numberList.Sum(), numberList.Count);
        }


        static void btn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clicked");
        }

        static bool IsSorted(List<CancellationFeeRule> feeRules)
        {
            for (int i = 1; i < feeRules.Count; i++)
            {
                if (feeRules[i - 1].TimeWindow.CompareTo(feeRules[i].TimeWindow) > 0)
                {
                    return false;
                }

                if (feeRules[i - 1].ChargeDays.CompareTo(feeRules[i].ChargeDays) < 0)
                {
                    return false;
                }

                if (feeRules[i - 1].ChargeDays.CompareTo(feeRules[i].ChargeDays) == 0)
                {
                    if (feeRules[i - 1].ChargePercentage.CompareTo(feeRules[i].ChargePercentage) <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static IEnumerable<int> getNumber()
        {
            yield return 10;
            yield return 20;
            yield return 30;
            yield return 40;
        }
    }
}
