using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMBKMMII
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            SqlDataReader reader;
            var datasource = @"DESKTOP-3FRST7F\SQLEXPRESS01";//your server
            var database = "webmbkm"; //your database name
            var username = "user2019"; //username of server to connect
            var password = "P@ssw0rd"; //password

            //your connection string 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);


            try
            {
                Console.WriteLine("Openning Connection ...");
                DataTable dt = new DataTable();
                //dt.Columns.Add("NIP");
                //dt.Columns.Add("Nama");
                //dt.Columns.Add("Password");
                //dt.Columns.Add("Email");
                //dt.Columns.Add("RoleID");
                //dt.Columns.Add("Prodi");
                //dt.Columns.Add("NamaProdi");

                //open connection
                conn.Open();
                //reader = new SqlCommand(@"select NIP, Nama, PIN, isnull(Email, '-') Email, Jabatan, Prodi, b.ACAD_PROG_DESCR NamaProdi from datadosenbaru a
                //left join OCS_Prodi_VW b on a.Prodi = b.ACAD_PROG
                //union all 
                //select  NIP, Nama, PIN, isnull(Email, '-') Email, Jabatan, Prodi, b.ACAD_PROG_DESCR NamaProdi from databaa a
                //left join OCS_Prodi_VW b on a.Prodi = b.ACAD_PROG", conn).ExecuteReader();

                SqlCommand command = new SqlCommand(@"select NIP, Nama, PIN, isnull(Email, '-') Email, Jabatan, Prodi, b.ACAD_PROG_DESCR NamaProdi from datadosenbaru a
                left join OCS_Prodi_VW b on a.Prodi = b.ACAD_PROG
                union all 
                select  NIP, Nama, PIN, isnull(Email, '-') Email, Jabatan, Prodi, b.ACAD_PROG_DESCR NamaProdi from databaa a
                left join OCS_Prodi_VW b on a.Prodi = b.ACAD_PROG", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                //conn.Close();
                foreach (DataRow row in dt.Rows)
                {
                    string roleid = string.Empty;

                    string NIP = row["NIP"].ToString();
                    string Nama = row["Nama"].ToString();
                    string PIN = row["PIN"].ToString();
                    string Email = row["Email"].ToString();
                    string Jabatan = row["Jabatan"].ToString();
                    string Prodi = row["Prodi"].ToString();
                    string NamaProdi = row["NamaProdi"].ToString();

                    if (Jabatan == "Dosen")
                        roleid = "1";
                    else if (Jabatan == "Kepala Program Studi")
                        roleid = "4";
                    else if (Jabatan.Contains("Warek"))
                        roleid = "5";
                    else if (Jabatan.Contains("Admin Fakultas"))
                        roleid = "2";
                    else if (Jabatan.Contains("BAA"))
                        roleid = "14";
                    else
                        roleid = "15";

                    string passworduser = BCrypt.Net.BCrypt.HashPassword(PIN, BCrypt.Net.BCrypt.GenerateSalt(12));
                    //dt.Rows.Add(NIP, Nama, passworduser, Email, roleid, Prodi, NamaProdi);


                    Console.WriteLine("Nama \n {0}", Nama);
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append(@"INSERT INTO [dbo].[User] ([UserName],[Password]
                           ,[Email]
                           ,[NoPegawai]
                           ,[Alamat]
                           ,[NoTelp]
                           ,[Token]
                           ,[RoleID]
                           ,[CreatedBy]
                           ,[CreatedDate]
                           ,[UpdatedBy]
                           ,[UpdatedDate]
                           ,[IsActive]
                           ,[IsDeleted]
                           ,[KodeProdi]
                           ,[NamaProdi]) VALUES ");
                    strBuilder.Append("('" + Nama + "', '" + passworduser + "', '" + Email + "', '" + NIP + "' , N'-', N'-', N'-','" + roleid + "', N'Admin', Getdate()" +
                        ", N'Admin', Getdate(), 1, 0, '" + Prodi + "', '" + NamaProdi + "')");

                    string sqlQuery = strBuilder.ToString();

                    //conn.Open();
                    using (SqlCommand cmdquery = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
                    {
                        cmdquery.ExecuteNonQuery(); //execute the Query
                        Console.WriteLine("Query Executed.");
                    }

                    strBuilder.Clear(); // clear all the string
                    Console.WriteLine("insert success \n {0}", Nama);
                }
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //        string roleid = string.Empty;

                //        string NIP = reader["NIP"].ToString();
                //        string Nama = reader["Nama"].ToString();
                //        string PIN = reader["PIN"].ToString();
                //        string Email = reader["Email"].ToString();
                //        string Jabatan = reader["Jabatan"].ToString();
                //        string Prodi = reader["Prodi"].ToString();
                //        string NamaProdi = reader["NamaProdi"].ToString();

                //        if (Jabatan == "Dosen")
                //            roleid = "1";
                //        else if (Jabatan == "Kepala Program Studi")
                //            roleid = "4";
                //        else if (Jabatan.Contains("Warek"))
                //            roleid = "5";
                //        else if (Jabatan.Contains("Admin Fakultas"))
                //            roleid = "14";
                //        else if (Jabatan.Contains("BAA"))
                //            roleid = "2";
                //        else
                //            roleid = "15";

                //        string passworduser = BCrypt.Net.BCrypt.HashPassword(PIN, BCrypt.Net.BCrypt.GenerateSalt(12));
                //        dt.Rows.Add(NIP, Nama, passworduser, Email, roleid, Prodi, NamaProdi);


                //        Console.WriteLine("Nama \n {0}", Nama);
                //    }

                //}
                //else
                //{

                //    Console.WriteLine("No rows found.");

                //}
                //conn.Close();


                Console.WriteLine("Connection successful!");


            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
    }
}
