using AlytaloXplat.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlytaloXplat.Data
{
    public class TempDatabase
    {
        readonly SQLiteAsyncConnection tDatabase;

        public TempDatabase(string dbPath)
        {
            tDatabase = new SQLiteAsyncConnection(dbPath);
            tDatabase.CreateTableAsync<TempDBModel>().Wait();
        }

        public Task<List<TempDBModel>> GetTempAsync()
        {
            return tDatabase.Table<TempDBModel>().ToListAsync();
        }
        public Task<TempDBModel> GetTemperaturesAsync(int id)
        {
            return tDatabase.Table<TempDBModel>().Where(i => i.LämpötilaId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTempAsync(TempDBModel temp)
        {
            if (temp.LämpötilaId != 0)
            {
                return tDatabase.UpdateAsync(temp);
            }

            else
            {
                return tDatabase.InsertAsync(temp);
            }
        }

        // This will delete the table from view and create a new empty table -- Reset
        public Task<int>DeleteTempAsync(TempDBModel temp)
        {
            tDatabase.DropTableAsync<TempDBModel>().Wait();
            tDatabase.CreateTableAsync<TempDBModel>().Wait();
            return tDatabase.UpdateAsync(temp);
        }
    }
}
