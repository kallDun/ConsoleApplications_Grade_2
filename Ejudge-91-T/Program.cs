using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Ejudge_91_T.AddOperation;
using static Ejudge_91_T.CellProperty;

namespace Ejudge_91_T
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt").Select(x => x.Split());
            ISet<string, int> set = new HashSet<string, int>();

            foreach (var task in data)
            {
                switch (task[0])
                {
                    case "add":
                        var key = task[1];
                        var value = int.Parse(task[2]);
                        var result = set.Add(key, value);

                        switch (result.key)
                        {
                            case Successful:
                                Console.WriteLine($"{key} was put to cell {result.value}");
                                break;
                            case Already:
                                Console.WriteLine($"{key} is already in cell {result.value}");
                                break;
                            case Replaced:
                                Console.WriteLine($"{key} was in cell {result.old_value} but now has been moved to cell {result.value}");
                                break;
                            case Overflowed:
                                Console.WriteLine($"hash table overflow");
                                break;
                        }
                        break;
                    case "delete":
                        var _key = task[1];
                        var _result = set.Delete(_key);
                        if (_result.key)
                        {
                            Console.WriteLine($"{_key} was deleted from cell {_result.value}");
                        }
                        else
                        {
                            Console.WriteLine($"there is no {_key} in table");
                        }
                        break;
                    case "printtable":
                        var table = set.Print();
                        Console.WriteLine(string.Join("\n", table.Select(x => $"{x.Key} {x.Value.Key} {x.Value.Value}")));
                        break;
                    case "search":
                        var key_ = task[1];
                        var result_ = set.Search(key_);
                        if (result_.key)
                        {
                            Console.WriteLine($"{key_} was found in cell {result_.value.Key} value {result_.value.Value}");
                        }
                        else
                        {
                            Console.WriteLine($"there is no {key_} in table");
                        }
                        break;
                }
            }
        }
    }


    struct BoolWrapper<T>
    {
        public bool key;
        public T value;
    }
    enum AddOperation
    {
        Successful, Already, Replaced, Overflowed
    }
    struct AddOperationWrapper<T>
    {
        public AddOperation key;
        public T value, old_value;
    }
    enum CellProperty
    {
        Empty, Normal, Deleted
    }
    struct CellWrapper<T>
    {
        public CellProperty Prop;
        public T Value;
    }
    class Pair<T, V>
    {
        public T Key;
        public V Value;
        public Pair(T key, V value)
        {
            Key = key;
            Value = value;
        }
    }

    interface ISet<T, V>
    {
        AddOperationWrapper<int> Add(T key, V value);
        Dictionary<int, Pair<T, V>> Print();
        BoolWrapper<int> Delete(T key);
        BoolWrapper<KeyValuePair<int, V>> Search(T key);
    }

    class HashSet<T, V> : ISet<T, V> where T : IComparable<T>
    {
        private const int cells_count = 2017;

        public int Count { get; private set; } = 0;
        private CellWrapper<Pair<T, V>>[] cells = new CellWrapper<Pair<T, V>>[cells_count];

        public Dictionary<int, Pair<T, V>> Print()
        {
            var dict = new Dictionary<int, Pair<T, V>>();
            for (int cell = 0; cell < cells_count; cell++)
            {
                if (cells[cell].Prop == Normal)
                {
                    dict.Add(cell, cells[cell].Value);
                }
            }
            return dict;
        }
        
        public AddOperationWrapper<int> Add(T key, V value)
        {
            if (Count == cells_count) return new AddOperationWrapper<int> { key = Overflowed };

            int cell;
            List<int> deleted;
            var exist = !FindCell(key, out cell, out deleted);

            if (exist)
            {
                if (deleted.Count == 0)
                {
                    cells[cell].Value.Value = value;
                    return new AddOperationWrapper<int> { key = Already, value = cell };
                }                    
                else
                {
                    Delete(cell);
                    Add(key, value, deleted[0]);
                    return new AddOperationWrapper<int> { key = Replaced, value = deleted[0], old_value = cell };
                }
            }
            else
            {
                if (deleted.Count == 0)
                {
                    Add(key, value, cell);
                    return new AddOperationWrapper<int> { key = Successful, value = cell };
                }
                else
                {
                    Add(key, value, deleted[0]);
                    return new AddOperationWrapper<int> { key = Successful, value = deleted[0] };
                }
            }            
        }
        private void Add(T key, V value, int cell)
        {
            cells[cell] = new CellWrapper<Pair<T, V>> { Prop = Normal, Value = new Pair<T, V>(key, value) };
            Count++;
        }

        public BoolWrapper<int> Delete(T key)
        {
            int cell;
            List<int> deleted;
            var contains = !FindCell(key, out cell, out deleted);

            if (!contains) return new BoolWrapper<int> { key = false };

            Delete(cell);
            return new BoolWrapper<int> { key = true, value = cell };
        }
        private void Delete(int cell)
        {
            Count--;
            cells[cell].Prop = Deleted;
            cells[cell].Value = null;
        }
        
        public BoolWrapper<KeyValuePair<int, V>> Search(T key)
        {
            int cell;
            List<int> deleted;
            var contains = !FindCell(key, out cell, out deleted);

            var wrapper = new BoolWrapper<KeyValuePair<int, V>> { key = contains };
            if (contains)
            {
                wrapper.value = new KeyValuePair<int, V>(cell, cells[cell].Value.Value); ;
            }
            return wrapper;
        }

        /// <summary>
        /// return false if key contains and true is empty cell succesfully added
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cell"></param>
        /// <param name="deleted"></param>
        /// <returns></returns>
        private bool FindCell(T key, out int cell, out List<int> deleted)
        {
            deleted = new List<int>();
            var main = MainHashFunc(key);
            cell = main;

            if (cells[cell].Prop != Empty)
            {
                if (cells[cell].Prop == Deleted) deleted.Add(cell);
                else if (cells[cell].Value.Key.CompareTo(key) == 0) return false;

                var secondary = SecondaryHashFunc(key);
                cell = GetNextCell(main, secondary);

                while (cells[cell].Prop != Empty)
                {
                    if (cells[cell].Prop == Deleted) deleted.Add(cell);
                    else if (cells[cell].Value.Key.CompareTo(key) == 0) return false;

                    cell = GetNextCell(cell, secondary);
                }
            }

            return true;
        }
        
        private int MainHashFunc(T key)
        {
            if (!(key is string)) throw new NotImplementedException();

            var arr = key as string;
            int hash = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                hash = (hash * 256 + arr[i]) % cells_count;
            }

            return hash;
        }
        private int SecondaryHashFunc(T key)
        {
            if (!(key is string)) throw new NotImplementedException();

            var arr = key as string;
            var sum = 0;

            for (int i = 0; i < arr.Length; i++) sum += arr[i];

            return (sum % (cells_count - 1)) + 1;
        }
        private int GetNextCell(int cell_main, int cell_secondary) => (cell_main + cell_secondary) % cells_count;
    }
}
