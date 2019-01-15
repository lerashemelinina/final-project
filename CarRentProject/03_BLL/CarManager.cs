using _01_DAL;
using _02_BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _03_BLL
{
    static public class CarManager
    {
        /// <summary>
        /// selects all cars from db table "Cars"
        /// and maps the DAL objects to BOL objects
        /// </summary>
        /// <returns></returns>
        static public CarModel[] SelectAllCars()
        {

            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    CarModel [] allCars =  ef.Cars.Select(dbCar => new CarModel
                    {

                        CarType = new CarTypeModel
                        {

                            Make = dbCar.CarType.Make,
                            Model = dbCar.CarType.Model,
                            Price = dbCar.CarType.Price,
                            DelayCharge = dbCar.CarType.DelayCharge,
                            Year = dbCar.CarType.Year,
                            IsAutomatic = dbCar.CarType.IsAutomatic
                        },

                        Mileage = dbCar.Mileage,
                        Image = dbCar.Image,
                        Branch = new BranchModel
                        {
                            Adress = dbCar.Branch.Adress,
                            Latitude = dbCar.Branch.Latitude,
                            Lingitude = dbCar.Branch.Lingitude,
                            BranchName = dbCar.Branch.BranchName
                        },

                        IsForRent = dbCar.IsForRent,
                        CarNumber = dbCar.CarNumber

                    }).ToArray();


                    for (int i = 0; i < allCars.Length; i++)
                    {
                        string temp = allCars[i].Image;
                        allCars[i].Image = Convert.ToBase64String(File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/CarImages/" + temp)));
                    }

                    return allCars;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// SelectCarByCarNumber selects a specific Car from the DB by the EF ref
        /// by the `carNumber` parameter
        /// and maps the DAL object to BOL object
        /// </summary>
        static public CarModel SelectCarByCarNumber(int carNumber)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    Car selectedCar = ef.Cars.FirstOrDefault(dbCar => dbCar.CarNumber == carNumber);
                    if (selectedCar == null)
                        return null;

                    return new CarModel
                    {
                        CarType = new CarTypeModel
                        {
                            Make = selectedCar.CarType.Make,
                            Model = selectedCar.CarType.Model,
                            Price = selectedCar.CarType.Price,
                            DelayCharge = selectedCar.CarType.DelayCharge,
                            Year = selectedCar.CarType.Year,
                            IsAutomatic = selectedCar.CarType.IsAutomatic
                        },

                        Mileage = selectedCar.Mileage,
                        Image = Convert.ToBase64String(File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/CarImages/" + selectedCar.Image))),
                        Branch = new BranchModel
                        {
                            Adress = selectedCar.Branch.Adress,
                            Latitude = selectedCar.Branch.Latitude,
                            Lingitude = selectedCar.Branch.Lingitude,
                            BranchName = selectedCar.Branch.BranchName
                        },

                        IsForRent = selectedCar.IsForRent,
                        CarNumber = selectedCar.CarNumber
                    };

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// inserts a new car to db table "Cars"
        /// maps "newCar" parameter (BOL object) to a "Car" (DAL object)
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="newCar"></param>
        /// <returns></returns>
        static public bool InsertNewCar(CarModel newCar)
        {
            try
            {

                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    CarType selectedCarType = ef.CarTypes.FirstOrDefault(dbCarType => (dbCarType.Make == newCar.CarType.Make)
                                                                                        && (dbCarType.Model == newCar.CarType.Model)
                                                                                        && (dbCarType.Year == newCar.CarType.Year));


                    if (selectedCarType == null)
                        return false;


                    Branch selectedBranch = ef.Branches.FirstOrDefault(dbBranch => dbBranch.BranchName == newCar.Branch.BranchName);

                    if (selectedBranch == null)
                        return false;

                    Car newDbCar = new Car
                    {
                        Mileage = newCar.Mileage,
                        Image = newCar.Image,
                        IsForRent = newCar.IsForRent,
                        CarNumber = newCar.CarNumber,
                        CarTypeId = selectedCarType.CarTypeId,
                        BranchId = selectedBranch.BranchId
                    };

                    ef.Cars.Add(newDbCar);
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
        /// updates a specific car from the DB by the EF ref
        /// by the "carNumber" parameter
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="newCar"></param>
        /// <returns></returns>
        static public bool UpdateCarByCarNumber(int carNumber, CarModel newCar)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    Car selectedCar = ef.Cars.FirstOrDefault(dbCar => (dbCar.CarNumber == carNumber));

                    if (selectedCar == null)
                        return false;


                    Branch selectedBranch = ef.Branches.FirstOrDefault(dbBranch => dbBranch.BranchName == newCar.Branch.BranchName);


                    if (selectedBranch == null)
                        return false;


                    selectedCar.Mileage = newCar.Mileage;
                    selectedCar.Image = newCar.Image;
                    selectedCar.IsForRent = newCar.IsForRent;
                    selectedCar.CarNumber = newCar.CarNumber;
                    selectedCar.BranchId = selectedBranch.BranchId;

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
        /// deletes a specific Car from the DB by the EF ref
        /// by the "carNumber" parameter
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="carNumber"></param>
        /// <returns></returns>

        static public bool DeleteCarByCarNumber(int carNumber)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    Car selectedCar = ef.Cars.FirstOrDefault(dbCar => (dbCar.CarNumber == carNumber));


                    if (selectedCar == null)
                        return false;

                    ef.Cars.Remove(selectedCar);
                    ef.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }


        static public CarModel[] CheckAvailibility(DateTime startRent, DateTime endRent)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    Order[] orders = ef.Orders.Where(dbOrder => (dbOrder.StartRent >= startRent && dbOrder.StartRent <= endRent)
                                                              || (dbOrder.EndRent >= startRent && dbOrder.EndRent <= endRent)).ToArray();

                    List<Car> cars = new List<Car>();
                    cars.AddRange(ef.Cars.ToList());

                    if (orders.Length > 0)
                    {
                        for (int i = 0; i < orders.Length; i++)
                        {
                            Order order = orders[i];
                            cars.Remove(ef.Cars.FirstOrDefault(dbCar => dbCar.CarId == order.CarId));
                        }
                    }


                    CarModel[] availableCars = cars.Select(dbCar => new CarModel
                    {
                        CarType = new CarTypeModel
                        {

                            Make = dbCar.CarType.Make,
                            Model = dbCar.CarType.Model,
                            Price = dbCar.CarType.Price,
                            DelayCharge = dbCar.CarType.DelayCharge,
                            Year = dbCar.CarType.Year,
                            IsAutomatic = dbCar.CarType.IsAutomatic
                        },

                        Mileage = dbCar.Mileage,
                        Image = dbCar.Image,
                        Branch = new BranchModel
                        {
                            Adress = dbCar.Branch.Adress,
                            Latitude = dbCar.Branch.Latitude,
                            Lingitude = dbCar.Branch.Lingitude,
                            BranchName = dbCar.Branch.BranchName
                        },

                        IsForRent = dbCar.IsForRent,
                        CarNumber = dbCar.CarNumber

                    }).ToArray();

                    for (int i = 0; i < availableCars.Length; i++)
                    {
                        string temp = availableCars[i].Image;
                        availableCars[i].Image = Convert.ToBase64String(File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/CarImages/" + temp)));
                    }

                    return availableCars;
                }
            

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}

















