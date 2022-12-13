namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> list = new List<TItem>(); 
        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly
        {
            get
            {
                return list.IsReadOnly;
            }
        }

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get { return list[index]; }
            set {
                TItem oldValue=list[index];
                list[index] = value;
                ElementChanged?.Invoke(this,value,oldValue,index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            list.Add(item);
            ElementInserted?.Invoke(this,item,list.Count-1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            IList<TItem> oldItem= new List<TItem>();
            list.Clear();
            for(int i=0;i < oldItem.Count;i++) {
            ElementRemoved?.Invoke(this,oldItem[i],i);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item)
        {
            return list.Contains(item);
        }

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            list.CopyTo(array,arrayIndex);
        }

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            ElementRemoved?.Invoke(this,item,list.IndexOf(item));
            return list.Remove(item);
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item)
        {
            return list.IndexOf(item);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            list.Add(item);
            ElementInserted?.Invoke(this,item,index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            ElementRemoved?.Invoke(this,list[index],index);
            list.Remove(list[index]);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj) =>
            // TODO improve
            base.Equals(obj);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode() =>
            // TODO improve
            base.GetHashCode();

        /// <inheritdoc cref="object.ToString" />
        public override string ToString() =>
            // TODO improve
            base.ToString();
    }
}
