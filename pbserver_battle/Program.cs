using Battle.config;
using Battle.data;
using Battle.data.sync;
using Battle.data.xml;
using Battle.network;
using CrashReporterDotNET;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;

namespace Battle
{
    internal class Program
    {
        private static DateTime GetLinkerTime(Assembly assembly, TimeZoneInfo target = null)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeFromUtc(epoch.AddSeconds(secondsSince1970), target ?? TimeZoneInfo.Local);
        }
        private static void Speed1(List<int> BoomPlayers)
        {
            foreach (int slot in BoomPlayers)
            {
            }
        }
        private static void Speed2(List<int> BoomPlayers)
        {
            for(int i = 0; i < BoomPlayers.Count; i++)
            {
                int slot = BoomPlayers[i];
            }
        }
        private static void NEW(float value)
        {

        }
        protected static void Main(string[] args)
        {

            Config.Load();
            Logger.checkDirectory();
            Console.Title = "Point Blank - Battle";
            Logger.info(@"     /-??-/_____________________________\-??-\");
            Logger.info(@"    /-??-/      Point Blank BATTLE       \-??-\");
            Logger.info(@"   /-??-/        By Flare Industries          \-??-\");
            Logger.info(@"/-??-/_______________________________________\-??-\");
            string date = GetLinkerTime(Assembly.GetExecutingAssembly(), null).ToString("dd/MM/yyyy HH:mm");
            Logger.info("|-??-|__________| Release date |______________|-??-|");
            Logger.info("|-??-|         '" + date + "'             |-??-|");
            Logger.info("|-??-|  Open Source       |-??-|");
            Logger.info("|-??-|________________________________________|-??-|");
            Logger.warning("[Aviso] Servidor ativo em " + Config.hosIp + ":" + Config.hosPort);
            Logger.warning("[Aviso] Sincronizar infos ao servidor: " + Config.sendInfoToServ);
            Logger.warning("[Aviso] Limite de drops: " + Config.maxDrop);
            Logger.warning("[Aviso] Duração da C4: (" + Config.plantDuration + "s/" + Config.defuseDuration + "s)");
            Logger.warning("[Aviso] Super munição: " + Config.useMaxAmmoInDrop);
            bool check = true;
           // if (!test(check, date, args, item2, LiveDate)) item2 = true;
            MappingXML.Load();
            CharaXML.Load();
            MeleeExceptionsXML.Load();
            ServersXML.Load();
            Logger.warning("|-??-|-----------------------------------|-??-|");
            if (check)
            {
                Battle_SyncNet.Start();
                BattleManager.init();
            }
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}