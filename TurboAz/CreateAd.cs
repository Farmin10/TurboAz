using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TurboAz.Classes;
using TurboAz.Utility;

namespace TurboAz
{
    public partial class CreateAd : Form
    {
        ClassInfoAdapter classInfoAdapter = new ClassInfoAdapter();
        public CreateAd()
        {
            InitializeComponent();
        }

        private void CreateAd_Load(object sender, EventArgs e)
        {

            GeneralMethods generalMethods = new GeneralMethods();
            generalMethods.SetBrandData(LkUpEtBrands);
            generalMethods.GetCarInfo(LkUpEtBanType, "1");
            generalMethods.GetCarInfo(LkUpEtColor, "2");
            generalMethods.GetCarInfo(LkUpEtFuel, "4");
            generalMethods.GetCarInfo(LkUpEtTransmission, "5");
            generalMethods.GetCarInfo(LkUpEtGearBox, "6");
            generalMethods.GetCarInfo(LkUpEtCities, "7");
            generalMethods.GetCarInfo(LkUpEtEngineCapacity, "8");
            generalMethods.SetYears(LkUpEtGraduationYear);
            GridControlPicture.DataSource = classInfoAdapter.GetImages("-1");
        }

        private void GetImageDataSource()
        {

        }
        private void LkUpEtBrands_EditValueChanged(object sender, EventArgs e)
        {
            LkUpEtModels.Properties.DataSource = classInfoAdapter.GetModels(LkUpEtBrands.EditValue.ToString());
            LkUpEtModels.Properties.DisplayMember = "Name";
            LkUpEtModels.Properties.ValueMember = "Id";
        }



