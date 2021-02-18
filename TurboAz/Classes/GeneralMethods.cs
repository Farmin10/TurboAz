using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;

namespace TurboAz.Classes
{
     class GeneralMethods
    {
        ClassInfoAdapter classInfoAdapter = new ClassInfoAdapter();
        public void GetCarInfo(LookUpEdit lookUpEdit, string typeId)
        {
            lookUpEdit.Properties.DataSource = classInfoAdapter.GetBanInfo(typeId);
            lookUpEdit.Properties.DisplayMember = "Name";
            lookUpEdit.Properties.ValueMember = "Id";
        }
        public void SetYears(LookUpEdit lookUpEdit)
        {
            List<int> yearList = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                yearList.Add(i);
            }
            lookUpEdit.Properties.DataSource = yearList;
        }
        public void SetBrandData(LookUpEdit lookUpEdit)
        {
            lookUpEdit.Properties.DataSource = classInfoAdapter.GetBrands();
            lookUpEdit.Properties.DisplayMember = "Name";
            lookUpEdit.Properties.ValueMember = "Id";
        }
    }
}
