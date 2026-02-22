using System;
using System.Data;
using Microsoft.Data.SqlClient;


namespace ADO_CRUD
{
    class Program
    {
        
        static string connectionString = "Data Source=FLEEK\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False;";

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("===== STUDENT CRUD APP =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Choose option: ");

                switch (Console.ReadLine())
                {
                    case "1": InsertStudent(); break;
                    case "2": ReadStudents(); break;
                    case "3": UpdateStudent(); break;
                    case "4": DeleteStudent(); break;
                    case "5": exit = true; break;
                    default: Console.WriteLine("Invalid option!"); break;
                }
            }
        }
        static void InsertStudent()
        {
            Console.Write("Enter Id: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Email: ");
            string email = Console.ReadLine()!;

            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (Id,Name, Age, Email) VALUES (@Id,@Name, @Age, @Email)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id",id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery(); 
                    Console.WriteLine(rows > 0 ? "Student Added!" : "Failed!");
                }
            }
        }
        
        static void ReadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            int id=reader.GetInt32(0);
                            string name=reader.GetString(1);
                            int age=reader.GetInt32(2);
                            string mail=reader.GetString(3);
                            Console.WriteLine($" {id} | {name} | {age} | {mail}");
                        }
                    }
                }
                
            }
            System.Console.WriteLine();
        }

        
        // static void ReadWithAdapter()
        // {
        //     using (SqlConnection con = new SqlConnection(connectionString))
        //     {
        //         string query = "SELECT * FROM Students";

        //         // SqlDataAdapter fills a DataTable (no need to keep connection open)
        //         SqlDataAdapter adapter = new SqlDataAdapter(query, con);
        //         DataTable dt = new DataTable();

        //         adapter.Fill(dt); // opens & closes connection automatically

        //         foreach (DataRow row in dt.Rows)
        //         {
        //             Console.WriteLine($"ID: {row["Id"]} | Name: {row["Name"]} | Age: {row["Age"]} | Email: {row["Email"]}");
        //         }
        //     }
        // }
        static void UpdateStudent()
        {
            ReadStudents(); 
            System.Console.WriteLine();

            Console.Write("Enter Student ID to update: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Enter New Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter New Age: ");
            int age = int.Parse(Console.ReadLine()!);

            Console.Write("Enter New Email: ");
            string email = Console.ReadLine()!;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET Name=@Name, Age=@Age, Email=@Email WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Student Updated!" : "ID not found!");
                }
            }
        }
        static void DeleteStudent()
        {
            ReadStudents(); 
            System.Console.WriteLine();

            Console.Write("Enter Student ID to delete: ");
            int id = int.Parse(Console.ReadLine()!);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine(rows > 0 ? "Student Deleted!" : "ID not found!");
                }
            }
        }
    }
}