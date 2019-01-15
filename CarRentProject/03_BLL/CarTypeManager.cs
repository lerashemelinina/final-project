using _01_DAL;
using _02_BOL;
using System;
using System.Linq;


namespace _03_BLL
{
   static public class CarTypeManager
    {
        /// <summary>
        /// selects all car types from db table "CarTypes"
        /// and maps the DAL objects to BOL objects
        /// </summary>
        /// <returns></returns>
        static public CarTypeModel[] SelectAllCarTypes()
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    return ef.CarTypes.Select(dbCarType => new CarTypeModel
                    {
                        Make = dbCarType.Make,
                        Model = dbCarType.Model,
                        Price = dbCarType.Price,
                        DelayCharge = dbCarType.DelayCharge,
                        Year = dbCarType.Year,
                        IsAutomatic = dbCarType.IsAutomatic
                    }).ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// GetCarTypeByMakeModelYear selects a specific CarType from the DB by the EF ref
        /// by the `make`, `model` and  `year` parameters
        /// and maps the DAL object to BOL object
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        static public CarTypeModel GetCarTypeByMakeModelYear(string make, string model, int year)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    CarType selectedCarType = ef.CarTypes.FirstOrDefault(dbCarType => dbCarType.Make== make&&dbCarType.Model== model&&dbCarType.Year== year);
                                                                                     
                    if (selectedCarType == null)
                        return null;

                    return new CarTypeModel
                    {
                        Make = selectedCarType.Make,
                        Model = selectedCarType.Model,
                        Price=selectedCarType.Price,
                        DelayCharge=selectedCarType.DelayCharge,
                        Year=selectedCarType.Year,
                        IsAutomatic=selectedCarType.IsAutomatic
                    };

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// inserts a new car type to db table "CarTypes"
        /// maps "newCarType" parameter (BOL object) to a "CarType" (DAL object)
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="newCarType"></param>
        /// <returns></returns>
        static public bool InsertNewCarType(CarTypeModel newCarType)
        {
            try
            {

                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    CarType newDbCarType = new CarType
                    {
                        Make = newCarType.Make,
                        Model = newCarType.Model,
                        Price = newCarType.Price,
                        DelayCharge = newCarType.DelayCharge,
                        Year = newCarType.Year,
                        IsAutomatic = newCarType.IsAutomatic
                    };

                    ef.CarTypes.Add(newDbCarType);
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
        /// updates a specific car type from the DB by the EF ref
        /// by the `make`, `model` and `year` parameters
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="newCarType"></param>
        /// <returns></returns>
        static public bool UpdateCarTypeByMakeModelYear(string make,string model,int year, CarTypeModel newCarType)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    CarType selectedCarType = ef.CarTypes.FirstOrDefault(dbCarType => (dbCarType.Make == make)
                                                                                       &&(dbCarType.Model==model)
                                                                                       &&(dbCarType.Year==year));
                                                                                    
                    if (selectedCarType == null)
                        return false;

                    selectedCarType.Make = newCarType.Make;
                    selectedCarType.Model = newCarType.Model;
                    selectedCarType.Price = newCarType.Price;
                    selectedCarType.DelayCharge = newCarType.DelayCharge;
                    selectedCarType.Year = newCarType.Year;
                    selectedCarType.IsAutomatic = newCarType.IsAutomatic;

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
        /// deletes a specific CarType from the DB by the EF ref
        /// by the "make", "model" and "year" parameters
        /// and returns bool value - if the action was success
        /// </summary>
        /// <param name="make"></param>
        /// <param name = "model" ></ param >
        /// <param name = "year" ></ param >
        /// <returns></returns>

        static public bool DeleteCarTypeByMakeModelYear(string make, string model, int year)
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {
                    CarType selectedCarType = ef.CarTypes.FirstOrDefault(dbCarType => (dbCarType.Make == make)
                                                                                        && (dbCarType.Model == model)
                                                                                        &&(dbCarType.Year==year));

                    if (selectedCarType == null)
                        return false;

                    ef.CarTypes.Remove(selectedCarType);
                    ef.SaveChanges();
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

