using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Utility;

namespace TurboAz.Classes
{
    class ClassInfoAdapter
    {
        SqlUtility sqlUtility = SqlUtility.GetInstance();
        public DataTable GetBrands()
        {
            string query = "Select Id,Name from TblCarBrands";
            return sqlUtility.GetDataWithAdapter(query);
        }
        public DataTable GetModels(string brandId)
        {
            string query = $"Select Id,Name from TblCarModels where BrandId={brandId}";
            return sqlUtility.GetDataWithAdapter(query);
        }
        public DataTable GetBanInfo(string typeId)
        {
            string query = $"Select Id,Name from TblBanInfo where TypeId={typeId}";
            return sqlUtility.GetDataWithAdapter(query);
        }
        public DataTable GetCarColor()
        {
            string query = "Select Id,Name from TblCarColor";
            return sqlUtility.GetDataWithAdapter(query);
        }
        public DataTable GetImages(string AdsId)
        {
            string query = $@"SELECT [Id]
                            ,[CarImage]
                            ,[AdsId]
                             FROM[dbo].[TblCarImages] where AdsId={AdsId}";
            return sqlUtility.GetDataWithAdapter(query);
        }
    }
}
