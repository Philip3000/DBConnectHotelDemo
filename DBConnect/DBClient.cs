using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Net;
using System.Xml.Linq;

namespace DBConnect
{
    public class DBClient
    {
        
            //Connectionstring
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DemoHotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        #region Facilities    
        private List<Facility> GetAllFacilities(SqlConnection connection)
            {
                Console.WriteLine("Calling -> GetAllFacilities");
                string queryStringAllFacilities = "Select * From DemoFacility";
                SqlCommand command = new SqlCommand(queryStringAllFacilities, connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Listing all facilities");
                if (!reader.HasRows)
                {
                    Console.WriteLine("No facilities in database");
                    reader.Close();
                    return null;
                }
                List<Facility> facilities = new List<Facility>();
            while (reader.Read())
            {
                Facility nextFacility = new Facility()
                {
                    Facility_No = reader.GetInt32(0),
                    FacilityName = reader.GetString(1)
                };
                facilities.Add(nextFacility);

                Console.WriteLine(nextFacility);

            }
                    reader.Close();

                    return facilities;
            }
        private static int DeleteFacility(SqlConnection connection, int facility_No)
        {
            Console.WriteLine("Calling -> DeleteFacility");

            string deleteCommandString = $"DELETE FROM DemoFacility WHERE Facility_No = {facility_No}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting facility #{facility_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }
        private int InsertFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("Calling -> InsertFacility");

            string insertCommandString = $"INSERT INTO DemoFacility VALUES({facility.Facility_No}, '{facility.FacilityName}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating hotel #{facility.Facility_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            return numberOfRowsAffected;
        }

        private int UpdateFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("Calling -> UpdateFacility");

            //This SQL command will update one row from the DemoHotel table: The one with primary key hotel_No
            string updateCommandString = $"UPDATE DemoFacility SET Name='{facility.FacilityName}' WHERE Facility_No = {facility.Facility_No}";
            Console.WriteLine($"SQL applied: {updateCommandString}");

            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating facility #{facility.Facility_No}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();
            return numberOfRowsAffected;
        }
        private int GetMaxFacilityNo(SqlConnection connection)
        {
            Console.WriteLine("Calling -> GetMaxFacilityNo");
            string queryStringMaxFacilityNo = "SELECT  MAX(Facility_No)  FROM DemoFacility";
            Console.WriteLine($"SQL applied: {queryStringMaxFacilityNo}");
            SqlCommand command = new SqlCommand(queryStringMaxFacilityNo, connection);
            SqlDataReader reader = command.ExecuteReader();

            int MaxFacility_No = 0;

            //Is there any rows in the query
            if (reader.Read())
            {
                //Yes, get max hotel_no
                MaxFacility_No = reader.GetInt32(0); 
            }

            reader.Close();

            Console.WriteLine($"Max facility#: {MaxFacility_No}");
            Console.WriteLine();

            //Return max hotel_no
            return MaxFacility_No;
        }
        #endregion

        #region hotels
        private int GetMaxHotelNo(SqlConnection connection)
            {
                Console.WriteLine("Calling -> GetMaxHotelNo");

                //This SQL command will fetch one row from the DemoHotel table: The one with the max Hotel_No
                string queryStringMaxHotelNo = "SELECT  MAX(Hotel_No)  FROM DemoHotel";
                Console.WriteLine($"SQL applied: {queryStringMaxHotelNo}");

                //Apply SQL command
                SqlCommand command = new SqlCommand(queryStringMaxHotelNo, connection);
                SqlDataReader reader = command.ExecuteReader();

                //Assume undefined value 0 for max hotel_no
                int MaxHotel_No = 0;

                //Is there any rows in the query
                if (reader.Read())
                {
                    //Yes, get max hotel_no
                    MaxHotel_No = reader.GetInt32(0); //Reading int fro 1st column
                }

                //Close reader
                reader.Close();

                Console.WriteLine($"Max hotel#: {MaxHotel_No}");
                Console.WriteLine();

                //Return max hotel_no
                return MaxHotel_No;
            }
            
            private int UpdateHotel(SqlConnection connection, Hotel hotel)
            {
                Console.WriteLine("Calling -> UpdateHotel");

                //This SQL command will update one row from the DemoHotel table: The one with primary key hotel_No
                string updateCommandString = $"UPDATE DemoHotel SET Name='{hotel.Name}', Address='{hotel.Address}' WHERE Hotel_No = {hotel.Hotel_No}";
                Console.WriteLine($"SQL applied: {updateCommandString}");

                //Apply SQL command
                SqlCommand command = new SqlCommand(updateCommandString, connection);
                Console.WriteLine($"Updating hotel #{hotel.Hotel_No}");
                int numberOfRowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
                Console.WriteLine();
                //Return number of rows affected
                return numberOfRowsAffected;
            }

            private int InsertHotel(SqlConnection connection, Hotel hotel)
            {
                Console.WriteLine("Calling -> InsertHotel");

                //This SQL command will insert one row into the DemoHotel table with primary key hotel_No
                string insertCommandString = $"INSERT INTO DemoHotel (Hotel_No, Name, Address) VALUES({hotel.Hotel_No}, '{hotel.Name}', '{hotel.Address}')";
                Console.WriteLine($"SQL applied: {insertCommandString}");

                //Apply SQL command
                SqlCommand command = new SqlCommand(insertCommandString, connection);

                Console.WriteLine($"Creating hotel #{hotel.Hotel_No}");
                int numberOfRowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
                Console.WriteLine();

                //Return number of rows affected 
                return numberOfRowsAffected;
            }
            
