using AbstractShopModel;
using System.Collections.Generic;

namespace AbstractShopService
{
    class DataListSingleton
    {
        private static DataListSingleton example;

        public List<Customer> Customers { get; set; }

        public List<Parts> Parts { get; set; }

        public List<Executer> Executers { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Stuff> Stuffs { get; set; }

        public List<StuffParts> StuffParts { get; set; }

        public List<Storage> Storages { get; set; }

        public List<StorageParts> StorageParts { get; set; }

        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Parts = new List<Parts>();
            Executers = new List<Executer>();
            Bookings = new List<Booking>();
            Stuffs = new List<Stuff>();
            StuffParts = new List<StuffParts>();
            Storages = new List<Storage>();
            StorageParts = new List<StorageParts>();
        }

        public static DataListSingleton GetExample()
        {
            if(example == null)
            {
                example = new DataListSingleton();
            }

            return example;
        }
    }
}
