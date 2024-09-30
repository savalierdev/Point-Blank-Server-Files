/*
 * Arquivo: LoggerGA.cs
 * Código criado pela MoMz Games
 * Última data de modificação: 09/10/2017
 * Sintam inveja, não nos atinge
 */

using Auth.data.managers;
using Core;
using Microsoft.VisualBasic.Devices;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class LoggerGA
    {
        public static async void updateRAM2()
        {
            while (true)
            {
                Console.Title = "Point Blank - Auth [Users: " + LoginManager._socketList.Count + "; Loaded accs: " + AccountManager.getInstance()._contas.Count + "; Used RAM: " + (GC.GetTotalMemory(true) / 1024) + " KB]";
                await Task.Delay(1000);
            }
        }
    }
}