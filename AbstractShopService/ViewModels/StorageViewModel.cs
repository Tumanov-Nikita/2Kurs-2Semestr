using System.Collections.Generic;

namespace AbstractShopService.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public List<StoragePartsViewModel> StorageParts { get; set; }
    }
}
