﻿using System;
using System.Collections.Generic;

namespace Pong.Core
{
    public interface _item { }

    public class BasicItem<T> : _item
    {
        public T data;
        public BasicItem(T t) { data = t; }
    }

    public class Item
    {
        public _item dataItem;
        public Item(_item dataItem) { this.dataItem = dataItem; }
    }

    public class GenericDatabase
    {
        private Dictionary<string, Item> items;

        public GenericDatabase()
        {
            items = new Dictionary<string, Item>();
        }

        public void DeleteItem(string name)
        {
            if (items.ContainsKey(name))
                items.Remove(name);
        }

        public bool GetData<T>(string name, out T result)
        {
            if (items.ContainsKey(name))
            {
                result = (items[name].dataItem as BasicItem<T>).data;
                return true;
            }
            result = default(T);
            return false;
        }

        public bool SetData<T>(string name, T data)
        {
            if (items.ContainsKey(name))
            {
                (items[name].dataItem as BasicItem<T>).data = data;
                return true;
            }
            items.Add(name, new Item(new BasicItem<T>(data)));
            return false;
        }
    }
}