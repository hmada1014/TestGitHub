using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGitHub_DataAccessLayer_
{
    public class clsDataAccessLayer
    {

        public static bool GetEmployeeInfoByID(int ID, ref string FirstName, ref string LastName, ref bool Gendor, ref DateTime DateOfBirth, ref int CountryID, ref int DepartmentID, ref DateTime HireDate, ref DateTime ExitDate, ref decimal MonthlySalary, ref decimal BonusPerc)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Employees where ID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Gendor = (bool)reader["Gendor"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    DepartmentID = (int)reader["DepartmentID"];
                    HireDate = (DateTime)reader["HireDate"];
                    ExitDate = (DateTime)reader["ExitDate"];
                    MonthlySalary = (decimal)reader["MonthlySalary"];
                    BonusPerc = (decimal)reader["BonusPerc"];


                }
                else
                {
                    IsFound = false;

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static int AddNewEmployee(string FirstName, string LastName, bool Gendor, DateTime DateOfBirth, int CountryID, int DepartmentID, DateTime HireDate, DateTime ExitDate, decimal MonthlySalary, decimal BonusPerc)
        {
            int EmployeeId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $"insert into Employees (FirstName,LastName,Gendor,DateOfBirth,CountryID,DepartmentID,HireDate,ExitDate,MonthlySalary,BonusPerc)" +
            $"Values(@FirstName,@LastName,@Gendor,@DateOfBirth,@CountryID,@DepartmentID,@HireDate,@ExitDate,@MonthlySalary,@BonusPerc) select scope_identity();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            command.Parameters.AddWithValue("@HireDate", HireDate);
            command.Parameters.AddWithValue("@ExitDate", ExitDate);
            command.Parameters.AddWithValue("@MonthlySalary", MonthlySalary);
            command.Parameters.AddWithValue("@BonusPerc", BonusPerc);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                {
                    EmployeeId = InsertID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return EmployeeId;
        }
        public static bool UpdateEmployeeByID(int ID, string FirstName, string LastName, bool Gendor, DateTime DateOfBirth, int CountryID, int DepartmentID, DateTime HireDate, DateTime ExitDate, decimal MonthlySalary, decimal BonusPerc)
        {
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update Employees 
set
		 FirstName = @FirstName,
LastName = @LastName,
Gendor = @Gendor,
DateOfBirth = @DateOfBirth,
CountryID = @CountryID,
DepartmentID = @DepartmentID,
HireDate = @HireDate,
ExitDate = @ExitDate,
MonthlySalary = @MonthlySalary,
BonusPerc = @BonusPerc
where ID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            command.Parameters.AddWithValue("@HireDate", HireDate);
            command.Parameters.AddWithValue("@ExitDate", ExitDate);
            command.Parameters.AddWithValue("@MonthlySalary", MonthlySalary);
            command.Parameters.AddWithValue("@BonusPerc", BonusPerc);


            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return (AffectedRows > 0);
        }

        public static bool DeleteEmployeeByID(int ID)
        {
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from Employees where  ID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }
        public static DataView GetAllEmployee()
        {
            DataTable dtEmployee = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = " select ID,FirstName,LastName,Gendor,DateOfBirth,CountryID,DepartmentID,HireDate,ExitDate,MonthlySalary,BonusPerc from Employees ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtEmployee.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return dtEmployee.DefaultView;
        }
        public static int GetTotalEmployees()
        {
            int Total = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(*) from Employees";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int total))
                {
                    Total = total;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return Total;
        }
        public static bool IsEmployeeExist(int ID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found = 1 from Employees where ID = @ID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static DataView SearchEmployeeByID(int ID)
        {
            DataTable dataTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Enter your query";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return dataTable.DefaultView;
        }



    }
}
