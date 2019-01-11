using AlytaloXplat.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlytaloXplat.Data
{
    public class SaunaDatabase
    {
        readonly SQLiteAsyncConnection sDatabase;

        public SaunaDatabase(string dbPath)
        {
            sDatabase = new SQLiteAsyncConnection(dbPath);
            sDatabase.CreateTableAsync<SaunaDBModel>().Wait();
        }

        public Task<List<SaunaDBModel>> GetSaunaAsync()

        {
            return sDatabase.Table<SaunaDBModel>().ToListAsync();
        }

        public Task<SaunaDBModel> GetSaunatAsync(int id)
        {
            return sDatabase.Table<SaunaDBModel>().Where(i => i.SaunaId == id).FirstOrDefaultAsync();
        }

        // Gets all the saunas where bool SaunanTila is true
        public Task<List<SaunaDBModel>> GetSaunaOn()
        {
            return sDatabase.QueryAsync<SaunaDBModel>("SELECT * FROM [SaunaDBModel] WHERE SaunanTila = 1");
        }

        public Task<int> SaveSaunaAsync(SaunaDBModel sauna)

        {

            if (sauna.SaunaId != 0)

            {

                return sDatabase.UpdateAsync(sauna);

            }

            else
            {

                return sDatabase.InsertAsync(sauna);

            }

        }

        public Task<int> DeleteSaunaAsync(SaunaDBModel sauna)

        {
            return sDatabase.DeleteAsync(sauna);
        }

    }
}
