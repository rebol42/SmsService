using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using serwersms;

namespace SmsSender
{
    public class SMS
    {
        private string _user;
        string _password;
        string _format;
        string _sender;
        public string _smsResponse;
        public string _smsError;


        public SMS(SMSParms smsParms)
        {
            _user = smsParms.user;
            _password = smsParms.password;
            _format = smsParms.format;
            _sender = smsParms.sender;
        }


        public async Task Send(string phone, string text)
        {

            var serwerssms = new SerwerSMS(_user,_password);
            var data = new Dictionary<string, string>();

            try
            {

                data.Add("details", "1");
                serwerssms.format = _format;

                var response = serwerssms.messages.sendSms(phone, text, _sender, data).ToString();
                this.SendResponse(response.ToString());
            }
            catch (Exception e)
            {
                this.SendError(e.Message);
            }
        

        }

        public string SendResponse(string _response = "")
        {
            _smsResponse = _response;
            return _smsResponse;
        }

        public string SendError(string _error="")
        {
            _smsError = _error;
            return _smsError;
        }
    }
}
