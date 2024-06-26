﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//Om GameObjects te updaten en te drawen.
namespace Pong.Core
{
    public abstract partial class GameObject
    {
        public partial class GameObjectManager
        {
            private List<GameObject> objects;
            private List<GameObject> objs;

            public GameObjectManager()
            {
                objects = new List<GameObject>();
                objs = new List<GameObject>();
            }

            public void Init()
            {
                for (int i = 0; i < Size; i++)
                    objects[i].Init();
            }

            public void Update(GameTime time)
            {
                for (int i = 0; i < Size; i++)
                    objects[i].Update(time);
            }

            public void Draw(GameTime time, SpriteBatch batch)
            {
                for (int i = 0; i < Size; i++)
                    objects[i].Draw(time, batch);
            }

            public void Add(GameObject o)
            {
                //dit kan omdat deze class defined is in de GameObject class
                o.manager = this;
                objects.Add(o);
            }

            public void Remove(GameObject o)
            {
                objects.Remove(o);
            }
            
            public void Clear()
            {
                objects.Clear();
            }
            /*Achterlichende functies for FindWithTag etc.
            Zie GameObject waarom.*/
            public GameObject FindWithTag(string tag)
            {
                for(int i = 0; i < Size; i++)
                {
                    if (objects[i].tag == tag)
                        return objects[i];
                }
                return null;
            }

            public GameObject[] FindAllWithTag(string tag)
            {
                for (int i = 0; i < Size; i++)
                {
                    if (objects[i].tag == tag)
                        objs.Add(objects[i]);
                }
                if (objs.Count == 0) return null;
                GameObject[] arr = objs.ToArray();
                objs.Clear();
                return arr;
            }

            public GameObject[] FindAllWithTags(string[] tags)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < tags.Length; j++)
                    {
                        if (objects[i].tag == tags[j])
                        {
                            objs.Add(objects[i]);
                            break;
                        }
                    }
                }
                if (objs.Count == 0) return null;
                GameObject[] arr = objs.ToArray();
                objs.Clear();
                return arr;
            }

            public int Size { get { return objects.Count; } }
        }
    }
}