using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Collections;

namespace FEMAIDE0._0
{
    /*
     * The TrueFx class is a static class that acts as an interface for the True FX server. 
     * This class will be able to :
     * 
     * 
     * 
     *
     * */
    internal static class TrueFX
    {

        private static readonly String[] ALLCURRENCIESARRAY =
            {"EUR/USD","USD/JPY","GBP/USD","EUR/GBP","CAD/CHF","CAD/JPY","CHF/JPY",
             "EUR/AUD","USD/CHF","EUR/JPY","EUR/CHF","USD/CAD","AUD/USD","GBP/JPY",
             "AUD/CAD","AUD/CHF","AUD/JPY","AUD/NZD","EUR/CAD","EUR/NOK","EUR/NZD",
             "GBP/CAD","GBP/CHF","NZD/JPY","NZD/USD","USD/NOK","USD/SEK"};
        private static readonly String ALLCURRENCIESSTRING =
                ("EUR/USD,USD/JPY,GBP/USD,EUR/GBP,CAD/CHF,CAD/JPY,CHF/JPY,EUR/AUD,USD/CHF," +
                 "EUR/JPY,EUR/CHF,USD/CAD,AUD/USD,GBP/JPY,AUD/CAD,AUD/CHF,AUD/JPY," +
                 "AUD/NZD,EUR/CAD,EUR/NOK,EUR/NZD,GBP/CAD,GBP/CHF,NZD/JPY," +
                 "NZD/USD,USD/NOK,USD/SEK");
        private static String sessionid;
        private readonly static String baseuri = "https://webrates.truefx.com/rates/connect.html?";
        private static WebRequest webrequest;
        private static WebResponse webresponse;
        //private static BackgroundWorker forexbatch = new BackgroundWorker();
        private static int frequencyinmilliseconds = 2000; //default is every two seconds
        //private static Boolean keyisinuse = false;
        private static Object batchinfolock = new object();
        private static String informationbatch;
        private static String informationcomplete;
        private static Thread seperatethread = new Thread(collectdataasync);

        private static Boolean pastmodeenabled = false;
        private static StreamReader csvreader;
        private static String fedatafolderpath = null;
        private static String folerpathsecondpart;
        private static String currentlineinformation;
        private static int number_of_lines_read;


        private static Hashtable pastmodecurrencypostion = new Hashtable() {

            { "EUR/USD",0 }, { "USD/JPY",0 }, { "GBP/USD",0 }, { "EUR/GBP",0 },
            { "CAD/CHF",0 }, { "CAD/JPY",0 }, { "CHF/JPY",0 }, { "EUR/AUD",0 },
            { "USD/CHF",0 }, { "EUR/JPY",0 }, { "EUR/CHF",0 }, { "USD/CAD",0 },
            { "AUD/USD",0 }, { "GBP/JPY",0 }, { "AUD/CAD",0 }, { "AUD/CHF",0 },
            { "AUD/JPY",0 }, { "AUD/NZD",0 }, { "EUR/CAD",0 }, { "EUR/NOK",0 },
            { "EUR/NZD",0 }, { "GBP/CAD",0 }, { "GBP/CHF",0 }, { "NZD/JPY",0 },
            { "NZD/USD",0 }, { "USD/NOK",0 }, { "USD/SEK",0 }, 

        };








        public static void initialize (  ) {




            seperatethread.Start();
        }

        private static void collectdataasync ( )
        {
            //I think all we need to do to enable past mode is to change this method a bit.
            //Something like if ( pastmode ), read the csv lines instead of the serevr information
            //I think that would work. Just have a private static field in TrueFx called past
            //mode or something, and then just enable it when it is enabled in the TrueFx
            //Mission Control

            // August 14th, Monday, 2017
            //
            // I need to be able to get to mission control interface without the program crashing because it can't get to the
            // interent. Thats the whole point of pastmode, to be able to trade offline, and with past data am I right?


            // In order for this method to run properly, we need to call the method "Authenticate" later on in the program 
            // before hand. Why? This is unreliable and unpredictable. I don't even have a check for that. GET ON THAT

            Boolean incompleteinformation;
            
            while (true)
            {

                incompleteinformation = false;
                webrequest = WebRequest.Create(baseuri + "id=" + sessionid);
                webresponse = webrequest.GetResponse();
                //The Reason for 5 characters long is that the True FX
                //server returns an Environment.NewLine if it is
                //unavailable, and that is represented by a \n\r.
                //and a nirmal response would be at least more than
                //that so...
                lock ( batchinfolock )
                {
                    informationbatch = new System.IO.StreamReader(
                    webresponse.GetResponseStream()).ReadToEnd();
                    foreach ( String currency in ALLCURRENCIESARRAY )
                    {
                        if ( ! ( informationbatch.Contains ( currency ) ) )
                        {
                            incompleteinformation = true;
                        }
                    }
                    if ( ! ( incompleteinformation ) ) { informationcomplete = informationbatch; }
                    //Console.WriteLine(informationbatch);
                }
                Thread.Sleep(frequencyinmilliseconds);
            }
        }



