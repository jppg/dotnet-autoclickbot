using System;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace AutoClickBot
{
    class Program
    {
        ChromeDriver _driver = new ChromeDriver("C:\\Aplics\\ChromeDriver");
        List<Result> _lstResults = new List<Result>();

        static void Main(string[] args)
        {

            string banner = @"     _              _                ____   _   _          _        ____            _   
    / \     _   _  | |_    ___      / ___| | | (_)   ___  | | __   | __ )    ___   | |_ 
   / _ \   | | | | | __|  / _ \    | |     | | | |  / __| | |/ /   |  _ \   / _ \  | __|
  / ___ \  | |_| | | |_  | (_) |   | |___  | | | | | (__  |   <    | |_) | | (_) | | |_ 
 /_/   \_\  \__,_|  \__|  \___/     \____| |_| |_|  \___| |_|\_\   |____/   \___/   \__|
                                                                                        ";

            Console.WriteLine(banner);

            Console.WriteLine("I'll open links inside the file 'Links.txt'");

            Program prg = new Program();
            prg.Login();
            prg.OpenLinks();        
            prg.ShowResults();
        }

        public void Login()
        {
            //TODO: adapt this block to the specific context
        }

        public void OpenLinks()
        {
            string[] links = System.IO.File.ReadAllLines(@"Links.txt");
            foreach(string lnk in links)
            {
                Result res = new Result(lnk);
                _driver.Url = lnk;
                res.EndTimestamp = DateTime.Now;

                _lstResults.Add(res);
            }
            _driver.Close();
        }

        public void ShowResults()
        {
            if(!System.IO.Directory.Exists("Results"))
                System.IO.Directory.CreateDirectory("Results");

            using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(string.Format(@"Results\{0}.html", DateTime.Now.ToString("yyyyMMddHHmmss"))))
            {
                file.WriteLine("<html><body><h1>Report of execution</h1>");
                file.WriteLine(string.Format("<h2>Created at {0}</h2>", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                file.WriteLine("<table border='1'><tr><th>URL</th><th>Elapsed time</th></tr>");
                foreach(Result res in _lstResults)
                {
                    file.WriteLine(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", res.Url, res.ElapsedMilliseconds));
                }
                file.WriteLine("</table></body></html>");
            }            
        }
    }

    class Result{

        public string Url {get; set;}
        public DateTime StartTimestamp {get; private set;}
        public DateTime EndTimestamp {get; set;}
        public double ElapsedMilliseconds {
            get
            {
                return EndTimestamp.Subtract(StartTimestamp).TotalMilliseconds;
            }
        }
        public Result(string url)
        {
            Url = url;
            StartTimestamp = DateTime.Now;
        }
    }
}
