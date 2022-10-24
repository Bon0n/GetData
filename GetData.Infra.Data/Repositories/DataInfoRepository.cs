using GetData.Domain.Entities;
using GetData.Infra.Data.Context;
using GetDataInfo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GetData.Infra.Data.Repositories
{
    public class DataInfoRepository : IDataInfoRepository
    {

        private AppDbContext _dataInfoContext;

        public DataInfoRepository(AppDbContext dataInfoContext)
        {
            _dataInfoContext = dataInfoContext;
        }
        public async Task<DataInfo> Create(DataInfo dataInfo)
        {
            _dataInfoContext.Add(dataInfo);
            await _dataInfoContext.SaveChangesAsync();
            return dataInfo;
        }

        public async Task<DataInfo> GetById(int? id)
        {
            return await _dataInfoContext.DataInfo.FindAsync(id);
        }

        public async Task<IEnumerable<DataInfo>> GetDataInfo()
        {
            return await _dataInfoContext.DataInfo.ToListAsync();
        }

        public async Task<DataInfo> Remove(DataInfo dataInfo)
        {
            _dataInfoContext.Remove(dataInfo);
            await _dataInfoContext.SaveChangesAsync();
            return dataInfo;
        }

        public async Task<DataInfo> Update(DataInfo dataInfo)
        {
            _dataInfoContext.Update(dataInfo);
            await _dataInfoContext.SaveChangesAsync();
            return dataInfo;
        }
    }
}