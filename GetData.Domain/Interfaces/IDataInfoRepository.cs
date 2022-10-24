using GetData.Domain.Entities;

namespace GetDataInfo.Domain.Interfaces
{
    public interface IDataInfoRepository
    {
        Task<DataInfo> GetById(int? id);
        Task<IEnumerable<DataInfo>> GetDataInfo();
        Task<DataInfo> Create(DataInfo dataInfo);
        Task<DataInfo> Update(DataInfo dataInfo);
        Task<DataInfo> Remove(DataInfo dataInfo);
    }
}