using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using DefaultNamespace;
using log4net.Config;
using TriatlonGUI.connectionUtils;
using TriatlonGUI.Domain;
using log4net.Config;
using TriatlonGUI;
using TriatlonGUI.Repository;
using TriatlonGUI.Service;

namespace TriatlonGUI
{
    internal class Program
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Configuration Settings for TriatlonDB " +
                     ConfigurationManager.ConnectionStrings["TriatlonDB"].ConnectionString);
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", ConfigurationManager.ConnectionStrings["TriatlonDB"].ConnectionString);
            ParticipantRepository participantRepository = new ParticipantDBRepo(props);
            RefereeRepository refereeRepository = new RefereeDBRepo(props);
            TrialRepository trialRepository = new TrialDBRepo(props);
            ResultRepository resultRepository = new ResultDBRepo(props);
            IService service = new ServiceImpl(participantRepository,trialRepository,resultRepository,refereeRepository);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Creează instanța formularului de login și afișează-l
            LoginForm loginForm = new LoginForm(service);
            Application.Run(loginForm);
            
        }

    }

}