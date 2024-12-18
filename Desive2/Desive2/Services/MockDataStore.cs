using Desive2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desive2.Services
{
    /// <summary>
    /// A mock implementation of the <see cref="IDataStore{T}"/> interface to simulate basic data operations for <see cref="Item"/> objects.
    /// This class stores items in memory and provides methods for adding, updating, deleting, and retrieving items.
    /// </summary>
    public class MockDataStore : IDataStore<Item>
    {
        // A private list to store the items in memory
        readonly List<Item> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDataStore"/> class.
        /// This constructor populates the data store with some initial mock items.
        /// </summary>
        public MockDataStore()
        {
            items = new List<Item>()
        {
            new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
            new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
            new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
            new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
            new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
            new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
        };
        }

        /// <summary>
        /// Asynchronously adds a new item to the data store.
        /// </summary>
        /// <param name="item">The item to add to the store.</param>
        /// <returns>A task that represents the asynchronous operation, with a result of <c>true</c> indicating success.</returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously updates an existing item in the data store.
        /// </summary>
        /// <param name="item">The item to update in the store.</param>
        /// <returns>A task that represents the asynchronous operation, with a result of <c>true</c> indicating success.</returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously deletes an item from the data store by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>A task that represents the asynchronous operation, with a result of <c>true</c> indicating success.</returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously retrieves an item from the data store by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, with a result of the item if found, or <c>null</c> if not.</returns>
        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Asynchronously retrieves all items from the data store.
        /// </summary>
        /// <param name="forceRefresh">Indicates whether to force a refresh of the data (not used in this mock implementation).</param>
        /// <returns>A task that represents the asynchronous operation, with a result of a list of all items.</returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }

}