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

        public void FetchAllActive<T>(ref List<T> list, string tag) where T : Entity
        {
            if (Children != null)
                foreach (Entity child in Children)
                {
                    if (child.Enable)
                    {
                        if (GetTag(tag) == child.Tag && child is T) // tag dont work
                        {
                            list.Add(child as T);
                        }
                        child.FetchAllActive<T>(ref list, tag);
                    }
                }
        }
        public void FetchAllActive<T>(ref List<T> list) where T : Entity
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

        public void FetchAll<T>(ref List<T> list, string tag) where T : Entity
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
        public void FetchAll<T>(ref List<T> list) where T : Entity
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

        public List<T> FetchChildren<T>(string tag) where T : Entity
        {
            List<T> list = new List<T>();
            foreach (Entity child in Children)
            {
                if (child is T && child.Tag == GetTag(tag))
                {
                    list.Add(child as T);
                }
            }
            return list;
        }
        public List<T> FetchChildren<T>() where T : Entity
        {
            List<T> list = new List<T>();
            foreach (Entity child in Children)
            {
                if (child is T)
                {
                    list.Add(child as T);
                }
            }
            return list;
        }

        public T GetChild<T>(string tag) where T : Entity
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
        public T GetChild<T>() where T : Entity
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


    public class InteractivEntity : Entity
    {
        public CollitionComponent Collider { get; protected set; }
        public int Priority { get; set; } = 0;

        /// <summary>
        /// Use this contructor only if you know all recuiermants needed
        /// </summary>
        public InteractivEntity()
        {

        }
        public InteractivEntity(CollitionComponent collitionComponent, int priority = 0)
        {
            Collider = collitionComponent;
            Adopt(collitionComponent);
            this.Priority = priority;
        }
        public virtual void Click(bool yes) { }
        public virtual void Hover(bool yes) { }
    }

    public class ExternalInteractiveEntity : InteractivEntity
    {
        private Action<bool> click;
        private Action<bool> hover;

        public Action<bool> ClickDel
        {
            get
            {
                return click;
            }

            set
            {
                this.click = value;
            }
        }

        public Action<bool> HoverDel
        {
            get
            {
                return hover;
            }

            set
            {
                this.hover = value;
            }
        }
        public ExternalInteractiveEntity(CollitionComponent collitionComponent, int priority = 0) : base(collitionComponent, priority)
        {

        }
        public ExternalInteractiveEntity(Action<bool> click, Action<bool> hover, CollitionComponent collitionComponent, int priority = 0) : base(collitionComponent, priority)
        {
            this.click = click;
            this.hover = hover;
        }
        public override void Hover(bool yes)
        {
            hover?.Invoke(yes);
        }
        public override void Click(bool yes)
        {
            click?.Invoke(yes);
        }
    }

}
