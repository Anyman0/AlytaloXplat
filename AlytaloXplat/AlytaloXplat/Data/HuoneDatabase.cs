using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace AlytaloXplat.Data
{
    public class HuoneDatabase
    {
        readonly SQLiteAsyncConnection hDatabase;

        public HuoneDatabase(string dbPath)

        {

            hDatabase = new SQLiteAsyncConnection(dbPath);

            hDatabase.CreateTableAsync<HuoneDBModel>().Wait();

        }

        public Task<List<HuoneDBModel>> GetRooms()

        {

            return hDatabase.Table<HuoneDBModel>().ToListAsync();

        }
       

        public Task<HuoneDBModel> GetRoomAsync(int id)

        {
            return hDatabase.Table<HuoneDBModel>().Where(i => i.huoneId == id).FirstOrDefaultAsync();
        }   
        
        // Gets the rooms where bool valonTila is true
        public Task<List<HuoneDBModel>> GetRoomsWithLightsOn()
        {
            return hDatabase.QueryAsync<HuoneDBModel>("SELECT * FROM [HuoneDBModel] WHERE valonTila = 1");
        }

        // Gets the rooms where bool valonTila is false
        public Task<List<HuoneDBModel>> GetRoomsWithLightsOff()
        {
            return hDatabase.QueryAsync<HuoneDBModel>("SELECT * FROM [HuoneDBModel] WHERE valonTila = 0");
        }

        public Task<int> SaveItemAsync(HuoneDBModel huone)

        {

            if (huone.huoneId != 0)

            {

                return hDatabase.UpdateAsync(huone);

            }

            else
            {

                return hDatabase.InsertAsync(huone);

            }

        }



        public Task<int> DeleteItemAsync(HuoneDBModel huone)

        {

            return hDatabase.DeleteAsync(huone);

        }

       
       
    }
}
