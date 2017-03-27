using Czaplicki.Universal.Chain;
using SFML.System;
using System;
using System.Collections.Generic;

namespace GangGang
{
    public class Entity : Chain<Entity>
    {
        private int Tag { get; set; } = -1;
        public bool Enable { get; set; } = true;
        public Entity Parent { get; private set; }
        public Entity Children { get; set; }

        private Vector2f offset = new Vector2f();
        public Vector2f Offset
        {
            get { return offset; }

            set
            {
                this.offset = value;
                OffsetChanged();
            }
        }
        public virtual Vector2f Position { get { return Parent?.Position + Offset ?? Offset; } }


        //public Vector2f Position { get { return Parent != null ? Parent.Position + Offset : Offset; } }

        protected virtual void OffsetChanged()
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    child.OffsetChanged();
                }
        }
        public virtual void Update()
        {
            if (!Enable)
            {
                return;
            }
            if (Children != null)
                foreach (Entity child in Children)
                {
                    child.Update();
                }

        }

        public void Adopt(Entity child)
        {
            if (child.Parent != null) //if it does have parent
            {
                child.Parent.Reject(child); //tell perant to reject child
            }
            //tell child you the new parent
            child.Parent = this;
            //if ew dont got any kids yet, set this one to the firt one
            if (Children == null)
            {
                Children = child;
            }
            else // if we do have kids allredy add it as a sibling to the first kid
            {
                Children.InsertElement(child);
            }

            child.OffsetChanged();
        }

        public void Reject(Entity child)
        {
            if (child == Children) // aka if firts child
            {
                Children = child.GetNext() as Entity; // set first child to next child
                if (child == Children)// if your still the firt child && set parent childern to null
                {
                    Children = null;
                }
            }
            //tell child it has no patent
            child.Parent = null;
            //tell kid to leave old siblings
            child.LeaveChain();

        }
        public void RejectAll()
        {
            if (Children != null)
            {
                foreach (Entity child in Children)
                {
                    child.Parent = null;
                }
                Children = null;
            }
        }

        public void FetchAllActive<T>(ref List<T> list, string tag) where T : class
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child.Enable)
                    {
                        if (GetTag(tag) == child.Tag && child is T) 
                        {
                            list.Add(child as T);
                        }
                        child.FetchAllActive<T>(ref list, tag);
                    }
                }
        }
        public void FetchAllActive<T>(ref List<T> list) where T : class
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child.Enable)
                    {
                        if (child is T)
                        {
                            list.Add(child as T);
                        }
                        child.FetchAllActive<T>(ref list);
                    }
                }
        }

        //use ref list insted af return value

        public void FetchAll<T>(ref List<T> list, string tag) where T : class
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child.Tag == GetTag(tag) && child is T)
                    {
                        list.Add(child as T);
                    }
                    child.FetchAll<T>(ref list, tag);
                }
        }
        public void FetchAll<T>(ref List<T> list) where T : class
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child is T)
                    {
                        list.Add(child as T);
                    }
                    child.FetchAll<T>(ref list);
                }
        }

        public void FetchChildren<T>(ref List<T> list, string tag) where T : class
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child is T && child.Tag == GetTag(tag))
                    {
                        list.Add(child as T);
                    }
                }
        }
        public void FetchChildren<T>(ref List<T> list) where T : class
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child is T)
                    {
                        list.Add(child as T);
                    }
                }
        }

        public T GetChild<T>(string tag) where T : class
        {
            foreach (Entity child in Children)
            {
                if (child.Tag == GetTag(tag) && child is T)
                {
                    return child as T;
                }
            }
            return null;
        }
        public T GetChild<T>() where T : class
        {
            foreach (Entity child in Children)
            {
                if (child is T)
                {
                    return child as T;
                }
            }
            return null;
        }


        public void SetTag(string tag)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i] == tag)
                {
                    this.Tag = i;
                    return;
                }
            }
            tags.Add(tag);
            this.Tag = tags.Count - 1;
        }

        //Static
        private static List<string> tags = new List<string>(); // not working

        private static int GetTag(string tag)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i] == tag)
                {
                    return i;
                }
            }
            return -1;
        }

    }

}
