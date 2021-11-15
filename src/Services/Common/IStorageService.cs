using System;

namespace Services.Common
{
    public interface IStorageService
    {
        string StorageBaseUri { get; }
        Uri CreateUploadUri(string filename);
    }
}