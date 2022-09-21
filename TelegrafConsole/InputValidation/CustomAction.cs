using Microsoft.Deployment.WindowsInstaller;
using System;
using System.IO;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace InputValidation
{
    public class CustomActions
    {
        private static bool PingHost(string HostOrIP)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(HostOrIP);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }

        [CustomAction]
        public static ActionResult ValidateInput(Session session)
        {
            session.Log("Begin InputValidation");
            //MessageBox.Show("Begin InputValidation");
            try
            {
                string host = session["HOST"];
                int port = int.Parse(session["PORT"]);

                //while (!PingHost(host) && !host.Contains(""))
                //{
                //    MessageBox.Show("\"" + host + "\" is not reachable. Please enter a valid proxy hostnam or IP address.",
                //        "Proxy Not Reachable",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //    host = session["HOST"];
                //}

                string prgfiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
                string telegrafdDir = Path.Combine(prgfiles + "\\Telegraf\\telegraf.d");
                // MessageBox.Show(prgfiles);

                if (!Directory.Exists(telegrafdDir))
                    Directory.CreateDirectory(telegrafdDir);

                //MessageBox.Show(host + ", " + port);
                Console.WriteLine(host + ", " + port);
                session.Log("CLA's received: Host: " + host + ", Port: " + port);

                File.WriteAllText(Path.Combine(telegrafdDir + "\\10-wavefront.conf"), "  [[outputs.wavefront]]\n");
                File.AppendAllText(Path.Combine(telegrafdDir + "\\10-wavefront.conf"), "    host = \"" + host + "\"\n");
                File.AppendAllText(Path.Combine(telegrafdDir + "\\10-wavefront.conf"), "    port = " + port);

                return ActionResult.Success;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e.Message);
                session.Log(e.Message);
                return ActionResult.Failure;
            }
        }
    }
}
