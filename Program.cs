using MySql.Data.MySqlClient;
using System;
using System.Data;


internal class Program
{
    private static void Main(string[] args)
    {
        //db_conection();
        int valor, validar;
        string opc;
        do
        {
            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine("1 Agregar");
            Console.WriteLine("2 Editar");
            Console.WriteLine("3 Eliminar");
            Console.WriteLine("4 Listar");
            Console.WriteLine("5 Buscar");
            Console.WriteLine("0 Salir");

            do
            {
                validar = 1;

                try
                {
                    Console.Write("Ingrese un valor entre 1 y 5:");
                    valor = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception error)
                {
                    validar = 0; valor = 0;
                    //Console.Write("Ingrese un valor entre 1 y 5: ");
                }

            } while (validar == 0);

            switch (valor)
            {
                case 1:
                    Console.WriteLine("Agregar");
                    Console.WriteLine("Ingrese el nombre:");
                    string nombre = Console.ReadLine();
                    InsertData(nombre);
                    //Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Editar");
                    Console.WriteLine("Ingrese el ID del registro a editar:");
                    int idEditar = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese el nuevo nombre:");
                    string nombreEditar = Console.ReadLine();
                    UpdateData(idEditar, nombreEditar);
                    //Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("Eliminar");
                    Console.WriteLine("Ingrese el ID del registro a eliminar:");
                    int idEliminar = Convert.ToInt32(Console.ReadLine());
                    DeleteData(idEliminar);
                    //Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine("Lista");
                    SelectData();
                    Console.ReadKey();
                    break;
                case 5:
                    Console.WriteLine("Buscar por ID");
                    Console.WriteLine("Ingrese el ID a buscar:");
                    int idBuscar = Convert.ToInt32(Console.ReadLine());
                    SelectDataById(idBuscar);
                    Console.ReadKey();
                    break;
                default:
                    if (valor != 0)
                    {
                        Console.WriteLine("Se ingreso un valor fuera de rango");
                        Console.ReadKey();
                    }
                    break;
            }


        } while (valor != 0);
    }
    public static void db_conection()
    {
        Console.WriteLine("C#: Conectando con MySQL");

        try
        {

            string connectionString;
            connectionString = "server=localhost;port=3306;uid=root;pwd='admin';database=db_p;charset=utf8;SslMode=none;";
            MySqlConnection con = new MySqlConnection(connectionString);

            con.Open();
            Console.WriteLine("Conection is " + con.State.ToString() + Environment.NewLine);


            MySqlCommand com = con.CreateCommand();

            //1.- MySQL INTER INTO
            com.CommandText = "INSERT INTO data(name) VALUES('Mel')";
            com.ExecuteNonQuery();
            Console.WriteLine("--Registro Insertado! Presione cualquier tecla para ver el resultado ...");
            Console.ReadKey();

            //2.- MYSQL UPDATE
            com.CommandText = "UPDATE data SET id = 4, name = 'Melany' WHERE id=3";
            com.ExecuteNonQuery();
            Console.WriteLine("--Registro Actualizado! Presione cualquier tecla para ver el resultado ...");
            Console.ReadKey();

            //3.- MYSQL DELETE
            com.CommandText = "DELETE FROM data WHERE id = 4";
            com.ExecuteNonQuery();
            Console.WriteLine("--Registro Borrado! Presione cualquier tecla para ver el resultado ...");
            Console.ReadKey();

            //4.-Read || MYSQL SELECT (FROM WHERE)
            com.CommandType = System.Data.CommandType.Text;
            // com.CommandText = "SELECT * FROM usuarios WHERE id = 1";
            // com.CommandText = "SELECT * FROM usuarios WHERE id NOT IN(1,4)";
            // com.CommandText = "SELECT * FROM usuarios WHERE nombre LIKE '%Miguel%'";
            // com.CommandText = "SELECT * FROM usuarios WHERE nombre LIKE '%Miguel%'";
            com.CommandText = "SELECT * FROM data";

            MySqlDataReader rd = com.ExecuteReader();

            string str = "[id]\t[name]" + Environment.NewLine;

            if (rd.HasRows)
            {

                while (rd.Read())
                {
                    str += Convert.ToString(rd.GetInt32(0)) + "\t" + rd.GetString(1) + "\t"+
                    Environment.NewLine;
                }
                Console.WriteLine(str);
                rd.Close();
            }
            else
            {
                Console.WriteLine("-->Lo siento, Registro no encontrado!<---\n");
            }

            rd.Close();
            con.Close();
            Console.WriteLine("Conection is " + con.State.ToString() + Environment.NewLine);

        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write("Error: " + ex.Message.ToString());
        }

        Console.WriteLine("Press any key to exit...");
        Console.Read();
    }

