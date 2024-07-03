using LibMember.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibMember.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Member>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT ID, FirstName, LastName,Address,Age FROM Members", connection))
                {
                    List<Member> Members = new List<Member>();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Member Member = new Member
                            {
                                ID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Address = reader.GetString(3),
                                Age = reader.GetInt32(4)
                            };
                            Members.Add(Member);
                        }
                    }

                    return Members;
                }
            }
        }

        public async Task <List<UserCred>> GetAllUserNameAsync()
         {
            List<UserCred> userCreds = new List<UserCred>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT UserName, Pswd FROM Members", connection))
                {
                                  
       
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserCred u1 =
                            new UserCred
                            {
                                UserName = reader.GetString(0).ToString(),
                                Password = reader.GetString(1).ToString()
                            };
                            userCreds.Add(u1);
                        }
                    }

                    return userCreds;
                } 
            }
        }
 

        public async Task<Member> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT ID, FirstName, LastName, Address, Age FROM Members WHERE ID = @Id", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Member
                            {
                                ID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Address = reader.GetString(3),
                                Age = reader.GetInt32(4)
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<int> AddAsync(Member Member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO Members (FirstName, LastName, Address, Age) VALUES (@Firstname, @LastName, @Address, @Age);", connection))
                {
                    command.Parameters.AddWithValue("@FirstName", Member.FirstName);
                    command.Parameters.AddWithValue("@LastName", Member.LastName);
                    command.Parameters.AddWithValue("@Address", Member.Address);
                    command.Parameters.AddWithValue("@Age", Member.Age);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task UpdateAsync(Member Member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE Members SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Age = @Age WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", Member.ID);
                    command.Parameters.AddWithValue("@FirstName", Member.FirstName);
                    command.Parameters.AddWithValue("@LastName", Member.LastName);
                    command.Parameters.AddWithValue("@Address", Member.Address);
                    command.Parameters.AddWithValue("@Age", Member.Age);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM Members WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
