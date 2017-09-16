using System;
using System.Collections.Generic;

namespace Pong.Core
{
    public interface BasicItem { }

    public class IntItem : BasicItem
    {
        public int data;
        public IntItem(int data) { this.data = data; }
    }
    public class FloatItem : BasicItem
    {
        public float data;
        public FloatItem(float data) { this.data = data; }
    }
    public class StringItem : BasicItem
    {
        public string data;
        public StringItem(string data) { this.data = data; }
    }

    public class Item
    {
        public BasicItem dataItem;
        public Item(BasicItem dataItem)
        {
            this.dataItem = dataItem;
        }
    }

    public static class DataManager
    {
        private static Dictionary<string, Item> items;

        static DataManager()
        {
            items = new Dictionary<string, Item>();
        }

        public static void StoreItem(string name, Item item)
        {
            items.Add(name, item);
        }
        public static void DeleteItem(string name)
        {
            if (items.ContainsKey(name))
                items.Remove(name);
        }
        
        public static int GetInt(string name)
        {
            if (items.ContainsKey(name))
                return (items[name].dataItem as IntItem).data;
            return 0;
        }
        public static float GetFloat(string name)
        {
            if (items.ContainsKey(name))
                return (items[name].dataItem as FloatItem).data;
            return 0.0f;
        }
        public static string GetString(string name)
        {
            if (items.ContainsKey(name))
                return (items[name].dataItem as StringItem).data;
            return "";
        }

        public static void StoreInt(string name, int data)
        {
            if (!items.ContainsKey(name))
                StoreItem(name, new Item(new IntItem(data)));
            else
                (items[name].dataItem as IntItem).data = data;
        }
        public static void StoreFloat(string name, float data)
        {
            if (!items.ContainsKey(name))
                StoreItem(name, new Item(new FloatItem(data)));
            else
                (items[name].dataItem as FloatItem).data = data;
        }
        public static void StoreString(string name, string data)
        {
            if (!items.ContainsKey(name))
                StoreItem(name, new Item(new StringItem(data)));
            else
                (items[name].dataItem as StringItem).data = data;
        }
    }
}