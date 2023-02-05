using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Xml;
using serwersms;
using SmsSender;
using System.Timers;

namespace SmsServiceTest
{
    class Program
    {
       

        static void Main(string[] args)
        {
              SMS sms;
             int IntervalInMinutes = 1;
            Timer timer;

            sms = new SMS(new SMSParms
            {
                user = "",
                password = "",
                format = "json",
                sender = "TEST"
            });

            timer = new Timer(IntervalInMinutes* 60000);


            try
            {
                sms.Send("telefon", "test");
                Console.WriteLine(sms._smsResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey(true);
            


    /*
    try
    {

        var serwerssms = new SerwerSMS("webapi_skati33", "Qw@szx1@#");
        var data = new Dictionary<string, string>();




    //    var response = serwerssms.senders.index(data).ToString();

     //   Console.WriteLine(response);


        String phone = "+48512198821";
        String text = "TEST";
        String sender = "TEST"; 


        data.Add("details", "1");
        serwerssms.format = "json";

        var response = serwerssms.messages.sendSms(phone, text, sender, data).ToString();
        Console.WriteLine(response);



    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    Console.ReadKey(true);
    */

        }


    }
}
