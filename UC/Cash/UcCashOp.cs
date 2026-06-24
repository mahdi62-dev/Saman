using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Saman.UC
{
    public partial class UcCashOp : DevExpress.XtraEditors.XtraUserControl
    {
        public byte _ModID { get; set; }
        private DataTable _dtEncCash;
        public int _EncID { get; set; }

        public int _PageID { get; set; }

        public int _EncType { get; set; }

        public int _CashID { get; set; }
        public UcCashOp()
        {
            InitializeComponent();

        }
        private void EncNull()
        {
            lblTitle.Text = null;
            lblTitle2.Text = null;
            lblTitleEncDate.Text = null;

            lblCashDate.Text = null;
            lblCashID.Text = null;

        }
        private void CashNull()
        {
            lblTitle.Text = null;
            lblTitle2.Text = null;
            lblTitleEncDate.Text = null;

        }
        private void UcCashOp_Load(object sender, EventArgs e)
        {
            LIB.UL.LangChange(2);

            ActiveControl = txtEncID;

            LIB.UL.FillLookup(luePayType, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "PayType", "ADName='PayType'", null);
            LIB.UL.FillLookup(lueBank, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "Bank", "ADName='Bank'", null);
            LIB.UL.FillLookup(lueTransType, "1", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "TransType", "ADName='TransType'", null);
            LIB.UL.FillLookup(luePayGroup, "0", DAL.cSet.cnStringA, false, "ID,Name", "vADR", "PrePayGroup", "ADName='PrePayGroup'", null);

        }

        private void EncSearch(int encID)
        {
            EncNull();

            txtEncID.ErrorText = null;

            var bllCash = new BLL.Cash.CashOp();
            _dtEncCash = bllCash.GetEncData(encID);

            if (_dtEncCash.Rows.Count > 0)
            {
                txtEncID.Text = _dtEncCash.Rows[0]["EncID"].ToString();
                lblCashID.Text = _dtEncCash.Rows[0]["CashID"].ToString();
                lblCashDate.Text = _dtEncCash.Rows[0]["CashDate"].ToString();

                _CashID = (int)_dtEncCash.Rows[0]["CashID"];
                _PageID = (int)_dtEncCash.Rows[0]["PageID"];

                lblTitle.Text = _dtEncCash.Rows[0]["FirstName"] + " " + _dtEncCash.Rows[0]["LastName"];
                lblTitle2.Text = _dtEncCash.Rows[0]["Age"] + " سال - فرزند " + _dtEncCash.Rows[0]["FatherName"]; ;
                lblTitleEncDate.Text = _dtEncCash.Rows[0]["StartDate"].ToString();

                txtPageDepID.Text=_dtEncCash.Rows[0]["DepID"].ToString();
                txtPageCode.Text = _dtEncCash.Rows[0]["PageCode"].ToString();
                lblPageDepName.Text = _dtEncCash.Rows[0]["DepName"].ToString();


                lblRemainAmount.Text = _dtEncCash.Rows[0]["RemainAmount"].ToString();
                int RA = Convert.ToInt32(lblRemainAmount.Text);
                lblRemainAmount.Text = (RA.ToString("N0"));

                lblPayAmount.Text = _dtEncCash.Rows[0]["PayAmount"].ToString();
                int PA = Convert.ToInt32(lblPayAmount.Text);
                lblPayAmount.Text = (PA.ToString("N0"));

                lblTotalAmount.Text = _dtEncCash.Rows[0]["TotalAmount"].ToString();
                int TA = Convert.ToInt32(lblTotalAmount.Text);
                lblTotalAmount.Text = (TA.ToString("N0"));

            }
            //var dalData = new DAL.Data();
            //gridSearch.DataSource = dalData.GetDataTable(_dtEncCash);
            else
            {
                EncNull();
                txtEncID.ErrorText = "بیماری یافت نشد";
            }
        }

        private void panMain_Paint(object sender, PaintEventArgs e)
        {
            App.Control.LookupEditLine(luePayType, e);
            App.Control.LookupEditLine(lueBank, e);
            //App.Control.LookupEditLine(lueTransType, e);
            App.Control.LookupEditLine(luePayGroup, e);
            App.Control.TextEditLine(txtBankDate, e);
            App.Control.TextEditLine(txtNote, e);
            App.Control.TextEditLine(txtBankNumber, e);


        }

        private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text))
            {
                EncSearch(int.Parse(txtEncID.Text));
            }
        }



        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            App.Control.TextEditLine(txtEncID, e);

        }

        private bool CheckValidation()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(txtEncID.Text))
            {
                txtEncID.ErrorText = "لطفا کد را وارد نمایید";

                isValid = false;

            }

            if (luePayType.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePayType.Text))
            {
                luePayType.ErrorText = "لطفا نوع پرداخت را انتخاب نمایید";
                isValid = false;
            }

            if (luePayGroup.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePayGroup.Text))
            {
                luePayGroup.ErrorText = "لطفا گروه پرداخت را انتخاب نمایید";
                isValid = false;
            }

            if (!string.IsNullOrEmpty(lblCashID.Text))
            {
                ucMessageCashNotSave1.Visible = true;
                isValid = false;
            }
            




            return isValid;
        }


        private void btnCashSave_Click(object sender, EventArgs e)
        {
            if (!CheckValidation()) 
                return;

            _CashID = Convert.ToInt32(lblCashID.Text);

            int amount = Convert.ToInt32(lblRemainAmount.Text);

            if (_CashID > 0)
            {
                return;
            }

            
            var con = new SqlConnection { ConnectionString = DAL.cSet.cnString };

            var com = new SqlCommand("spCashSave")
            {
                Connection = con,
                CommandType = CommandType.StoredProcedure
            };

            com.Parameters.AddRange(new[]
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@EncID", txtEncID.Text),
                new SqlParameter("@PageID", _PageID),
                new SqlParameter("@CashType", 1),
                new SqlParameter("@PrePayGroup", luePayGroup.EditValue),
                new SqlParameter("@PayType", luePayType.EditValue),
                new SqlParameter("@Bank", lueBank.EditValue),
                new SqlParameter("@TransType", lueTransType.EditValue),
                new SqlParameter("@BankNumber", txtBankNumber.Text),
                new SqlParameter("@BankDate", txtBankDate.Text),
                new SqlParameter("@Note", txtNote.Text),

            });
            //con.Open();
            //var adapt = new SqlDataAdapter { SelectCommand = com };
            //var dt = new DataTable();
            //adapt.Fill(dt);
            //con.Close();





    

            var dalData = new DAL.Data();
            var dtCash = new DataTable();
            dtCash = dalData.GetDataTable(com);



           lblCashID.Text = dtCash.Rows[0]["CashID"].ToString();
           lblCashDate.Text = dtCash.Rows[0]["CashDate"].ToString();


           lblRemainAmount.Text = dtCash.Rows[0]["RemainAmount"].ToString();
           lblPayAmount.Text = dtCash.Rows[0]["PayAmount"].ToString();

            ucMessageCashSave1.Visible = true;
           
            
        }




        private void btnEsc_Click(object sender, EventArgs e)
        {
            var panUC = (this.Parent) as DevExpress.XtraEditors.PanelControl;
            panUC.Controls.Clear();

            var ucDefult = new UcDefult() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
            panUC.Controls.Add(ucDefult);
        }

       
    }
}
