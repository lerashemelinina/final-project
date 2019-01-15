using _01_DAL;
using _02_BOL;
using System;
using System.Linq;

namespace _03_BLL
{
    static public class OrderManager
    {
        /// <summary>
        /// selects all orders from db table "Orders"
        /// and maps the DAL objects to BOL objects
        /// </summary>
        /// <returns></returns>
        static public OrderModel[] SelectAllOrders()
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    return ef.Orders.Select(dbOrder => new OrderModel
                    {
                        StartRent = dbOrder.StartRent,
                        EndRent = dbOrder.EndRent,
                        ReturnDate = (DateTime) dbOrder.ReturnDate,

                        User = new UserModel
                        {
                            FullName = dbOrder.User.FullName,
                            IdentityNumber = dbOrder.User.IdentityNumber,
                            UserName = dbOrder.User.UserName,
                            BirthDate = dbOrder.User.BirthDate,
                            IsMale = dbOrder.User.IsMale,
                            Email = dbOrder.User.Email,
                            Password = dbOrder.User.Password,
                            UserRole = dbOrder.User.UserRole,
                            Image = dbOrder.User.Image
                        },

                        Car = new CarModel
                        {
                            Mileage = dbOrder.Car.Mileage,
                            Image = dbOrder.Car.Image,
                            IsForRent = dbOrder.Car.IsForRent,
                            CarNumber = dbOrder.Car.CarNumber,

                            CarType = new CarTypeModel
                            {
                                Make = dbOrder.Car.CarType.Make,
                                Model = dbOrder.Car.CarType.Model,
                                Price = dbOrder.Car.CarType.Price,
                                DelayCharge = dbOrder.Car.CarType.DelayCharge,
                                Year = dbOrder.Car.CarType.Year,
                                IsAutomatic = dbOrder.Car.CarType.IsAutomatic
                            },

                            Branch = new BranchModel
                            {
                                Adress = dbOrder.Car.Branch.Adress,
                                Latitude = dbOrder.Car.Branch.Latitude,
                                Lingitude = dbOrder.Car.Branch.Lingitude,
                                BranchName = dbOrder.Car.Branch.BranchName
                            }
                        }

                    }).ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// SelectOrderByStartRentAndCarNumber selects a specific Order from the DB by the EF ref
        /// by the `startDate` and `carNumber` parameters
        /// and maps the DAL object to BOL object
        /// </summary>
        static public OrderModel GetOrderByStartRentAndCarNumber(DateTime startRent, int carNumber)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    Order selectedOrder = ef.Orders.FirstOrDefault(dbOrder => (dbOrder.StartRent == startRent)
                                                                              && (dbOrder.Car.CarNumber == carNumber));
                    if (selectedOrder == null)
                        return null;

                    return new OrderModel
                    {
                        StartRent = selectedOrder.StartRent,
                        EndRent = selectedOrder.EndRent,
                        ReturnDate = (DateTime)selectedOrder.ReturnDate,

                        User = new UserModel
                        {
                            FullName = selectedOrder.User.FullName,
                            IdentityNumber = selectedOrder.User.IdentityNumber,
                            UserName = selectedOrder.User.UserName,
                            BirthDate = selectedOrder.User.BirthDate,
                            IsMale = selectedOrder.User.IsMale,
                            Email = selectedOrder.User.Email,
                            Password = selectedOrder.User.Password,
                            UserRole = selectedOrder.User.UserRole,
                            Image = selectedOrder.User.Image
                        },

                        Car = new CarModel
                        {
                            CarType = new CarTypeModel
                            {
                                Make = selectedOrder.Car.CarType.Make,
                                Model = selectedOrder.Car.CarType.Model,
                                Price = selectedOrder.Car.CarType.Price,
                                DelayCharge = selectedOrder.Car.CarType.DelayCharge,
                                Year = selectedOrder.Car.CarType.Year,
                                IsAutomatic = selectedOrder.Car.CarType.IsAutomatic
                            },

                            Mileage = selectedOrder.Car.Mileage,
                            Image = selectedOrder.Car.Image,
                            Branch = new BranchModel
                            {
                                Adress = selectedOrder.Car.Branch.Adress,
                                Latitude = selectedOrder.Car.Branch.Latitude,
                                Lingitude = selectedOrder.Car.Branch.Lingitude,
                                BranchName = selectedOrder.Car.Branch.BranchName
                            },

                            IsForRent=selectedOrder.Car.IsForRent,
                            CarNumber=selectedOrder.Car.CarNumber
                        }

                    };

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// inserts a new order to db table "Orders"
        /// maps "newOrder" parameter (BOL object) to a "Order" (DAL object)
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        static public bool InsertOrder(OrderModel newOrder)
        {
            try
            {

                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    User selectedUser = ef.Users.FirstOrDefault(dbUser => dbUser.UserName == newOrder.User.UserName);

                    if (selectedUser == null)
                        return false;

                    Car selectedCar = ef.Cars.FirstOrDefault(dbCar => dbCar.CarNumber == newOrder.Car.CarNumber);

                    if (selectedCar == null)
                        return false;

                    Order newDbOrder = new Order
                    {

                        StartRent = newOrder.StartRent,
                        EndRent = newOrder.EndRent,
                        ReturnDate = newOrder.ReturnDate,
                        UserId = selectedUser.UserId,
                        CarId = selectedCar.CarId
                    };

                    ef.Orders.Add(newDbOrder);
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
        /// updates a specific Order from the DB by the EF ref
        /// by the startDate and carNumber parameters
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="startRent"></param>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        static public bool UpdateOrderByStartDateAndCarNumber(DateTime startRent, int carNumber, OrderModel newOrder)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    Order selectedOrder = ef.Orders.FirstOrDefault(dbOrder => ((dbOrder.StartRent == startRent)
                                                                    && (dbOrder.Car.CarNumber == carNumber)));

                    if (selectedOrder == null)
                        return false;

                    Car selectedCar = ef.Cars.FirstOrDefault(dbCar => dbCar.CarNumber == carNumber);

                    if (selectedCar == null)
                        return false;

                    selectedOrder.StartRent = newOrder.StartRent;
                    selectedOrder.EndRent = newOrder.EndRent;
                    selectedOrder.ReturnDate = newOrder.ReturnDate;
                    selectedOrder.Car.CarId = selectedCar.CarId;

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
        /// deletes a specific Order from the DB by the EF ref
        /// by the "startDate" and "carNumber" parameters
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="startRent"></param>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        static public bool DeleteOrderByStartDateAndCarNumber(DateTime startRent, int carNumber)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    Order selectedOrder = ef.Orders.FirstOrDefault(dbOrder => (dbOrder.StartRent == startRent)
                                                                                && (dbOrder.Car.CarNumber == carNumber));

                    if (selectedOrder == null)
                        return false;

                    ef.Orders.Remove(selectedOrder);
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

