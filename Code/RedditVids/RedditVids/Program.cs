using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace RedditVids
{
    class Program
    {
        static void Main(string[] args)
        {
            string ans = "";
            do
            {
                string result = "";
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        Console.Write("Enter your reddit video url: ");
                        string url = Console.ReadLine();
                        string htmlCode = client.DownloadString(url);
                        int x = htmlCode.IndexOf("https://v.redd.it/");
                        if (x == -1)
                            throw new Exception("Error finding the video link. Try again.");
                        result = $"https://v.redd.it/{htmlCode[(x + 18)..].Substring(0, htmlCode[(x + 18)..].IndexOf('/'))}/DASH_720.mp4";
                    }
                    Console.Write("Your video link is: ");
                    Console.WriteLine(result);
                    OpenBrowser(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}.");
                }
                Console.Write("Do you want to try another link? (Y/N): ");
                ans = Console.ReadLine();
            } while (ans[0].ToString().ToUpper() == "Y");
        }

        public static void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else
            {
                throw new Exception("Error finding your OS.");
            }
        }
    }
}
