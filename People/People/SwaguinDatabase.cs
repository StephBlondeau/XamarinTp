using System;
using System.Collections.Generic;
using System.Linq;
using People.Models;
using SQLite;
using System.Threading.Tasks;

namespace People
{
    public class SwaguinDatabase
    {
        private readonly SQLiteAsyncConnection conn;

        public SwaguinDatabase(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Contact>().Wait();
            //conn.CreateTableAsync<Contact>().Wait();
        }

        public async Task AddNewContactAsync(string firstname)
        {
            //insert a new contact into the Contact table
            var result = await conn.InsertAsync(new Contact { FirstName = firstname });
        }

        public async Task<List<Contact>> GetAllContactAsync()
        {
            //return a list of people saved to the Person table in the database
            return await conn.Table<Contact>().ToListAsync();
        }

    }
}