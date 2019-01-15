using _01_DAL;
using _02_BOL;
using System;
using System.Linq;

namespace _03_BLL
{
    static public class BranchManager
    {

        /// <summary>
        /// SelectAllBranches reads all the Branshes from the DB by the EF ref
        /// and maps the DAL objects to BOL objects
        /// </summary>
        static public BranchModel[] SelectAllBranches()
        {
            try
            {
                using (RentCarDBEntities ef = new RentCarDBEntities())
                {

                    return ef.Branches.Select(dbBranch => new BranchModel
                    {
                        Adress = dbBranch.Adress,
                        Latitude = dbBranch.Latitude,
                        Lingitude = dbBranch.Lingitude,
                        BranchName = dbBranch.BranchName
                    }).ToArray();

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

