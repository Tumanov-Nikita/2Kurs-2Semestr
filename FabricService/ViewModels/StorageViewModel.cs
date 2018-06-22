using System.Collections.Generic;

namespace FabricService.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public List<StoragePartsViewModel> StorageParts { get; set; }
    }
}