        public static double parsebidprice ( String bidbigfigurestring, String bidpointsstring )
        {

            //The bidbigfigurestring and the bidpointsstring should
            //be passed in as the raw information [ 2 ], and information [ 3 ].
            double bidbigfigure = Double.Parse(bidbigfigurestring);
            double bidpoints = Double.Parse(bidpointsstring);
            if (!((bidbigfigure.ToString()).Contains("."))) { bidbigfigurestring = (bidbigfigure.ToString() + "."); }
            else if (bidbigfigure.ToString().Length == 3) { bidbigfigurestring = bidbigfigure.ToString() + "0"; }
            else { bidbigfigurestring = bidbigfigure.ToString(); }
            bidpointsstring = (bidpoints).ToString();
            if (bidpointsstring.Length < 3) { bidpointsstring = ("0" + bidpointsstring); }
            return ( Double.Parse(bidbigfigurestring + bidpointsstring ) );

        }



        public static double parseofferprice ( String offerbigfigurestring, String offerpointsstring )
        {

            double offerbigfigure = Double.Parse( offerbigfigurestring );
            double offerpoints = Double.Parse( offerpointsstring );
            if (!((offerbigfigure.ToString()).Contains("."))) { offerbigfigurestring = (offerbigfigure.ToString() + "."); }
            else if (offerbigfigure.ToString().Length == 3) { offerbigfigurestring = offerbigfigure.ToString() + "0"; }
            else { offerbigfigurestring = offerbigfigure.ToString(); }
            offerpointsstring = (offerpoints).ToString();
            if (offerpointsstring.Length < 3) { offerpointsstring = ("0" + offerpointsstring); }
            return ( Double.Parse(offerbigfigurestring + offerpointsstring ) );

        }



        public static int getfrequency ( ) { return frequencyinmilliseconds;  }


        public static String getinformationbatch ( ) { return informationbatch; }

        public static String getinformationcomplete ( ) { return informationcomplete;  }

        public static void enablepastmode ( ) { pastmodeenabled = true; }

        public static void disablepastmode ( ) { pastmodeenabled = false; }

        public static Boolean getpastmodestate ( ) { return pastmodeenabled;  }

        public static void setfedatafolderpath ( String path ) { fedatafolderpath = path; }

        public static int setfrequency ( int milliseconds ) {
            int minimumfrequency = 2000;
            int maximumfrequency = 5000;
            if ( minimumfrequency <= milliseconds && milliseconds >= maximumfrequency )
            {
                frequencyinmilliseconds = milliseconds;
                return milliseconds;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR WITHIN | CLASS : TrueFX | METHOD : setfrequency |" +
                    " THE FREQUENCY GIVEN IS NOT WITHIN THE RANGE " + minimumfrequency + "~" +
                    maximumfrequency + ". RETURNING A NEGATIVE NUMBER...");
                return -1;
                    
            }

        }



        // I know exactly where I use this now. I use this in the 


        public static Boolean Authenticate(String username, String password)
        {
            /*String allcurrencies =
                ("EUR/USD,USD/JPY,GBP/USD,EUR/GBP,CAD/CHF,CAD/JPY,CHF/JPY,EUR/AUD,USD/CHF," +
                 "EUR/JPY,EUR/CHF,USD/CAD,AUD/USD,GBP/JPY,AUD/CAD,AUD/CHF,AUD/JPY," +
                 "AUD/NZD,EUR/CAD,EUR/NOK,EUR/NZD,GBP/CAD,GBP/CHF,NZD/JPY," +
                 "NZD/USD,USD/NOK,USD/SEK");*/
            webrequest = WebRequest.Create
                (baseuri + "u=" + username +
                 "&p=" + password + "&q=" + username +
                 password + "&c=" + ALLCURRENCIESSTRING + "&f=csv");
            String requestURL = (baseuri + "u=" + username +
                 "&p=" + password + "&q=" + username +
                 password + "&c=" + ALLCURRENCIESSTRING + "&f=csv");
            Console.WriteLine(requestURL);
            webresponse = webrequest.GetResponse();
            //The least amount the authentification length could return
            //I think.


            //PROBLEM: Why do i have to authenticate with the internet in order to just practice trading in 
            //past mode? That shouldn't be the case. I need to fix that

            // This gets the number of BYTES of the response, not the character count XD
            // We need an streamreader to the read the data returned from the resource

            StreamReader authenticationResponseData = new StreamReader(webresponse.GetResponseStream());
            String content = authenticationResponseData.ReadToEnd();
            if (content.Equals("not authorized") || content.Equals("not authorized\n")) {
                return false;
            }
            sessionid = content;
            return true;
        }
        