        private void BtnCreateAd_Click(object sender, EventArgs e)
        {
            if (EmptyCheckControl())
            {
                if (MessageBox.Show("Elanı yerləşdirmək istədiyinizə əminsiniz?", "Sual", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InsertAllInfo();
                }
            }
        }
        private bool EmptyCheckControl()
        {
            bool control = true;
            if (LkUpEtBrands.EditValue == null)
            {
                LkUpEtBrands.ErrorText = "Marka daxil edin";
                control = false;
            }
            if (LkUpEtModels.EditValue == null)
            {
                LkUpEtModels.ErrorText = "Model daxil edin";
                control = false;
            }
            if (LkUpEtBanType.EditValue == null)
            {
                LkUpEtBanType.ErrorText = "Ban növünü daxil edin";
                control = false;
            }
            if (LkUpEtColor.EditValue == null)
            {
                LkUpEtColor.ErrorText = "Rəng daxil edin";
                control = false;
            }
            if (LkUpEtFuel.EditValue == null)
            {
                LkUpEtFuel.ErrorText = "Yanacaq növünü daxil edin";
                control = false;
            }
            if (LkUpEtTransmission.EditValue == null)
            {
                LkUpEtTransmission.ErrorText = "Ötürücünü daxil edin";
                control = false;
            }
            if (LkUpEtGearBox.EditValue == null)
            {
                LkUpEtGearBox.ErrorText = "Sürətlər qutusu daxil edin";
                control = false;
            }
            if (LkUpEtGraduationYear.EditValue == null)
            {
                LkUpEtGraduationYear.ErrorText = "Buraxlış ilini daxil edin";
                control = false;
            }
            if (LkUpEtEngineCapacity.EditValue == null)
            {
                LkUpEtEngineCapacity.ErrorText = "Mühərrikin həcmini daxil edin";
                control = false;
            }
            if (TxtName.Text == "")
            {
                TxtName.ErrorText = "Adınızı daxil edin";
                control = false;
            }
            if (TxtEmail.Text == "")
            {
                TxtEmail.ErrorText = "Emaili daxil edin";
                control = false;
            }
            if (LkUpEtCities.EditValue == null)
            {
                LkUpEtCities.ErrorText = "Şəhəri daxil edin";
                control = false;
            }
            if (CdVwImage.DataRowCount < 3)
            {
                MessageBox.Show("ən az 3 şəkil əlavə olunmalıdı");
                control = false;
            }
            return control;
        }
        private void InsertAllInfo()
        {
            SqlTransaction sqlTransaction=null;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SqlUtility.GetInstance().conString);
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                string insertedId = InsertAd(sqlTransaction);
                InsertAdsImage(sqlTransaction, insertedId);
                sqlTransaction.Commit();
                sqlConnection.Close();
                MessageBox.Show("melumat yadda saxlanildi");
                this.Close();
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                MessageBox.Show("melumat yadda saxlanilan zaman xəta baş verdi");
            }
            
        }
        private string InsertAd(SqlTransaction sqlTransaction)
        {
            string query = @"INSERT INTO [dbo].[TblSavedAds]
           ([ModelId]
           ,[BanTypeId]
           ,[Walk]
           ,[ColorId]
           ,[Price]
           ,[CurrencyId]
           ,[IsCredit]
           ,[IsBarter]
           ,[FuelTypeId]
           ,[TransmissionId]
           ,[Year]
           ,[EngineCapacityId]
           ,[EnginePower]
           ,[Note]
           ,[AlloyWheels]
           ,[CentralLocking]
           ,[LeatherInterior]
           ,[SeatVentilation]
           ,[ABS]
           ,[Parktronic]
           ,[Xenon]
           ,[Luke]
           ,[AirConditioning]
           ,[RearViewCamera]
           ,[RainSensor]
           ,[HeatedSeats]
           ,[SideCurtains]
           ,[Name]
           ,[CityId]
           ,[Email])
     VALUES
           (@ModelId
           ,@BanTypeId
           ,@Walk
           ,@ColorId
           ,@Price
           ,@CurrencyId
           ,@IsCredit
           ,@IsBarter
           ,@FuelTypeId
           ,@TransmissionId
           ,@Year
           ,@EngineCapacityId
           ,@EnginePower
           ,@Note
           ,@AlloyWheels
           ,@CentralLocking 
           ,@LeatherInterior
           ,@SeatVentilation
           ,@ABS
           ,@Parktronic
           ,@Xenon
           ,@Luke
           ,@AirConditioning
           ,@RearViewCamera
           ,@RainSensor
           ,@HeatedSeats
           ,@SideCurtains
           ,@Name
           ,@CityId
           ,@Email);SELECT SCOPE_IDENTITY();";


            SqlCommand sqlCommand = new SqlCommand(query, sqlTransaction.Connection);
            sqlCommand.Transaction = sqlTransaction;
            sqlCommand.Parameters.Add("ModelId", SqlDbType.Int).Value = LkUpEtModels.EditValue;
            sqlCommand.Parameters.Add("BanTypeId", SqlDbType.Int).Value = LkUpEtBanType.EditValue;
            sqlCommand.Parameters.Add("Walk", SqlDbType.Int).Value = NcUDWalks.Value;
            sqlCommand.Parameters.Add("ColorId", SqlDbType.Int).Value = LkUpEtColor.EditValue;
            sqlCommand.Parameters.Add("Price", SqlDbType.Int).Value = NcUDPrice.Value;
            sqlCommand.Parameters.Add("CurrencyId", SqlDbType.Int).Value = RgCurrency.EditValue;
            sqlCommand.Parameters.Add("IsCredit", SqlDbType.Bit).Value = ChkCredit.Checked;
            sqlCommand.Parameters.Add("IsBarter", SqlDbType.Bit).Value = ChkBarter.Checked;
            sqlCommand.Parameters.Add("FuelTypeId", SqlDbType.Int).Value = LkUpEtFuel.EditValue;
            sqlCommand.Parameters.Add("TransmissionId", SqlDbType.Int).Value = LkUpEtTransmission.EditValue;
            sqlCommand.Parameters.Add("Year", SqlDbType.Int).Value = LkUpEtGraduationYear.EditValue;
            sqlCommand.Parameters.Add("EngineCapacityId", SqlDbType.Int).Value = LkUpEtEngineCapacity.EditValue;
            sqlCommand.Parameters.Add("EnginePower", SqlDbType.Int).Value = NcUDEnginePower.Value;
            sqlCommand.Parameters.Add("Note", SqlDbType.NVarChar).Value = METext.EditValue;
            sqlCommand.Parameters.Add("AlloyWheels", SqlDbType.Bit).Value = CEAlloyWeels.Checked;
            sqlCommand.Parameters.Add("CentralLocking", SqlDbType.Bit).Value = CECentralBlock.Checked;
            sqlCommand.Parameters.Add("LeatherInterior", SqlDbType.Bit).Value = CELeatherSolon.Checked;
            sqlCommand.Parameters.Add("SeatVentilation", SqlDbType.Bit).Value = CESeatVentil.Checked;
            sqlCommand.Parameters.Add("ABS", SqlDbType.Bit).Value = CEAbc.Checked;
            sqlCommand.Parameters.Add("Parktronic", SqlDbType.Bit).Value = CEParkRadar.Checked;
            sqlCommand.Parameters.Add("Xenon", SqlDbType.Bit).Value = CEKsenonLamp.Checked;
            sqlCommand.Parameters.Add("Luke", SqlDbType.Bit).Value = CELuk.Checked;
            sqlCommand.Parameters.Add("AirConditioning", SqlDbType.Bit).Value = CEConditioner.Checked;
            sqlCommand.Parameters.Add("RearViewCamera", SqlDbType.Bit).Value = CEBackCamera.Checked;
            sqlCommand.Parameters.Add("RainSensor", SqlDbType.Bit).Value = CERainSensor.Checked;
            sqlCommand.Parameters.Add("HeatedSeats", SqlDbType.Bit).Value = CESeatHeat.Checked;
            sqlCommand.Parameters.Add("SideCurtains", SqlDbType.Bit).Value = CESideCurtains.Checked;
            sqlCommand.Parameters.Add("Name", SqlDbType.NVarChar).Value = TxtName.EditValue;
            sqlCommand.Parameters.Add("CityId", SqlDbType.Int).Value = LkUpEtCities.EditValue;
            sqlCommand.Parameters.Add("Email", SqlDbType.NVarChar).Value = TxtEmail.EditValue;

            return sqlCommand.ExecuteScalar().ToString();
        }
        private void InsertAdsImage(SqlTransaction sqlTransaction, string AdsId)
        {
            DataTable dataTable = (DataTable)GridControlPicture.DataSource;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                string query = @"INSERT INTO[dbo].[TblCarImages]
                            ([CarImage]
                            ,[AdsId])
                            VALUES
                             (@CarImage
                            ,@AdsId)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlTransaction.Connection);
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Add("CarImage", SqlDbType.VarBinary).Value = dataRow["CarImage"];
                sqlCommand.Parameters.Add("AdsId", SqlDbType.Int).Value = AdsId;
                sqlCommand.ExecuteNonQuery();
            }

        }
        private void GControlPicture_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button == GControlPicture.CustomHeaderButtons[0])
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files | *.jpg;*.png;*.jpeg;";
                DataTable dataTable = (DataTable)GridControlPicture.DataSource;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in openFileDialog.FileNames)
                    {
                        dataTable.Rows.Add(0, GetByteImage(fileName));
                        GetByteImage(fileName);
                    }
                }
                GridControlPicture.Refresh();
            }
        }
        private byte[] GetByteImage(string fileName)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            imageByteArray = binaryReader.ReadBytes((int)fileStream.Length);
            binaryReader.Close();
            fileStream.Close();
            return imageByteArray;
        }
    }
}
