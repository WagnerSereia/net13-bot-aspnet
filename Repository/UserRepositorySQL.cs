﻿using SimpleBot.Interfaces.Repository;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SimpleBot.Repository
{
    public class UserRepositorySQL : IUserProfileRepository
    {
        private readonly SqlConnection client;

        public UserRepositorySQL(string connectionString)
        {
            client = new SqlConnection(connectionString);            
        }
        public UserProfile GetProfile(string id)
        {
            UserProfile userProfile = null;
            try
            {
                client.Open();
                string sql = "select * from UserProfile where idUser=@idUser";
                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@idUser", SqlDbType.VarChar,50).Value = id;

                SqlDataReader rs = myCommand.ExecuteReader();
                if (rs.Read())
                {
                    userProfile = new UserProfile();
                    userProfile.IdUser = (string)rs["IdUser"];                    
                    userProfile.Visitas = (int)rs["Visitas"];
                }
                rs.Close();
            }            
            finally
            {
                client.Close();
            }
                        
            return userProfile;
        }

        public void SetProfile(string id, ref UserProfile profile)
        {
            profile.Visitas += 1;
            this.Update(profile);            
        }

        public void Update(UserProfile profile)
        { 
            try
            {
                client.Open();
                string sql = "UPDATE [UserProfile] SET Visitas=@Visitas where idUser=@IdUser";
                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@IdUser", SqlDbType.VarChar, 50).Value = profile.IdUser;
                myCommand.Parameters.Add("@Visitas", SqlDbType.Int).Value = profile.Visitas;

                myCommand.ExecuteNonQuery();
            }
            finally
            {
                client.Close();
            }            
        }

        public void Insert(UserProfile profile)
        {
            try
            {
                client.Open();
                string sql = "insert into [UserProfile] (IdUser,Visitas)values(@IdUser,@Visitas)";

                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@IdUser", SqlDbType.VarChar, 50).Value = profile.IdUser;
                myCommand.Parameters.Add("@Visitas", SqlDbType.Int).Value = profile.Visitas;

                myCommand.ExecuteNonQuery();
            }            
            finally
            {
                client.Close();
            }
        }

        public void RemoveUserProfile(UserProfile profile)
        {
            try
            {
                client.Open();
                string sql = "Delete from [UserProfile] where idUser=@IdUser";
                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@IdUser", SqlDbType.VarChar, 50).Value = profile.IdUser;

                myCommand.ExecuteNonQuery();
            }
            finally
            {
                client.Close();
            }
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}