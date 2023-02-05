using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SmsSender;
using SmsService.Repositories;
using System.Configuration;
using System.Timers;
using Cipher;

namespace SmsService
{
    public partial class SmsRunService : ServiceBase
    {
        private int IntervalInMinutes;
        private ErrorRepository _errorRepository = new ErrorRepository();
        private SMS _sms;
        private Timer _timer;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private StringCipher _stringCipher = new StringCipher("53CC1150-2E4D-49CB-82BA-A4B58EB8E9E3");

        public SmsRunService()
        {
            InitializeComponent();

            // to ustawiamy na pierwszym uruchomieniu zeby na podstawie utworzonych klas Domains/Wrappers/Configurations utworzyl baze danych.
            /*
            using (var context = new ApplicationDbContext())
            {
                var errors = context.Errors.ToList();
            }
            
            */
            IntervalInMinutes = 1;


            _sms = new SMS(new SMSParms
            {
                user = ConfigurationManager.AppSettings["user"],
                password = DecryptSmsPassword(),
                format = ConfigurationManager.AppSettings["format"],
                sender = ConfigurationManager.AppSettings["sender"],

            });

            _timer = new Timer(IntervalInMinutes * 60000);


        }
          

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += DoWork;
            _timer.Start();
            Logger.Info("Service started...");
        }

        protected override void OnStop()
        {
        }
        private async void DoWork(object sender, ElapsedEventArgs e)
        {

            try
            {
                await SendError();

            }
            catch (Exception ex)
            {

                Logger.Error(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task SendError()
        {
            var error = _errorRepository.GetLastErrors();
            var users = _errorRepository.GetUsers();

            if (error == null || !error.Any())
                return;

            if (users == null || !users.Any())
                return;


            foreach (var usr in users)
            {
                foreach (var err in error)
                {
                    string errMeg = err.Message + err.Date;

                    await _sms.Send(usr.phone, errMeg);

                    if (_sms._smsResponse != "")
                        _errorRepository.UpdateError(err);

                    Logger.Info($"Log z wysłania wiadomości {_sms._smsResponse}");

                    

                    if (_sms.SendError() != "")
                        Logger.Error(_sms._smsError);
                }
            }

          
        }



        private string DecryptSmsPassword()
        {
            // pobieramy hasło  z configa 
            var encryptedPassword = ConfigurationManager.AppSettings["password"];

            //sprawdzamy czy hasło jest zaszyfrowane
            if (encryptedPassword.StartsWith("encrypt:"))

            {
                encryptedPassword = _stringCipher.Encrypt(encryptedPassword.Replace("encrypt:", ""));

                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["password"].Value = encryptedPassword;
                configFile.Save();
            }

            return _stringCipher.Decrypt(encryptedPassword);
        }
    }
}
