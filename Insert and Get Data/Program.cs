using System;
using System.Data;
using System.Data.SqlClient;

namespace Insert_and_Get_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-OPC709QK; " +
                                "initial catalog = {0}; " +
                                "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Mencari Data");
                                        Console.WriteLine("4. Menghapus Data");
                                        Console.WriteLine("5. Keluar");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA Customer\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;

                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA MAHASISWA\n");
                                                    Console.WriteLine("Masukkan ID Customer :");
                                                    string IdCus = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Customer:");
                                                    string NmCus = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Depan Customer:");
                                                    string NmaDpn = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Belakang Customer:");
                                                    string NmaBel = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Customer:");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(IdCus, NmCus, NmaDpn, NmaBel, Almt, notlpn, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                break;
                                            case '4':
                                                break;
                                            case '5':
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid Option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;

                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From Customer", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void search(SqlConnection con)
        {
        }
        public void delete(SqlConnection con)
        {

        }
        public void insert(string IdCus, string NmCus, string NmaDep, string NmaBel,string Almt, string notlpn, SqlConnection con)
        {
            string str = "";
            str = "insert into Customer (id_customer, nama_customer, namadep_customer, namabel_customer,alamat_customer, tlp_customer)"
            + " values(@id_customer,@namacus,namadep,namabel,@alamat,@notlpn)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id_customer", IdCus)); 
            cmd.Parameters.Add(new SqlParameter("namacus", NmCus));
            cmd.Parameters.Add(new SqlParameter("namadep", NmaDep));
            cmd.Parameters.Add(new SqlParameter("namabel", NmaBel));
            cmd.Parameters.Add(new SqlParameter("alamat", Almt));
            cmd.Parameters.Add(new SqlParameter("notlpn", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}