        private static String GetRelevantData ( String currencyrate, String truefxdata )
        {
            
            if (!(truefxdata.Contains(currencyrate))) {
                Console.WriteLine("The Current Batch Of TrueFX Data Does" +
                    "Not Contain The Request Currency Rate " + currencyrate +
                    ". MessageFrom: TureFX Class | Method: GetRelevantData(" +
                    "String currencyrate, Stringtruefxdata ");
                return null;
            }
            String relevantdata = truefxdata.Substring(
                truefxdata.IndexOf(currencyrate));
            return (relevantdata.Substring(0,
                relevantdata.IndexOf('\n')));

        }




        private static String[] PackageRelevantDataIntoIntArray(String relevantdata)
        {
            String[] stringinformation = new String[9];
            for (int j = 0; j < stringinformation.Length; j++)
            {
                if (j == stringinformation.Length - 1) {
                    stringinformation[j] = relevantdata.Substring(relevantdata.IndexOf('\n') + 1);
                    continue;
                }
                stringinformation[j] = relevantdata.Substring(
                    0, relevantdata.IndexOf(','));
                relevantdata = relevantdata.Substring(
                    relevantdata.IndexOf(',') + 1);
            }
            //int[] information = new int[8];
            //stringinformation.CopyTo(information, 1);
            return stringinformation;

        }
        public static String[] RequestCurrency ( String currencyrate )
        {
            //Heres another problem. Most of the ways RequestCurrency
            //Is used runs through an array, meaning it changes every single time
            //How do we keep track of where the stream left off? Food for thought.


            //PROBLEM: Pastmode isn't working!!!! Because it reads the same line
            //over and over!!! This is because the stream isn't conscience of 
            //where it left off when it comes back. I tried to fix this with an
            //if statement below the pastmodeenabled if statement, however I'm
            //just hit with a null exception every single time because I never 
            //initialized it!!!

            if (pastmodeenabled && (fedatafolderpath != null) )
            {

                number_of_lines_read = (int)pastmodecurrencypostion[currencyrate];
                Console.WriteLine( "Number of lines read: " + number_of_lines_read);
       
                folerpathsecondpart = currencyrate.Replace('/', 'v');
                //if (!(csvreader.Equals ( File.OpenText(fedatafolderpath + "\\" +  folerpathsecondpart + ".txt"))))
                //{
                //I need to add a fail safe check to this statement soon
                csvreader = File.OpenText(fedatafolderpath + "\\" + folerpathsecondpart + ".txt");
                //}

                //If the file is an empty file
                if (csvreader.Peek() == -1)
                {
                    Console.Error.WriteLine("There is no information to draw upon from" +
                        " the csv data file '" + fedatafolderpath + folerpathsecondpart + ".txt" +
                        "'. Returning null. Messgae From: TrueFX Class | Method: RequestCurrency(" +
                        " String currencyrate");
                    return null;
                }

                for ( int i = 0; i < number_of_lines_read; i++ )
                {
                    
                    currentlineinformation = csvreader.ReadLine();

                    //If the reader came to the end of the file
                    if (csvreader.Peek() == -1)
                    {
                        Console.Error.WriteLine("There is no more information to draw upon from" +
                            " the csv data file '" + fedatafolderpath + folerpathsecondpart + ".txt" +
                            "'. Returning null. Messgae From: TrueFX Class | Method: RequestCurrency(" +
                            " String currencyrate");
                        return null;
                    }

                }

                currentlineinformation = csvreader.ReadLine();
                Console.WriteLine("Current Line Information" + currentlineinformation);
                Console.WriteLine("Position before update: " + pastmodecurrencypostion[currencyrate]);
                pastmodecurrencypostion[currencyrate] = ( ( int ) pastmodecurrencypostion[currencyrate] + 1 );
                Console.WriteLine( "Position after change: " + pastmodecurrencypostion[currencyrate] );

                
                return PackageRelevantDataIntoIntArray(currentlineinformation);

            }


            //Console.WriteLine(informationbatch);


            lock (batchinfolock)
            {
                if (!(informationbatch.Contains(currencyrate)))
                    {
                        Console.WriteLine("The Current Batch Of TrueFX Data Does" +
                            "Not Contain The Request Currency Rate " + currencyrate +
                            ". MessageFrom: TureFX Class | Method: GetRelevantData(" +
                            "String currencyrate, Stringtruefxdata ");
                    return (
                PackageRelevantDataIntoIntArray(
                    GetRelevantData(
                        currencyrate, informationcomplete
                     )
                 )
             );
                }
                



                return (
                    PackageRelevantDataIntoIntArray(
                        GetRelevantData(
                            currencyrate, informationbatch
                         )
                     )
                 );
            }
                
        }





    }
        
}
