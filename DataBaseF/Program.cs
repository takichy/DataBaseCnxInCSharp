using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace DataBaseF
{
    class Program
    {
        public Program()
        {
            Home();
        }

        static String accessChaineConnection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\Abdel\Documents\Etudiant.mdb";
        static String sqlserverConnection = "Data Source=DESKTOP-5ECAUIM;Initial Catalog = master; Integrated Security = True";
        static String OracleConnection = "Data Source = localhost / XE; Persist Security Info = True; User ID = abdel; Password = abdel; Unicode = True";

        static void Main(string[] args)
        {
            new Program();
            Console.ReadKey();
        }

        public void selectDataAccess(String query, OleDbConnection cnx)
        {
            OleDbDataReader reader = null;
            OleDbCommand command = new OleDbCommand(query, cnx);
            String[] tab = query.Split(' ');
            String QueryType = tab[0];
            String[] queryColommeUsed = tab[1].Split(',');
            for (int i = 0; i < queryColommeUsed.Length; i++){
                if (queryColommeUsed[i] == "*"){
                    Console.Write("id\t\t: nom\t\t: prénom\t\t: ");
                }
                else{
                    Console.Write(queryColommeUsed[i] + "\t\t: ");
                }
            }
            Console.WriteLine("\n_____________________________________________");
            reader = command.ExecuteReader();
            while (reader.Read()){
                for (int i = 0; i < reader.FieldCount; i++){
                    Console.Write(reader[i].ToString() + "\t\t: ");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("\n_____________________________________________");
        }
        public void selectDataSql(String query, SqlConnection cnx)
        {
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand(query, cnx);

            String[] tab = query.Split(' ');
            String QueryType = tab[0];
            String[] queryColommeUsed = tab[1].Split(',');

            for (int i = 0; i < queryColommeUsed.Length; i++){
                if (queryColommeUsed[i] == "*"){
                    Console.Write("id\t\t| nom\t\t| prénom\t\t| ");
                }
                else{
                    Console.Write(queryColommeUsed[i] + "\t\t| ");
                }
            }
            Console.WriteLine("\n_____________________________________________");
            reader = command.ExecuteReader();
            while (reader.Read()){
                for (int i = 0; i < reader.FieldCount; i++){
                    Console.Write(reader[i].ToString() + "\t\t| ");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("\n_____________________________________________");
        }
        public void selectDataOracle(String query, OracleConnection cnx)
        {
            OracleDataReader reader = null;
            OracleCommand command = new OracleCommand(query, cnx);
            String[] tab = query.Split(' ');
            String QueryType = tab[0];
            String[] queryColommeUsed = tab[1].Split(',');
            for (int i = 0; i < queryColommeUsed.Length; i++)
            {
                if (queryColommeUsed[i] == "*")
                {
                    Console.Write("id\t\t: nom\t\t: prénom\t\t: ");
                }
                else
                {
                    Console.Write(queryColommeUsed[i] + "\t\t: ");
                }
            }
            Console.WriteLine("\n______________________________________________");
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i].ToString() + "\t\t: ");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("\n_______________________________________________");
        }
        public void updateDataAccess(String query, OleDbConnection cnx, String QueryType)
        { 
            OleDbCommand command = new OleDbCommand(query, cnx);
            try{
                int returnedNumber = command.ExecuteNonQuery();
                Console.WriteLine("Le nombre de ligne affecter par'" + QueryType + "' est : " + returnedNumber);
            }
            catch (OleDbException ex){
                Console.WriteLine("\n********\n\t ERREUR :" + ex.Message);
            }
        }
        public void updateDataSql(String query, SqlConnection cnx, String QueryType)
        { 
            SqlCommand command = new SqlCommand(query, cnx);
            try{
                int returnedNumber = command.ExecuteNonQuery();
                Console.WriteLine("Le nombre de ligne affecter par'" + QueryType + "' est : " + returnedNumber);
            }
            catch (OleDbException ex){
                Console.WriteLine("\n********\n\t ERREUR :" + ex.Message);
            }
        }
        public void updateDataOracle(String query, OracleConnection cnx, String QueryType)
        {
            OracleCommand command = new OracleCommand(query, cnx);
            try{
                int returnedNumber = command.ExecuteNonQuery();
                Console.WriteLine("Le nombre de ligne affecter par '" + QueryType + "' est : " + returnedNumber);
            }
            catch (OleDbException ex){
                Console.WriteLine("\n********\n\t ERREUR :" + ex.Message);
            }
        }

        public void Home()
        {
            const int maxMenuItems = 4;
            int selector = 0;
            bool good = false;
            while (selector != maxMenuItems){
                Console.Clear();
                DrawTitle();
                DrawMenu(maxMenuItems);
                good = int.TryParse(Console.ReadLine(), out selector);
                if (good){
                    switch (selector){
                        case 1:
                            Console.Clear();
                            DrawStarLine();
                            Console.WriteLine("'ACCESS DataBase' vs voulez saisire la chaine de connexion ou bien de connecter automatiquement");
                            DrawStarLine();
                            Console.WriteLine(" 1 pour automatique");
                            Console.WriteLine(" 2 pour Manuel");
                            Console.WriteLine(" 3 pour retour");
                            DrawStarLine();
                            Console.WriteLine("Enter votre choix: taper 1,2,3 ou {0} pour Quitter", 3);
                            DrawStarLine();
                            string path = @"C:\Users\Public\Documents\access.txt";
                            int selector2 = 0;
                            bool good2 = false;
                            good2 = int.TryParse(Console.ReadLine(), out selector2);
                            if (good){
                                switch (selector2){
                                    case 1:{
                                            try{
                                                accessChaineConnection = File.ReadAllText(path, Encoding.UTF8);
                                                OleDbConnection cnx = new OleDbConnection(accessChaineConnection);

                                                cnx.Open();
                                                Console.WriteLine("Access Connected");
                                                Console.WriteLine("**********************\n Enter la requete SQL => ");
                                                String query = Console.ReadLine();

                                                String[] tab = query.Split(' ');
                                                String QueryType = tab[0];
                                                if (QueryType == "select" || QueryType == "SELECT"){
                                                    selectDataAccess(query, cnx);
                                                }
                                                else{
                                                    updateDataAccess(query, cnx, QueryType);
                                                }
                                            }
                                            catch (Exception ex){
                                                Console.WriteLine(ex.GetBaseException().ToString());
                                            }
                                            break;
                                    }
                                    case 2:{
                                            Console.WriteLine(" Taper La Chaine de Connexion !");
                                            DrawStarLine();
                                            String chainedeCnx = Console.ReadLine();
                                            File.AppendAllText(path, chainedeCnx);
                                            try
                                            {
                                                OleDbConnection cnx = new OleDbConnection(chainedeCnx);

                                                cnx.Open();
                                                Console.WriteLine("Access Connected");
                                                Console.WriteLine("**********************\n Enter la requete SQL => ");
                                                String query = Console.ReadLine();

                                                String[] tab = query.Split(' ');
                                                String QueryType = tab[0];
                                                if (QueryType == "select" || QueryType == "SELECT"){
                                                    selectDataAccess(query, cnx);
                                                }
                                                else{
                                                    updateDataAccess(query, cnx, QueryType);
                                                }
                                            }
                                            catch (Exception ex){
                                                Console.WriteLine(ex.GetBaseException().ToString());
                                            }
                                            break;
                                    }
                                    case 3:{
                                            Console.Clear();
                                            DrawTitle();
                                            DrawMenu(maxMenuItems);
                                            break;
                                    }
                                }
                            }
                            break;
                        case 2:
                            Console.Clear();
                            DrawStarLine();
                            Console.WriteLine("'SQLSERVER DataBase' vs voulez saisire la chaine de connexion ou de connecter automatiquement");
                            DrawStarLine();
                            Console.WriteLine(" 1 pour automatique");
                            Console.WriteLine(" 2 pour Manuel");
                            Console.WriteLine(" 3 pour retour (choisir une autre base données)");
                            DrawStarLine();
                            Console.WriteLine("Enter votre choix: taper 1,2,3 ou {0} pour Quitter", 3);
                            DrawStarLine();
                            string path2 = @"C:\Users\Public\Documents\sqlserver.txt";
                            int selector3 = 0;
                            bool good3 = false;
                            good3 = int.TryParse(Console.ReadLine(), out selector3);
                            if (good){
                                switch (selector3){
                                    case 1:{
                                            sqlserverConnection = File.ReadAllText(path2, Encoding.UTF8);
                                            try{
                                                SqlConnection cnx = new SqlConnection(sqlserverConnection);
                                                cnx.Open();
                                                Console.WriteLine("Sql Server Connected");
                                                Console.WriteLine("**********************\n Enter la requete SQL => ");
                                                String query = Console.ReadLine();

                                                String[] tab = query.Split(' ');
                                                String QueryType = tab[0];
                                                if (QueryType == "select" || QueryType == "SELECT"){
                                                    selectDataSql(query, cnx);
                                                }
                                                else{
                                                    updateDataSql(query, cnx, QueryType);
                                                }
                                            }
                                            catch (Exception ex){
                                                Console.WriteLine(ex.GetBaseException().ToString());

                                            }
                                            break;
                                        }
                                    case 2:{
                                            Console.WriteLine(" Taper La Chaine de Connexion !");
                                            DrawStarLine();
                                            String chainedeCnx = Console.ReadLine();
                                            File.AppendAllText(path2, chainedeCnx);
                                            try
                                            {
                                                SqlConnection cnx = new SqlConnection(chainedeCnx);
                                                cnx.Open();
                                                Console.WriteLine("Sql Server Connected ");
                                                Console.WriteLine("**********************\n Enter la requete SQL => ");
                                                String query = Console.ReadLine();

                                                String[] tab = query.Split(' ');
                                                String QueryType = tab[0];
                                                if (QueryType == "select" || QueryType == "SELECT"){
                                                    selectDataSql(query, cnx);
                                                }
                                                else{
                                                    updateDataSql(query, cnx, QueryType);
                                                }

                                            }
                                            catch (Exception ex){
                                                Console.WriteLine(ex.GetBaseException().ToString());
                                            }
                                            break;
                                    }
                                    case 3:{
                                            Console.Clear();
                                            DrawTitle();
                                            DrawMenu(maxMenuItems);
                                            break;
                                        }
                                }
                            }
                            break;
                        case 3:
                            Console.Clear();
                            DrawStarLine();
                            Console.WriteLine("'ORACLE DataBase' vs voulez saisire la chaine de connexion ou de connecter automatiquement");
                            DrawStarLine();
                            Console.WriteLine(" 1 pour automatique");
                            Console.WriteLine(" 2 pour Manuel");
                            Console.WriteLine(" 3 pour retour (choisir une autre base données)");
                            DrawStarLine();
                            Console.WriteLine("Enter votre choix: taper 1,2,3 ou {0} pour Quitter", 3);
                            DrawStarLine();
                            string path3 = @"C:\Users\Public\Documents\oracle.txt";
                            int selector4 = 0;
                            bool good4 = false;
                            good4 = int.TryParse(Console.ReadLine(), out selector4);
                            if (good){
                                switch (selector4){
                                    case 1:{
                                            OracleConnection = File.ReadAllText(path3, Encoding.UTF8);
                                            try{
                                                OracleConnection cnx = new OracleConnection(OracleConnection);

                                                cnx.Open();
                                                Console.WriteLine("oracle Connected");
                                                Console.WriteLine("**********************\n Enter la requete SQL => ");
                                                String query = Console.ReadLine();

                                                String[] tab = query.Split(' ');
                                                String QueryType = tab[0];
                                                if (QueryType == "select" || QueryType == "SELECT"){
                                                    selectDataOracle(query, cnx);
                                                }
                                                else{
                                                    updateDataOracle(query, cnx, QueryType);
                                                }
                                            }
                                            catch (Exception ex){
                                                Console.WriteLine(ex.GetBaseException().ToString());
                                            }
                                            break;
                                        }
                                    case 2:{
                                            Console.WriteLine(" Taper La Chaine de Connexion !");
                                            DrawStarLine();
                                            String chainedeCnx = Console.ReadLine();
                                            File.AppendAllText(path3, chainedeCnx);
                                            try
                                            {
                                                OracleConnection cnx = new OracleConnection(chainedeCnx);

                                                cnx.Open();
                                                Console.WriteLine("oracle Connected");
                                                Console.WriteLine("**********************\n Enter la requete SQL => ");
                                                String query = Console.ReadLine();

                                                String[] tab = query.Split(' ');
                                                String QueryType = tab[0];
                                                if (QueryType == "select" || QueryType == "SELECT"){
                                                    selectDataOracle(query, cnx);
                                                }
                                                else{
                                                    updateDataOracle(query, cnx, QueryType);
                                                }
                                            }
                                            catch (Exception ex){
                                                Console.WriteLine(ex.GetBaseException().ToString());
                                            }
                                            break;
                                        }
                                    case 3:{
                                            Console.Clear();
                                            DrawTitle();
                                            DrawMenu(maxMenuItems);
                                            break;
                                        }
                                }
                            }
                            break;
                        default:
                            if (selector != maxMenuItems){
                                ErrorMessage();
                            }
                            break;
                    }
                }
                else{
                    ErrorMessage();
                }
                Console.ReadKey();
            }

            void ErrorMessage()
            {
                Console.WriteLine("Êrreur, Cliquer au clavier pour continuer.");
            }
            void DrawStarLine()
            {
                Console.WriteLine("********************************");
            }
            void DrawTitle()
            {
                DrawStarLine();
                Console.WriteLine("(: (: (:   OULIDI OMALI Abdelhay   :) :) :)");
                Console.WriteLine("(: (: (:   Bienvenue sur le projet de connection des base de données :) :) :)");
                DrawStarLine();
            }
            void DrawMenu(int maxitems)
            {
                DrawStarLine();
                Console.WriteLine(" 1 pour Access");
                Console.WriteLine(" 2 pour SqlServer");
                Console.WriteLine(" 3 pour Oracle");
                Console.WriteLine(" 4 pour Quitter !");
                DrawStarLine();
                Console.WriteLine("Enter votre choix: taper 1,2,3 ou {0} pour Quitter", maxitems);
                DrawStarLine();
                Console.WriteLine("Choisir une base de données: ");
            }
        }
    }
}
