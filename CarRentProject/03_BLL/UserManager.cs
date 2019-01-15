using _01_DAL;
using _02_BOL;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace _03_BLL
{
    static public class UserManager
    {
        /// <summary>
        /// selects all users from db table "Users"
        /// and maps the DAL objects to BOL objects
        /// </summary>
        /// <returns></returns>
        static public UserModel[] SelectAllUsers()
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    UserModel[] allUsers = ef.Users.Select(dbUser => new UserModel
                    {
                        FullName = dbUser.FullName,
                        IdentityNumber = dbUser.IdentityNumber,
                        UserName = dbUser.UserName,
                        BirthDate = (DateTime)dbUser.BirthDate,
                        IsMale = dbUser.IsMale,
                        Email = dbUser.Email,
                        Password = dbUser.Password,
                        UserRole = dbUser.UserRole,
                        Image = dbUser.Image

                    }).ToArray();

                    for (int i = 0; i < allUsers.Length; i++)
                    {
                       string temp;

                        if (allUsers[i].Image != null)
                            temp = allUsers[i].Image;
                        else
                            temp = "NoPhoto.jpg";

                        allUsers[i].Image = Convert.ToBase64String(File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/UserImages/" + temp)));
                       
                    }

                    return allUsers;
                }
            }
            catch (Exception exept)
            {
                Console.WriteLine(exept.ToString());
                return null;
            }
        }


        /// <summary>
        /// SelectUserByUserName selects a specific User from the DB by the EF ref
        /// by the `userName` parameter
        /// and maps the DAL object to BOL object
        /// </summary>
        static public UserModel SelectUserByUserName(string userName)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    User selectedUser = ef.Users.FirstOrDefault(dbUser => dbUser.UserName == userName);
                    if (selectedUser == null)
                        return null;

                    return new UserModel
                    {
                        FullName= selectedUser.FullName,
                        IdentityNumber= selectedUser.IdentityNumber,
                        UserName= selectedUser.UserName,
                        BirthDate= selectedUser.BirthDate,
                        IsMale= selectedUser.IsMale,
                        Email= selectedUser.Email,
                        Password= selectedUser.Password,
                        UserRole= selectedUser.UserRole,
                        Image= selectedUser.Image
                    };

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// SelectUserByUserNameAndPassword selects a specific User from the DB by the EF ref
        /// by the `userName` and  `password` parameters
        /// and maps the DAL object to BOL object
        /// </summary>
        static public UserModel SelectUserByUserNameAndPassword(string userName, string password)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    User selectedUser = ef.Users.FirstOrDefault(dbUser => dbUser.UserName == userName
                                                                          && dbUser.Password== password);
                    if (selectedUser == null)
                        return null;

                    return new UserModel
                    {
                        FullName = selectedUser.FullName,
                        IdentityNumber = selectedUser.IdentityNumber,
                        UserName = selectedUser.UserName,
                        BirthDate = selectedUser.BirthDate,
                        IsMale = selectedUser.IsMale,
                        Email = selectedUser.Email,
                        Password = selectedUser.Password,
                        UserRole = selectedUser.UserRole,
                        Image = selectedUser.Image
                    };

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        static public UserModel SelectUserByEmailAndPassword(string email, string password)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    User selectedUser = ef.Users.FirstOrDefault(dbUser => dbUser.Email == email
                                                                          && dbUser.Password == password);
                    if (selectedUser == null)
                        return null;

                    return new UserModel
                    {
                        FullName = selectedUser.FullName,
                        IdentityNumber = selectedUser.IdentityNumber,
                        UserName = selectedUser.UserName,
                        BirthDate = selectedUser.BirthDate,
                        IsMale = selectedUser.IsMale,
                        Email = selectedUser.Email,
                        Password = selectedUser.Password,
                        UserRole = selectedUser.UserRole,
                        Image = selectedUser.Image
                    };

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// inserts a new user to db table "Users"
        /// maps "newUser" parameter (BOL object) to a "User" (DAL object)
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        static public bool InsertUser(UserModel newUser)
        {
            try
            {

                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    User newDbUser = new User
                    {
                        FullName = newUser.FullName,
                        IdentityNumber = newUser.IdentityNumber,
                        UserName = newUser.UserName,
                        BirthDate = newUser.BirthDate,
                        IsMale = newUser.IsMale,
                        Email = newUser.Email,
                        Password = newUser.Password,
                        UserRole = newUser.UserRole,
                        Image = newUser.Image
                    };

                    ef.Users.Add(newDbUser);
                    ef.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }



        /// <summary>
        /// updates a specific User from the DB by the EF ref
        /// by the `fullName` parameter
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newUser"></param>
        /// <returns></returns>
        static public bool UpdateUserByUserName(string userName, UserModel newUser)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    User selectedUser = ef.Users.FirstOrDefault(dbUser => dbUser.UserName == userName);

                    if (selectedUser == null)
                        return false;

                    selectedUser.FullName = newUser.FullName;
                    selectedUser.IdentityNumber = newUser.IdentityNumber;
                   // selectedUser.UserName = newUser.UserName;
                    selectedUser.BirthDate = newUser.BirthDate;
                    selectedUser.IsMale = newUser.IsMale;
                    selectedUser.Email = newUser.Email;
                    selectedUser.Password = newUser.Password;
                    selectedUser.UserRole = newUser.UserRole;
                    selectedUser.Image = newUser.Image;

                    ef.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }


        /// <summary>
        /// deletes a specific User from the DB by the EF ref
        /// by the `userName` parameter
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        static public bool DeleteUserByUserName(string userName)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    User selectedUser = ef.Users.FirstOrDefault(dbUser => dbUser.UserName == userName);

                    if (selectedUser == null)
                        return false;

                    ef.Users.Remove(selectedUser);
                    ef.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
