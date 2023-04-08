using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ForeachLoopBenchmarks>();
BenchmarkRunner.Run<EmptyListBench>();

public class ForeachLoopBenchmarks
{
    private readonly struct TestStruct
    {
        public int Value1 { get; init; }
        public long Value2 { get; init; }
        public string Value3 { get; init; }
    }

    private class TestObj
    {
        public int Value1 { get; init; }
        public long Value2 { get; init; }
        public string Value3 { get; init; }
    }
    
    private const int NumberOfItems = 100_000;
    private byte[] _randomArray;
    private TestStruct[] _structs;
    private List<TestStruct> _listStructs;
    private TestObj[] _objects;
    private List<TestObj> _listObjects;

    [GlobalSetup]
    public void Setup()
    {
        var rnd = new Random();
        _randomArray = new byte[NumberOfItems];
        rnd.NextBytes(_randomArray);

        _structs = new TestStruct[NumberOfItems];
        for (var i = 0; i < NumberOfItems; i++)
        {
            _structs[i] = new TestStruct
            {
                Value1 = rnd.Next(),
                Value2 = rnd.NextInt64(),
                Value3 = new string((char) rnd.Next(), rnd.Next(1, 200))
            };
        }

        _listStructs = _structs.ToList();
        
        _objects = new TestObj[NumberOfItems];
        for (var i = 0; i < NumberOfItems; i++)
        {
            _objects[i] = new TestObj
            {
                Value1 = rnd.Next(),
                Value2 = rnd.NextInt64(),
                Value3 = new string((char) rnd.Next(), rnd.Next(1, 200))
            };
        }

        _listObjects = _objects.ToList();
    }

    [Benchmark]
    public int ForLoopByte()
    {
        var j = 0;
        for (var i = 0; i < NumberOfItems; i++)
        {
            if (_randomArray[i] == 1)
            {
                j++;
            }
        }

        return j;
    }

    [Benchmark]
    public int ForeachLoopByte()
    {
        var j = 0;
        foreach (var b in _randomArray)
        {
            if (b == 1)
            {
                j++;
            }
        }

        return j;
    }
    
    [Benchmark]
    public int ForLoopStruct()
    {
        var j = 0;
        for (var i = 0; i < NumberOfItems; i++)
        {
            if (_structs[i].Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }

    [Benchmark]
    public int ForeachLoopStruct()
    {
        var j = 0;
        foreach (var b in _structs)
        {
            if (b.Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }
    
    [Benchmark]
    public int ForLoopObj()
    {
        var j = 0;
        for (var i = 0; i < NumberOfItems; i++)
        {
            if (_objects[i].Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }

    [Benchmark]
    public int ForeachLoopObj()
    {
        var j = 0;
        foreach (var b in _objects)
        {
            if (b.Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }
    
    [Benchmark]
    public int ForLoopList()
    {
        var j = 0;
        for (var i = 0; i < NumberOfItems; i++)
        {
            if (_listStructs[i].Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }

    [Benchmark]
    public int ForeachLoopList()
    {
        var j = 0;
        foreach (var b in _listStructs)
        {
            if (b.Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }
    
    [Benchmark]
    public int ForLoopObjectList()
    {
        var j = 0;
        for (var i = 0; i < NumberOfItems; i++)
        {
            if (_listObjects[i].Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }

    [Benchmark]
    public int ForeachLoopObjectList()
    {
        var j = 0;
        foreach (var b in _listObjects)
        {
            if (b.Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }
}

public class EmptyListBench
{
    private class TestObj
    {
        public int Value1 { get; init; }
        public long Value2 { get; init; }
        public string Value3 { get; init; }
    }
    
    private List<TestObj> _emptyList;

    [GlobalSetup]
    public void Setup()
    {
        _emptyList = new List<TestObj>();
    }

    [Benchmark]
    public int ForLoopByte()
    {
        var j = 0;
        for (var i = 0; i < _emptyList.Count; i++)
        {
            if (_emptyList[i].Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }

    [Benchmark]
    public int ForeachLoopByte()
    {
        var j = 0;
        foreach (var b in _emptyList)
        {
            if (b.Value1 == 1)
            {
                j++;
            }
        }

        return j;
    }
}