            private List<Hotel> ListAllHotels(SqlConnection connection)
            {
                Console.WriteLine("Calling -> ListAllHotels");

                //This SQL command will fetch all rows and columns from the DemoHotel table
                string queryStringAllHotels = "SELECT * FROM DemoHotel";
                Console.WriteLine($"SQL applied: {queryStringAllHotels}");
                //Apply SQL command
                SqlCommand command = new SqlCommand(queryStringAllHotels, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Listing all hotels:");

                //NO rows in the query 
                if (!reader.HasRows)
                {
                    //End here
                    Console.WriteLine("No hotels in database");
                    reader.Close();

                    //Return null for 'no hotels found'
                    return null;
                }

                //Create list of hotels found
                List<Hotel> hotels = new List<Hotel>();
                while (reader.Read())
                {
                    //If we reached here, there is still one hotel to be put into the list 
                    Hotel nextHotel = new Hotel()
                    {
                        Hotel_No = reader.GetInt32(0), //Reading int from 1st column
                        Name = reader.GetString(1),    //Reading string from 2nd column
                        Address = reader.GetString(2)  //Reading string from 3rd column
                    };
                    //Add hotel to list
                    hotels.Add(nextHotel);

                    Console.WriteLine(nextHotel);
                }

                //Close reader
                reader.Close();
                Console.WriteLine();

                //Return list of hotels
                return hotels;
            }

            private Hotel GetHotel(SqlConnection connection, int hotel_no)
            {
                Console.WriteLine("Calling -> GetHotel");

                //This SQL command will fetch the row with primary key hotel_no from the DemoHotel table
                string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE hotel_no = {hotel_no}";
                Console.WriteLine($"SQL applied: {queryStringOneHotel}");

                //Prepare SQK command
                SqlCommand command = new SqlCommand(queryStringOneHotel, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"Finding hotel#: {hotel_no}");

                //NO rows in the query? 
                if (!reader.HasRows)
                {
                    //End here
                    Console.WriteLine("No hotels in database");
                    reader.Close();

                    //Return null for 'no hotel found'
                    return null;
                }   //Fetch hotel object from teh database
                Hotel hotel = null;
                if (reader.Read())
                {
                    hotel = new Hotel()
                    {
                        Hotel_No = reader.GetInt32(0), //Reading int fro 1st column
                        Name = reader.GetString(1),    //Reading string from 2nd column
                        Address = reader.GetString(2)  //Reading string from 3rd column
                    };

                    Console.WriteLine(hotel);
                }

                //Close reader
                reader.Close();
                Console.WriteLine();

                //Return found hotel
                return hotel;
            }
        private static int DeleteHotel(SqlConnection connection, int hotel_no)
        {
            Console.WriteLine("Calling -> DeleteHotel");

            //This SQL command will delete one row from the DemoHotel table: The one with primary key hotel_No
            string deleteCommandString = $"DELETE FROM DemoHotel  WHERE Hotel_No = {hotel_no}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            //Apply SQL command
            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Deleting hotel #{hotel_no}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            //Return number of rows affected
            return numberOfRowsAffected;
        }
        #endregion
        public void Start()
            {
                //Apply 'using' to connection (SqlConnection) in order to call Dispose (interface IDisposable) 
                //whenever the 'using' block exits
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open connection
                    connection.Open();

                    //List all hotels in the database
                    ListAllHotels(connection);

                    //Create a new hotel with primary key equal to current max primary key plus 1
                    Hotel newHotel = new Hotel()
                    {
                        Hotel_No = GetMaxHotelNo(connection) + 1,
                        Name = "New Hotel",
                        Address = "Maglegaardsvej 2, 4000 Roskilde"
                    };

                    //Insert the hotel into the database
                    InsertHotel(connection, newHotel);

                    //List all hotels including the the newly inserted one
                    ListAllHotels(connection);

                    //Get the newly inserted hotel from the database in order to update it 
                    Hotel hotelToBeUpdated = GetHotel(connection, newHotel.Hotel_No);

                    //Alter Name and Addess properties
                    hotelToBeUpdated.Name += "(updated)";
                    hotelToBeUpdated.Address += "(updated)";

                    //Update the hotel in the database 
                    UpdateHotel(connection, hotelToBeUpdated);

                    //List all hotels including the updated one
                    ListAllHotels(connection);

                    //Get the updated hotel in order to delete it
                    Hotel hotelToBeDeleted = GetHotel(connection, hotelToBeUpdated.Hotel_No);

                    //Delete the hotel
                    DeleteHotel(connection, hotelToBeDeleted.Hotel_No);

                    //List all hotels - now without the deleted one
                    ListAllHotels(connection);

                    Facility facility = new Facility();
                    facility.FacilityName = "New Facility";
                    facility.Facility_No = GetMaxFacilityNo(connection) + 1;
                    GetAllFacilities(connection);
                    InsertFacility(connection, facility);
                    facility.FacilityName = "Other facility";
                    UpdateFacility(connection, facility);
                    GetAllFacilities(connection);
                    DeleteFacility(connection, facility.Facility_No);
                    GetAllFacilities(connection);


            }
        }
        }
    }
