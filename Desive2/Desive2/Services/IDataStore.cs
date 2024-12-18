using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desive2
{
    /// <summary>
    /// Defines a contract for a data store with basic CRUD (Create, Read, Update, Delete) operations.
    /// </summary>
    /// <typeparam name="T">The type of item to be stored in the data store.</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Asynchronously adds an item to the data store.
        /// </summary>
        /// <param name="item">The item to be added to the data store.</param>
        /// <returns>Returns a task representing the asynchronous operation. The result is <c>true</c> if the item was added successfully; otherwise, <c>false</c>.</returns>
        Task<bool> AddItemAsync(T item);

        /// <summary>
        /// Asynchronously updates an existing item in the data store.
        /// </summary>
        /// <param name="item">The item with updated values.</param>
        /// <returns>Returns a task representing the asynchronous operation. The result is <c>true</c> if the item was updated successfully; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateItemAsync(T item);

        /// <summary>
        /// Asynchronously deletes an item from the data store based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the item to be deleted.</param>
        /// <returns>Returns a task representing the asynchronous operation. The result is <c>true</c> if the item was deleted successfully; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteItemAsync(string id);

        /// <summary>
        /// Asynchronously retrieves an item from the data store based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the item to retrieve.</param>
        /// <returns>Returns a task representing the asynchronous operation. The result is the item if found, or <c>null</c> if not.</returns>
        Task<T> GetItemAsync(string id);

        /// <summary>
        /// Asynchronously retrieves all items from the data store.
        /// </summary>
        /// <param name="forceRefresh">Specifies whether to force a refresh of the data from the data store (optional, defaults to <c>false</c>).</param>
        /// <returns>Returns a task representing the asynchronous operation. The result is an enumerable collection of items.</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }

}
