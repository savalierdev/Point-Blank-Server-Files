/*
 * Arquivo: Program.cs
 * Código criado pela MoMz Games
 * Última data de modificação: 25/03/2017
 * Sintam inveja, não nos atinge
 */

using Core;
using Core.filters;
using Core.managers;
using Core.managers.events;
using Core.managers.server;
using Core.models.account.players;
using Core.server;
using Core.sql;
using Core.xml;
using CrashReporterDotNET;
using Game.data.sync;
using Game.data.managers;
using Game.data.xml;
using Npgsql;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Game
{
    public class Programm
    {
        public static void Main(string[] args)
        {

            Console.Title = "PB Game Server - 1.15.42 / 1.15.41 / 1.15.37";
            Logger.StartedFor = "game";
            Logger.checkDirectorys();
            StringUtil header = new StringUtil();
            header.AppendLine(@"     /-??-/_____________________________\-??-\");
            header.AppendLine(@"    /-??-/       Point Blank Game Server        \-??-\");
            header.AppendLine(@"   /-??-/          by Flare Industries         \-??-\");
            header.AppendLine(@"/-??-/_______________________________________\-??-\");
            string date = ComDiv.GetLinkerTime(Assembly.GetExecutingAssembly(), null).ToString("dd/MM/yyyy HH:mm");
            header.AppendLine("|-??-|__________| Build Date |______________|-??-|");
            header.AppendLine("|-??-|         '" + date + "'             |-??-|");
            header.AppendLine("|-??-|   Open-Source        |-??-|");
            header.AppendLine("|-??-|________________________________________|-??-|");
            Logger.info(header.getString());
            ConfigGS.Load();
                BasicInventoryXML.Load();
                ServerConfigSyncer.GenerateConfig(ConfigGS.configId);
                ServersXML.Load();
                ChannelsXML.Load(ConfigGS.serverId);
                EventLoader.LoadAll();
                TitlesXML.Load();
                TitleAwardsXML.Load();
                ClanManager.Load();
                NickFilter.Load();
                MissionCardXML.LoadBasicCards(1);
                BattleServerXML.Load();
                RankXML.Load();
                RankXML.LoadAwards();
                ClanRankXML.Load();
                MissionAwards.Load();
                MissionsXML.Load();
                Translation.Load();
                ShopManager.Load(1);
                ClassicModeManager.LoadList();
                RandomBoxXML.LoadBoxes();
                CupomEffectManager.LoadCupomFlags();
                bool check = true;
                Game_SyncNet.Start();
                if (check)
                {
                    bool started = GameManager.Start();
                    Logger.warning("[Aviso] Padrão de textos: " + ConfigGB.EncodeText.EncodingName);
                    Logger.warning("[Bilgi] Sunucu Modu : " + (ConfigGS.isTestMode ? "Testes" : "Público"));
                    Logger.warning(StartSuccess());
                    if (started)
                        LoggerGS.updateRAM();
                }
           // }
            Process.GetCurrentProcess().WaitForExit();
        }
        private static string StartSuccess()
        {
            if (Logger.erro)
                return "[Bilgilendirme] Sunucu Başlatılamadı!";
            return "[Bilgilendirme] Sunucu Aktif. (" + DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + ")";
        }
        public static bool Create(int rank, ItemsModel msg)
        {
            try
            {
                using (NpgsqlConnection connection = SQLjec.getInstance().conn())
                {
                    NpgsqlCommand command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@rank", rank);
                    command.Parameters.AddWithValue("@id", msg._id);
                    command.Parameters.AddWithValue("@name", msg._name);
                    command.Parameters.AddWithValue("@count", (int)msg._count);
                    command.Parameters.AddWithValue("@equip", msg._equip);
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO info_rank_awards(rank_id,item_id,item_name,item_count,item_equip)VALUES(@rank,@id,@name,@count,@equip)";
                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                    return true;
                }
            }
            catch { return false; }
        }
    }
}