using System.Data.SqlClient;
using PeopleRegistration2.Models;
using System.Data;

namespace PeopleRegistration2.Repository
{
    public class UserRepository
    {
        public String ConnectionString { get; set; }

        public UserRepository()
        {
            this.ConnectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("TrainingDatabaseSystemConnection");
        }
        public IList<UserModel> Listar()
        {
            IList<UserModel> lista = new List<UserModel>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                String query = "SELECT IDPEOPLE, PERSON_NAME, ADDRESS_PERSON FROM PEOPLE";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    //Recuperando dados...
                    UserModel userModel = new UserModel();
                    userModel.IdUser = Convert.ToInt32(dataReader["IDPEOPLE"]);
                    userModel.UserName = dataReader["PERSON_NAME"].ToString();
                    userModel.Address = dataReader["ADDRESS_PERSON"].ToString();
                    //Adicionado o modelo da lista...
                    lista.Add(userModel);
                }
                connection.Close();
            } // Finaliza o objeto connection

            // Retorna a lista
            return lista;
        }

        public UserModel Query(int id)
        {
            UserModel userModel = new UserModel();

            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("TrainingDatabaseSystemConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query = "SELECT IDPEOPLE, PERSON_NAME, ADDRESS_PERSON FROM PEOPLE WHERE IDPEOPLE = @IDPEOPLE";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@IDPEOPLE", SqlDbType.Int);
                command.Parameters["@IDPEOPLE"].Value = id;
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    //Recuperando dados...
                    userModel.IdUser = (int)dataReader["IDPEOPLE"];
                    userModel.UserName = dataReader["PERSON_NAME"].ToString();
                    userModel.Address = dataReader["ADDRESS_PERSON"].ToString();
                }
                connection.Close();
            } // Finaliza

            // Retorna a lista
            return userModel;
        }

        public void Insert(UserModel userModel)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                String query = "INSERT INTO PEOPLE (PERSON_NAME, ADDRESS_PERSON) VALUES (@userName, @userAddress)";

                SqlCommand command = new SqlCommand(query, connection);

                //Adicionando o valor ao comando
                command.Parameters.Add("@userName", SqlDbType.Text);
                command.Parameters["@userName"].Value = userModel.UserName;
                command.Parameters.Add("@userAddress", SqlDbType.Text);
                command.Parameters["@userAddress"].Value = userModel.Address;

                // Abrindo a conexão com o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(UserModel userModel)
        {
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("TrainingDatabaseSystemConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query = "UPDATE PEOPLE SET PERSON_NAME = @userName, ADDRESS_PERSON = @userAddress WHERE IDPEOPLE = @IDPEOPLE";

                SqlCommand command = new SqlCommand(query, connection);

                //Adicionando o valor ao comando
                command.Parameters.Add("@userName", SqlDbType.Text);
                command.Parameters["@userName"].Value = userModel.UserName;
                command.Parameters.Add("@IDPEOPLE", SqlDbType.Int);
                command.Parameters["@IDPEOPLE"].Value = userModel.IdUser;
                command.Parameters.Add("@userAddress", SqlDbType.Text);
                command.Parameters["@userAddress"].Value = userModel.Address;

                // Abrindo a conexão com o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("TrainingDatabaseSystemConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query = "DELETE PEOPLE WHERE IDPEOPLE = @IDPEOPLE";

                SqlCommand command = new SqlCommand(query, connection);

                //Adicionando o valor ao comando
                command.Parameters.Add("@IDPEOPLE", SqlDbType.Int);
                command.Parameters["@IDPEOPLE"].Value = id;

                // Abrindo a conexão com o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
