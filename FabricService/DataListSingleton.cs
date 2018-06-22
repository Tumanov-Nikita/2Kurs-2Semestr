using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabricModel;

namespace FabricService
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Customer> Customers { get; set; }

        public List<Part> Parts { get; set; }

        public List<Executer> Executers { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Stuff> Stuffs { get; set; }

        public List<StuffPart> StuffParts { get; set; }

        public List<Storage> Storages { get; set; }

        public List<StoragePart> StorageParts { get; set; }

        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Parts = new List<Part>();
            Executers = new List<Executer>();
            Bookings = new List<Booking>();
            Stuffs = new List<Stuff>();
            StuffParts = new List<StuffPart>();
            Storages = new List<Storage>();
            StorageParts = new List<StoragePart>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }

    }
}
