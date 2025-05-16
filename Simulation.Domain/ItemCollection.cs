using Simulation.Domain.Exceptions;
using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Domain
{
    public class ItemCollection<TKey> : IReadOnlyDictionary<TKey, float> where TKey : IItem
    {
        private readonly Dictionary<TKey, float> items;

        public ItemCollection()
        {
            items = [];
        }

        public ItemCollection(IDictionary<TKey, float> dictionary)
        {
            items = new Dictionary<TKey, float>(dictionary);
        }

        public ItemCollection(IDictionary<string, float> dictionary)
        {
            items = dictionary.ToDictionary(x => (TKey)(IItem.FindByKey(x.Key) ?? throw new Exception($"There is no item with key `{x.Key}´")), x => x.Value);
        }

        public ItemCollection(ItemCollection<TKey> collection)
        {
            items = collection.ToDictionary();
        }

        public ItemCollection(TKey item, float amount)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            ArgumentOutOfRangeException.ThrowIfNegative(amount, nameof(amount));

            items = new Dictionary<TKey, float>()
            {
                { item, amount }
            };
        }

        public bool ContainsKey(TKey item)
        {
            return items.ContainsKey(item);
        }

        /// <summary>
        /// Are there enough item in this collection
        /// </summary>
        /// <param name="item">Item to be checked</param>
        /// <param name="amount">Amount of item</param>
        /// <returns></returns>
        public bool HasEnough(TKey item, float amount)
        {
            return items.GetValueOrDefault(item, 0) >= amount;
        }

        /// <summary>
        /// Are there enough resources in this collection
        /// </summary>
        /// <param name="collection">Resources to be checked</param>
        /// <exception cref="NotEnoughResourceException"></exception>
        public void CheckEnough(ItemCollection<TKey> collection)
        {
            foreach (var item in collection)
            {
                var exist = items.GetValueOrDefault(item.Key, 0);
                if (!HasEnough(item.Key, item.Value))
                    throw new NotEnoughResourceException(item.Key, item.Value, exist);
            }
        }

        public void Add(TKey item, float amount)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            ArgumentOutOfRangeException.ThrowIfNegative(amount, nameof(amount));

            float newAmount = items.GetValueOrDefault(item, 0) + amount;
            items[item] = newAmount;
        }

        /// <summary>
        /// Collection içinde belirtilen itemdan yeterince var mı? Yoksa <see cref="NotEnoughResourceException"/> fırlatır.
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="amount">Miktar</param>
        /// <exception cref="NotEnoughResourceException"></exception>
        public void CheckEnough(TKey item, float amount)
        {
            var exist = items.GetValueOrDefault(item, 0);
            NotEnoughResourceException.ThrowIfNotEnough(item, amount, exist);
        }

        public Dictionary<TKey, float> ToDictionary()
        {
            return items.ToDictionary(x => x.Key, x => x.Value);
        }

        public ItemCollection<TCast> CastTo<TCast>() where TCast : IItem
        {
            var dictionary = items.ToDictionary(x =>
            {
                if (x.Key is TCast item)
                    return item;
                throw new InvalidCastException($"[{x.Key.Key}] is not a {typeof(TCast).Name} type");
            }, x => x.Value);
            return new ItemCollection<TCast>(dictionary);
        }

        public ItemCollection<TCast> IsTypeOf<TCast>() where TCast: IItem
        {
            Dictionary<TCast, float> dictionary = [];
            foreach (var item in items)
            {
                if (item.Key is TCast castedItem)
                    dictionary[castedItem] = item.Value;
            }
            return new ItemCollection<TCast>(dictionary);
        }

        public Dictionary<TKey, float>.KeyCollection Keys => items.Keys;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, float>.Keys => items.Keys;

        public IEnumerable<float> Values => items.Values;

        public int Count => items.Count;

        public float this[TKey key]
        {
            get
            {
                return items[key];
            }
            internal set
            {
                items[key] = value;
            }
        }

        public static ItemCollection<TKey> operator +(ItemCollection<TKey> v1, ItemCollection<TKey> v2)
        {
            ArgumentNullException.ThrowIfNull(v1, nameof(v1));
            ArgumentNullException.ThrowIfNull(v2, nameof(v2));

            var result = new ItemCollection<TKey>(v2);

            foreach (var item in v1)
            {
                if (result.ContainsKey(item.Key))
                    result[item.Key] += item.Value;
                else
                    result[item.Key] = item.Value;
            }

            return result;
        }

        public static ItemCollection<TKey> operator -(ItemCollection<TKey> v1, ItemCollection<TKey> v2)
        {
            ArgumentNullException.ThrowIfNull(v1, nameof(v1));
            ArgumentNullException.ThrowIfNull(v2, nameof(v2));

            var result = new ItemCollection<TKey>(v1);

            foreach (var item in v2)
            {
                var current = result.GetValueOrDefault(item.Key);
                var newValue = current - item.Value;

                if (newValue < 0)
                    throw new NotEnoughResourceException(item.Key, item.Value, current);

                result[item.Key] = newValue;
            }

            return result;
        }

        public static ItemCollection<TKey> operator *(ItemCollection<TKey> v1, int factor)
        {
            ArgumentNullException.ThrowIfNull(v1, nameof(v1));

            var result = new ItemCollection<TKey>(v1);

            foreach (var key in result.Keys)
            {
                result[key] *= factor;
            }

            return result;
        }

        public static ItemCollection<TKey> operator *(ItemCollection<TKey> v1, double factor)
        {
            ArgumentNullException.ThrowIfNull(v1, nameof(v1));

            var result = new ItemCollection<TKey>(v1);

            foreach (var key in result.Keys)
            {
                result[key] = Convert.ToInt32(result[key] * factor);
            }

            return result;
        }

        public static ItemCollection<TKey> operator /(ItemCollection<TKey> v1, int divider)
        {
            ArgumentNullException.ThrowIfNull(v1, nameof(v1));

            var result = new ItemCollection<TKey>(v1);

            foreach (var key in result.Keys)
            {
                result[key] /= divider;
            }

            return result;
        }

        public override bool Equals(object? obj)
        {
            return items.Equals(obj);
        }

        public override int GetHashCode()
        {
            return items.GetHashCode();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out float value)
        {
            return items.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<TKey, float>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
