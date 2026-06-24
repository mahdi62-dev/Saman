//using System;
//using System.Data;


//namespace Saman.BLL.Opr
//{
//   public class OprCalc
//    { 
//        public DataTable CheckAnesK (DataTable _dtServiceReg)
//        {
//           // _dtServiceReg = CheckRinoplastiAnesK(_dtServiceReg);

//            var dtOpr = _dtServiceReg.Select("OprItem=1 AND SMID=0").CopyToDataTable();

//            double bookAnesK = 0;
//            for (int i = 0; i < dtOpr.Rows.Count; i++)
//            {
//                if (Convert.ToDouble(dtOpr.Rows[i]["BookAnesK"]) == 0)
//                    dtOpr.Rows[i]["BookAnesK"] = 3;
//            }

//            double maxAnesK = 0;
//            double anesK = 0;
//            int anesRecNo = 0;
//            int anesServiceID = 0;
//            int anesServiceCode = 0;
//            string anesServiceName = null;

//            int ServiceCode = 0;
//            int RinoCount = 0;

//            for (int i = 0; i < dtOpr.Rows.Count; i++)
//            {
//                ServiceCode = Convert.ToInt32(dtOpr.Rows[i]["ServiceCode"]);
//                if (ServiceCode == 300070 || ServiceCode == 300075 || ServiceCode == 300080 || ServiceCode == 100290 || ServiceCode == 100295 || ServiceCode == 200645)
//                    RinoCount += 1;

//                anesK = Convert.ToDouble(dtOpr.Rows[i]["BookAnesK"]);
//                if (anesK > maxAnesK)
//                {
//                    maxAnesK = anesK;
//                    anesRecNo = Convert.ToInt32(dtOpr.Rows[i]["RecNo"]);
//                    anesServiceID = Convert.ToInt32(dtOpr.Rows[i]["ServiceID"]);
//                    anesServiceCode = Convert.ToInt32(dtOpr.Rows[i]["ServiceCode"]);
//                    anesServiceName = _dtServiceReg.Rows[i]["Name"].ToString();
//                }
//            }


//            int curRecNo = 0;
//            int curServiceID = 0;
//            int curServiceCode = 0;
//            int curSMID = 0;
//            string curServiceName = null;

//            double SumK49 = 0;


//            for (int i = 0; i < _dtServiceReg.Rows.Count; i++)
//            {

//                if (Convert.ToInt32(_dtServiceReg.Rows[i]["SMCode"]) == 49)
//                    SumK49 += Convert.ToDouble(_dtServiceReg.Rows[i]["KFinal"]);


//                if (Convert.ToInt32(_dtServiceReg.Rows[i]["OprItem"]) ==2)
//                {
//                    curRecNo = Convert.ToInt32(_dtServiceReg.Rows[i]["RecNo"]);
//                    curServiceID = Convert.ToInt32(_dtServiceReg.Rows[i]["ServiceID"]);
//                    curServiceCode = Convert.ToInt32(_dtServiceReg.Rows[i]["ServiceCode"]);
//                    curSMID = Convert.ToInt32(_dtServiceReg.Rows[i]["SMID"]);


//                    if (curRecNo != anesRecNo && curServiceID != anesServiceID)
//                    {
//                        _dtServiceReg.Rows[i]["RecNo"] = anesRecNo;
//                        _dtServiceReg.Rows[i]["ServiceID"] = anesServiceID;
//                        _dtServiceReg.Rows[i]["ServiceCode"] = anesServiceCode;
//                        _dtServiceReg.Rows[i]["Name"] = anesServiceName;

//                        if (curSMID == 0)
//                        {
//                            _dtServiceReg.Rows[i]["KBase"] = maxAnesK;

//                        }
//                        else
//                        {

//                        }
//                    }


//                }
//            }


//            for (int i = 0; i < _dtServiceReg.Rows.Count; i++)
//            {
//                if (Convert.ToInt32(_dtServiceReg.Rows[i]["SMCode"]) == 39 && RinoCount>0 && SumK49<=1 )
//                {
//                    _dtServiceReg.Rows[i].Delete();
//                    _dtServiceReg.AcceptChanges();
//                }

//            }


//            return _dtServiceReg;
//        }


//        private DataTable CheckRinoplastiAnesK(DataTable _dtServiceReg)
//        {
//            bool IsRino = false;
//            int ServiceCode = 0;
//            int AnesK = 0, AnesTimeK = 0;
//            double TotalAnesK = 0;

//            int RecoveryRowNum = 0;
//            int RecoveryK = 0;
//            int RecoveryCRUD = 0;
//            ServiceCode = Convert.ToInt32(_dtServiceReg.Rows[0]["ServiceCode"]);
//            if (ServiceCode == 300070 || ServiceCode == 300075 || ServiceCode == 300080)
//                IsRino = true;


//            for (int i = 0; i < _dtServiceReg.Rows.Count; i++)
//            {
//                if (Convert.ToInt32(_dtServiceReg.Rows[i]["OprItem"]) == 2)
//                {
//                    TotalAnesK += Convert.ToDouble(_dtServiceReg.Rows[i]["KBase"]);
//                    if (Convert.ToDouble(_dtServiceReg.Rows[i]["SMID"]) == 11)
//                        AnesTimeK = Convert.ToInt32(_dtServiceReg.Rows[i]["KBase"]);

//                    if (Convert.ToDouble(_dtServiceReg.Rows[i]["SMID"]) == 9)
//                    {
//                        RecoveryRowNum = i;
//                        RecoveryK = Convert.ToInt32(_dtServiceReg.Rows[i]["KBase"]);

//                        RecoveryCRUD = Convert.ToInt32(_dtServiceReg.Rows[i]["CRUD"]);
//                    }
//                }

//            }


//            if (IsRino)
//            {
//                if(RecoveryCRUD==2) _dtServiceReg.Rows[RecoveryRowNum]["CRUD"]=4;
//                else
//                {
//                    _dtServiceReg.Rows[RecoveryRowNum].Delete();
//                }

//            }

//            return _dtServiceReg;

//        }


//    }
//}

