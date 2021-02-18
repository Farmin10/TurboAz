using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurboAz.Classes;
using TurboAz.Utility;

namespace TurboAz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void BtnPlaceAd_Click(object sender, EventArgs e)
        {
            CreateAd createAd = new CreateAd();
            createAd.ShowDialog();
            GetCars();
        }
        GeneralMethods generalMethods = new GeneralMethods();
        ClassInfoAdapter classInfoAdapter = new ClassInfoAdapter();
        private void Form1_Load(object sender, EventArgs e)
        {
            generalMethods.SetBrandData(LkUEAllBrands);
            generalMethods.GetCarInfo(LkUEPrice, "3");
            generalMethods.GetCarInfo(LkUECity, "7");
            generalMethods.SetYears(LkUpEtStart);
            generalMethods.SetYears(LkUEEnd);
            GetCars();
        }

        private void LkUEAllBrands_EditValueChanged(object sender, EventArgs e)
        {

            LkUEModels.Properties.DataSource = classInfoAdapter.GetModels(LkUEAllBrands.EditValue.ToString());
            LkUEModels.Properties.DisplayMember = "Name";
            LkUEModels.Properties.ValueMember = "Id";
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            GetCars();
            MessageBox.Show("Axtaris bitdi", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void GetCars()
        {
            string query = $@"SELECT ADS.Id,ADS.Price
	                                ,(BRD.Name+' '+MDL.Name) CAR_NAME   
                                    ,ADS.Year
	                                ,ADS.Walk
			                        ,GN.Name CityName
	                                ,(SELECT TOP(1) IMG.CarImage FROM TblCarImages IMG WHERE IMG.AdsId = ADS.ID) CAR_IMAGE
                                    FROM [dbo].TblSavedAds ADS
			                        JOIN TblCarModels MDL ON MDL.Id = ADS.ModelId	
			                        JOIN TblCarBrands BRD ON MDL.Id = BRD.Id
	                                JOIN TblBanInfo GN ON GN.Id = ADS.CityId
                                    WHERE 1=1";

            if (LkUEAllBrands.EditValue != null)
            {
                query = query + $" AND MDL.[Id]={LkUEAllBrands.EditValue}";
            }

            if (LkUEModels.EditValue != null)
            {
                query = query + $" AND ADS.[ModelId]={LkUEModels.EditValue}";
            }

            if (TxtMin.EditValue != null)
            {
                query = query + $" AND ADS.[Price]>={TxtMin.EditValue}";
            }

            if (TxtMax.EditValue != null)
            {
                query = query + $" AND ADS.[Price]<={TxtMax.EditValue}";
            }

            if (LkUpEtStart.EditValue != null)
            {
                query = query + $" AND ADS.[Year]>={LkUpEtStart.EditValue}";
            }

            if (LkUEEnd.EditValue != null)
            {
                query = query + $" AND ADS.[Year]<={LkUEEnd.EditValue}";
            }

            if (LkUECity.EditValue != null)
            {
                query = query + $" AND ADS.[CityId]={LkUECity.EditValue}";
            }


            if (CECredit.Checked)
            {
                query = query + $" AND ADS.[IsCredit]={CECredit.Checked}";
            }


            if (CEBarter.Checked)
            {
                query = query + $" AND ADS.[IsBarter]={CEBarter.Checked}";
            }


            DataTable dataTable =  SqlUtility.GetInstance().GetDataWithAdapter(query);
            GdClCarShop.DataSource = dataTable;
        }

        private void GdClCarShop_Click(object sender, EventArgs e)
        {

        }
    }
}
