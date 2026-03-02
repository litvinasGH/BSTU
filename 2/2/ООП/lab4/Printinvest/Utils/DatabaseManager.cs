using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Printinvest.Models;
using Printinvest.Models.Enums;

namespace Printinvest.Data
{
    public static class SERDB
    {
        public static DatabaseManager database;
    }

    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            connectionString = ConfigurationManager.ConnectionStrings["PrintInvestConnection"].ConnectionString;
            EnsureDatabaseExists();
        }

        private void EnsureDatabaseExists()
        {
            var masterConnectionString = connectionString.Replace("Database=PRINTINVESTDB", "Database=master");
            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                var checkDbQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = 'PRINTINVESTDB'";
                using (var command = new SqlCommand(checkDbQuery, connection))
                {
                    int dbExists = (int)command.ExecuteScalar();
                    if (dbExists == 0)
                    {
                        string createDbScript = @"
                            CREATE DATABASE PRINTINVESTDB;
                            USE PRINTINVESTDB;
                            CREATE TABLE Users (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                Login NVARCHAR(50) NOT NULL UNIQUE,
                                Password NVARCHAR(100) NOT NULL,
                                Name NVARCHAR(100) NOT NULL,
                                IsAdmin BIT NOT NULL DEFAULT 0,
                                PhotoUri NVARCHAR(255)
                            );
                            CREATE TABLE Equipment (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                Model NVARCHAR(200) NOT NULL,
                                Price DECIMAL(10,2) NOT NULL,
                                Brand NVARCHAR(50),
                                ImagePath NVARCHAR(255)
                            );
                            CREATE TABLE Services (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                Name NVARCHAR(200) NOT NULL,
                                Price DECIMAL(10,2) NOT NULL,
                                DurationDays INT,
                                Type NVARCHAR(50)
                            );
                            CREATE TABLE Comments (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                EquipmentId INT NOT NULL,
                                Author NVARCHAR(100) NOT NULL,
                                Text NVARCHAR(MAX) NOT NULL,
                                Timestamp DATETIME NOT NULL DEFAULT GETDATE(),
                                Rating DECIMAL(3,2),
                                FOREIGN KEY (EquipmentId) REFERENCES Equipment(Id) ON DELETE CASCADE
                            );
                            CREATE TABLE Equipment_Services (
                                EquipmentId INT NOT NULL,
                                ServiceId INT NOT NULL,
                                PRIMARY KEY (EquipmentId, ServiceId),
                                FOREIGN KEY (EquipmentId) REFERENCES Equipment(Id) ON DELETE CASCADE,
                                FOREIGN KEY (ServiceId) REFERENCES Services(Id) ON DELETE CASCADE
                            );
                            CREATE PROCEDURE AddEquipment
                                @Model NVARCHAR(200),
                                @Price DECIMAL(10,2),
                                @Brand NVARCHAR(50),
                                @ImagePath NVARCHAR(255)
                            AS
                            BEGIN
                                INSERT INTO Equipment (Model, Price, Brand, ImagePath)
                                VALUES (@Model, @Price, @Brand, @ImagePath);
                                SELECT SCOPE_IDENTITY();
                            END;
                            CREATE PROCEDURE GetEquipmentByBrand
                                @Brand NVARCHAR(50)
                            AS
                            BEGIN
                                SELECT * FROM Equipment WHERE Brand = @Brand OR @Brand = 'None';
                            END;";
                        string[] sqlStatements = createDbScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var statement in sqlStatements)
                        {
                            using (var createCommand = new SqlCommand(statement.Trim(), connection))
                            {
                                createCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        // CRUD для Equipment
        public int AddEquipment(Equipment equipment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("AddEquipment", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Model", equipment.Model);
                            command.Parameters.AddWithValue("@Price", equipment.Price);
                            command.Parameters.AddWithValue("@Brand", equipment.Brand.ToString());
                            command.Parameters.AddWithValue("@ImagePath", (object)equipment.ImagePath ?? DBNull.Value);
                            var equipmentId = Convert.ToInt32(command.ExecuteScalar());

                            if (equipment.SupportedServices != null)
                            {
                                foreach (var serviceType in equipment.SupportedServices)
                                {
                                    var serviceId = GetOrCreateService(serviceType, connection, transaction);
                                    using (var linkCommand = new SqlCommand(
                                        "INSERT INTO Equipment_Services (EquipmentId, ServiceId) VALUES (@EquipmentId, @ServiceId)",
                                        connection, transaction))
                                    {
                                        linkCommand.Parameters.AddWithValue("@EquipmentId", equipmentId);
                                        linkCommand.Parameters.AddWithValue("@ServiceId", serviceId);
                                        linkCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            transaction.Commit();
                            equipment.Id = equipmentId;
                            return equipmentId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Equipment> GetEquipment(string brand = "None")
        {
            var equipmentList = new List<Equipment>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetEquipmentByBrand", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Brand", brand);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var equipment = new Equipment
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Model = reader["Model"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Brand = (Brand)Enum.Parse(typeof(Brand), reader["Brand"].ToString()),
                                ImagePath = reader["ImagePath"].ToString()
                            };
                            equipmentList.Add(equipment);
                        }
                    }
                }

                foreach (var equipment in equipmentList)
                {
                    equipment.Comments = new ObservableCollection<Comment>(GetComments(equipment.Id, connection));
                    equipment.SupportedServices = GetSupportedServices(equipment.Id, connection);
                }
            }
            return equipmentList;
        }

        public void UpdateEquipment(Equipment equipment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "UPDATE Equipment SET Model=@Model, Price=@Price, Brand=@Brand, ImagePath=@ImagePath WHERE Id=@Id",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", equipment.Id);
                            command.Parameters.AddWithValue("@Model", equipment.Model);
                            command.Parameters.AddWithValue("@Price", equipment.Price);
                            command.Parameters.AddWithValue("@Brand", equipment.Brand.ToString());
                            command.Parameters.AddWithValue("@ImagePath", (object)equipment.ImagePath ?? DBNull.Value);
                            command.ExecuteNonQuery();
                        }

                        using (var deleteServicesCommand = new SqlCommand(
                            "DELETE FROM Equipment_Services WHERE EquipmentId=@EquipmentId",
                            connection, transaction))
                        {
                            deleteServicesCommand.Parameters.AddWithValue("@EquipmentId", equipment.Id);
                            deleteServicesCommand.ExecuteNonQuery();
                        }

                        if (equipment.SupportedServices != null)
                        {
                            foreach (var serviceType in equipment.SupportedServices)
                            {
                                var serviceId = GetOrCreateService(serviceType, connection, transaction);
                                using (var linkCommand = new SqlCommand(
                                    "INSERT INTO Equipment_Services (EquipmentId, ServiceId) VALUES (@EquipmentId, @ServiceId)",
                                    connection, transaction))
                                {
                                    linkCommand.Parameters.AddWithValue("@EquipmentId", equipment.Id);
                                    linkCommand.Parameters.AddWithValue("@ServiceId", serviceId);
                                    linkCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteEquipment(int equipmentId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("DELETE FROM Equipment WHERE Id=@Id", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", equipmentId);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // CRUD для Service
        public int AddService(Service service)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "INSERT INTO Services (Name, Price, DurationDays, Type) VALUES (@Name, @Price, @DurationDays, @Type); SELECT SCOPE_IDENTITY();",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Name", service.Name);
                            command.Parameters.AddWithValue("@Price", service.Price);
                            command.Parameters.AddWithValue("@DurationDays", service.DurationDays);
                            command.Parameters.AddWithValue("@Type", service.Type.ToString());
                            var serviceId = Convert.ToInt32(command.ExecuteScalar());
                            transaction.Commit();
                            service.Id = serviceId;
                            return serviceId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Service> GetServices()
        {
            var services = new List<Service>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Services", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                DurationDays = Convert.ToInt32(reader["DurationDays"]),
                                Type = (ServiceType)Enum.Parse(typeof(ServiceType), reader["Type"].ToString())
                            });
                        }
                    }
                }
            }
            return services;
        }

        public void UpdateService(Service service)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "UPDATE Services SET Name=@Name, Price=@Price, DurationDays=@DurationDays, Type=@Type WHERE Id=@Id",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", service.Id);
                            command.Parameters.AddWithValue("@Name", service.Name);
                            command.Parameters.AddWithValue("@Price", service.Price);
                            command.Parameters.AddWithValue("@DurationDays", service.DurationDays);
                            command.Parameters.AddWithValue("@Type", service.Type.ToString());
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteService(int serviceId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("DELETE FROM Services WHERE Id=@Id", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", serviceId);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // CRUD для User
        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "INSERT INTO Users (Login, Password, Name, IsAdmin, PhotoUri) VALUES (@Login, @Password, @Name, @IsAdmin, @PhotoUri); SELECT SCOPE_IDENTITY();",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Login", user.Login);
                            command.Parameters.AddWithValue("@Password", user.Password);
                            command.Parameters.AddWithValue("@Name", user.Name);
                            command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                            command.Parameters.AddWithValue("@PhotoUri", (object)user.PhotoUri?.ToString() ?? DBNull.Value);
                            var userId = Convert.ToInt32(command.ExecuteScalar());
                            transaction.Commit();
                            user.Id = userId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Login = reader["Login"].ToString(),
                                Password = reader["Password"].ToString(),
                                Name = reader["Name"].ToString(),
                                IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
                                PhotoUri = reader["PhotoUri"] != DBNull.Value ? new Uri(reader["PhotoUri"].ToString()) : null
                            });
                        }
                    }
                }
            }
            return users;
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "UPDATE Users SET Login=@Login, Password=@Password, Name=@Name, IsAdmin=@IsAdmin, PhotoUri=@PhotoUri WHERE Id=@Id",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", user.Id);
                            command.Parameters.AddWithValue("@Login", user.Login);
                            command.Parameters.AddWithValue("@Password", user.Password);
                            command.Parameters.AddWithValue("@Name", user.Name);
                            command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                            command.Parameters.AddWithValue("@PhotoUri", (object)user.PhotoUri?.ToString() ?? DBNull.Value);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("DELETE FROM Users WHERE Id=@Id", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", userId);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // CRUD для Comment
        public void AddComment(Comment comment, int equipmentId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "INSERT INTO Comments (EquipmentId, Author, Text, Timestamp, Rating) VALUES (@EquipmentId, @Author, @Text, @Timestamp, @Rating); SELECT SCOPE_IDENTITY();",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@EquipmentId", equipmentId);
                            command.Parameters.AddWithValue("@Author", comment.Author);
                            command.Parameters.AddWithValue("@Text", comment.Text);
                            command.Parameters.AddWithValue("@Timestamp", comment.Timestamp);
                            command.Parameters.AddWithValue("@Rating", comment.Rating);
                            var commentId = Convert.ToInt32(command.ExecuteScalar());
                            transaction.Commit();
                            comment.Id = commentId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Comment> GetComments(int equipmentId, SqlConnection connection = null)
        {
            var comments = new List<Comment>();
            bool shouldCloseConnection = false;

            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            try
            {
                using (var command = new SqlCommand("SELECT * FROM Comments WHERE EquipmentId = @EquipmentId", connection))
                {
                    command.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new Comment
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Author = reader["Author"].ToString(),
                                Text = reader["Text"].ToString(),
                                Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                                Rating = Convert.ToDouble(reader["Rating"])
                            });
                        }
                    }
                }
            }
            finally
            {
                if (shouldCloseConnection)
                    connection.Close();
            }

            return comments;
        }

        public void UpdateComment(Comment comment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand(
                            "UPDATE Comments SET Author=@Author, Text=@Text, Timestamp=@Timestamp, Rating=@Rating WHERE Id=@Id",
                            connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", comment.Id);
                            command.Parameters.AddWithValue("@Author", comment.Author);
                            command.Parameters.AddWithValue("@Text", comment.Text);
                            command.Parameters.AddWithValue("@Timestamp", comment.Timestamp);
                            command.Parameters.AddWithValue("@Rating", comment.Rating);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteComment(int commentId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("DELETE FROM Comments WHERE Id=@Id", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", commentId);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Метод для сохранения всех Equipment
        public void SaveAllEquipment(List<Equipment> equipmentList)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var equipment in equipmentList)
                        {
                            using (var checkCommand = new SqlCommand(
                                "SELECT Id FROM Equipment WHERE Model = @Model",
                                connection, transaction))
                            {
                                checkCommand.Parameters.AddWithValue("@Model", equipment.Model);
                                var existingId = checkCommand.ExecuteScalar();

                                int equipmentId;
                                if (existingId != null)
                                {
                                    equipment.Id = Convert.ToInt32(existingId);
                                    UpdateEquipmentInTransaction(equipment, connection, transaction);
                                    equipmentId = equipment.Id;
                                }
                                else
                                {
                                    equipmentId = AddEquipment(equipment);
                                }

                                if (equipment.Comments != null)
                                {
                                    foreach (var comment in equipment.Comments)
                                    {
                                        AddComment(comment, equipmentId);
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpdateEquipmentInTransaction(Equipment equipment, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand(
                "UPDATE Equipment SET Model=@Model, Price=@Price, Brand=@Brand, ImagePath=@ImagePath WHERE Id=@Id",
                connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", equipment.Id);
                command.Parameters.AddWithValue("@Model", equipment.Model);
                command.Parameters.AddWithValue("@Price", equipment.Price);
                command.Parameters.AddWithValue("@Brand", equipment.Brand.ToString());
                command.Parameters.AddWithValue("@ImagePath", (object)equipment.ImagePath ?? DBNull.Value);
                command.ExecuteNonQuery();
            }

            using (var deleteServicesCommand = new SqlCommand(
                "DELETE FROM Equipment_Services WHERE EquipmentId=@EquipmentId",
                connection, transaction))
            {
                deleteServicesCommand.Parameters.AddWithValue("@EquipmentId", equipment.Id);
                deleteServicesCommand.ExecuteNonQuery();
            }

            if (equipment.SupportedServices != null)
            {
                foreach (var serviceType in equipment.SupportedServices)
                {
                    var serviceId = GetOrCreateService(serviceType, connection, transaction);
                    using (var linkCommand = new SqlCommand(
                        "INSERT INTO Equipment_Services (EquipmentId, ServiceId) VALUES (@EquipmentId, @ServiceId)",
                        connection, transaction))
                    {
                        linkCommand.Parameters.AddWithValue("@EquipmentId", equipment.Id);
                        linkCommand.Parameters.AddWithValue("@ServiceId", serviceId);
                        linkCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        // Вспомогательные методы
        private int GetOrCreateService(ServiceType serviceType, SqlConnection connection, SqlTransaction transaction)
        {
            var selectQuery = "SELECT Id FROM Services WHERE Type = @Type";
            using (var selectCommand = new SqlCommand(selectQuery, connection, transaction))
            {
                selectCommand.Parameters.AddWithValue("@Type", serviceType.ToString());
                var result = selectCommand.ExecuteScalar();
                if (result != null) return Convert.ToInt32(result);
            }

            var insertQuery = "INSERT INTO Services (Name, Price, DurationDays, Type) VALUES (@Name, @Price, @DurationDays, @Type); SELECT SCOPE_IDENTITY();";
            using (var insertCommand = new SqlCommand(insertQuery, connection, transaction))
            {
                insertCommand.Parameters.AddWithValue("@Name", serviceType.ToString());
                insertCommand.Parameters.AddWithValue("@Price", 0m);
                insertCommand.Parameters.AddWithValue("@DurationDays", 0);
                insertCommand.Parameters.AddWithValue("@Type", serviceType.ToString());
                return Convert.ToInt32(insertCommand.ExecuteScalar());
            }
        }

        private List<ServiceType> GetSupportedServices(int equipmentId, SqlConnection connection)
        {
            var services = new List<ServiceType>();
            var query = @"
                SELECT s.Type 
                FROM Equipment_Services es 
                JOIN Services s ON es.ServiceId = s.Id 
                WHERE es.EquipmentId = @EquipmentId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EquipmentId", equipmentId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add((ServiceType)Enum.Parse(typeof(ServiceType), reader["Type"].ToString()));
                    }
                }
            }
            return services;
        }
    }
}