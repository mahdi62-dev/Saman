using System;
using System.Collections.Generic;
using Saman.SmsWebService;

namespace Saman.APP;

public class SMS
{
    public string SendSms(string UserName, string Password, string MessaSmsText, long MobileNumber)
    {
        var ErrorMessage = string.Empty;

        try
        {
            var smslinedID = 0;

            var ws = new SendReceive();


            var smsLine = ws.GetSMSLines(UserName, Password, ref ErrorMessage);

            smslinedID = smsLine[0].ID;


            if (!int.TryParse(smslinedID.ToString(), out smslinedID)) throw new Exception("شماره آی دی معتبر نیست");

            var sendDetaiLs = new List<WebServiceSmsSend>();
            {
                var isFlash = false;

                sendDetaiLs.Add(new WebServiceSmsSend
                    {
                        MessageBody = MessaSmsText,
                        MobileNo = MobileNumber,
                        IsFlash = isFlash
                    }
                );
            }

            /////////////Send Info To WebService
            var result = ws.SendMessage(
                UserName,
                Password,
                sendDetaiLs.ToArray(),
                smslinedID,
                null,
                ref ErrorMessage);
        }
        catch
        {
            ErrorMessage = "مشکل در ارسال اس ام اس";
        }

        return ErrorMessage;
    }
}