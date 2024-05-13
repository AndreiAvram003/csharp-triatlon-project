using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using DefaultNamespace;
using log4net.Config;
using Triatlon.connectionUtils;
using Triatlon.Domain;
using log4net.Config;
using Triatlon.Repository;

namespace Triatlon
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
            ParticipantDBRepo repo = new ParticipantDBRepo(props);
            foreach (Participant p in repo.GetAllSorted())
            {
                Console.WriteLine(p.ToString());
            }
            
        }
        
        
    }
}