using System;
using System.Collections.Concurrent;

namespace Demo
{
    // Was having issues in performance so implemented a pool to try to help
    // Ended up being completely separate memory issue (fixed), but no reason to take out now
    // Overkill as we shouldn't be able to create enough objs to effect and doing so isn't too intensive, but ended up being good practice i guess
    public class ObjectPool<T> where T : class, new()
    {
        // Bag for objs
        private ConcurrentBag<T> objects;
        // Don't want to hold > max
        private int maxObjects;
        
        public ObjectPool()
        {
            objects = new ConcurrentBag<T>();
            // Should be a fine limit, max at once is 8 but can stack
            maxObjects = 50;
        }

        // Grab obj from bag, standard implementation
        public T GetObject()
        {
            T item;
            if (objects.TryTake(out item))
                return item;
            return new T();
        }

        // Put pack into bag, standard implementation
        public void PutObject(T item)
        {
            if (objects.Count < maxObjects)
                objects.Add(item);
        }
    }
}