    public static void InsertData(string name)
    {
        try
        {
            string connectionString = "server=localhost;port=3306;uid=root;pwd='admin';database=db_p;charset=utf8;SslMode=none;";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            MySqlCommand com = con.CreateCommand();
            com.CommandText = "InsertData"; // Nombre del procedimiento almacenado
            com.CommandType = CommandType.StoredProcedure;

            // Agregar parámetro al procedimiento almacenado
            com.Parameters.AddWithValue("@name", name);

            com.ExecuteNonQuery();
            Console.WriteLine("--Registro Insertado! Presione cualquier tecla para ver el resultado ...");
            Console.ReadKey();

            con.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write("Error: " + ex.Message.ToString());
        }
    }

    public static void UpdateData(int id, string name)
    {
        try
        {
            string connectionString = "server=localhost;port=3306;uid=root;pwd='admin';database=db_p;charset=utf8;SslMode=none;";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            MySqlCommand com = con.CreateCommand();
            com.CommandText = "UpdateData"; // Nombre del procedimiento almacenado
            com.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al procedimiento almacenado
            com.Parameters.AddWithValue("@idParam", id);
            com.Parameters.AddWithValue("@nameParam", name);

            com.ExecuteNonQuery();
            Console.WriteLine("--Registro Actualizado! Presione cualquier tecla para ver el resultado ...");
            Console.ReadKey();

            con.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write("Error: " + ex.Message.ToString());
        }
    }

    public static void DeleteData(int id)
    {
        try
        {
            string connectionString = "server=localhost;port=3306;uid=root;pwd='admin';database=db_p;charset=utf8;SslMode=none;";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            MySqlCommand com = con.CreateCommand();
            com.CommandText = "DeleteData"; // Nombre del procedimiento almacenado
            com.CommandType = CommandType.StoredProcedure;

            // Agregar parámetro al procedimiento almacenado
            com.Parameters.AddWithValue("@idParam", id);

            com.ExecuteNonQuery();
            Console.WriteLine("--Registro Borrado! Presione cualquier tecla para ver el resultado ...");
            Console.ReadKey();

            con.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write("Error: " + ex.Message.ToString());
        }
    }

    public static void SelectData()
    {
        try
        {
            string connectionString = "server=localhost;port=3306;uid=root;pwd='admin';database=db_p;charset=utf8;SslMode=none;";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            MySqlCommand com = con.CreateCommand();
            com.CommandText = "SelectData"; // Nombre del procedimiento almacenado
            com.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = com.ExecuteReader();

            string str = "[id]\t[name]" + Environment.NewLine;

            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    str += Convert.ToString(rd.GetInt32(0)) + "\t" + rd.GetString(1) + "\t" + Environment.NewLine;
                }
                Console.WriteLine(str);
                rd.Close();
            }
            else
            {
                Console.WriteLine("-->Lo siento, Registro no encontrado!<---\n");
            }

            con.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write("Error: " + ex.Message.ToString());
        }
    }

    public static void SelectDataById(int id)
    {
        try
        {
            string connectionString = "server=localhost;port=3306;uid=root;pwd='admin';database=db_p;charset=utf8;SslMode=none;";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            MySqlCommand com = con.CreateCommand();
            com.CommandText = "SelectDataById"; // Nombre del procedimiento almacenado
            com.CommandType = CommandType.StoredProcedure;

            // Agregar parámetro al procedimiento almacenado
            com.Parameters.AddWithValue("@idParam", id);

            MySqlDataReader rd = com.ExecuteReader();

            string str = "[id]\t[name]" + Environment.NewLine;

            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    str += Convert.ToString(rd.GetInt32(0)) + "\t" + rd.GetString(1) + "\t" + Environment.NewLine;
                }
                Console.WriteLine(str);
                rd.Close();
            }
            else
            {
                Console.WriteLine("-->Lo siento, Registro no encontrado!<---\n");
            }

            con.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.Write("Error: " + ex.Message.ToString());
        }
    }
}
